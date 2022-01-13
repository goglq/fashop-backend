using FashopBackend.Core.Error;
using HotChocolate;

namespace FashopBackend.Graphql.Errors;

public class GraphQLErrorFilter : IErrorFilter
{
    public IError OnError(IError error)
    {
        if (error.Exception is null)
            return error;

        if (error.Exception is NotRegisteredEmail)
        {
            error
                .WithMessage(error.Exception.Message)
                .WithCode("NOT_AUTH");
        }
        
        //error.WithMessage(error.Exception.Message);

        return error;
    }
}