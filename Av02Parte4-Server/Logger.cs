using System.Text;

namespace Av02Parte4_Server
{
    public static class Logger
    {
        private static readonly string LogPath = "log.txt";

        public static async Task LogAsync(string ipRemetente, string nomeRemetente, string ipsDestinatarios, string nomesDestinatarios, string acao, string detalhe = "")
        {
            string timestamp = DateTime.Now.ToString("dd/MM/yyyy; HH:mm");
            string linhaLog = $"{timestamp}; {ipRemetente}; {nomeRemetente}; {ipsDestinatarios}; {nomesDestinatarios}; {acao}";

            if (!string.IsNullOrWhiteSpace(detalhe))
                linhaLog += $":{detalhe}";

            try
            {
                await File.AppendAllTextAsync(LogPath, linhaLog + Environment.NewLine, Encoding.UTF8);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[LOG ERRO] {ex.Message}");
            }
        }

        public static async Task LogAsync(string detalhe = "")
        {
            string timestamp = DateTime.Now.ToString("dd/MM/yyyy; HH:mm");

            var linhaLog = $"{timestamp}:{detalhe}";

            try
            {
                await File.AppendAllTextAsync(LogPath, linhaLog + Environment.NewLine, Encoding.UTF8);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[LOG ERRO] {ex.Message}");
            }
        }
    }
}
