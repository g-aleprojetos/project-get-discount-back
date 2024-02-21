namespace project_get_discount_back._2_Infrastructure.Schemas.Responses
{
    public record ErroResponse(string Codigo, string Mensagem);
    public class ErrosResponse
    {
        public IEnumerable<ErroResponse> Erros { get; }

        public ErrosResponse(string codigo, string mensagem) => Erros = new List<ErroResponse> { new(codigo, mensagem)};


    }
}
