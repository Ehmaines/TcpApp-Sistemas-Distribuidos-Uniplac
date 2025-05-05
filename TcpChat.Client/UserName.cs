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
    public partial class UserName : Form
    {
        public string Username { get; set; }
        public UserName()
        {
            InitializeComponent();
        }

        private void buttonUserNameOk_Click(object sender, EventArgs e)
        {
            Username = textBoxUserName.Text; // pega o texto do TextBox
            this.DialogResult = DialogResult.OK; // fecha o modal com "sucesso"
            this.Close();
        }

        private void textBoxUserName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ' ')
            {
                e.Handled = true;
            }
        }
    }
}
