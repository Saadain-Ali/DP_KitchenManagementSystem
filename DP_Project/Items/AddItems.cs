using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DP_Project.Items
{
    public partial class AddItems : Form
    {
        Item item;
        readonly Func<int> v;

        public AddItems()
        {
            
        }

        public AddItems(Func<int> v)
        {
            this.v = v;
            InitializeComponent();
        }

        private void AddItems_Load(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            ItemsFactory itemsFactory = new ItemsFactory();
           
            item = itemsFactory.CreateItem(comboBox2.Text);
            item.Id = Convert.ToInt32(textBox5.Text.Trim()); ;
            item.Name = textBox3.Text;
            item.price = textBox4.Text;
            item.Category = comboBox2.Text;
            Item_Manager _Manager = new Item_Manager();
            _Manager.AddItem(item);
            v();

        }

        private void button5_Click(object sender, EventArgs e)
        {
            ItemsFactory itemsFactory = new ItemsFactory();

            item = itemsFactory.CreateItem(comboBox2.Text);
            item.Id = Convert.ToInt32(textBox5.Text.Trim()); ;
            item.Name = textBox3.Text;
            item.price = textBox4.Text;
            item.Category = comboBox2.Text;
            Item_Manager _Manager = new Item_Manager();
            _Manager.updateItem(item);
            v();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ItemsFactory itemsFactory = new ItemsFactory();

            item = itemsFactory.CreateItem(comboBox2.Text);
            item.Id = Convert.ToInt32(textBox5.Text.Trim());
            item.Name = textBox3.Text;
            item.price = textBox4.Text;
            item.Category = comboBox2.Text;
            Item_Manager _Manager = new Item_Manager();
            _Manager.DeleteItem(item);
            v();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Item_Manager _Manager = new Item_Manager();
            Item item = _Manager.Retrieve(Convert.ToInt32(textBox5.Text.Trim()) );
            if (item == null)
            {
                MessageBox.Show("Item not found");
            }
            else
            {
                textBox5.Text = item.Id.ToString();
                textBox3.Text = item.Name;
                textBox4.Text = item.price;
                comboBox2.Text = item.Category;
                v();
            }
            
        }
    }
}
