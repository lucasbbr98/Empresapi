using System;

namespace Models
{
    public class CVMSource: Model
    {
        public int CompanyId { get; set; }
        public DateTime UpdatedOn { get; set; }
        public string Url { get; set; }
        public int SequenceNumber { get; set; }
        public string Document { get; set; }
        public DateTime ReferenceDate { get; set; }
        public string Situation { get; set; }

        public CVMSource() { }
    }
}
