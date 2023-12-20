namespace currency_converter.Data.Models.Dto.SubscriptionDtos
{
    public class SubscriptionForCreationDto
    {
        public string Name { get; set; }
        public int ConverterLimit { get; set; }
        public float UsdPrice { get; set; }
        public string SubscriptionPicture { get; set; }
    }
}
