namespace Lab4
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
            keyLabel = new Label();
            fileSelectButton = new Button();
            pauseButton = new Button();
            cancelButton = new Button();
            textBox1 = new TextBox();
            progressBar1 = new ProgressBar();
            timerLabel = new Label();
            DecryptButton = new Button();
            EncryptButton = new Button();
            SuspendLayout();
            // 
            // keyLabel
            // 
            keyLabel.AutoSize = true;
            keyLabel.Location = new Point(104, 18);
            keyLabel.Name = "keyLabel";
            keyLabel.Size = new Size(64, 15);
            keyLabel.TabIndex = 0;
            keyLabel.Text = "Enter a key";
            // 
            // fileSelectButton
            // 
            fileSelectButton.Location = new Point(251, 45);
            fileSelectButton.Name = "fileSelectButton";
            fileSelectButton.Size = new Size(75, 23);
            fileSelectButton.TabIndex = 2;
            fileSelectButton.Text = "SELECT FILE";
            fileSelectButton.UseVisualStyleBackColor = true;
            fileSelectButton.Click += fileSelectButton_Click;
            // 
            // pauseButton
            // 
            pauseButton.Location = new Point(170, 183);
            pauseButton.Name = "pauseButton";
            pauseButton.Size = new Size(75, 23);
            pauseButton.TabIndex = 3;
            pauseButton.Text = "Pause";
            pauseButton.UseVisualStyleBackColor = true;
            pauseButton.Click += pauseButton_Click;
            // 
            // cancelButton
            // 
            cancelButton.Location = new Point(251, 183);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(75, 23);
            cancelButton.TabIndex = 4;
            cancelButton.Text = "Cancel";
            cancelButton.UseVisualStyleBackColor = true;
            cancelButton.Click += cancelButton_Click;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(12, 45);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(233, 23);
            textBox1.TabIndex = 5;
            // 
            // progressBar1
            // 
            progressBar1.Location = new Point(12, 155);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(314, 23);
            progressBar1.Step = 1;
            progressBar1.TabIndex = 6;
            // 
            // timerLabel
            // 
            timerLabel.AutoSize = true;
            timerLabel.Location = new Point(12, 128);
            timerLabel.Name = "timerLabel";
            timerLabel.Size = new Size(25, 15);
            timerLabel.TabIndex = 7;
            timerLabel.Text = "123";
            // 
            // DecryptButton
            // 
            DecryptButton.Location = new Point(170, 74);
            DecryptButton.Name = "DecryptButton";
            DecryptButton.Size = new Size(75, 23);
            DecryptButton.TabIndex = 8;
            DecryptButton.Text = "Decrypt";
            DecryptButton.UseVisualStyleBackColor = true;
            DecryptButton.Click += DecryptButton_Click;
            // 
            // EncryptButton
            // 
            EncryptButton.Location = new Point(89, 74);
            EncryptButton.Name = "EncryptButton";
            EncryptButton.Size = new Size(75, 23);
            EncryptButton.TabIndex = 9;
            EncryptButton.Text = "Encrypt";
            EncryptButton.UseVisualStyleBackColor = true;
            EncryptButton.Click += EncryptButton_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(331, 218);
            Controls.Add(EncryptButton);
            Controls.Add(DecryptButton);
            Controls.Add(timerLabel);
            Controls.Add(progressBar1);
            Controls.Add(textBox1);
            Controls.Add(cancelButton);
            Controls.Add(pauseButton);
            Controls.Add(fileSelectButton);
            Controls.Add(keyLabel);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label keyLabel;
        private Button fileSelectButton;
        private Button pauseButton;
        private Button cancelButton;
        private TextBox textBox1;
        private ProgressBar progressBar1;
        private Label timerLabel;
        private Button DecryptButton;
        private Button EncryptButton;
    }
}
