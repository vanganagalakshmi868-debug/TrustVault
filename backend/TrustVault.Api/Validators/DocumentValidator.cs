namespace TrustVault.Api.Validators;
public static class DocumentValidator
{
    public static bool IsValid(IFormFile? file)
    {
        if (file == null || file.Length == 0) return false;
        if (file.Length > 10 * 1024 * 1024) return false; // 10MB max
        return true;
    }
}