using DAL.Configuration.Database;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Services.Implementation
{
    public class TagRepository : ITagRepository
    {
        private readonly HerdsyncDBContext _context;

        public TagRepository(HerdsyncDBContext context)
        {
            _context = context;
        }
        public async Task AddTag(stl_Species_Tag_Lookup stl)
        {
            if (stl == null)
                throw new ArgumentNullException(nameof(stl));

            _context.SpeciesTag.Add(stl);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateTag(stl_Species_Tag_Lookup stl)
        {
            var existing = await _context.SpeciesTag.FindAsync(stl.Id);
            if (existing == null)
                throw new InvalidOperationException("Tag not found.");

            // Update fields
            existing.stl_Tag_Id = stl.stl_Tag_Id;
            existing.spd_Id = stl.spd_Id;

            await _context.SaveChangesAsync();
        }
        public async Task<List<stl_Species_Tag_Lookup>> GetAllTagsAsync()
        {
            return await _context.SpeciesTag.ToListAsync();
        }
    }
}
