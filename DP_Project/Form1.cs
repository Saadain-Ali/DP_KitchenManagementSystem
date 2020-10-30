using DP_Project.Items;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DP_Project
{
    public partial class Form1 : Form
    {
        Dictionary<string, string> ItemData;

        List<Item> ord = new List<Item>();
        Order order;
        int orderCount = 0;
        int total = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DisplayData();
            ResetOrder();
        }

        private void ResetOrder()
        {
            SqlConnection sql = connection.Get();
            SqlCommand cmd = new SqlCommand("delete from [Order_Table]",sql);
            sql.Open();
            cmd.ExecuteNonQuery();
            sql.Close();
        }


        // Using Singleton Demo  //Abstract Demo
        private int DisplayData()
        {
            comboBox1.Items.Clear();
            SqlConnection sql = connection.Get();
            DataTable dt = new DataTable();
            SqlDataAdapter adapt = new SqlDataAdapter("select * from [Item_Table]", sql);
            adapt.Fill(dt);
            ItemData = GetDict(dt);
            comboBox1.Items.AddRange(ItemData.Keys.ToArray());
            sql.Close();
            comboBox1.Text = "Burger";
            textBox1.Text = "Order no :" + orderCount+1;
            return 0;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox3.Text = ItemData[(comboBox1.Text)].Split('~')[0];
            textBox2.Text = 1.ToString();
        }

        internal Dictionary<string, string> GetDict(DataTable dt)
        {
            return dt.AsEnumerable()
              .ToDictionary<DataRow, string, string>(row => row.Field<string>(1),
                                        row => row.Field<string>(2) + "~" + row.Field<string>(3));
        }


        private void fill_list()
        {
            listView1.Items.Clear();
            foreach (var item in ord)
            {
                ListViewItem l = new ListViewItem(item.Name);
                l.SubItems.Add(item.quantity.ToString());
                listView1.Items.Add(l);
            }

        }

    
        private void button2_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked || radioButton2.Checked)
            {

                if (listView1.Items.Count <=0 )
                {
                    MessageBox.Show("Please Make your order");
                }
                else
                {
                    orderCount++;
                    int total = 0;
                    foreach (Item i in ord)
                    {
                        total += Convert.ToInt32(i.getPrice(i.Name));
                    }

                    order = new Order(orderCount, ord, total);
                    order.OrderID = orderCount;
                    if (radioButton1.Checked)
                    {
                        order.SetOrderType(new TakeAway());
                    }
                    else
                    {
                        order.SetOrderType(new DineIn());
                    }
                    
                    CheckOut checkOut = new CheckOut(order, this.resseter);
                    checkOut.Show();
                }
                
            }
            else
            {
                MessageBox.Show("Please Select the Order Type");
            }
           
        }


        #region Itemfactory demo 
        private void button1_Click(object sender, EventArgs e)
        {
            ItemsFactory itemsFactory = new ItemsFactory();

            Item i = itemsFactory.CreateItem(ItemData[(comboBox1.Text)].Split('~')[1]);
            i.Name = comboBox1.Text;
            i.price = (Convert.ToInt32(ItemData[(comboBox1.Text)].Split('~')[0]) * Convert.ToInt32(textBox2.Text)).ToString();
            total += Convert.ToInt32(i.price);
            amount_lbl.Text = total.ToString();
            i.quantity = Convert.ToInt32(textBox2.Text);
            i.Category = ItemData[(comboBox1.Text)].Split('~')[1];
            ord.Add(i);
            fill_list();
        }
        #endregion


        public int resseter(string c) //Reset Controls
        {

            DisplayData();
            amount_lbl.Text = "";
            textBox1.ResetText();
            textBox2.ResetText();
            textBox3.ResetText();
            listView1.Items.Clear();
            ord.Clear();
            textBox1.Text = "Order no :" + (orderCount+1).ToString();
            return 1 ;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton button = (RadioButton)sender;
            if (radioButton1.Checked)
            {
                radioButton2.Checked = false;
            }
            else
            {
                radioButton1.Checked = false;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AddItems addItems = new AddItems(this.DisplayData);
            addItems.Show();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            //Show dataview of order history
            OrderHistory orderHistory = new OrderHistory();
            orderHistory.Show();
        }
    }
}
