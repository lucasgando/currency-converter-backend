using currency_converter.Data.Entities;

namespace currency_converter.Helpers
{
    public static class Converter
    {
        public static float Convert(float amount, Currency from, Currency to)
        {
            return (amount * from.ConversionIndex) / to.ConversionIndex;
        }
    }
}
