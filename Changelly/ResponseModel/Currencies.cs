using System;
using System.Collections.Generic;
using System.Text;

namespace Changelly.ResponseModel
{
    public class Currencies
    {
       public List<Currency> currencies { get; set; }
    }
    public class Currency
    {
        public string Name { get; set; }
    }
}
