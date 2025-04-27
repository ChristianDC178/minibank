using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniBank.Exceptions;

public class MinibankRepositoryException : Exception
{

    public MinibankRepositoryException(string message)
        : base(message)
    {

    }

    public MinibankRepositoryException(string message, Exception innerException)
        : base(message, innerException)
    {

    }
}
