using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace project_get_discount_back.Results
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


        public static ResultError LoginEmailEmpty => new("Email ou Senha errado.", "U001");
        public static ResultError LoginPasswordEmpty => new("Email ou Senha errado.", "U002");
        public static ResultError NonExistentEmail => new("Email não existe.", "U003");
        public static ResultError NonExistentPassword => new("Password não existe.", "U004");
        public static ResultError EmailDeleted => new("Email não existe.", "U005");
        public static ResultError WrongEmailOrPassword => new("Email ou Senha errado.", "U006");
        public static ResultError DoesNotContainPassword => new("Email ou Senha errado.", "U007");
        public static ResultError RegisterNameEmpty => new("Nome obrigatório.", "R001");
        public static ResultError RegisterEmailEmpty => new("Email obrigatório.", "R002");
        public static ResultError RegisterRoleInvalido => new("Tipo de acesso Invalido.", "R003");
        public static ResultError AlreadyRegisteredUser => new("Usuário já cadastrado.", "R004");
        public static ResultError ErrorWhenSendingEmail => new ("Erro ao enviar o email.", "R005");
        public static ResultError RegisterEmailPasswordEmpty => new("Email vazio.", "P001");
        public static ResultError RegisterPasswordEmpty => new("Senha vazia.", "P002");
        public static ResultError UserNotFound => new("Não foi encontrado nenhum usuário.", "P003");
        public static ResultError EmailNotDelivered => new("Não foi possível entregar o email.", "P004");

    }
}
