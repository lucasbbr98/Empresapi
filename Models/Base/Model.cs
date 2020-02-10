using System;

namespace Models
{
    using Attributes;

    // Properties and rules for database services 

    public abstract class Model
    {
        [QueryIgnore(Ignore = IgnoreType.InsertAndUpdate)]
        public int Id { get; set; }

        [QueryIgnore(Ignore = IgnoreType.InsertAndUpdate)]
        public DateTime CreatedOn { get; set; }

        [QueryIgnore(Ignore = IgnoreType.InsertAndUpdate)]
        public DateTime ModifiedOn { get; set; }

        [QueryIgnore(Ignore = IgnoreType.Insert)]
        public DateTime? DeactivatedOn { get; set; }

    }
}
