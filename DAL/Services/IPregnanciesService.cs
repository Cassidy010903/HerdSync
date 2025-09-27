using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Services
{
    public interface IPregnanciesService
    {
        public Task ArchiveAndDeletePregnancyAsync(Guid id, string deletedBy);
    }
}
