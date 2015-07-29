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
    public partial class Form5 : Form
    {
        DB db = DB.getInstance();
        MySqlConnection con;


        public int check = 0; //상위인지 하위인지 문장인지 판별

        private static Form5 instance;

        public Form5()
        {
            InitializeComponent();
            instance = this;
            con = db.getDBConnection();
            
        }

        public static Form5 getinstance
        {
            get { return instance; }
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            this.Height = 120;
            this.Width = 509;
            button1.Location = new Point(27, 21);
            button2.Location = new Point(190, 21);
            button3.Location = new Point(350, 21);

            label1.Hide();   //상위항목 숨기기
            comboBox1.Hide();
            button4.Hide();

            comboBox2.Hide(); //하위항목 숨기기
            button5.Hide();

            listBox1.Hide(); //문장항목 숨기기
            button6.Hide();
            comboBox3.Hide();
            comboBox4.Hide();

            comboBox1.Text = "";
            comboBox2.Text = "";
            listBox1.Items.Clear();
            comboBox3.Text = "";
            comboBox4.Text = "";

            Form7.getinstance.check = 2;
        }

        private void button1_Click(object sender, EventArgs e) // 상위항목 삭제
        {
            this.Height = 325;

            label1.Location = new Point(164, 111);  
            comboBox1.Location = new Point(167, 158);
            button4.Location = new Point(222,199);

            label1.Show();    // 상위항목 보이기
            comboBox1.Show();
            button4.Show();

            comboBox2.Hide(); //하위항목 숨기기
            button5.Hide();

            listBox1.Hide(); //문장항목 숨기기
            button6.Hide();
            comboBox3.Hide();
            comboBox4.Hide();

            string query = "select * from s_title";
            MySqlCommand command = new MySqlCommand(query, con);
            MySqlDataReader reader = command.ExecuteReader();
            comboBox1.Items.Clear();
            while(reader.Read())
            {
                string title = reader.GetString("title");
                comboBox1.Items.Add(title);
            }
            reader.Close();
        }

        private void button4_Click(object sender, EventArgs e)  //상위항목 삭제 버튼
        {
            check = 0;

            if(comboBox1.Text=="")
            {
                MessageBox.Show("항목을 선택해주세요", "에러");
            }
            else
            {
                Form6.getinstance.ShowDialog();
            }
        }


        private void button2_Click(object sender, EventArgs e) //하위항목 삭제
        {
            this.Height = 325;

            comboBox1.Text = "";
            comboBox2.Text = "";

            label1.Location = new Point(164, 111);
            comboBox1.Location = new Point(55, 159);
            comboBox2.Location = new Point(286,159);
            button5.Location = new Point(222, 219);

            label1.Show();   //상위항목과 공동으로 쓰는 툴
            comboBox1.Show();
            comboBox2.Show(); //새로 추가된거
            button5.Show();

            button4.Hide(); //상위항목 숨기기

            listBox1.Hide(); //문장 항목 숨기기
            button6.Hide();
            comboBox3.Hide();
            comboBox4.Hide();


            string query = "select * from s_title";
            MySqlCommand command = new MySqlCommand(query, con);
            MySqlDataReader reader = command.ExecuteReader();
            comboBox1.Items.Clear();
            while(reader.Read())
            {
                string title = reader.GetString("title");
                comboBox1.Items.Add(title);
            }
            reader.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string com1_tex = comboBox1.Text;

            string query = "select detail from s_detail where title=@title";
            MySqlCommand command = new MySqlCommand(query, con);
            command.Parameters.AddWithValue("@title", com1_tex);
            MySqlDataReader reader = command.ExecuteReader();
            comboBox2.Items.Clear();
            while(reader.Read())
            {
                string detail = reader.GetString("detail");
                comboBox2.Items.Add(detail);
            }
            reader.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            check = 1;

            if(comboBox3.Text=="")
            {
                MessageBox.Show("항목을 선택해주세요", "에러");
            }
            else
            {
                Form6.getinstance.ShowDialog();
            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Height = 546;

            label1.Location = new Point(164, 111);
            comboBox4.Location = new Point(55, 159);
            comboBox3.Location = new Point(286, 159);
            listBox1.Location = new Point(57, 247);
            button6.Location = new Point(218, 402);

            label1.Show();   //상위항목과 공동으로 쓰는 툴
            comboBox4.Show();
            comboBox3.Show(); //새로 추가된거
            listBox1.Show();
            button6.Show();

            button5.Hide(); //하위항목 숨기기
            comboBox2.Hide();
            comboBox1.Hide();

            button4.Hide(); //상위항목 숨기기

            comboBox4.Text = "";
            comboBox3.Text = "";

            string query = "select * from s_title";
            MySqlCommand command = new MySqlCommand(query,con);
            MySqlDataReader reader = command.ExecuteReader();
            comboBox4.Items.Clear();
            while(reader.Read())
            {
                string title = reader.GetString("title");
                comboBox4.Items.Add(title);
            }
            reader.Close();
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            string com4_tex = comboBox4.Text;
            string com3_tex = comboBox3.Text;

            string query = "select contents from s_db where title=@title and detail=@detail";
            MySqlCommand command = new MySqlCommand(query,con);
            command.Parameters.AddWithValue("@title", com4_tex);
            command.Parameters.AddWithValue("@detail", com3_tex);
            MySqlDataReader reader = command.ExecuteReader();
            listBox1.Items.Clear();
            while(reader.Read())
            {
                string contents = reader.GetString("contents");
                listBox1.Items.Add(contents);
            }
            reader.Close();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            string com4_tex = comboBox4.Text;

            string query = "select detail from s_detail where title=@title";
            MySqlCommand command = new MySqlCommand(query, con);
            command.Parameters.AddWithValue("@title", com4_tex);
            MySqlDataReader reader = command.ExecuteReader();
            comboBox3.Items.Clear();
            while (reader.Read())
            {
                string detail = reader.GetString("detail");
                comboBox3.Items.Add(detail);
            }
            reader.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if(Convert.ToString(listBox1.SelectedItem)=="")
            {
                MessageBox.Show("항목을 선택해주세요.", "에러");
            }
            else
            {
                check = 2;
                Form6.getinstance.ShowDialog();
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Form5_FormClosed(object sender, FormClosedEventArgs e)
        {
            string query = "select * from s_title"; //리스트박스에 상위항목 보이기
            MySqlCommand command = new MySqlCommand(query, con);
            MySqlDataReader reader = command.ExecuteReader();
            Form1.getinstance.listBox2.Items.Clear();
            while (reader.Read())
            {
                string contents = reader.GetString("title");
                Form1.getinstance.listBox2.Items.Add(contents);
            }
            reader.Close();
        }





    }
}
