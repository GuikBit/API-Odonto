namespace DentistaApi.Models
{
    public class Administrador : User
    {
        public void SetRole()
        {
            if (this.Role == null || this.Role == "")
            {
                this.Role = "Admin";
            }

        }
    }
}