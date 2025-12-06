using Microsoft.EntityFrameworkCore;


namespace LeadQualifier.Infrastructure.ExceptionHandling;


public static class DbUpdateExceptionHandler
{
    public static string Normalize(Exception ex)
    {
        if (ex is DbUpdateException dbEx && dbEx.InnerException != null)
        {
            return dbEx.InnerException.Message;
        }


        return ex.Message;
    }
}