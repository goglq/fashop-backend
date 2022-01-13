using System;

namespace FashopBackend.Core.Error;

public class NotMatchingPasswordException : ApplicationException
{
    public NotMatchingPasswordException() : base("неверный пароль")
    {
        
    }
}