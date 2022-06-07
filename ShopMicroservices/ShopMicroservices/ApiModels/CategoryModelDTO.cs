﻿using ShopMicroservices.Models.Base;

namespace ShopMicroservices.Models
{
    public class CategoryModelDTO : IModelDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }

        public CategoryModelDTO()
        {

        }
    }
}
