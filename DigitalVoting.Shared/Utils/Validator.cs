using System.Text.RegularExpressions;

namespace DigitalVoting.Shared.Utils
{
    public static class Validator
    {
        public static bool ValidUsername(string username)
        {
            var regex = new Regex(@"^(?=[a-zA-Z])[-\w.]{0,23}([a-zA-Z\d]|(?<![-.])_)$");

            return regex.IsMatch(username);
        }

        public static bool ValidPassword(string password)
        {
            var regex = new Regex(@"^.*(?=.{8,})(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!*@#$%^&+=]).*$");

            return regex.IsMatch(password);
        }
    }
}