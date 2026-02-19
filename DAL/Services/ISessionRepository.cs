using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Services
{
    public interface ISessionRepository
    {
        Task<ase_Active_Session> StartAsync(Guid programId, string? notes = null);
        Task EndAsync(Guid sessionId);
        ase_Active_Session? Current { get; }
        public event Action<ast_Animal_Session_Treatment>? OnAnimalAction;
        Task<ase_Active_Session?> ResumeAsync();

        // called by LiveList to add extra treatments or edit dose
        Task AddExtraTreatmentAsync(Guid animalActionId, Guid treatmentId, string? doseOverride = null);

    }
}
