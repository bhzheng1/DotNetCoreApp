using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace WebApi_Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FormDataController : ControllerBase
{
    [HttpPost("upload")]
    public Task<IActionResult> UploadFile(IFormFile file, IFormCollection formData)
    {
        Debug.WriteLine(file.FileName);
        Debug.WriteLine(file.Length);
        formData.TryGetValue("formData", out var formDataValue);
        Debug.WriteLine(formDataValue);
        return Task.FromResult<IActionResult>(Ok());
    }
}