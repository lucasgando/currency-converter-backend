namespace currency_converter.Data.Interfaces
{
    public class ExchangeRatesResponse
    {
        public bool Success { get; set; }
        public long Timestamp { get; set; }
        public string Base { get; set; }
        public string Date { get; set; }
        public Dictionary<string, float> Rates { get; set; }
    }
}
