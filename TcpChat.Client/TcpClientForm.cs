using Microsoft.VisualBasic;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Av02Parte4
{
    public partial class TcpClientForm : Form
    {
        private TcpClientApp _client;
        public TcpClientForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        public void AddMessage(string mensagem)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string>(AddMessage), mensagem);
                return;
            }

            textBoxMessageReceived.AppendText(mensagem + Environment.NewLine);
        }

        public void ReloadUsers(string[] connectedUsers)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string[]>(ReloadUsers), new object[] { connectedUsers });
                return;
            }

            listBoxConnectedUsers.Items.Clear();
            listBoxConnectedUsers.Items.AddRange(connectedUsers);
        }

        public async Task SaveReceivedFileAsync(string fileName, byte[] fileContent)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => SaveReceivedFileInternal(fileName, fileContent)));
                return;
            }

            await SaveFileAsync(fileName, fileContent);
        }

        private void SaveReceivedFileInternal(string fileName, byte[] fileContent)
        {
            _ = SaveFileAsync(fileName, fileContent);
        }

        private async Task SaveFileAsync(string fileName, byte[] fileContent)
        {
            FileReceived modal = new FileReceived(fileName, fileContent);
            modal.ShowDialog();
        }

        private async void buttonSend_Click(object sender, EventArgs e)
        {
            string message = textBoxSendMessage.Text.Trim();
            if (!string.IsNullOrEmpty(message))
            {
                if (listBoxConnectedUsers.SelectedItems.Count > 0)
                {
                    var command = "/whisper ";
                    foreach (var item in listBoxConnectedUsers.SelectedItems)
                    {
                        command = command + item.ToString() + ',';
                    }
                    message = $"{command} {message}";
                }

                await _client.SendMessageAsync(message);
                textBoxSendMessage.Clear();
                textBoxSendMessage.Focus();
            }
        }

        private async void buttonConnect_Click(object sender, EventArgs e)
        {
            string name = null;
            using (var modal = new UserName())
            {
                if (modal.ShowDialog(this) == DialogResult.OK)
                {
                    name = modal.Username;
                }
            }

            if (!string.IsNullOrWhiteSpace(name))
            {
                _client = new TcpClientApp("127.0.0.1", 8080, this);
                await _client.StartAsync();

                await _client.SendMessageAsync("/setname " + name);
                buttonSend.Enabled = true;
                buttonConnect.Enabled = false;
                buttonFile.Enabled = true;
                TcpClientForm.ActiveForm.Text = "Chat - " + name;
            }
        }

        private async void buttonExit_Click(object sender, EventArgs e)
        {
            await _client.SendMessageAsync("/disconnect");
            buttonSend.Enabled = false;
            buttonConnect.Enabled = true;
            listBoxConnectedUsers.Items.Clear();
        }

        private async void buttonFile_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "C:\\";
                openFileDialog.Filter = "All files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName;
                    if (listBoxConnectedUsers.SelectedItems.Count > 0)
                    {
                        var namesToSendFile = "";
                        foreach (var item in listBoxConnectedUsers.SelectedItems)
                        {
                            namesToSendFile = namesToSendFile + item.ToString() + ',';
                        }
                        await _client.SendFileWhisperAsync(filePath, namesToSendFile);
                        return;
                    }
                    await _client.SendFileAsync(filePath);
                }
            }
        }
        private void textBoxSendMessage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                buttonSend.PerformClick();
            }
        }
    }
}
