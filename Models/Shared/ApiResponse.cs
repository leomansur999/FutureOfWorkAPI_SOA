namespace FutureOfWorkAPI.Models.Shared;

public class ApiResponse<T>
{
    public bool Sucesso { get; set; }
    public string? Mensagem { get; set; }
    public T? Dados { get; set; }

    public static ApiResponse<T> Ok(T dados, string? mensagem = null)
        => new() { Sucesso = true, Dados = dados, Mensagem = mensagem };

    public static ApiResponse<T> Falha(string mensagem)
        => new() { Sucesso = false, Mensagem = mensagem, Dados = default };
}
