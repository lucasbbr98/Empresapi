namespace Models
{
    public class Company : Model
    {
        public string Cnpj { get; set; }
        public string Name { get; set;}
        public int CvmCode { get; set; } 
        public string Sector { get; set; }
        public string Cep { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Area { get; set; }
    }
}
