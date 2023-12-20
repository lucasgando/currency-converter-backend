using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace currency_converter.Data.Entities
{
    public class Subscription
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public int ConverterLimit { get; set; }
        public float UsdPrice { get; set; }
        public string SubscriptionPicture { get; set; }
        public List<User> Users { get; set; }
    }
}
