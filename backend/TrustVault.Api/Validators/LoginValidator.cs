namespace TrustVault.Api.Validators;
public static class LoginValidator
{
    public static bool IsValid(string email, string password)
        => !string.IsNullOrWhiteSpace(email) && email.Contains("@") && !string.IsNullOrWhiteSpace(password) && password.Length >= 6;
}