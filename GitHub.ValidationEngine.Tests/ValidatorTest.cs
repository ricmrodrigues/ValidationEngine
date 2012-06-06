using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GitHub.ValidationEngine.Engine;
using GitHub.ValidationEngine.Tests.DummyEntities;

using GitHub.ValidationEngine.DataStructures;
using NUnit.Framework;

namespace GitHub.ValidationEngine.Tests
{
    [TestFixture]
    public class ValidatorTest
    {
        private Validator GetValidator()
        {
            return new Validator()
                {
                    Name = "test validation",
                    RuleSets = new List<RuleSet>()
                        {
                            new RuleSet()
                                {
                                    OnInvalid = (prop) =>
                                        {
                                            //do nothing
                                        },
                                    Rules = new List<Rule>()
                                        {
                                            new Rule()
                                                {
                                                    Name = "Test rule",
                                                    ValidationExpression = "^.*TEST.*$",
                                                    FieldName = "TestProp",
                                                    FieldPath = "TestEntity.SubClassArray",
                                                    ArrayIndex = 0
                                                }
                                        }
                                }
                        }
                };
        }

        [Test]
        public void Test_validate_succeed()
        {

            Validator v = GetValidator();

            TestEntity t = new TestEntity()
                {
                    SubClassArray = new SubEntity[]
                        {
                            new SubEntity()
                                {
                                    TestProp = "this is a TEST"
                                }
                        }
                };

            ValidationResult validationResult = v.Execute(t);

            Assert.IsTrue(validationResult.SucceededRules.Count == 1);
        }

        [Test]
        public void Test_validate_fail()
        {

            Validator v = GetValidator();

            TestEntity t = new TestEntity()
            {
                SubClassArray = new SubEntity[]
                        {
                            new SubEntity()
                                {
                                    TestProp = "this is a not..."
                                }
                        }
            };

            ValidationResult validationResult = v.Execute(t);

            Assert.IsTrue(validationResult.FailedRules.Count == 1);
        }
    }
}
