using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DP_Project.Items;

namespace DP_Project
{

    #region Factory Pattern || Factory class  it creates diff Item implementations ie Burger etc
    class ItemsFactory
    {
        public Item CreateItem(string type)
        {
            Item item;

            if (type.Equals("Burger"))
                item = new Burger();
            else if (type.Equals("IceCream"))
                item = new IceCream();

            else if (type.Equals("Drinks"))
                item = new Drinks();
            else
            {
                MessageBox.Show("Invalid Item");
                item = new Burger();
            }
            return item;
        }
        #endregion
    }
}
