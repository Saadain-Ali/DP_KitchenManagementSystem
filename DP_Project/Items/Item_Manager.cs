using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DP_Project.Items
{
    class Item_Manager
    {
       
        public void AddItem(Item item)
        {
            //insert query

            if (item != null)
            {
                SqlConnection con = connection.Get();
                SqlCommand cmd = new SqlCommand("insert into Item_Table(id,Name,Price,Category) values(@id,@name,@price,@category)", con);
                con.Open();
                cmd.Parameters.AddWithValue("@id", item.Id);
                cmd.Parameters.AddWithValue("@name", item.Name);
                cmd.Parameters.AddWithValue("@price", item.price);
                cmd.Parameters.AddWithValue("@category", item.Category);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Record Inserted Successfully");
            }
            else
            {
                MessageBox.Show("Please Provide Details!");
            }
        }
        public void DeleteItem(Item item)
        {
            //delete query

            if (item != null)
            {
                SqlConnection con = connection.Get();
                SqlCommand cmd = new SqlCommand("delete Item_Table where Name=@name", con);
                con.Open();
                cmd.Parameters.AddWithValue("@name", item.Name);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Record Deleted Successfully!");
               
            }
            else
            {
                MessageBox.Show("Please Select Record to Delete");
            }
        }

        public void updateItem(Item item)
        {
            
            if (item != null)
            {
                
                SqlConnection con = connection.Get();
                SqlCommand cmd = new SqlCommand("update [Item_Table] set id=@id,Name=@name,Price=@price,Category=@category where id=@id", con);
                con.Open();
                cmd.Parameters.AddWithValue("@id", item.Id);
                cmd.Parameters.AddWithValue("@name", item.Name);
                cmd.Parameters.AddWithValue("@price", item.price);
                cmd.Parameters.AddWithValue("@category", item.Category);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Record Inserted Successfully");
            }
            else
            {
                MessageBox.Show("Please Provide Details!");
            }
        }

        public Item Retrieve(int id) 
        {
            SqlConnection sql = connection.Get();
            DataTable dt = new DataTable();
            SqlDataAdapter adapt = new SqlDataAdapter("select * from [Item_Table]", sql);
            adapt.Fill(dt);
            ItemsFactory itemsFactory = new ItemsFactory();
            Item item =itemsFactory.CreateItem("Burger");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                item.Id = Convert.ToInt32(dt.Rows[i][0]);//.Cells[0].Value.ToString()) ;
                item.Name = dt.Rows[i][1].ToString() ;
                item.price = dt.Rows[i][2].ToString();
                item.Category = dt.Rows[i][3].ToString();
                if (id == item.Id)
                {
                    sql.Close();
                    return item;
                }

            }
            return null;
            ////comboBox1.DataSource = dt;
            ////comboBox1.DisplayMember = "Name";
            //ItemData = GetDict(dt);
            //comboBox1.Items.AddRange(ItemData.Keys.ToArray());
            
        }
    }
}
