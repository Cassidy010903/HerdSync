using DAL.Models;
using DAL.Services;

namespace BLL.Services.Implementation
{
    public class SessionService : ISessionService
    {
        private readonly ISessionRepository _repo;

        public SessionService(ISessionRepository repo)
        {
            _repo = repo;
            _repo.OnAnimalAction += (action) => OnAnimalAction?.Invoke(action);
        }

        public event Action<ast_Animal_Session_Treatment>? OnAnimalAction;

        public ase_Active_Session? Current => _repo.Current;

        public Task<ase_Active_Session> StartAsync(Guid programId, string? notes = null)
        {
            return _repo.StartAsync(programId, notes);
        }

        public Task EndAsync(Guid sessionId)
        {
            return _repo.EndAsync(sessionId);
        }

        public Task AddExtraTreatmentAsync(Guid animalActionId, Guid treatmentId, string? doseOverride = null)
        {
            return _repo.AddExtraTreatmentAsync(animalActionId, treatmentId, doseOverride);
        }

        public Task<ase_Active_Session?> ResumeAsync()
        {
            return _repo.ResumeAsync();
        }
    }
}