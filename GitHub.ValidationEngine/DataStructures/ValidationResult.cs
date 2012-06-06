using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GitHub.ValidationEngine.Engine;

namespace GitHub.ValidationEngine.DataStructures
{
    public class ValidationResult
    {
        public bool Succeeded { get; set; }
        public List<Rule> SucceededRules { get; set; }
        public List<Rule> FailedRules { get; set; }
        public List<Rule> NotRanRules { get; set; }
    }
}
