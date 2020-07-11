using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace BookShelfAPI.Storage
{
    public class BlobBookStorage : IBookStorage
    {
        private readonly AzureBlobStorage _storage;

        public BlobBookStorage(IConfiguration configuration)
        {
            _storage = new AzureBlobStorage(configuration);
        }
         public BlobBookStorage()
        {
            _storage = new AzureBlobStorage();
        }
        public async Task<string> PersistBookFile(Stream file, string bookName)
        {
            try
            {
                CheckParameters(file, bookName);
                var formattedBookName = bookName.Replace(' ', '-');
                return await _storage.UploadFile(file, formattedBookName);
            }
            catch
            {
                throw;
            }

        }
        public async Task<string> PersistCoverFile(Stream file, string coverName)
        {
            try
            {
                CheckParameters(file, coverName);
                var formattedCoverName = coverName.Replace(' ', '-');
                return await _storage.UploadFile(file, formattedCoverName);
            }
            catch
            {
                throw;
            }
        }
        public void CheckParameters(Stream file, string bookName)
        {
            if (bookName is null)
            {
                throw new ArgumentNullException(nameof(bookName));
            }
            if (file is null)
            {
                throw new ArgumentNullException(nameof(file));
            }
        }
        public void DeleteBookFileRelated(string fileUri)
        {
            if(fileUri is null)
            {
                throw new ArgumentException(nameof(fileUri));
            }
            var coverName = ExtractFileNameFromUri(fileUri);
            _storage.RemoveFile(coverName);
        }

        public string ExtractFileNameFromUri(string uri)
        {
            if(uri is null)
            {
                throw new ArgumentNullException(nameof(uri));
            }
            var splittedUrl = uri.Split('/');
            return splittedUrl[splittedUrl.Length-1];
        }
    }
}