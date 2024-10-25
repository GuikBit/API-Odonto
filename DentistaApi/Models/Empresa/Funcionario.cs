namespace DentistaApi.Models;

public class Funcionario : User
{
    public string RG { get; set; }
    public string RgUF { get; set; }
    public string OrgEmissor { get; set; }
    public string PisPasep { get; set; }
    public string CTPSN { get; set; }
    public string CTPSSerie { get; set; }
    public string CTPSUF { get; set; }
    public DateTime DataAdmissao { get; set; }
    public DateTime DataRescisao { get; set; }
    public string RegistroN { get; set; }

    public int IdEndereco { get; set; }
    public Endereco Endereco { get; set; }

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

    




