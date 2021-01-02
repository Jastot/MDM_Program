using Arhive_MDM.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Arhive_MDM.Data.Repositories
{
    interface IWorkerRepository
    {
        /// <summary>Gets worker.</summary>
        /// <param name="workerId">Id of the worker.</param>
        /// <returns>Returns worker.</returns>
        Task<Worker> GetWorker(int workerId);

        /// <summary>Gets worker.</summary>
        /// <param name="login">Login of the worker.</param>
        /// <returns>Returns worker.</returns>
        Task<Worker> GetWorker(string login);

        /// <summary>Gets all workers.</summary>
        /// <returns>Returns all workers.</returns>
        Task<List<Worker>> GetWorkers();

        /// <summary>Creates worker.</summary>
        /// <param name="worker">worker data.</param>
        /// <returns>Returns id of the new worker.</returns>
        Task<int> CreateWorker(Worker worker);

        /// <summary>Updates worker.</summary>
        /// <param name="worker">worker data.</param>
        Task UpdateWorker(Worker worker);

        /// <summary>Removes worker.</summary>
        /// <param name="worker">worker data.</param>
        Task RemoveWorker(Worker worker);
    }
}
