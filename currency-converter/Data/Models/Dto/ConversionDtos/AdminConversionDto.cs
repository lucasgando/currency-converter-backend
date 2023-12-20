using currency_converter.Data.Models.Dto.CurrencyDtos;

namespace currency_converter.Data.Models.Dto.ConversionDtos
{
    public class AdminConversionDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public float Amount { get; set; }
        public float Result { get; set; }
        public CurrencyDto FromCurrency { get; set; }
        public float FromCurrencyIndex { get; set; }
        public CurrencyDto ToCurrency { get; set; }
        public float ToCurrencyIndex { get; set; }
    }
}
