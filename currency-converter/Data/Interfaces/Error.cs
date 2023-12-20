using currency_converter.Data.Models.Enums;

namespace currency_converter.Data.Interfaces
{
    public class Error
    {
        public ErrorEnum Code {  get; set; }
        public string Message { get; set; }
    }
}
