namespace Testing_02_Exo.BLL.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Desc { get; set; }
        public double Price { get; set; }
        public bool IsFrozen { get; set; }
    }
}
