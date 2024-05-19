namespace DentistaApi.Models;

public class Funcionario : User
{

    public int IdCargo { get; set; }
    public Cargo cargo { get; set; }
    
}
