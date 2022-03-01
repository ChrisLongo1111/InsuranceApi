namespace Repository.Models
{
    public class Quote
    {
        public Quote()
        {
            Employees = new List<Employee>();
            Name = "";
        }
        public long Id { get; set; } 
        public string Name { get; set; }    
        public ICollection<Employee> Employees { get; set; }
    }
}
