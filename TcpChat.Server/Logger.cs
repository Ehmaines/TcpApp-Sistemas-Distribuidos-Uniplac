using Av02Parte4_Server_UI;
using System.Text;

namespace Av02Parte4_Server
{
    public class Logger
    {
        private TcpServerForm _form;
        private readonly string LogPath = "log.txt";

        public Logger(TcpServerForm form)
        {
            _form = form;
        }

        public async Task LogAsync(string ipRemetente, string nomeRemetente, string acao, List<Client> destinatarios = null, string detalhe = "")
        {
            string timestamp = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
            if (destinatarios == null)
                destinatarios = new List<Client>();
            var ipsDestinatarios = String.Join(", ", destinatarios.Select(d => d.client.Client.RemoteEndPoint.ToString()));
            var nomesDestinatarios = String.Join(", ", destinatarios.Select(d => d.Name));

            if (string.IsNullOrWhiteSpace(ipsDestinatarios))
                ipsDestinatarios = "Servidor";

            string linhaLog = $"{timestamp}; {ipRemetente}; {nomeRemetente}; {ipsDestinatarios}; {nomesDestinatarios}; {acao} ";

            if (!string.IsNullOrWhiteSpace(detalhe))
                linhaLog += $":{detalhe}";

            try
            {
                _form.AddMessage(linhaLog);
                await File.AppendAllTextAsync(LogPath, linhaLog + Environment.NewLine, Encoding.UTF8);
            }
            catch (Exception ex)
            {
                _form.AddMessage($"[LOG ERRO] {ex.Message}");
            }
        }

        public async Task LogAsync(string detalhe = "")
        {
            string timestamp = DateTime.Now.ToString("dd/MM/yyyy HH:mm");

            var linhaLog = $"{timestamp}:{detalhe}";

            try
            {
                _form.AddMessage(linhaLog);
                await File.AppendAllTextAsync(LogPath, linhaLog + Environment.NewLine, Encoding.UTF8);
            }
            catch (Exception ex)
            {
                _form.AddMessage($"[LOG ERRO] {ex.Message}");
            }
        }
    }
}
