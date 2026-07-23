public sealed record AuthResultDto(
    int UserId,
    string FullName,
    string Email,
    string AccessToken,
    string RefreshToken,
    DateTime ExpiresAt);