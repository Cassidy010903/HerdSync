//using DAL.Models;
//using DAL.Services;

//namespace DAL.Repositories.Implementation
//{
//    public class ProgramService : IProgramService
//    {
//        private readonly IProgramRepository _repo;

//        public ProgramService(IProgramRepository repo)
//        {
//            _repo = repo;
//        }

//        public Task<List<prg_Program>> ListAsync()
//        {
//            return _repo.ListAsync();
//        }

//        public Task<prg_Program?> GetAsync(Guid programId)
//        {
//            return _repo.GetAsync(programId);
//        }
//    }
//}