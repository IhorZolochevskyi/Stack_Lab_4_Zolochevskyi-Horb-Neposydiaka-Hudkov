using System;
using System.ComponentModel;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using ClassLib;

namespace Lab4
{
    public partial class Form1 : Form
    {
        //private string selectedFilePath;
        //private string encryptionKey;
        //public Form1()
        //{
        //    InitializeComponent();
        //    progressBar1.Hide();
        //    cancelButton.Hide();
        //    pauseButton.Hide();
        //}
        //private void fileSelectButton_Click(object sender, EventArgs e)
        //{
        //    using (OpenFileDialog openFileDialog = new OpenFileDialog())
        //    {
        //        if (openFileDialog.ShowDialog() == DialogResult.OK)
        //        {
        //            selectedFilePath = openFileDialog.FileName;
        //        }
        //    }
        //}
        private BackgroundWorker backgroundWorker;
        private string selectedFilePath;
        private string encryptionKey;
        private bool isPaused = false;
        private ManualResetEvent pauseEvent = new ManualResetEvent(true);
        private System.Windows.Forms.Timer timer;
        private int secondsElapsed;

        public Form1()
        {
            InitializeComponent();
            InitializeBackgroundWorker();
            progressBar1.Hide();
            cancelButton.Hide();
            pauseButton.Hide();
            timerLabel.Text = "";
        }

        private void InitializeBackgroundWorker()
        {
            backgroundWorker = new BackgroundWorker();
            backgroundWorker.WorkerReportsProgress = true;
            backgroundWorker.WorkerSupportsCancellation = true;
            backgroundWorker.DoWork += BackgroundWorker_DoWork;
            backgroundWorker.ProgressChanged += BackgroundWorker_ProgressChanged;
            backgroundWorker.RunWorkerCompleted += BackgroundWorker_RunWorkerCompleted;
        }

        private void fileSelectButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    selectedFilePath = openFileDialog.FileName;
                }
            }
        }

        private void EncryptButton_Click(object sender, EventArgs e)
        {
            progressBar1.Show();
            cancelButton.Show();
            pauseButton.Show();
            encryptionKey = textBox1.Text;
            if (string.IsNullOrEmpty(encryptionKey) || string.IsNullOrEmpty(selectedFilePath))
            {
                MessageBox.Show("Please select a file and enter a key.");
                return;
            }
            timer = new System.Windows.Forms.Timer();
            timer.Interval = 1; // 1 секунда
            timer.Tick += Timer_Tick;
            timer.Start(); // Запуск таймера

            secondsElapsed = 0;

            backgroundWorker.RunWorkerAsync();
            timer.Stop();
        }
        private void Timer_Tick(object sender, EventArgs e) // Обработчик события Tick
        {
            secondsElapsed++;
            timerLabel.Text = $"Прошло секунд: {secondsElapsed}"; // Обновление Label
        }
        private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            var encryptor = new Encryptor(Encoding.UTF8.GetBytes(encryptionKey), new byte[16]);
            var fileService = new FileService(selectedFilePath, backgroundWorker, pauseEvent);

            try
            {
                fileService.EncryptFile(encryptor, fileService.fileSize);
            }
            catch (Exception ex)
            {
                e.Result = ex.Message;
            }
        }

        private void BackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }

        private void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                MessageBox.Show("Operation canceled.");
            }
            else if (e.Error != null || e.Result != null)
            {
                MessageBox.Show($"An error occurred: {e.Error?.Message ?? e.Result.ToString()}");
            }
            else
            {
                MessageBox.Show("File encrypted successfully!");
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            if (backgroundWorker.IsBusy)
            {
                backgroundWorker.CancelAsync();
            }
        }

        private void pauseButton_Click(object sender, EventArgs e)
        {
            if (isPaused)
            {
                pauseEvent.Set();
                pauseButton.Text = "Pause";
                isPaused = false;
            }
            else
            {
                pauseEvent.Reset();
                pauseButton.Text = "Resume";
                isPaused = true;
            }
        }

        private void DecryptButton_Click(object sender, EventArgs e)
        {
            progressBar1.Show();
            cancelButton.Show();
            pauseButton.Show();
            encryptionKey = textBox1.Text;

            if (string.IsNullOrEmpty(encryptionKey) || string.IsNullOrEmpty(selectedFilePath))
            {
                MessageBox.Show("Please select a file and enter a key.");
                return;
            }
            //qweqweqweqweqw==
            backgroundWorker.DoWork -= BackgroundWorker_DoWork;
            backgroundWorker.DoWork += BackgroundWorker_DoWork_Decrypt;

            backgroundWorker.RunWorkerAsync();
        }
        private void BackgroundWorker_DoWork_Decrypt(object sender, DoWorkEventArgs e)
        {
            var encryptor = new Encryptor(Encoding.UTF8.GetBytes(encryptionKey), new byte[16]);
            var fileService = new FileService(selectedFilePath, backgroundWorker, pauseEvent);

            try
            {
                fileService.DecryptFile(encryptor, fileService.fileSize);
            }
            catch (Exception ex)
            {
                e.Result = ex.Message;
            }
        }
    }
}
