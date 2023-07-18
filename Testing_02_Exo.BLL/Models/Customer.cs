namespace Testing_02_Exo.BLL.Models
{
    public class Customer
    {
        public Customer(int id, string firstName, string lastName, string address)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Address = address;
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        // TODO : Create to Address class
        public string Address { get; set; }
    }
}
