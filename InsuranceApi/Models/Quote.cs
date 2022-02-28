namespace InsuranceApi.Models
{
    public class Quote
    {
        public Quote()
        {
            Employees = new List<Employee>();
        }
        public int Id { get; set; } 
        public ICollection<Employee> Employees { get; set; }
    }
}
