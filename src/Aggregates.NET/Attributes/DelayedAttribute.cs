﻿using System;
using System.Reflection;

namespace Aggregates.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    public class DelayedAttribute : Attribute
    {
        public DelayedAttribute(Type type, string KeyProperty = "", int Count = -1, int DelayMs = -1)
        {
            this.Type = type;
            if(Count != -1)
                this.Count = Count;
            if(DelayMs != -1)
                this.Delay = DelayMs;

            if (Count > 200000)
                throw new ArgumentException($"{nameof(Count)} too large - maximum is 200000");

            if (!this.Count.HasValue && !this.Delay.HasValue)
                throw new ArgumentException($"{nameof(Count)} or {nameof(DelayMs)} is required to use Delayed attribute");

            if (!string.IsNullOrEmpty(KeyProperty))
            {
                var prop = type.GetProperty(KeyProperty, BindingFlags.Instance | BindingFlags.Public);
                if (prop == null) return;

                this.KeyProperty = KeyProperty;
                this.KeyPropertyFunc = (o) => prop.GetValue(o).ToString();
            }

        }

        public Type Type { get; private set; }
        public int? Count { get; private set; }
        public int? Delay { get; private set; }
        public string KeyProperty { get; private set; }
        public Func<object, string> KeyPropertyFunc { get; private set; }
    }
    
}
