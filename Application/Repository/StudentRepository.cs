using System;
using System.IO;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Persistence;
using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Application.Repository
{
    public class StudentRepository : GenericRepository<Student>, IStudent
    {
        private readonly GeneratorPDFDbContext _context;


        public StudentRepository(GeneratorPDFDbContext context) : base(context)
        {
            _context = context;
        }
        
        public override async Task<Student> GetByIdAsync(int id)
        {
            return await _context.Students
                .Include(p => p.Notes).ThenInclude(p => p.Subject)
                .FirstOrDefaultAsync(p => p.Id == id);

        }
        public override async Task<IEnumerable<Student>> GetAllAsync()
        {
            return await _context.Students
                .Include(p => p.Notes).ThenInclude(p => p.Subject)
                .ToListAsync();
        }
    }
}
