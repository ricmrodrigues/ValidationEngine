using System;
using System.Collections.Generic;
using System.Reflection;

namespace GitHub.ValidationEngine.DataStructures
{
    public class ComparableProperty
    {
        public object Entity { get; set; }
        public PropertyInfo PropertyInfo { get; set; }

        public string PropertyValue
        {
            get
            {
                if (this.PropertyInfo.PropertyType.IsArray && this._arrayIndex.HasValue)
                {
                    Array arr = (Array)this.PropertyInfo.GetValue(this.Entity, null);
                    return (string)arr.GetValue(this._arrayIndex.Value);
                }
                else
                {
                    return (string)this.PropertyInfo.GetValue(this.Entity, null);
                }
            }
        }
        private int? _arrayIndex;

        public ComparableProperty(object entity, PropertyInfo propertyInfo, int? arrayIndex)
        {
            this._arrayIndex = arrayIndex;
            this.PropertyInfo = propertyInfo;
            this.Entity = entity;
        }
    }
}
