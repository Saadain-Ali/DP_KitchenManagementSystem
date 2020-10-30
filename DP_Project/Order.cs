using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DP_Project.Items;

namespace DP_Project
{

    #region strategy pattern

    //strategy interface
    public interface orderBehaviour
    {
        void Serve();
    }

    #region strategy implement classes
    class DineIn : orderBehaviour
    {
        public void Serve()
        {
            MessageBox.Show("Your Table is ready to be served");
        }
    }
    class TakeAway : orderBehaviour
    {
        public void Serve()
        {
            MessageBox.Show("Your meal is ready for take away");
        }
    }
    #endregion


    public class Order   //Context class for Strategy pattern  
    {
        public int OrderID { get; set; }
        public List<Item> OrderItems { get; set; }
        public int OrderPrice { get; set; }
        
        orderBehaviour orderBehaviour;

        public Order(int orderid, List<Item> l,int price)
        {
            OrderID = 1;
            OrderPrice = price;
            OrderItems = l;
        }

        //strategy pattern
        public void SetOrderType(orderBehaviour orderBehaviour)
        {
            this.orderBehaviour = orderBehaviour;
        }
        public string ShowOrderType()
        {
            if (orderBehaviour is TakeAway)
            {
                return "Take Away";
            } else
                return "Dine In";
        }

        public void ServeOrder()
        {
            if ( OrderID > 0)
            {
                SqlConnection con = connection.Get();
                SqlCommand cmd = new SqlCommand("insert into Order_Table(id,Items,Price,Type,Quantity,DateTime) values(@id,@items,@price,@type,@quantity,@date)", con);
                con.Open();
                cmd.Parameters.AddWithValue("@id", OrderID);
                string itemsList = "";
                int quan = 0 ;
                foreach (Item item in OrderItems)
                {
                    itemsList +="["  + item.Id +" "+ item.Name + "]";
                    quan += item.quantity;
                }
                
                cmd.Parameters.AddWithValue("@items", itemsList);
                cmd.Parameters.AddWithValue("@price", OrderPrice);
                cmd.Parameters.AddWithValue("@type", ShowOrderType());
                cmd.Parameters.AddWithValue("@quantity", quan);
                cmd.Parameters.AddWithValue("@date", DateTime.Now.ToString());
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Record Inserted Successfully");
            }
            else
            {
                MessageBox.Show("Please Provide Details!");
            }
            orderBehaviour.Serve();
        }

        public void ChangeOrderType(orderBehaviour orderBehaviour)
        {
           this.orderBehaviour =  orderBehaviour;
        }


        #endregion


        #region Factory pattern
        // item Store class for factory pattern
        public Item OrderItem(string type)
        {

            Item item;
            ItemsFactory itemsFactory = new ItemsFactory();

            item = itemsFactory.CreateItem(type);
            return item;

        }
        #endregion
    }
}
