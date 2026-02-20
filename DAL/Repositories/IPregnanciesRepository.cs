namespace DAL.Services
{
    public interface IPregnanciesRepository
    {
        public Task ArchiveAndDeletePregnancyAsync(Guid id, string deletedBy);
    }
}