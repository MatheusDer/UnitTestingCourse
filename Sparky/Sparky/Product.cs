namespace Sparky
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }

        private double price;

        public Product(int price)
        {
            this.price = price;
        }

        public double GetPrice(Customer customer)
        {
            if (customer.IsPlatinum)
                return price *= .8;

            return price;
        }
    }
}
