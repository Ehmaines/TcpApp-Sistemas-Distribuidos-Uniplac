namespace Av02Parte4
{
    partial class Form1
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
            groupBoxMessageReceived = new GroupBox();
            textBoxMessageReceived = new TextBox();
            groupBoxConnectedUsers = new GroupBox();
            listBoxConnectedUsers = new ListBox();
            groupBoxSendMessage = new GroupBox();
            textBoxSendMessage = new TextBox();
            buttonSend = new Button();
            groupBoxActions = new GroupBox();
            buttonExit = new Button();
            buttonFile = new Button();
            buttonConnect = new Button();
            groupBoxMessageReceived.SuspendLayout();
            groupBoxConnectedUsers.SuspendLayout();
            groupBoxSendMessage.SuspendLayout();
            groupBoxActions.SuspendLayout();
            SuspendLayout();
            // 
            // groupBoxMessageReceived
            // 
            groupBoxMessageReceived.BackColor = SystemColors.Control;
            groupBoxMessageReceived.Controls.Add(textBoxMessageReceived);
            groupBoxMessageReceived.Location = new Point(12, 12);
            groupBoxMessageReceived.Name = "groupBoxMessageReceived";
            groupBoxMessageReceived.Size = new Size(1019, 586);
            groupBoxMessageReceived.TabIndex = 0;
            groupBoxMessageReceived.TabStop = false;
            groupBoxMessageReceived.Text = "Mensagens Recebidas";
            // 
            // textBoxMessageReceived
            // 
            textBoxMessageReceived.BackColor = SystemColors.ControlLightLight;
            textBoxMessageReceived.BorderStyle = BorderStyle.FixedSingle;
            textBoxMessageReceived.Cursor = Cursors.IBeam;
            textBoxMessageReceived.Location = new Point(6, 22);
            textBoxMessageReceived.Multiline = true;
            textBoxMessageReceived.Name = "textBoxMessageReceived";
            textBoxMessageReceived.ReadOnly = true;
            textBoxMessageReceived.Size = new Size(1007, 558);
            textBoxMessageReceived.TabIndex = 0;
            // 
            // groupBoxConnectedUsers
            // 
            groupBoxConnectedUsers.Controls.Add(listBoxConnectedUsers);
            groupBoxConnectedUsers.Location = new Point(1037, 12);
            groupBoxConnectedUsers.Name = "groupBoxConnectedUsers";
            groupBoxConnectedUsers.Size = new Size(213, 586);
            groupBoxConnectedUsers.TabIndex = 1;
            groupBoxConnectedUsers.TabStop = false;
            groupBoxConnectedUsers.Text = "Usuários Conectados";
            // 
            // listBoxConnectedUsers
            // 
            listBoxConnectedUsers.FormattingEnabled = true;
            listBoxConnectedUsers.ItemHeight = 15;
            listBoxConnectedUsers.Location = new Point(6, 22);
            listBoxConnectedUsers.Name = "listBoxConnectedUsers";
            listBoxConnectedUsers.SelectionMode = SelectionMode.MultiSimple;
            listBoxConnectedUsers.Size = new Size(207, 559);
            listBoxConnectedUsers.TabIndex = 0;
            // 
            // groupBoxSendMessage
            // 
            groupBoxSendMessage.Controls.Add(textBoxSendMessage);
            groupBoxSendMessage.Controls.Add(buttonSend);
            groupBoxSendMessage.Location = new Point(17, 616);
            groupBoxSendMessage.Name = "groupBoxSendMessage";
            groupBoxSendMessage.Size = new Size(1233, 53);
            groupBoxSendMessage.TabIndex = 2;
            groupBoxSendMessage.TabStop = false;
            groupBoxSendMessage.Text = "Envio de Mensagens";
            // 
            // textBoxSendMessage
            // 
            textBoxSendMessage.Location = new Point(6, 22);
            textBoxSendMessage.Name = "textBoxSendMessage";
            textBoxSendMessage.Size = new Size(1122, 23);
            textBoxSendMessage.TabIndex = 0;
            textBoxSendMessage.KeyDown += textBoxSendMessage_KeyDown;
            // 
            // buttonSend
            // 
            buttonSend.Cursor = Cursors.Hand;
            buttonSend.Enabled = false;
            buttonSend.Location = new Point(1134, 18);
            buttonSend.Name = "buttonSend";
            buttonSend.Size = new Size(93, 29);
            buttonSend.TabIndex = 1;
            buttonSend.Text = "Enviar";
            buttonSend.UseVisualStyleBackColor = true;
            buttonSend.Click += buttonSend_Click;
            // 
            // groupBoxActions
            // 
            groupBoxActions.Controls.Add(buttonExit);
            groupBoxActions.Controls.Add(buttonFile);
            groupBoxActions.Controls.Add(buttonConnect);
            groupBoxActions.Location = new Point(18, 678);
            groupBoxActions.Name = "groupBoxActions";
            groupBoxActions.Size = new Size(1232, 55);
            groupBoxActions.TabIndex = 3;
            groupBoxActions.TabStop = false;
            groupBoxActions.Text = "Ações";
            // 
            // buttonExit
            // 
            buttonExit.Cursor = Cursors.Hand;
            buttonExit.Location = new Point(204, 20);
            buttonExit.Name = "buttonExit";
            buttonExit.Size = new Size(93, 29);
            buttonExit.TabIndex = 3;
            buttonExit.Text = "Sair";
            buttonExit.UseVisualStyleBackColor = true;
            buttonExit.Click += buttonExit_Click;
            // 
            // buttonFile
            // 
            buttonFile.Cursor = Cursors.Hand;
            buttonFile.Location = new Point(105, 20);
            buttonFile.Name = "buttonFile";
            buttonFile.Size = new Size(93, 29);
            buttonFile.TabIndex = 2;
            buttonFile.Text = "Arquivo";
            buttonFile.UseVisualStyleBackColor = true;
            buttonFile.Click += buttonFile_Click;
            // 
            // buttonConnect
            // 
            buttonConnect.Cursor = Cursors.Hand;
            buttonConnect.Location = new Point(6, 20);
            buttonConnect.Name = "buttonConnect";
            buttonConnect.Size = new Size(93, 29);
            buttonConnect.TabIndex = 0;
            buttonConnect.Text = "Conectar";
            buttonConnect.UseVisualStyleBackColor = true;
            buttonConnect.Click += buttonConnect_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1262, 742);
            Controls.Add(groupBoxActions);
            Controls.Add(groupBoxSendMessage);
            Controls.Add(groupBoxConnectedUsers);
            Controls.Add(groupBoxMessageReceived);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            groupBoxMessageReceived.ResumeLayout(false);
            groupBoxMessageReceived.PerformLayout();
            groupBoxConnectedUsers.ResumeLayout(false);
            groupBoxSendMessage.ResumeLayout(false);
            groupBoxSendMessage.PerformLayout();
            groupBoxActions.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBoxMessageReceived;
        public TextBox textBoxMessageReceived;
        private GroupBox groupBoxConnectedUsers;
        private ListBox listBoxConnectedUsers;
        private GroupBox groupBoxSendMessage;
        private TextBox textBoxSendMessage;
        private GroupBox groupBoxActions;
        private Button buttonConnect;
        private Button buttonExit;
        private Button buttonFile;
        private Button buttonSend;
    }
}
