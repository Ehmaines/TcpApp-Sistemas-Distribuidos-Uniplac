namespace Av02Parte4
{
    partial class UserName
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            textBoxUserName = new TextBox();
            label1 = new Label();
            buttonUserNameOk = new Button();
            buttonUserNameCancel = new Button();
            SuspendLayout();
            // 
            // textBoxUserName
            // 
            textBoxUserName.Location = new Point(12, 29);
            textBoxUserName.Name = "textBoxUserName";
            textBoxUserName.Size = new Size(269, 23);
            textBoxUserName.TabIndex = 0;
            textBoxUserName.KeyPress += textBoxUserName_KeyPress;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(18, 8);
            label1.Name = "label1";
            label1.Size = new Size(96, 15);
            label1.TabIndex = 1;
            label1.Text = "Digite Seu Nome";
            // 
            // buttonUserNameOk
            // 
            buttonUserNameOk.Location = new Point(12, 69);
            buttonUserNameOk.Name = "buttonUserNameOk";
            buttonUserNameOk.Size = new Size(75, 23);
            buttonUserNameOk.TabIndex = 2;
            buttonUserNameOk.Text = "Ok";
            buttonUserNameOk.UseVisualStyleBackColor = true;
            buttonUserNameOk.Click += buttonUserNameOk_Click;
            // 
            // buttonUserNameCancel
            // 
            buttonUserNameCancel.Location = new Point(93, 69);
            buttonUserNameCancel.Name = "buttonUserNameCancel";
            buttonUserNameCancel.Size = new Size(75, 23);
            buttonUserNameCancel.TabIndex = 3;
            buttonUserNameCancel.Text = "Cancelar";
            buttonUserNameCancel.UseVisualStyleBackColor = true;
            // 
            // UserName
            // 
            AcceptButton = buttonUserNameOk;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = buttonUserNameCancel;
            ClientSize = new Size(293, 116);
            Controls.Add(buttonUserNameCancel);
            Controls.Add(buttonUserNameOk);
            Controls.Add(label1);
            Controls.Add(textBoxUserName);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "UserName";
            StartPosition = FormStartPosition.CenterParent;
            Text = "UserName";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBoxUserName;
        private Label label1;
        private Button buttonUserNameOk;
        private Button buttonUserNameCancel;
    }
}