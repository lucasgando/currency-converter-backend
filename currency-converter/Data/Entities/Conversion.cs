using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace currency_converter.Data.Entities
{
    public class Conversion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public User User { get; set; }
        public DateTime Date { get; set; }
        public int FromCurrencyId { get; set; }
        [ForeignKey(nameof(FromCurrencyId))]
        public Currency FromCurrency { get; set; }
        public float FromCurrencyIndex { get; set; }
        public float Amount { get; set; }
        public int ToCurrencyId { get; set; }
        [ForeignKey(nameof(ToCurrencyId))]
        public Currency ToCurrency { get; set; }
        public float ToCurrencyIndex { get; set; }
        public float Result { get; set; }
    }
}
