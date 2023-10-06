/*using API.Services;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace API.Controllers
{
    public class StudentController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPdfService _pdfService;

        public StudentController(IUnitOfWork unitOfWork, IPdfService pdfService)
        {
            _pdfService = pdfService;
            _unitOfWork = unitOfWork;
        }

        [HttpGet("generate-pdf/{studentId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GeneratePDF(int studentId)
        {
            var student = await _unitOfWork.Students.GetByIdAsync(studentId);

            if (student == null)
            {
                return BadRequest("Estudiante no encontrado");
            }

            try
            {
                var html = await _unitOfWork.Students.GeneratePDFReport(student);
                var pdfBytes = _pdfService.GeneratePdf(html);
                return File(pdfBytes, "application/pdf", "informe.pdf");
            }
            catch (Exception ex)
            {
                // Maneja errores aquí.
                return BadRequest($"Error al generar el informe: {ex.Message}");
            }
        }
    }

}*/
using System.IO;
using API.Dtos;
using API.Services;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
namespace API.Controllers;
public class StudentController : Controller
{
    private readonly ICompositeViewEngine _viewEngine;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPdfService _pdfService;

    public StudentController(ICompositeViewEngine viewEngine, IUnitOfWork unitOfWork, IPdfService pdfService)
    {
        _pdfService = pdfService;
        _unitOfWork = unitOfWork;
        _viewEngine = viewEngine;
    }

    [HttpGet("generate-pdf/{studentId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GeneratePDF(int studentId)
        {
            var student = await _unitOfWork.Students.GetByIdAsync(studentId);

            if (student == null)
            {
                return BadRequest("Estudiante no encontrado");
            }

            try
            {
                var html = GenerateHtml();
                var pdfBytes = _pdfService.GeneratePdf(html);
                return File(pdfBytes, "application/pdf", "informe.pdf");
            }
            catch (Exception ex)
            {
                // Maneja errores aquí.
                return BadRequest($"Error al generar el informe: {ex.Message}");
            }
        }
    public string GenerateHtml()
    {
        var model = new StudentDto
        {
            NameStudent = "Nombre del estudiante",
            StudentIdentification = "12345",
            Profile = "ruta/de/imagen.jpg",
            Notes = new List<NotesDto>
            {
                new NotesDto { Subject = "Matemáticas", Note = 3.0 },
                new NotesDto { Subject = "Ciencias", Note = 4.6 },
            }
        };



        // Crea una instancia de la clase StringWriter para capturar la salida HTML.
        var sw = new StringWriter();

        // Configura el contexto de vista.
        var viewContext = new ViewContext
        {
            HttpContext = HttpContext,
            RouteData = RouteData,
            ActionDescriptor = new ActionDescriptor(),
            ViewData = new ViewDataDictionary<StudentDto>(new EmptyModelMetadataProvider(), new ModelStateDictionary())
            {
                Model = model // Asigna el modelo a la vista.
            },
            Writer = sw
        };

        var viewName = "report";
        var viewResult = _viewEngine.FindView(ControllerContext, viewName, false);


        if (viewResult.View == null)
        {
            return null;
        }

        // Renderiza la vista en una cadena.
        var viewEngineResult = viewResult.View.RenderAsync(viewContext);

        // Espera a que se complete la renderización.
        viewEngineResult.Wait();

        // Obtiene la cadena HTML generada.
        var html = sw.ToString();

        return html;
    }
}

