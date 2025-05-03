using Av02Parte4_Server;

namespace Av02Parte4_Server_UI
{
    public partial class FormServer : Form
    {
        private TcpServer _server;
        public FormServer()
        {
            InitializeComponent();
        }

        private async void FormServer_Load(object sender, EventArgs e)
        {
            _server = new TcpServer(8080, this);
            await _server.StartAsync();
        }

        public void AddMessage(string mensagem)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string>(AddMessage), mensagem);
                return;
            }

            textBoxLogs.AppendText(mensagem + Environment.NewLine);
        }

        public void ResetConnectedClients(string[] connectedClients)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string[]>(ResetConnectedClients), new object[] { connectedClients });
                return;
            }

            listBoxConnectedUser.Items.Clear();
            listBoxConnectedUser.Items.AddRange(connectedClients);
        }
    }
}
