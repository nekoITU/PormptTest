
namespace ProjectName.ControllersExceptions;

public class BusinessException : Exception
{
    public string Code { get; }
    public string Description { get; }
    public string Category { get; }

    public BusinessException(string code, string description, string category)
    {
        Code = code;
        Description = description;
        Category = category;
    }
}

public class TechnicalException : Exception
{
    public string Code { get; }
    public string Description { get; }

    public string Category { get; }

    public TechnicalException(string code, string description, string category)
    {
        Code = code;
        Description = description;
        Category = category;    
    }
}

public static class ExceptionSerializer
{
    public static Types.ResponseException Serialize(Exception exception)
    {
        if (exception is BusinessException businessException)
        {
            return new Types.ResponseException
            {
                Id = Guid.NewGuid(),
                Code = businessException.Code,
                Description = businessException.Description,
                Category = businessException.Category
            };
        }
        else if (exception is TechnicalException technicalException)
        {
            return new Types.ResponseException
            {
                Id = Guid.NewGuid(),
                Code = technicalException.Code,
                Description = technicalException.Description,
                Category = technicalException.Category 
            };
        }
        else
        {
            // Default to technical exception for any other exceptions
            return new Types.ResponseException
            {
                Id = Guid.NewGuid(),
                Code = "1001",
                Description = "A technical exception has occurred, please contact your system administrator"
            };
        }
    }
}