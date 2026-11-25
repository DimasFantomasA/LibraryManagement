
namespace LibraryManagement
{
    internal static class UserSession
    {
        public static string Username { get; set; } = "";

        public static string Role { get; set; } = "";

        public static bool IsAdmin =>
            Role == "administrator";
    }
}
