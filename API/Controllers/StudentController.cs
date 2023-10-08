using API.Dtos;
using API.Services;
using AutoMapper;
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
    private readonly IMapper _mapper;

    public StudentController(ICompositeViewEngine viewEngine, IUnitOfWork unitOfWork, IPdfService pdfService, IMapper mapper)
    {
        _mapper = mapper;
        _pdfService = pdfService;
        _unitOfWork = unitOfWork;
        _viewEngine = viewEngine;
    }
    [HttpGet("Student/Report/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<StudentDto>> GetById(int id)
    {
        var student = await _unitOfWork.Students.GetByIdAsync(id);
        if(student == null)
        {
            return BadRequest();
        }

        return _mapper.Map<StudentDto>(student);
    }
    [HttpGet("Students/Reports")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<StudentDto>>> Get()
    {
        var student = await _unitOfWork.Students.GetAllAsync();
        if(student == null)
        {
            return BadRequest();
        }

        return _mapper.Map<List<StudentDto>>(student);
    }

    [HttpGet("generate-reportStudent/{studentId}")]
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
            var studentData = _mapper.Map<StudentDto>(student);
            var html = GenerateHtml(studentData);
            var pdfBytes = _pdfService.GeneratePdf(html);
            return File(pdfBytes, "application/pdf", $"reportStudent-{student.NameStudent}.pdf");
        }
        catch (Exception ex)
        {
            return BadRequest($"Error al generar el informe: {ex.Message}");
        }
    }

    [HttpGet("generate-reportStudents")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GeneratePDFs()
    {
        var students = await _unitOfWork.Students.GetAllAsync();

        try
        {            
            var studentsData = _mapper.Map<List<StudentDto>>(students);
            
            var htmlContents = new List<string>();
            foreach (var studentData in studentsData)
            {
                var html = GenerateHtml(studentData);
                htmlContents.Add(html);
            }
            var pdfBytes = _pdfService.GeneratePdfs(htmlContents);
            return File(pdfBytes, "application/pdf", "informe.pdf");
        }
        catch (Exception ex)
        {
            // Maneja errores aquí.
            return BadRequest($"Error al generar el informe: {ex.Message}");
        }
    }
    public string GenerateHtml(StudentDto student)
    {
        // Crea una instancia de la clase StringWriter para capturar la salida HTML.
        var sw = new StringWriter();
        // Configura el contexto de vista.
        var viewContext = new ViewContext
        {
            HttpContext = HttpContext,
            RouteData = RouteData,
            ActionDescriptor = new ActionDescriptor(),
            ViewData = new ViewDataDictionary<StudentDto>(new EmptyModelMetadataProvider(), new ModelStateDictionary())
            {Model = student }, // Asigna el modelo a la vista.
            Writer = sw
        };
        var viewName = "report";
        var viewResult = _viewEngine.FindView(ControllerContext, viewName, false);
        if (viewResult.View == null)
        { return null;}
        // Renderiza la vista en una cadena.
        var viewEngineResult = viewResult.View.RenderAsync(viewContext);
        // Espera a que se complete la renderización.
        viewEngineResult.Wait();
        // Obtiene la cadena HTML generada.
        var html = sw.ToString();
        return html;
    }
}

