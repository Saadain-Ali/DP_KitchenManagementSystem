using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP_Project.Items
{
    public abstract class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string price { get; set; }
        public string Category { get; set; }

        public int quantity { get; set; }

        public string getPrice(String name) 
        {
            return price;
        }

        public string getPrice(int Id)
        {
            return price;
        }

    }
}
