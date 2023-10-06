using System;
using System.IO;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Persistence;
using DinkToPdf;
using DinkToPdf.Contracts;

namespace Application.Repository
{
    public class StudentRepository : GenericRepository<Student>, IStudent
    {
        private readonly GeneratorPDFDbContext _context;


        public StudentRepository(GeneratorPDFDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<string> GeneratePDFReport(Student student)
        {
            try
            {
                var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "Views", "report.cshtml");

                if (!File.Exists(fullPath))
                {
                    throw new FileNotFoundException("La vista report.html no se encuentra en la ubicación especificada.");
                }

                var htmlContent = await File.ReadAllTextAsync(fullPath);
                htmlContent = htmlContent.Replace("@Model.ImagenUrl", student.Profile);
                htmlContent = htmlContent.Replace("@Model.Nombre", student.NameStudent);
                htmlContent = htmlContent.Replace("@Model.Codigo", student.StudentIdentification);
                var listaDePruebas = new List<Prueba>
                {
                    new Prueba() { Materia = "Matemáticas", Valor = "5" },
                    new Prueba() { Materia = "Ciencias", Valor = "4" },
                    new Prueba() { Materia = "Historia", Valor = "3" }
                };
                var tableHtml = "<thead><tr><th>Materia</th><th>Nota</th></tr></thead><tbody>";
                foreach (var prueba in listaDePruebas)
                {
                    tableHtml += $"<tr><td>{prueba.Materia}</td><td>{prueba.Valor}</td></tr>";
                }
                tableHtml += "</tbody>";
                htmlContent = htmlContent.Replace("@Model.TablaNotas", tableHtml);
                var htmlReady = htmlContent;
                return htmlReady;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }


    }

    public class Prueba
    {

        public string Materia { get; set; }
        public string Valor { get; set; }
    }
}