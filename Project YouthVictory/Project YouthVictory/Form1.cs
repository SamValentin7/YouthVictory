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
using System.IO;

namespace Project_YouthVictory
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            GetBook();
            ShowQuestion();
            CountRows();
            button1.Enabled = false;
            //passwordtb.Text = "Valentin777";
            //dataGridView1.Visible = false;
        }

        private int CountRows()
        {
            int numRows = dataGridView1.Rows.Count;
            return numRows;
        }
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
                dataGridView1.DataSource = ds.Tables[0];
                Con.Close();
            }
            catch
            {
                MessageBox.Show(@"Something's wrong!! Close the application and enter again!");
            }
        }

        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Documents\YouthVictory.mdf;Integrated Security=True;Connect Timeout=30");

        private void button1_Click(object sender, EventArgs e)
        {
            AddBook form = new AddBook();
            form.Show();
        }

        private void addQ_Click(object sender, EventArgs e)
        {
            AddQuestions form = new AddQuestions();
            form.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            GetBook();
        }

        int i = 0;
        private void showbtn_Click(object sender, EventArgs e)
        {
            try
            {
                ShowQuestion();
                if (i < CountRows()-1)
                {
                    string v = dataGridView1.Rows[i].Cells[2].Value.ToString();
                    richTextBox1.Text = v;
                    i += 1;
                }
                else
                {
                    i = 0;
                }   
            }
            catch { }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            dataGridView1.Visible = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            dataGridView1.Visible = true;
            ShowQuestion();
        }

        private void restartbtn_Click(object sender, EventArgs e)
        {
            i = 0;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ShowQuestion();
            Random rnd = new Random();
            string v = dataGridView1.Rows[rnd.Next(0, CountRows()-1)].Cells[2].Value.ToString();
            richTextBox1.Text = v;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ShowQuestion();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if(passwordtb.Text == "Valentin777")
            {
                button1.Enabled = true;
            }
            else
            {
                MessageBox.Show(@"Wrong password!!!Only Samciucov Valentin can add Books!");
            }
        }
    }
}
