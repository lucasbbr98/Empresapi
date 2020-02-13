using System;

namespace Models.Attributes
{
    public enum IgnoreType { Get, Insert, Update, InsertAndUpdate, All, None };

    // Defines in which database operations a property should be ignored
    public class QueryIgnoreAttribute : Attribute
    {
        public IgnoreType Ignore { get; set; }
    }
}
