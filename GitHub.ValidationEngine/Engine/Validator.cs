using System.Collections.Generic;
using GitHub.ValidationEngine.DataStructures;

namespace GitHub.ValidationEngine.Engine
{
    public class Validator
    {
        public string Name { get; set; }
        //public bool Standalone { get; set; } //not being used still
        public List<RuleSet> RuleSets { get; set; }
        private List<Rule> SucceededRules { get; set; }
        private List<Rule> FailedRules { get; set; }
        private List<Rule> NotRanRules { get; set; }

        public ValidationResult Execute(params object[] entities)
        {
            SucceededRules = new List<Rule>();
            FailedRules = new List<Rule>();
            NotRanRules = new List<Rule>();

            bool succeed = false;

            foreach (var ruleSet in RuleSets)
            {
                succeed = ruleSet.Run(entities);
                SucceededRules.AddRange(ruleSet.SucceededRules);
                FailedRules.AddRange(ruleSet.FailedRules);
                NotRanRules.AddRange(ruleSet.NotRanRules);
                if (!succeed)
                {
                    break;
                }
            }

            return new ValidationResult()
                {
                    FailedRules = this.FailedRules,
                    SucceededRules = this.SucceededRules,
                    NotRanRules = this.NotRanRules,
                    Succeeded = succeed
                };
        }
    }
}
