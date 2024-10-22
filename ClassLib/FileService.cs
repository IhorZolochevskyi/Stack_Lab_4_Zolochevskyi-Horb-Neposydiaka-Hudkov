using System;
using System.ComponentModel;
using System.IO;
using System.Threading;

namespace ClassLib
{
    public class FileService
    {
        private readonly string inputFilePath;
        private readonly BackgroundWorker backgroundWorker;
        private readonly ManualResetEvent pauseEvent;
        public long fileSize;

        public FileService(string inputFilePath, BackgroundWorker backgroundWorker, ManualResetEvent pauseEvent)
        {
            this.inputFilePath = inputFilePath;
            this.backgroundWorker = backgroundWorker;
            this.pauseEvent = pauseEvent;
            this.fileSize = new FileInfo(inputFilePath).Length;
        }

        public long EncryptFile(IEncryptor encryptor,long sizeOfFile)
        {
            using (FileStream inputFile = new FileStream(inputFilePath, FileMode.Open))
            using (FileStream outputFile = new FileStream(inputFilePath + ".enc", FileMode.Create))
            {
                byte[] buffer = new byte[sizeOfFile];
                int bytesRead;
                long totalBytesRead = 0;

                while ((bytesRead = inputFile.Read(buffer, 0, buffer.Length)) > 0)
                {
                    pauseEvent.WaitOne(); // Ожидание для паузы

                    if (backgroundWorker.CancellationPending)
                    {
                        throw new OperationCanceledException();
                    }

                    byte[] encryptedData = encryptor.Encrypt(buffer.Take(bytesRead).ToArray());
                    outputFile.Write(encryptedData, 0, encryptedData.Length);

                    totalBytesRead += bytesRead;
                    int progress = (int)((double)totalBytesRead / fileSize * 100);
                    backgroundWorker.ReportProgress(progress);
                }
            }
            return fileSize;
        }
        public void DecryptFile(IEncryptor encryptor, long sizeOfFile)
        {
            using (FileStream inputFile = new FileStream(inputFilePath, FileMode.Open))
            using (FileStream outputFile = new FileStream(inputFilePath.Replace(".enc", ""), FileMode.Create))
            {
                byte[] buffer = new byte[sizeOfFile];
                int bytesRead;
                long totalBytesRead = 0;
                
                while ((bytesRead = inputFile.Read(buffer, 0, buffer.Length)) > 0)
                {
                    pauseEvent.WaitOne(); // Ожидание для паузы

                    if (backgroundWorker.CancellationPending)
                    {
                        throw new OperationCanceledException();
                    }

                    byte[] decryptedData = encryptor.Decrypt(buffer.Take(bytesRead).ToArray());
                    outputFile.Write(decryptedData, 0, decryptedData.Length);

                    totalBytesRead += bytesRead;
                    int progress = (int)((double)totalBytesRead / fileSize * 100);
                    backgroundWorker.ReportProgress(progress);
                }
            }
        }
    }
}