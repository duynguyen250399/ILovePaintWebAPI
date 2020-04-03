namespace DataLayer.Entities
{
    public class Color
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string ColorCode { get; set; }
        public int ProductID { get; set; }
        public virtual Product Product { get; set; }
    }
}
