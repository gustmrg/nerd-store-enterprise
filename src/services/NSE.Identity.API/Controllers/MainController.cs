using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace NSE.Identity.API.Controllers;

[ApiController]
public abstract class MainController : ControllerBase
{
    protected ICollection<string> Errors { get; set; } = new List<string>();
    
    protected ActionResult CustomResponse(object result = null)
    {
        if (!HasErrors())
        {
            return Ok(result);
        }
        
        return BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]>
        {
            { "Messages", Errors.ToArray() }, 
        }));
    }

    protected ActionResult CustomResponse(ModelStateDictionary modelState)
    {
        var errors = modelState.Values.SelectMany(e => e.Errors);

        foreach (var error in errors)
        {
            AddError(error.ErrorMessage);
        }
        
        return CustomResponse();
    }
    
    protected bool HasErrors() => Errors.Any();
    
    protected void AddError(string error) => Errors.Add(error);
    
    protected void ClearErrors() => Errors.Clear();
}