using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP_Project.Items
{
    class Items_Quantity : Item
    {
        public int quantity { get; set; }
        public Items_Quantity(string ItemName, int quan)
        {
            quantity = quan;
        }
    }
}
