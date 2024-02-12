using System.Diagnostics.CodeAnalysis;

namespace project_get_discount_back.Result
{

    [ExcludeFromCodeCoverage]
    public struct ResultError
    {
        public ResultError(string mensagem, string code)
        {
            Mensagem = mensagem;
            Code = code;
        }

        public string Mensagem { get; private set; }
        public string Code { get; private set; }


        public static ResultError EmailVazio => new("Email ou Senha errado.", "U001");
        public static ResultError PasswordVazio => new("Email ou Senha errado.", "U002");
        public static ResultError EmailNaoExiste => new("Email não existe.", "U003");
        public static ResultError EmailDeletado => new("Email não existe.", "U004");
        public static ResultError EmailOuPasswordErrado => new("Email ou Senha errado.", "U005");

    }
}
