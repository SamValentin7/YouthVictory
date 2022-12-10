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
    public partial class AddQuestions : Form
    {
        public AddQuestions()
        {
            InitializeComponent();
            GetBook();
            ShowQuestion();
            DeleteText();
        }

        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Documents\YouthVictory.mdf;Integrated Security=True;Connect Timeout=30");
        private void GetBook()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("select * from BookTbl1", Con);
            SqlDataReader Rdr;
            Rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("Id", typeof(int));
            dt.Load(Rdr);
            bookcb.ValueMember = "Book";
            bookcb.DataSource = dt;
            Con.Close();
        }

        private void ShowQuestion()
        {
            try
            {
                Con.Open();
                string Query = "select * from " + bookcb.Text + "";
                SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
                SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
                var ds = new DataSet();
                sda.Fill(ds);
                QuestionDGV.DataSource = ds.Tables[0];
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
            richTextBox1.Text = "";
        }
        private void savebtn_Click(object sender, EventArgs e)
        {
            if (richTextBox1.Text == "" || booktb.Text == "" || bookcb.SelectedIndex == -1)
            {
                MessageBox.Show("Mising Data", "Info");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into " + bookcb.Text + " values(@PN,@PCat)", Con);
                    cmd.Parameters.AddWithValue("@PN", booktb.Text);
                    cmd.Parameters.AddWithValue("@PCat", richTextBox1.Text);
                    cmd.ExecuteNonQuery();
                    //MessageBox.Show(@"Question Saved!");
                    Con.Close();
                    ShowQuestion();
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
            if (richTextBox1.Text == "" || booktb.Text == "" || bookcb.SelectedIndex == -1)
            {
                MessageBox.Show("Missing Data", "Info");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("update " + bookcb.Text + " set Nr=@PN, Question=@PCat where Id=@PrKey", Con);
                    cmd.Parameters.AddWithValue("@PN", booktb.Text);
                    cmd.Parameters.AddWithValue("@PCat", richTextBox1.Text);
                    cmd.Parameters.AddWithValue("@PrKey", key);
                    cmd.ExecuteNonQuery();
                    //MessageBox.Show(@"Question Edited!");
                    Con.Close();
                    ShowQuestion();
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
                    SqlCommand cmd = new SqlCommand("delete from " + bookcb.Text + " where Id=@PrKey", Con);
                    cmd.Parameters.AddWithValue("@PrKey", key);
                    cmd.ExecuteNonQuery();
                    //MessageBox.Show(@"Question Deleted!");
                    Con.Close();
                    ShowQuestion();
                    DeleteText();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void QuestionDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                QuestionDGV.ReadOnly = true;
                booktb.Text = QuestionDGV.Rows[e.RowIndex].Cells[1].Value.ToString();
                richTextBox1.Text = QuestionDGV.Rows[e.RowIndex].Cells[2].Value.ToString();
                if (booktb.Text == "")
                {
                    key = 0;
                }
                else
                {
                    key = Convert.ToInt32(QuestionDGV.Rows[e.RowIndex].Cells[0].Value.ToString());
                }
            }
            catch
            {

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ShowQuestion();
        }

        private void AddQuestions_Load(object sender, EventArgs e)
        {

        }
    }
}
