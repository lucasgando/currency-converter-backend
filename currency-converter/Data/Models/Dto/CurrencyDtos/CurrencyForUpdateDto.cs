﻿using System.ComponentModel.DataAnnotations;

namespace currency_converter.Data.Models.Dto.CurrencyDtos
{
    public class CurrencyForUpdateDto
    {
        [Required]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
        public float ConversionIndex { get; set; }
        public string Flag { get; set; }
    }
}
