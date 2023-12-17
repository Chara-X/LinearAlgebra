using System;

namespace LinearAlgebra.Attributes
{
    public class DisplayNameAttribute : Attribute
    {
        public string Name { get; set; }

        public DisplayNameAttribute(string name)
        {
            Name = name;
        }
    }
}
