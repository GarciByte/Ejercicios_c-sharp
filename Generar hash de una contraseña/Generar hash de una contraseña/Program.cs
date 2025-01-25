using static System.Runtime.InteropServices.JavaScript.JSType;

class Program
{
    static void Main(string[] args)
    {
        string password = "123456";
        string hashedPassword = PasswordHelper.Hash(password);
        bool iguales = PasswordHelper.Verify(password, hashedPassword);

        Console.WriteLine($"Password: {password}");
        Console.WriteLine($"Hashed Password: {hashedPassword}");

        if (iguales)
        {
            Console.WriteLine($"La contraseña {password} coincide con el hash \"{hashedPassword}\".");
        }
    }
}

internal class PasswordHelper
{
    public static string Hash(string password)
    {
        byte[] inputBytes = System.Text.Encoding.UTF8.GetBytes(password);
        byte[] inputHash = System.Security.Cryptography.SHA256.HashData(inputBytes);
        return Convert.ToBase64String(inputHash);
    }

    public static bool Verify(string password, string hashedPassword)
    {
        string hashedInputPassword = Hash(password);
        return hashedInputPassword == hashedPassword;
    }

}
