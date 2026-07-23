using ECommerce.Application.Common.Constants;
using ECommerce.Application.Common.Errors;
using ECommerce.Application.Common.Interfaces;
using ECommerce.Application.Common.Models;
using ECommerce.Application.Features.Authentication;
using ECommerce.Application.Features.Authentication.DTOs;
using ECommerce.Application.Features.Users.DTOs;
using ECommerce.Domain.Entities;
using ECommerce.Infrastructure.Authentication.Jwt;
using ECommerce.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;


public sealed class IdentityService : IIdentityService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly ECommerce.Application.Common.Interfaces.IApplicationDbContext _context;
    private readonly IJwtTokenGenerator _jwtGenerator;
    private readonly IRefreshTokenGenerator _refreshTokenGenerator;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly JwtOptions _jwtOptions;

    public IdentityService(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        ECommerce.Application.Common.Interfaces.IApplicationDbContext context,
        IJwtTokenGenerator jwtGenerator,
        IRefreshTokenGenerator refreshTokenGenerator,
        IDateTimeProvider dateTimeProvider,
        IOptions<JwtOptions> options)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _context = context;
        _jwtGenerator = jwtGenerator;
        _refreshTokenGenerator = refreshTokenGenerator;
        _dateTimeProvider = dateTimeProvider;
        _jwtOptions = options.Value;
    }
    private static Error ToIdentityError(
    IEnumerable<IdentityError> errors)
    {
        return new Error(
            "Identity.Validation",
            string.Join(
                Environment.NewLine,
                errors.Select(x => x.Description)),
            ErrorType.Validation);
    }
    public async Task<Result<AuthResultDto>> RegisterAsync(
    RegisterRequest request,
    CancellationToken cancellationToken = default)
    {
        var exists = await _userManager.FindByEmailAsync(request.Email);

        if (exists is not null)
            return Result<AuthResultDto>.Failure(AuthenticationErrors.EmailAlreadyExists);

        var user = new ApplicationUser
        {
            FullName = request.FullName,
            Email = request.Email,
            UserName = request.Email
        };

        var result = await _userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
        {
            var description = string.Join(
                Environment.NewLine,
                result.Errors.Select(e => e.Description));

            return Result<AuthResultDto>.Failure(
                new Error("Identity.Create", description, ErrorType.Validation));
        }

        await _userManager.AddToRoleAsync(user, Roles.Customer);

        return await GenerateTokensAsync(user, cancellationToken);
    }

    public async Task<Result<AuthResultDto>> LoginAsync(
    LoginRequest request,
    CancellationToken cancellationToken = default)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user is null)
            return Result<AuthResultDto>.Failure(AuthenticationErrors.InvalidCredentials);

        var result = await _signInManager.CheckPasswordSignInAsync(
            user,
            request.Password,
            false);

        if (!result.Succeeded)
            return Result<AuthResultDto>.Failure(AuthenticationErrors.InvalidCredentials);

        return await GenerateTokensAsync(user, cancellationToken);
    }

    private async Task<Result<AuthResultDto>> GenerateTokensAsync(
    ApplicationUser user,
    CancellationToken cancellationToken)
    {
        var jwt = await _jwtGenerator.GenerateAsync(user);

        var refreshTokenValue = _refreshTokenGenerator.Generate();

        var refreshToken = new RefreshToken(
            user.Id,
            refreshTokenValue,
            _dateTimeProvider.UtcNow.AddDays(
                _jwtOptions.RefreshTokenExpirationDays));

        user.RefreshTokens.Add(refreshToken);

        await _userManager.UpdateAsync(user);

        return Result<AuthResultDto>.Success(
            new AuthResultDto(
                user.Id,
                user.FullName,
                user.Email!,
                jwt.AccessToken,
                refreshToken.Token,
                jwt.ExpiresAt));
    }
    public async Task<Result<AuthResultDto>> RefreshTokenAsync(
    string refreshToken,
    CancellationToken cancellationToken = default)
    {
        var token = await _context.RefreshTokens
            .FirstOrDefaultAsync(
                x => x.Token == refreshToken,
                cancellationToken);

        if (token is null)
            return Result<AuthResultDto>.Failure(
                AuthenticationErrors.InvalidRefreshToken);

        if (!token.IsActive)
            return Result<AuthResultDto>.Failure(
                AuthenticationErrors.InvalidRefreshToken);

        var user = await _userManager.FindByIdAsync(token.UserId.ToString());

        if (user is null)
            return Result<AuthResultDto>.Failure(
                AuthenticationErrors.UserNotFound);

        token.Revoke();

        await _context.SaveChangesAsync(cancellationToken);

        return await GenerateTokensAsync(user, cancellationToken);
    }
    public async Task<Result> LogoutAsync(
    int userId,
    CancellationToken cancellationToken = default)
    {
        var tokens = await _context.RefreshTokens
            .Where(x => x.UserId == userId && x.IsActive)
            .ToListAsync(cancellationToken);

        foreach (var token in tokens)
            token.Revoke();

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }

    public async Task<Result> ChangePasswordAsync(
    int userId,
    ChangePasswordRequest request,
    CancellationToken cancellationToken = default)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());

        if (user is null)
            return Result.Failure(AuthenticationErrors.UserNotFound);

        var result = await _userManager.ChangePasswordAsync(
            user,
            request.CurrentPassword,
            request.NewPassword);

        if (!result.Succeeded)
            return Result.Failure(
                ToIdentityError(result.Errors));

        return Result.Success();
    }


    public async Task<Result<UserProfileDto>> GetProfileAsync(
    int userId,
    CancellationToken cancellationToken = default)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());

        if (user is null)
            return Result<UserProfileDto>.Failure(
                AuthenticationErrors.UserNotFound);

        return Result<UserProfileDto>.Success(
            new UserProfileDto(
                user.Id,
                user.FullName,
                user.Email!));
    }

    public async Task<Result> UpdateProfileAsync(
    int userId,
    UpdateProfileRequest request,
    CancellationToken cancellationToken = default)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());

        if (user is null)
            return Result.Failure(AuthenticationErrors.UserNotFound);

        user.FullName = request.FullName;

        var result = await _userManager.UpdateAsync(user);

        if (!result.Succeeded)
            return Result.Failure(
                ToIdentityError(result.Errors));

        return Result.Success();
    }
}