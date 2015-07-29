using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace project
{
    public partial class Form1 : Form
    {
        DB db = DB.getInstance();
        //DB connect;
        MySqlConnection con;

        private static Form1 instance;

        public Form1()
        {
            InitializeComponent();
            con = db.getDBConnection();
            //con.Open();
            instance = this;
        }

        public static Form1 getinstance
        {
            get { return instance; }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string query = "select * from s_title"; //리스트박스에 상위항목 보이기
            MySqlCommand command = new MySqlCommand(query, con);
            MySqlDataReader reader = command.ExecuteReader();
            listBox2.Items.Clear();
            while (reader.Read())
            {
                string contents = reader.GetString("title");
                listBox2.Items.Add(contents);
            }
            reader.Close();

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2.getinstance.ShowDialog();
        }

       

        

        

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            richTextBox1.Text += listBox1.SelectedItem;
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form3.getinstance.ShowDialog();
        }

        

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void listBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void listBox2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            string list2_tex = Convert.ToString(listBox2.SelectedItem);
            string query = "select detail from s_detail where title=@title";
            MySqlCommand command = new MySqlCommand(query, con);
            command.Parameters.AddWithValue("@title", list2_tex);
            MySqlDataReader reader = command.ExecuteReader();
            listBox3.Items.Clear();
            while (reader.Read())
            {
                string contents = reader.GetString("detail");
                listBox3.Items.Add(contents);


            }
            reader.Close();

            listBox1.Items.Clear();
        }

        private void listBox3_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            string list2_tex = Convert.ToString(listBox2.SelectedItem);
            string list3_tex = Convert.ToString(listBox3.SelectedItem);
            string query = "select contents from s_db where title=@title and detail= @detail";
            MySqlCommand command = new MySqlCommand(query, con);
            command.Parameters.AddWithValue("@title", list2_tex);
            command.Parameters.AddWithValue("@detail", list3_tex);
            MySqlDataReader reader = command.ExecuteReader();
            listBox1.Items.Clear();
            while (reader.Read())
            {
                string contents = reader.GetString("contents");
                listBox1.Items.Add(contents);


            }
            reader.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form5.getinstance.ShowDialog();
        }


 
    }
}
