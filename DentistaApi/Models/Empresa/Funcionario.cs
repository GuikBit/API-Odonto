namespace DentistaApi.Models;

public class Funcionario : User
{

    public int IdCargo { get; set; }
    public Cargo cargo { get; set; }
    public void SetRole(int nivelAcesso)
    {
        if (string.IsNullOrEmpty(Role))
        {
            if (nivelAcesso == 1)
            {
                this.Role = "Recepcao";
            }
            else if (nivelAcesso == 2)
            {
                this.Role = "Financeiro";
            }
            else if (nivelAcesso == 3)
            {
                this.Role = "Fatumento";
            }
            else if (nivelAcesso == 4)
            {
                this.Role = "Dentista";
            }
            else
            {
                this.Role = "SemAcessoSistema";
            }
        } 

    }
}

    




