using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Collections;

namespace Project_YouthVictory
{
    public partial class AddBook : Form
    {
        public AddBook()
        {
            InitializeComponent();
            ShowBook();
            DeleteText();
        }

        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Documents\YouthVictory.mdf;Integrated Security=True;Connect Timeout=30");

        private void ShowBook()
        {
            try
            {
                Con.Open();
                string Query = "select * from BookTbl1";
                SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
                SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
                var ds = new DataSet();
                sda.Fill(ds);
                BookDGV.DataSource = ds.Tables[0];
                Con.Close();
            }
            catch
            {
                MessageBox.Show(@"Something's wrong!! Close the window and enter again!");
            }
        }

        private void DeleteText()
        {
            booktb.Text = "";
        }

        private void savebtn_Click(object sender, EventArgs e)
        {
            if (booktb.Text == "")
            {
                MessageBox.Show("Missing Data", "Info");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into BookTbl1 values(@CatN)", Con);
                    cmd.Parameters.AddWithValue("@CatN", booktb.Text);
                    cmd.ExecuteNonQuery();
                    //MessageBox.Show(@"Book saved!");
                    Con.Close();
                    ShowBook();
                    DeleteText();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }
        int key;
        private void editbtn_Click(object sender, EventArgs e)
        {
            if (booktb.Text == "")
            {
                MessageBox.Show("Missing Data", "Info");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Update BookTbl1 set Book=@CN where Id=@CKey", Con);
                    cmd.Parameters.AddWithValue("@CN", booktb.Text);
                    cmd.Parameters.AddWithValue("@CKey", key);
                    cmd.ExecuteNonQuery();
                    //MessageBox.Show(@"Book Edited!");
                    Con.Close();
                    ShowBook();
                    DeleteText();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void deletebtn_Click(object sender, EventArgs e)
        {
            if (key == 0)
            {
                MessageBox.Show("Missing Data", "Info");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("delete from BookTbl1 where Id=@CKey", Con);
                    cmd.Parameters.AddWithValue("@CKey", key);
                    cmd.ExecuteNonQuery();
                    //MessageBox.Show(@"Book Delited!");
                    Con.Close();
                    ShowBook();
                    DeleteText();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void BookDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                booktb.Text = BookDGV.Rows[e.RowIndex].Cells[1].Value.ToString();
                if (booktb.Text == "")
                {
                    key = 0;
                }
                else
                {
                    key = Convert.ToInt32(BookDGV.Rows[e.RowIndex].Cells[0].Value.ToString());
                }
            }
            catch { }
        }
    }
}
