﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Arhive_MDM.Models;

namespace Arhive_MDM.Data.Repositories
{
    interface IDocumentsRepository
    {
        /// <summary>Gets document.</summary>
        /// <param name="documentId">Id of the document.</param>
        /// <returns>Returns document.</returns>
        Task<Documents> GetDocuments(int documentId);

        /// <summary>Gets all documents.</summary>
        /// <returns>Returns all documents.</returns>
        Task<List<Documents>> GetDocuments();

        Task<List<Documents>> GetDocumentsByTime(DateTime dataFrom, DateTime dataTo);

        /// <summary>Creates document.</summary>
        /// <param name="document">document data.</param>
        /// <returns>Returns id of the new document.</returns>
        Task<int> CreateDocuments(Documents document);

        /// <summary>Updates document.</summary>
        /// <param name="document">document data.</param>
        Task UpdateDocuments(Documents document);

        /// <summary>Removes document.</summary>
        /// <param name="document">document data.</param>
        Task RemoveDocuments(Documents document);
    }
}
