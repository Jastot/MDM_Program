using System;
using System.IO;
using System.Threading.Tasks;

namespace Arhive_MDM.Data.Repositories
{
    public class FileManager: IFileManager
    {
        private readonly string _storage;

        public FileManager()
        {
            var platform = Environment.OSVersion.Platform;
            var homePath = (platform == PlatformID.Unix || platform == PlatformID.MacOSX)
                ? Environment.GetEnvironmentVariable("HOME")
                : Environment.ExpandEnvironmentVariables("%HOMEDRIVE%%HOMEPATH%");
            
            _storage = Path.Combine(Path.Combine(homePath, "Desktop"), "PDFDocumentsStorage");
            Directory.CreateDirectory(_storage);
        }

        /// <inheritdoc />
        public bool FileExists(int FileId, string fileName)
        {
            var filePath = Path.Combine(_storage, FileId.ToString(), fileName);
            return File.Exists(filePath);
        }


        /// <inheritdoc />
        public string CreateFileFolder(string FileId)
        {
            var Folder = Path.Combine(_storage, FileId);
            Directory.CreateDirectory(Folder);
            return Folder;
        }

        /// <inheritdoc />
        public string GetFile(int guideId, string fileName)
        {
            return Path.Combine(_storage, guideId.ToString(), fileName);
        }

        /// <inheritdoc />
        public void DeleteFolder(int guideId)
        {
            var folderPath = Path.Combine(_storage, guideId.ToString());
            Directory.Delete(folderPath, true);
        }

       
    }
}
