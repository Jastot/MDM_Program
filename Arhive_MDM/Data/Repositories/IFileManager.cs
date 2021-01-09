using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arhive_MDM.Data.Repositories
{
    interface IFileManager
    {
        /// <summary>Checks if file exists.</summary>
        /// <param name="guideId">Id of the guide.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>Returns file existence status.</returns>
        bool FileExists(int guideId, string fileName);





        /// <summary>Saves guide file.</summary>
        /// <param name="guideId">Id of the guide.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="file">File from array of bytes.</param>
        string CreateFileFolder(string fileName);

        /// <summary>Returns file as byte array.</summary>
        /// <param name="guideId">Id of the guide.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>Returns file as byte array.</returns>
        string GetFile(int guideId, string fileName);

        /// <summary>Deletes file.</summary>
        /// <param name="guideId">Id of the guide.</param>
        /// <param name="fileName">Name of the file.</param>

        void DeleteFolder(int guideId);
    }
}
