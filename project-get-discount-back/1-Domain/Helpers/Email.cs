using MailKit;
using MailKit.Net.Imap;
using MailKit.Net.Smtp;
using MailKit.Search;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using project_get_discount_back._1_Domain.Interfaces;
using project_get_discount_back.Entities;
using System.Text.RegularExpressions;

namespace project_get_discount_back._1_Domain.Helpers
{
    public class Email(IConfiguration configuration) : IEmail
    {
        private readonly IConfiguration _configuration = configuration;

        async Task<bool> IEmail.SendEmailAsync(User user, EmailType emailType)
        {
            try
            {
                string subject = emailType == EmailType.CREATEUSER ? "Cadastro de usuário" : "Cadastro de senha";
                string page = emailType == EmailType.CREATEUSER ? UserRegistrationPage(user) : PasswordRegistrationPage(user);
                string host = _configuration.GetValue<string>("SMTP:Host") ?? "";
                string hostIn = _configuration.GetValue<string>("SMTP:HostIn") ?? "";
                string nameSistema = _configuration.GetValue<string>("SMTP:Name") ?? "";
                string userName = _configuration.GetValue<string>("SMTP:UserName") ?? "";
                string password = _configuration.GetValue<string>("SMTP:Password") ?? "";
                int port = _configuration.GetValue<int>("SMTP:Port");

                if (string.IsNullOrEmpty(host) || string.IsNullOrEmpty(hostIn) || string.IsNullOrEmpty(nameSistema) || string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
                {
                    return false;
                }

                var email = new MimeMessage();
                email.Priority = MessagePriority.Urgent;
                email.From.Add(new MailboxAddress(nameSistema, userName));
                email.To.Add(new MailboxAddress("", user.Email));
                email.Subject = subject;
                email.Body = new TextPart(TextFormat.Html) { Text = page };


                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync(host, port, SecureSocketOptions.StartTls);
                    await client.AuthenticateAsync(userName, password);
                    await client.SendAsync(email);
                    await client.DisconnectAsync(true);
                }

                using (var client = new ImapClient())
                {
                    using (var cancel = new CancellationTokenSource())
                    {
                        client.Connect(hostIn, 993, true, cancel.Token);
                        client.Authenticate(userName, password, cancel.Token);
                        var folderName = "notificacao email";
                        var inbox = client.GetFolder(folderName);
                        inbox.Open(FolderAccess.ReadWrite, cancel.Token);

                        var unreadQuery = SearchQuery.NotSeen;

                        foreach (var uid in inbox.Search(unreadQuery, cancel.Token))
                        {
                            var message = inbox.GetMessage(uid, cancel.Token);
                            inbox.AddFlags(uid, MessageFlags.Seen, true, cancel.Token);
                            if (CheckEmail(message.TextBody, user.Email))
                            {
                                client.Disconnect(true, cancel.Token);
                                return false;
                            }
                        }
                        client.Disconnect(true, cancel.Token);
                        return true;
                    }
                }
            }
            catch
            {
                return false;
            }
        }

        public static bool CheckEmail(string textBody, string email)
        {
            var emailPattern = @"[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}";
            var regex = new Regex(emailPattern);
            var match = regex.Match(textBody);

            if (match.Success && match.Value == email)
            {
                return true;
            }
            return false;
        }


        public static string GetFirstName(string fullName)
        {
            string[] NameParts = fullName.Split(' ');
            string firstName = NameParts[0];

            return $"{char.ToUpper(firstName[0])}{firstName[1..].ToLower()}";
        }

        private static string UserRegistrationPage(User user)
        {
            string imageUrl = "https://raw.githubusercontent.com/g-aleprojetos/project-get-discount-back/main/project-get-discount-back/1-Domain/Assets/logo.png";
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

        private static string PasswordRegistrationPage(User user)
        {
            string imageUrl = "https://raw.githubusercontent.com/g-aleprojetos/project-get-discount-back/main/project-get-discount-back/1-Domain/Assets/logo.png";
            string Response = "<div style=\"font-family:Arial, sans-serif;width:100%;background-color:#f4f4f4;text-align:center;margin:10px;\">";
            Response += "<header style=\"background-color:#333;padding:10px 0;width:100%;\">";
            Response += $"<img src=\"{imageUrl}\" alt=\"Icone da empresa\" width=\"100\" height=\"100\" />";
            Response += " </header>";
            Response += "<h1>Cadastro de Senha</h1>";
            Response += $"<h2>Olá {GetFirstName(user.Name)},</h2>";
            Response += "<h3>Parabéns sua senha foi cadastrada com sucesso!</h3>";
            Response += "<p style=\"margin-bottom: 20px;\">Clique no botão abaixo para logar:</p>";
            Response += "<button style=\"background-color: #083CF5; color: #fff; padding: 10px 20px; border: none; border-radius: 5px; cursor: pointer; margin-top: 20px;\">";
            Response += "<a href=\"www.google.com.br\" target=\"_blank\" style=\"text-decoration: none; color: #fff;\">Logar</a>";
            Response += "</button>";
            Response += "<p style=\"margin-top: 20px;\">Obrigado!</p>";
            Response += "<footer style=\"background-color: #333; color: #fff; text-align: center;position: absolute; bottom: 0; width: 100%;\">";
            Response += "<p>by g.aleprojetos</p>";
            Response += "</footer>";
            Response += "</body>";
            return Response;
        }

        public enum EmailType
        {
            CREATEUSER,
            CREATERPASSWORD
        }
    }
}
