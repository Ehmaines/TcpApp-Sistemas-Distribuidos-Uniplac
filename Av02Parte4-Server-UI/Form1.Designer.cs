namespace Av02Parte4_Server_UI
{
    partial class FormServer
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            groupBoxLogs = new GroupBox();
            textBoxLogs = new TextBox();
            groupBoxConnectedUsers = new GroupBox();
            listBoxConnectedUser = new ListBox();
            groupBoxLogs.SuspendLayout();
            groupBoxConnectedUsers.SuspendLayout();
            SuspendLayout();
            // 
            // groupBoxLogs
            // 
            groupBoxLogs.Controls.Add(textBoxLogs);
            groupBoxLogs.Location = new Point(12, 12);
            groupBoxLogs.Name = "groupBoxLogs";
            groupBoxLogs.Size = new Size(593, 438);
            groupBoxLogs.TabIndex = 0;
            groupBoxLogs.TabStop = false;
            groupBoxLogs.Text = "Logs";
            // 
            // textBoxLogs
            // 
            textBoxLogs.Location = new Point(11, 22);
            textBoxLogs.Multiline = true;
            textBoxLogs.Name = "textBoxLogs";
            textBoxLogs.Size = new Size(576, 410);
            textBoxLogs.TabIndex = 0;
            // 
            // groupBoxConnectedUsers
            // 
            groupBoxConnectedUsers.Controls.Add(listBoxConnectedUser);
            groupBoxConnectedUsers.Location = new Point(611, 12);
            groupBoxConnectedUsers.Name = "groupBoxConnectedUsers";
            groupBoxConnectedUsers.Size = new Size(200, 438);
            groupBoxConnectedUsers.TabIndex = 1;
            groupBoxConnectedUsers.TabStop = false;
            groupBoxConnectedUsers.Text = "Usuários Conectados";
            // 
            // listBoxConnectedUser
            // 
            listBoxConnectedUser.FormattingEnabled = true;
            listBoxConnectedUser.ItemHeight = 15;
            listBoxConnectedUser.Location = new Point(6, 22);
            listBoxConnectedUser.Name = "listBoxConnectedUser";
            listBoxConnectedUser.SelectionMode = SelectionMode.None;
            listBoxConnectedUser.Size = new Size(189, 409);
            listBoxConnectedUser.TabIndex = 0;
            // 
            // FormServer
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(817, 457);
            Controls.Add(groupBoxConnectedUsers);
            Controls.Add(groupBoxLogs);
            Name = "FormServer";
            Text = "Server";
            Load += FormServer_Load;
            groupBoxLogs.ResumeLayout(false);
            groupBoxLogs.PerformLayout();
            groupBoxConnectedUsers.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBoxLogs;
        private TextBox textBoxLogs;
        private GroupBox groupBoxConnectedUsers;
        private ListBox listBoxConnectedUser;
    }
}
