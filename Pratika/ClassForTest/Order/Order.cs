namespace Pratika.ClassForTest.Order
{
    using Pratika.ClassForTest.Product;
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public List<Product> Products { get; set; }
    }

}
