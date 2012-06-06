using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GitHub.ValidationEngine.Tests.DummyEntities
{
    internal class TestEntity
    {
        public SubEntity[] SubClassArray { get; set; }
        public string[] StringArray { get; set; }
        public SubEntity SubClass { get; set; }
    }

    internal class SubEntity
    {
        public string TestProp { get; set; }
        public string[] AnotherSub { get; set; }
    }
}
