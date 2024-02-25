using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using project_get_discount_back._1_Domain.Interfaces;
using project_get_discount_back.Entities;

namespace project_get_discount_back._1_Domain.Helpers
{
    public class Email(IConfiguration configuration) : IEmail
    {
        private readonly IConfiguration _configuration = configuration;

        async Task<bool> IEmail.SendEmailAsync(User user, string subject, string messageBody)
        {
            try
            {
                string host = _configuration.GetValue<string>("SMTP:Host") ?? "";
                string nameSistema = _configuration.GetValue<string>("SMTP:Name") ?? "";
                string userName = _configuration.GetValue<string>("SMTP:UserName") ?? "";
                string password = _configuration.GetValue<string>("SMTP:Password") ?? "";
                int port = _configuration.GetValue<int>("SMTP:Port");

                if (string.IsNullOrEmpty(host) || string.IsNullOrEmpty(nameSistema) || string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
                {
                    return false;
                }

                var message = new MimeMessage();
                message.Priority = MessagePriority.Urgent;
                message.From.Add(new MailboxAddress(nameSistema, userName));
                message.To.Add(new MailboxAddress("", user.Email));
                message.Subject = subject;
                message.Body = new TextPart(TextFormat.Html) { Text = PageEmail(user) };


                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync(host, port, SecureSocketOptions.StartTls);
                    await client.AuthenticateAsync(userName, password);
                    await client.SendAsync(message);
                    await client.DisconnectAsync(true);
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public static string GetFirstName(string fullName)
        {
            string[] NameParts = fullName.Split(' ');
            string firstName = NameParts[0];

            return $"{char.ToUpper(firstName[0])}{firstName[1..].ToLower()}";
        }

        private static string PageEmail(User user)
        {
            string imageUrl = "https://raw.githubusercontent.com/g-aleprojetos/project-get-discount-back/feature/configura-email/project-get-discount-back/1-Domain/Assets/logo.png";
            string Response = "<div style=\"font-family:Arial, sans-serif;width:100%;background-color:#f4f4f4;text-align:center;margin:10px;\">";
            Response += "<header style=\"background-color:#333;padding:10px 0;width:100%;\">";
            Response += $"<img src=\"{imageUrl}\" alt=\"Icone da empresa\" width=\"100\" height=\"100\" />";
            Response += " </header>";
            Response += "<h1>Cadastro de Usuário</h1>";
            Response += $"<h2>Olá {GetFirstName(user.Name)},</h2>";
            Response += "<h3>Seu cadastro como usuário no nosso sistema foi realizado com sucesso! Abaixo estão os detalhes do seu cadastro:</h3>";
            Response += "<table width=\"100%\" align=\"center\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">";
            Response += "<tr>";
            Response += "<td align=\"center\">";
            Response += "<ul style=\"list-style-type:none;padding:0;margin:10px 0;\">";
            Response += $"<li><strong>Nome:</strong> {user.Name}</li>";
            Response += $"<li><strong>Email:</strong> {user.Email}</li>";
            Response += "</ul>";
            Response += "</td>";
            Response += "</tr>";
            Response += "</table>";
            Response += "<p style=\"margin-bottom: 20px;\">Clique no botão abaixo para cadastrar uma senha segura e acessar o sistema:</p>";
            Response += "<button style=\"background-color: #083CF5; color: #fff; padding: 10px 20px; border: none; border-radius: 5px; cursor: pointer; margin-top: 20px;\">";
            Response += "<a href=\"www.google.com.br\" target=\"_blank\" style=\"text-decoration: none; color: #fff;\">Cadastrar Senha</a>";
            Response += "</button>";
            Response += "<p style=\"margin-top: 20px;\">Obrigado!</p>";
            Response += "<footer style=\"background-color: #333; color: #fff; text-align: center;position: absolute; bottom: 0; width: 100%;\">";
            Response += "<p>by g.aleprojetos</p>";
            Response += "</footer>";
            Response += "</body>";
            return Response;
        }
    }
}
