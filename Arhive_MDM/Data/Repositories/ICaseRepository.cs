using Arhive_MDM.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Arhive_MDM.Data.Repositories
{
    interface ICaseRepository
    {
        /// <summary>Gets Case.</summary>
        /// <param name="caseId">Id of the Case.</param>
        /// <returns>Returns Case.</returns>
        Task<Case> GetCase(int caseId);

        /// <summary>Gets all Case.</summary>
        /// <returns>Returns all Case.</returns>
        Task<List<Case>> GetCases();

        /// <summary>Get all worker Case.</summary>
        /// <param name="workerId">Id of the worker Cases.</param>
        /// <returns>Returns all worker's Cases.</returns>
        Task<List<Case>> GetWorkerCases(int workerId);

        /// <summary>Creates Case.</summary>
        /// <param name="Case">Case data.</param>
        /// <returns>Returns id of the new Case.</returns>
        Task<int> CreateCase(Case Case);

        /// <summary>Updates Case.</summary>
        /// <param name="Case">Case data.</param>
        Task UpdateCase(Case Case);

        /// <summary>Removes Case and it's content.</summary>
        /// <param name="Case">Case data.</param>
        Task RemoveCase(Case Case);

    }
}
