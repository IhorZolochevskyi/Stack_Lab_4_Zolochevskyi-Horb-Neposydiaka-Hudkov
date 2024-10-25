using System;
using System.ComponentModel;
using System.IO;
using System.Threading;

namespace ClassLib
{
    public class FileService
    {
        private string filePath;
        private BackgroundWorker backgroundWorker;
        private ManualResetEvent pauseEvent;

        public long fileSize { get; private set; } 

        public FileService(string filePath, BackgroundWorker backgroundWorker, ManualResetEvent pauseEvent)
        {
            this.filePath = filePath;
            this.backgroundWorker = backgroundWorker;
            this.pauseEvent = pauseEvent;
            this.fileSize = new FileInfo(filePath).Length;
        }

        public void EncryptFile(Encryptor encryptor, long fileSize)
        {
            using (FileStream fsInput = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            using (FileStream fsOutput = new FileStream(filePath + ".enc", FileMode.Create, FileAccess.Write))
            {
                byte[] buffer = new byte[4096];
                int bytesRead;
                long totalBytesRead = 0;

                while ((bytesRead = fsInput.Read(buffer, 0, buffer.Length)) > 0)
                {
                    pauseEvent.WaitOne();

                    byte[] encrypted = encryptor.Encrypt(buffer.Take(bytesRead).ToArray());
                    fsOutput.Write(encrypted, 0, encrypted.Length);

                    totalBytesRead += bytesRead;
                    int progressPercentage = (int)((double)totalBytesRead / fileSize * 100);
                    backgroundWorker.ReportProgress(progressPercentage);

                    if (backgroundWorker.CancellationPending)
                    {
                        throw new OperationCanceledException();
                    }
                }
            }
        }

        public void DecryptFile(Encryptor encryptor, long fileSize)
        {
            using (FileStream fsInput = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            using (FileStream fsOutput = new FileStream(filePath.Replace(".enc", ""), FileMode.Create, FileAccess.Write))
            {
                byte[] buffer = new byte[fileSize];
                int bytesRead;
                long totalBytesRead = 0;

                while ((bytesRead = fsInput.Read(buffer, 0, buffer.Length)) > 0)
                {
                    pauseEvent.WaitOne();

                    byte[] decrypted = encryptor.Decrypt(buffer.Take(bytesRead).ToArray());
                    fsOutput.Write(decrypted, 0, decrypted.Length);

                    totalBytesRead += bytesRead;
                    int progressPercentage = (int)((double)totalBytesRead / fileSize * 100);
                    backgroundWorker.ReportProgress(progressPercentage);

                    if (backgroundWorker.CancellationPending)
                    {
                        throw new OperationCanceledException();
                    }
                }
            }
        }
    }
}
