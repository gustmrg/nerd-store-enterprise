using FluentValidation.Results;
using NSE.Core.Data;

namespace NSE.Core.Messages;

public abstract class CommandHandler
{
    protected CommandHandler()
    {
        ValidationResult = new ValidationResult();
    }
    
    protected ValidationResult ValidationResult { get; set; }

    protected void AddError(string message)
    {
        ValidationResult.Errors.Add(new ValidationFailure(string.Empty, message));
    }

    protected async Task<ValidationResult> SaveDataAsync(IUnitOfWork unitOfWork)
    {
        if (!await unitOfWork.Commit()) AddError("Something went wrong saving data");
        
        return ValidationResult;
    }
}