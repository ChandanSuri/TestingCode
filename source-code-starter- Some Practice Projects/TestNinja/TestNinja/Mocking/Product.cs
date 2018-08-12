namespace TestNinja.Mocking
{
    public class Product
    {
        public float ListPrice { get; set; }

        public float GetPrice(ICustomer customer) // Changed Customer to ICustomer to show how MOCK ABUSE!!!
        {
            if (customer.IsGold)
                return ListPrice * 0.7f;

            return ListPrice;
        }
    }

    public class Customer : ICustomer
    {
        public bool IsGold { get; set; }
    }
}