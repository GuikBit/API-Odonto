using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace DentistaApi.Models
{
    public class Administrador : User
    {

        public void SetRole()
        {
            if (string.IsNullOrEmpty(Role))
            {
                this.Role = "Admin";
            }

        }
    }
}