using System;

namespace FashopBackend.Core.Error;

public class NotRegisteredEmail : ApplicationException
{
    public NotRegisteredEmail() : base("неверная почта")
    {
        
    }
}