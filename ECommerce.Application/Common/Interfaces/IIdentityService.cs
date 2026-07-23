using ECommerce.Application.Features.Authentication.Commands.Login;
using ECommerce.Application.Features.Authentication.DTOs;
using ECommerce.Application.Features.Users.DTOs;
using System;
using System.Collections.Generic;
using System.Text;


namespace ECommerce.Application.Common.Interfaces
{
    public interface IIdentityService
    {
        Task<Result<AuthResultDto>> RegisterAsync(
            RegisterRequest request,
            CancellationToken cancellationToken = default);

        Task<Result<AuthResultDto>> LoginAsync(
            LoginRequest request,
            CancellationToken cancellationToken = default);

        Task<Result<AuthResultDto>> RefreshTokenAsync(
            string refreshToken,
            CancellationToken cancellationToken = default);

        Task<Result> ChangePasswordAsync(
            int userId,
            ChangePasswordRequest request,
            CancellationToken cancellationToken = default);

        Task<Result> LogoutAsync(
            int userId,
            CancellationToken cancellationToken = default);

        Task<Result<UserProfileDto>> GetProfileAsync(
            int userId,
            CancellationToken cancellationToken = default);

        Task<Result> UpdateProfileAsync(
            int userId,
            UpdateProfileRequest request,
            CancellationToken cancellationToken = default);
    }
}
