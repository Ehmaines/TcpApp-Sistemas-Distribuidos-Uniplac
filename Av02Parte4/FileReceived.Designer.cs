namespace Av02Parte4
{
    partial class FileReceived
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
            groupBoxFileReceived = new GroupBox();
            buttonCancel = new Button();
            buttonDownloadFile = new Button();
            labelQuestDownload = new Label();
            labelFileName = new Label();
            labelFileReceived = new Label();
            groupBoxFileReceived.SuspendLayout();
            SuspendLayout();
            // 
            // groupBoxFileReceived
            // 
            groupBoxFileReceived.Controls.Add(buttonCancel);
            groupBoxFileReceived.Controls.Add(buttonDownloadFile);
            groupBoxFileReceived.Controls.Add(labelQuestDownload);
            groupBoxFileReceived.Controls.Add(labelFileName);
            groupBoxFileReceived.Controls.Add(labelFileReceived);
            groupBoxFileReceived.Font = new Font("Segoe UI", 9F);
            groupBoxFileReceived.Location = new Point(12, 12);
            groupBoxFileReceived.Name = "groupBoxFileReceived";
            groupBoxFileReceived.Size = new Size(541, 246);
            groupBoxFileReceived.TabIndex = 0;
            groupBoxFileReceived.TabStop = false;
            groupBoxFileReceived.Text = "Arquivo Recebido";
            // 
            // buttonCancel
            // 
            buttonCancel.Font = new Font("Segoe UI", 16F);
            buttonCancel.Location = new Point(276, 196);
            buttonCancel.Name = "buttonCancel";
            buttonCancel.Size = new Size(206, 39);
            buttonCancel.TabIndex = 5;
            buttonCancel.Text = "Cancelar";
            buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonDownloadFile
            // 
            buttonDownloadFile.Font = new Font("Segoe UI", 16F);
            buttonDownloadFile.Location = new Point(6, 196);
            buttonDownloadFile.Name = "buttonDownloadFile";
            buttonDownloadFile.Size = new Size(221, 39);
            buttonDownloadFile.TabIndex = 4;
            buttonDownloadFile.Text = "Baixar";
            buttonDownloadFile.UseVisualStyleBackColor = true;
            buttonDownloadFile.Click += buttonDownloadFile_Click;
            // 
            // labelQuestDownload
            // 
            labelQuestDownload.AutoSize = true;
            labelQuestDownload.Font = new Font("Segoe UI", 16F);
            labelQuestDownload.Location = new Point(6, 163);
            labelQuestDownload.Name = "labelQuestDownload";
            labelQuestDownload.Size = new Size(249, 30);
            labelQuestDownload.TabIndex = 3;
            labelQuestDownload.Text = "Deseja Baixar o arquivo?";
            // 
            // labelFileName
            // 
            labelFileName.AutoSize = true;
            labelFileName.Font = new Font("Segoe UI", 16F);
            labelFileName.ForeColor = SystemColors.MenuHighlight;
            labelFileName.Location = new Point(6, 101);
            labelFileName.Name = "labelFileName";
            labelFileName.Size = new Size(245, 30);
            labelFileName.TabIndex = 2;
            labelFileName.Text = "NOMEDOARQUIVO.TXT";
            // 
            // labelFileReceived
            // 
            labelFileReceived.AutoSize = true;
            labelFileReceived.Font = new Font("Segoe UI", 16F);
            labelFileReceived.Location = new Point(6, 35);
            labelFileReceived.Name = "labelFileReceived";
            labelFileReceived.Size = new Size(185, 30);
            labelFileReceived.TabIndex = 0;
            labelFileReceived.Text = "Arquivo Recebido";
            // 
            // FileReceived
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = buttonCancel;
            ClientSize = new Size(561, 269);
            Controls.Add(groupBoxFileReceived);
            Name = "FileReceived";
            StartPosition = FormStartPosition.CenterParent;
            Text = "FileReceived";
            groupBoxFileReceived.ResumeLayout(false);
            groupBoxFileReceived.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBoxFileReceived;
        private Label labelFileReceived;
        private Button buttonDownloadFile;
        private Label labelQuestDownload;
        private Label labelFileName;
        private Button buttonCancel;
    }
}