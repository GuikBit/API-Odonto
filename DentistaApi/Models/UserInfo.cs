using DentistaApi.Models.Utils;

namespace DentistaApi.Models;

public class UserInfo
{
    public string Login { get; set; } = "";
    public string Password { get; set; } = "";

    public string GerarHash()
    {
        return Password.GerarHash();
    }
}
