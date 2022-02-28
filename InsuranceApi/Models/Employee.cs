namespace InsuranceApi.Models
{
    public class Employee : Person
    {
        public Employee()
        {
            Dependents = new List<Dependent>();
        }
        public ICollection<Dependent> Dependents { get; set; }
    }
}
