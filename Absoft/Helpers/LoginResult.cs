using Absoft.ViewModels;

namespace Absoft.Helpers
{
    public class LoginResult
    {
        public LoginResult()
        {
        }

        public LoginResult(bool status, string token, UserSessionViewModel user)
        {
            Status = status;
            Token = token;
            User = user;
        }

        public LoginResult(bool status, string message)
        {
            Status = status;
            Message = message;
        }

        public string Token { get; set; }
        public UserSessionViewModel User { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }
    }
}
