namespace DentistaApi.Models;

public enum Status
{
    Aprovado = 1,
    Pendente = 2,
    Rejeitado = 3
}

public enum TipoRegistro
{
    InicioExpediente = 1,
    FimExpediente = 2,
    InicioAlmoco = 3,
    FimAlmoco = 4,
    InicioPausa = 5,
    FimPausa = 6
}