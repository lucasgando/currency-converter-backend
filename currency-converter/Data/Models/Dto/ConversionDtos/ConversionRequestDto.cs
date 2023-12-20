namespace currency_converter.Data.Models.Dto.ConversionDtos
{
    public class ConversionRequestDto
    {
        public float Amount { get; set; }
        public int FromCurrencyId { get; set; }
        public int ToCurrencyId { get; set; }
    }
}
