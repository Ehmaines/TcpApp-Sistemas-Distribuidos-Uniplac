using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Av02Parte4
{
    public partial class FileReceived : Form
    {
        private string _fileName;
        private byte[] _fileContent;

        public FileReceived(string filename, byte[] fileContent)
        {
            InitializeComponent();
            _fileName = filename;
            _fileContent = fileContent;
            labelFileName.Text = filename;
        }

        private async void buttonDownloadFile_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.FileName = _fileName;
                saveFileDialog.Filter = "All files (*.*)|*.*";
                saveFileDialog.Title = "Salvar arquivo recebido";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    await File.WriteAllBytesAsync(saveFileDialog.FileName, _fileContent);
                    MessageBox.Show($"Arquivo salvo com sucesso: {saveFileDialog.FileName}");
                }
            }
            this.Close();
        }
    }
}
