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
            return ErrorBuilder.FromError(error).SetMessage(error.Exception.Message).SetCode("NOT_AUTH").Build();

        return ErrorBuilder.FromError(error).SetMessage(error.Exception.Message).Build();
    }
}