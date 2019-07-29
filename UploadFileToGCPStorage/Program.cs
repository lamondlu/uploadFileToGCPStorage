using System;
using System.IO;
using Google.Cloud.Storage.V1;

namespace UploadFileToGCPStorage
{
    class Program
    {
        private static readonly string _bucketName = "";
        private static readonly string _projectId = "";

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            StorageClient storageClient = StorageClient.Create();

            try
            {
                var bucket = storageClient.GetBucket(_bucketName);

                // foreach (var storageObject in storageClient.ListObjects(_bucketName, ""))
                // {
                //     using (var outputFile = File.OpenWrite(storageObject.Name))
                //     {
                //         storageClient.DownloadObject(_bucketName, storageObject.Name, outputFile);
                //     }
                // }
                using (var f = File.OpenRead("test.txt"))
                {
                    storageClient.UploadObject(_bucketName, "test.txt", null, f);
                    Console.WriteLine($"Uploaded test.txt.");
                }
            }
            catch (Google.GoogleApiException e)
            when (e.Error.Code == 409)
            {
                // The bucket already exists.  That's fine.
                Console.WriteLine(e.Error.Message);
            }

            Console.ReadLine();
        }
    }

    public class FileUploader
    {
        private readonly StorageClient _storageClient;


        public FileUploader()
        {
            _storageClient = StorageClient.Create();
        }

        public void GetBucket(string bucketName)
        {
            var bucket = _storageClient.GetBucket(bucketName);
        }
    }
}
