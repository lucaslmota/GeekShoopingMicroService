﻿namespace GeekShooping.ProductAPI.Data.ValueObjects
{
    public class ProductVO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }

        public string? Description { get; set; }

        public string? CategoryName { get; set; }

        public string? ImageURL { get; set; }
    }
}
