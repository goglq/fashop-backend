using HotChocolate;

namespace FashopBackend.Graphql.Errors;

public class GraphQLErrorFilter : IErrorFilter
{
    public IError OnError(IError error)
    {
        if (error.Exception is not null)
        {
            error.WithMessage(error.Exception.Message);
        }

        return error;
    }
}