using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniBank.AccountsSrv.Application.Dtos.Responses;

public record CreateAccountResponse
(
    string FirstName,
    string LastName, 
    DateTime BirthDate
);

