using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniBank.CustomersSrv.Console;

public class Filters
{
    public uint[]? Years { get; set; }
    public string[]? Makes { get; set; }
    public string[]? Models { get; set; }
}
