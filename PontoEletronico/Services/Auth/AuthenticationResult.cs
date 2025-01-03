using DentistaApi.Models;

namespace DentistaApi.Services
{
    internal class AuthenticationResult
    {
        public EReturnStatus Status { get; set; }
        public User Usuario { get; set; }
        public string Token { get; set; }
    }
}