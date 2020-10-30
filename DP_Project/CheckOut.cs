using DP_Project.Items;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DP_Project
{
    public partial class CheckOut : Form
    {
        public int OrderNumber { get; }

        List<Item> items;
        int total = 0;
        private string ordeType;
        public Func<string,int> v;
        Order o;

        public CheckOut(Order order)
        {
            
        }

        public CheckOut(Order order, Func<string, int> v) : this(order)
        {
            this.o = order;
            this.OrderNumber = order.OrderID;
            this.items = order.OrderItems;
            this.total = order.OrderPrice;
            this.ordeType = order.ShowOrderType();
            this.v = v;
            InitializeComponent();
        }

        private void CheckOut_Load(object sender, EventArgs e)
        {
            label1.Text = "Order no : " + OrderNumber.ToString();
            var bindingList = new BindingList<Item>(items);
            var source = new BindingSource(bindingList, null);
            dataGridView1.DataSource = source;
            dataGridView1.Columns.Remove("Id") ;
            label3.Text = ordeType;
            if (ordeType == "Take Away")
            {
                radioButton1.Checked = true;
            }
            else 
            {
                radioButton2.Checked = true;
            }


            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (ordeType == "Take Away")
            {
                MessageBox.Show($"Your Order is ready Please proceed to checkout \n Your bill is {total}");
            }
            else
            {
                MessageBox.Show($"Your Table is Ready \n Your bill is {total}");
            }
            o.ServeOrder(); //saves data to sql
            int i = v("");
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Order Cancled");
            this.Close();
        }



        #region this part is using strategy pattern to call Order context class to change OrderBehaviour
        private void button3_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked) //radiobutton1 is for take away
            {
                o.ChangeOrderType(new TakeAway());  
            }
            else
                o.ChangeOrderType(new DineIn());   //radiobutto2 is for Dine in
            
            label3.Text = o.ShowOrderType();
            
        }

        #endregion 
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton button = (RadioButton)sender;
            if (radioButton1.Checked)
            {
                radioButton2.Checked = false;
                ordeType = "Take Away";
            }
            else
            {
                radioButton1.Checked = false;
                ordeType = "Dine In";
            }
        }
    }
}
