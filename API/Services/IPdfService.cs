
using Domain.Interfaces;

namespace API.Services;
public interface IPdfService
{
    byte[] GeneratePdf(string htmlContent);
}
