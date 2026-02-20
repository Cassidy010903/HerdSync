using DAL.Models;

namespace BLL.Services
{
    public interface ISessionService
    {
        Task<ase_Active_Session> StartAsync(Guid programId, string? notes = null);

        Task EndAsync(Guid sessionId);

        Task<ase_Active_Session?> ResumeAsync();

        public event Action<ast_Animal_Session_Treatment>? OnAnimalAction;

        Task AddExtraTreatmentAsync(Guid animalActionId, Guid treatmentId, string? doseOverride = null);

        ase_Active_Session? Current { get; }
    }
}