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

    public void SetRole(int perfil)
    {
        if (string.IsNullOrEmpty(Role))
        {
            if (perfil == 1)
            {
                this.Role = "RECEPCAO";
            }
            else if (perfil == 2)
            {
                this.Role = "FINANCEIRO";
            }
            else if (perfil == 3)
            {
                this.Role = "FATURAMENTO";
            }
            else if (perfil == 4)
            {
                this.Role = "DENTISTA";
            }
            else
            {
                this.Role = "SemAcessoSistema";
            }
        } 

    }
}

    




