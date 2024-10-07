using System;
using System.Threading;
using System.Threading.Tasks;

namespace ffiledownloader
{
    public delegate void ProgressChangedHandler(int percentage);
    public delegate void DownloadCompletedHandler();

    class FileDownloader
    {
        public event ProgressChangedHandler ProgressChanged;
        public event DownloadCompletedHandler DownloadCompleted;

        public void DownloadFile()
        {
            Task.Run(() =>
            {
                for (int i = 0; i < 100; i++) 
                {
                    OnProgressChanged(i);
                    Thread.Sleep(500);
                }
                OnDownloadCompleted();
            });
        }

        protected virtual void OnProgressChanged(int percentage)
        {
            ProgressChanged.Invoke(percentage); 
        }

        protected virtual void OnDownloadCompleted()
        {
            DownloadCompleted.Invoke(); 
        }
    }

    class UserInterface
    {
        public void OnProgressChanged(int percentage)
        {
            Console.WriteLine($"Download progress: {percentage}%");
        }

        public void OnDownloadCompleted()
        {
            Console.WriteLine("Download completed! Congratulations on successfully downloading the file.");
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            FileDownloader downloader = new FileDownloader();
            UserInterface userInterface = new UserInterface();

            
            downloader.ProgressChanged += userInterface.OnProgressChanged;
            downloader.DownloadCompleted += userInterface.OnDownloadCompleted; 

            Console.WriteLine("Starting file download...");
            downloader.DownloadFile();

            
            Console.ReadLine(); 
        }
    }
}
