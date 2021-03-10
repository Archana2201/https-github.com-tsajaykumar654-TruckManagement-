using System;
using System.Collections.Generic;

#nullable disable

namespace Truck.Entity
{
    public partial class Ecom_TopicDetails_Category
    {
        public int TopicDetails_Category_ID { get; set; }
        public int? FK_SubCategory { get; set; }
        public bool TopicDetails_FrontYN { get; set; }
        public int? FK_Topic_ID { get; set; }
        public bool isActive { get; set; }

        public virtual Ecom_Topic FK_Topic { get; set; }
    }
}
