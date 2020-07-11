using System;
using System.IO;
using System.Threading.Tasks;

namespace BookShelfAPI.Storage
{
    public interface IBookStorage
    {
        Task<string> PersistBookFile(Stream file, string bookName);
        Task<string> PersistCoverFile(Stream file, string coverName);
        void DeleteBookFileRelated(string fileUri);
    }
}