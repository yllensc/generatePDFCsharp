using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces;
    public interface IStudent : IGenericRepository<Student>
    {
        Task<string> GeneratePDFReport(Student student);
    }