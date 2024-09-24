using Amazon.Auth.AccessControlPolicy.ActionIdentifiers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniBank.MongoDB;

public class BsonClassMapRegistrationException : Exception
{

    public BsonClassMapRegistrationException(string message) : base(message)
    { 
    
    }


}
