using System.Collections.Generic;
using GitHub.ValidationEngine.DataStructures;
using GitHub.ValidationEngine.Engine;
using GitHub.ValidationEngine.Tests.DummyEntities;
using NUnit.Framework;


namespace GitHub.ValidationEngine.Tests
{
    [TestFixture]
    public class RuleTests
    {
        [Test]
        public void Validate_a_rule_with_valid_data_and_subclass_array()
        {
            Rule r = new Rule()
                {
                    Name = "Test rule",
                    ValidationExpression = "^.*TEST.*$",
                    FieldName = "TestProp",
                    FieldPath = "TestEntity.SubClassArray",
                    ArrayIndex = 0
                };

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

            bool validationResult = r.Validate(t);

            Assert.IsTrue(validationResult);
        }

        [Test]
        public void Validate_a_rule_with_valid_data_and_basetype_array()
        {
            Rule r = new Rule()
            {
                Name = "Test rule",
                ValidationExpression = "^.*TEST.*$",
                FieldName = "StringArray",
                FieldPath = "TestEntity",
                ArrayIndex = 1
            };

            TestEntity t = new TestEntity()
            {
                StringArray = new string[] { "some value", "this is a TEST" }
            };

            bool validationResult = r.Validate(t);

            Assert.IsTrue(validationResult);
        }

        [Test]
        public void Validate_a_rule_with_invalid_data_and_subclass_array_allpositions()
        {
            Rule r = new Rule()
            {
                Name = "Test rule",
                ValidationExpression = "^.*TEST.*$",
                FieldName = "StringArray",
                FieldPath = "TestEntity",
            };

            TestEntity t = new TestEntity()
            {
                StringArray = new string[] { "some value", "this is a TEST" }
            };

            bool validationResult = r.Validate(t);

            Assert.IsFalse(validationResult);
        }


        [Test]
        public void Validate_a_rule_with_invalid_data_and_subclass_array_with_string_array_prop_allpositions()
        {
            Rule r = new Rule()
            {
                Name = "Test rule",
                ValidationExpression = "^.*TEST.*$",
                FieldName = "AnotherSub",
                FieldPath = "TestEntity.SubClassArray",
            };

            TestEntity t = new TestEntity()
            {
                SubClassArray = new SubEntity[]
                        {
                            new SubEntity()
                                {
                                    AnotherSub = new string[]
                                        {
                                            "array pos 1", "array pos 2"
                                        }
                                }
                        }
            };

            bool validationResult = r.Validate(t);

            Assert.IsFalse(validationResult);
        }

        [Test]
        public void Validate_a_rule_with_valid_data_and_subclass_not_array()
        {
            Rule r = new Rule()
            {
                Name = "Test rule",
                ValidationExpression = "^.*TEST.*$",
                FieldName = "TestProp",
                FieldPath = "TestEntity.SubClass",
            };

            TestEntity t = new TestEntity()
            {
                SubClass = new SubEntity
                        {
                            TestProp = "some value TEST"
                        }
            };

            bool validationResult = r.Validate(t);

            Assert.IsTrue(validationResult);
        }

        [Test]
        public void Validate_a_rule_with_non_existing_field()
        {
            Rule r = new Rule()
            {
                Name = "Test rule",
                ValidationExpression = "^.*TEST.*$",
                FieldName = "Unexistent prop",
                FieldPath = "TestEntity.SubClass",
            };

            TestEntity t = new TestEntity()
            {
                SubClass = new SubEntity
                {
                    TestProp = "some value TEST"
                }
            };

            bool validationResult = r.Validate(t);

            Assert.IsFalse(validationResult);
        }


        [Test]
        public void Validate_a_rule_with_valid_data_and_fieldcomparison()
        {
            Rule r = new Rule()
            {
                Name = "Test rule",
                FieldName = "StringArray",
                FieldPath = "TestEntity",
                FieldComparisonList = new List<FieldComparison>()
                    {
                        new FieldComparison()
                            {
                                FieldName = "AnotherSub",
                                FieldPath = "SubEntity"
                            }
                    }
            };

            TestEntity t = new TestEntity()
                {
                    StringArray = new string[] {"valor1", "valor2"}
                };

            SubEntity t1 = new SubEntity()
                {
                    AnotherSub = new string[] {"valor1", "valor2"}
                };

            bool validationResult = r.Validate(t, t1);

            Assert.IsTrue(validationResult);
        }

        [Test]
        public void Validate_a_rule_with_invalid_data_and_fieldcomparison()
        {
            Rule r = new Rule()
            {
                Name = "Test rule",
                FieldName = "StringArray",
                FieldPath = "TestEntity",
                FieldComparisonList = new List<FieldComparison>()
                    {
                        new FieldComparison()
                            {
                                FieldName = "AnotherSub",
                                FieldPath = "SubEntity"
                            }
                    }
            };

            TestEntity t = new TestEntity()
            {
                StringArray = new string[] { "valor1", "valor0" }
            };

            SubEntity t1 = new SubEntity()
            {
                AnotherSub = new string[] { "valor1", "valor2" }
            };

            bool validationResult = r.Validate(t, t1);

            Assert.IsFalse(validationResult);
        }

        [Test]
        public void Validate_a_rule_with_valid_data_and_customvalidator()
        {
            Rule r = new Rule()
            {
                Name = "Test rule",
                FieldName = "TestProp",
                FieldPath = "TestEntity.SubClass",
                CustomValidator = (prop) =>
                    {
                        return true;
                    }
            };

            TestEntity t = new TestEntity()
            {
                SubClass = new SubEntity
                {
                    TestProp = "some value TEST"
                }
            };

            bool validationResult = r.Validate(t);

            Assert.IsTrue(validationResult);
        }

        [Test]
        public void Validate_a_rule_with_invalid_data_and_fieldcomparison_compared_arrays_different_length()
        {
            Rule r = new Rule()
            {
                Name = "Test rule",
                FieldName = "StringArray",
                FieldPath = "TestEntity",
                FieldComparisonList = new List<FieldComparison>()
                    {
                        new FieldComparison()
                            {
                                FieldName = "AnotherSub",
                                FieldPath = "SubEntity"
                            }
                    }
            };

            TestEntity t = new TestEntity()
            {
                StringArray = new string[] { "valor1" }
            };

            SubEntity t1 = new SubEntity()
            {
                AnotherSub = new string[] { "valor1", "valor2" }
            };

            bool validationResult = r.Validate(t, t1);

            Assert.IsFalse(validationResult);
        }

        //TODO
        //ruleset oninvalid
    }
}
