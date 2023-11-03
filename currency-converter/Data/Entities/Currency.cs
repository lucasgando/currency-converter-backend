namespace currency_converter.Data.Entities
{
    public class Currency
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
        public float ConversionIndex {  get; set; }
    }
}
