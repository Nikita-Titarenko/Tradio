using Microsoft.EntityFrameworkCore;

namespace Tradio.Infrastructure;

public static class DbContextExtensions
{
    public static IQueryable<object> AsGenericQueryable(this DbContext context, Type type)
    {
        return (IQueryable<object>)context.GetType()
            .GetMethod("Set", Type.EmptyTypes)!
            .MakeGenericMethod(type)
            .Invoke(context, null)!;
    }
}           