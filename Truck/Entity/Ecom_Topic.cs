using System;
using System.Collections.Generic;

#nullable disable

namespace Truck.Entity
{
    public partial class Ecom_Topic
    {
        public Ecom_Topic()
        {
            Ecom_TopicDetails_Categories = new HashSet<Ecom_TopicDetails_Category>();
            Ecom_TopicDetails_Products = new HashSet<Ecom_TopicDetails_Product>();
        }

        public int Topic_ID { get; set; }
        public string Topic_Name { get; set; }
        public string Topic_Description { get; set; }
        public bool Topic_CategoryYN { get; set; }
        public string Brand_Image { get; set; }
        public bool? ADDYN { get; set; }
        public bool? Slider { get; set; }
        public bool isActive { get; set; }

        public virtual ICollection<Ecom_TopicDetails_Category> Ecom_TopicDetails_Categories { get; set; }
        public virtual ICollection<Ecom_TopicDetails_Product> Ecom_TopicDetails_Products { get; set; }
    }
}
