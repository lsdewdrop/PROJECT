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
    public partial class Form2 : Form
    {
        DB db = DB.getInstance();
        MySqlConnection con;

        private static Form2 instance;

        public Form2()
        {
            InitializeComponent();
            instance = this;
            con = db.getDBConnection();
          
        }

        public static Form2 getinstance
        {
            get { return instance; }
        }

        private void button1_Click(object sender, EventArgs e)  //상위항목
        {
            this.Height = 510;
            listBox1.Location = new Point(27, 137);
            label1.Location = new Point(282, 153);
            textBox1.Location = new Point(286,191);
            button4.Location = new Point(413,239);
            listBox1.Show(); //상위항목 보이기
            label1.Show();
            textBox1.Show();
            button4.Show();
            label2.Hide(); //하위항목 숨기기
            comboBox1.Hide();
            listBox2.Hide();
            textBox2.Hide();
            button5.Hide();
            label4.Hide(); //문장추가 숨기기
            comboBox2.Hide();
            listBox3.Hide();
            textBox3.Hide();
            button6.Hide();
            comboBox3.Hide();

            string query = "select * from s_title"; //리스트박스에 보여주기
            MySqlCommand command = new MySqlCommand(query, con);
            MySqlDataReader reader = command.ExecuteReader();
            listBox1.Items.Clear();
            while (reader.Read())
            {
                string contents = reader.GetString("title");
                listBox1.Items.Add(contents);

            }
            reader.Close();
        }

        private void Form2_Load(object sender, EventArgs e) // 추가 기능 열릴 때
        {
            this.Height = 120;
            this.Width = 509;
            button1.Location = new Point(27, 21);
            button2.Location = new Point(190, 21);
            button3.Location = new Point(350, 21);

            listBox1.Hide(); //상위항목 숨기기
            label1.Hide();
            textBox1.Hide();
            button4.Hide();

            label2.Hide(); //하위항목 숨기기
            comboBox1.Hide();
            listBox2.Hide();
            textBox2.Hide();
            button5.Hide();

            label4.Hide();
            comboBox2.Hide();
            listBox3.Hide();
            textBox3.Hide();
            button6.Hide();
            comboBox3.Hide();
            Form7.getinstance.check = 0;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        
        private void button4_Click(object sender, EventArgs e)  //상위항목 추가버튼
        {
            string tex = textBox1.Text;
            if (tex == "") //아무것도 입력을 안했을 때
            {
                MessageBox.Show("항목을 입력해주세요.", "에러");
            }
            else
            {
                int check = 0;

                string query = "select * from s_title"; //중복검사
                MySqlCommand command = new MySqlCommand(query, con);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string contents = reader.GetString("title");
                    if (tex == contents)
                    {
                        check = 1;
                    }

                }
                reader.Close();
                

                if (check == 0) //중복 아닐때
                {
                    string inq = "insert into s_title (title) values (@title)"; //s_title에 삽입
                    MySqlCommand mc = new MySqlCommand();
                    mc.Connection = con;
                    mc.CommandText = inq;
                    mc.Prepare();
                    mc.Parameters.AddWithValue("@title", tex);
                    mc.ExecuteNonQuery();


                    reader = command.ExecuteReader(); //삽입후 다시 목록 보이기
                    listBox1.Items.Clear();
                    while (reader.Read())
                    {
                        string contents = reader.GetString("title");
                        listBox1.Items.Add(contents);

                    }
                    reader.Close();
                    Form7.getinstance.ShowDialog();
                }
                else //중복 일때
                {
                    MessageBox.Show("이미 있는 항목입니다.", "에러");
                }
            }
            textBox1.Text = "";
            
        }

        private void button2_Click(object sender, EventArgs e)  //하위항목
        {
            this.Height = 510;
            listBox1.Hide();//상위항목 숨기기
            textBox1.Hide();
            button4.Hide();
            label2.Location = new Point(181, 102);
            comboBox1.Location = new Point(208, 137);
            listBox2.Location = new Point(54, 227);
            label1.Location = new Point(276,228);
            textBox2.Location = new Point(280,263);
            button5.Location = new Point(413,304);
            label1.Show();
            label2.Show(); //하위항목 보이기
            comboBox1.Show();
            listBox2.Show();
            textBox2.Show();
            button5.Show();

            label4.Hide(); //문장추가 숨기기
            comboBox2.Hide();
            listBox3.Hide();
            textBox3.Hide();
            button6.Hide();
            comboBox3.Hide();

            comboBox1.Text = "선택";
            listBox2.Items.Clear();

            string query = "select * from s_title"; //콤보박스에 상위항목 보이기
            MySqlCommand command = new MySqlCommand(query, con);
            MySqlDataReader reader = command.ExecuteReader();
            comboBox1.Items.Clear();
            while (reader.Read())
            {
                string contents = reader.GetString("title");
                comboBox1.Items.Add(contents);
            }
            reader.Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)  //하위항목 추가버튼
        {
            string com1_tex = comboBox1.Text;
            string tex2_tex = textBox2.Text;
            if (tex2_tex == "") //아무것도 입력안했을때
            {
                MessageBox.Show("항목을 입력해주세요.", "에러");
            }
            else //입력했을때
            {
                int check = 0;
                string query = "select detail from s_detail where title= @title";
                MySqlCommand command = new MySqlCommand(query, con);
                command.Parameters.AddWithValue("@title", com1_tex);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string contents = reader.GetString("detail");
                    if (contents == tex2_tex)
                    {
                        check = 1;
                    }
                }
                reader.Close();

                if (check == 0) //중복 아닐때
                {
                    string inq = "insert into s_detail (title,detail) values (@title,@detail)"; //s_detail에 삽입
                    MySqlCommand mc = new MySqlCommand();
                    mc.Connection = con;
                    mc.CommandText = inq;
                    mc.Prepare();
                    mc.Parameters.AddWithValue("@title", com1_tex);
                    mc.Parameters.AddWithValue("@detail", tex2_tex);
                    mc.ExecuteNonQuery();


                    reader = command.ExecuteReader(); //삽입후 다시 리스트에 보여주기
                    listBox2.Items.Clear();
                    while (reader.Read())
                    {
                        string contents = reader.GetString("detail");
                        listBox2.Items.Add(contents);

                    }
                    reader.Close();
                    Form7.getinstance.ShowDialog();
                }
                else //중복일때
                {
                    MessageBox.Show("이미 있는 항목입니다.", "에러");
                }
            }
            textBox2.Text = "";
            comboBox1.Text = "선택";
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string com1_tex = comboBox1.Text;
            string query = "select detail from s_detail where title=@title"; //리스트박스에 보여주기
            MySqlCommand command = new MySqlCommand(query, con);
            command.Parameters.AddWithValue("@title", com1_tex);
            MySqlDataReader reader = command.ExecuteReader();
            listBox2.Items.Clear();
            while (reader.Read())
            {
                string contents = reader.GetString("detail");
                listBox2.Items.Add(contents);
            }
            reader.Close();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string com3_tex = comboBox3.Text;
            string com2_tex = comboBox2.Text;
            string query = "select contents from s_db where title=@title and detail=@detail"; //리스트박스에 보여주기
            MySqlCommand command = new MySqlCommand(query, con);
            command.Parameters.AddWithValue("@title", com3_tex);
            command.Parameters.AddWithValue("@detail", com2_tex);
            MySqlDataReader reader = command.ExecuteReader();
            listBox3.Items.Clear();
            while (reader.Read())
            {
                string contents = reader.GetString("contents");
                listBox3.Items.Add(contents);
            }
            reader.Close();
        }

        private void listBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e) //문장 추가 버튼
        {
            string com3_tex=comboBox3.Text;
            string com2_tex= comboBox2.Text;

            if(com3_tex=="" || com2_tex=="") //항목을 선택하지 않았을때
            {
                MessageBox.Show("항목을 선택하지 않으셨습니다.선택하여 주십시오.", "에러");
            }
            else //항목을 선택하였을 때
            {
                string tex = textBox3.Text;
                string inq = "insert into s_db (title,detail,contents) values (@title,@detail,@contents)"; //s_detail에 삽입
                MySqlCommand mc = new MySqlCommand();
                mc.Connection = con;
                mc.CommandText = inq;
                mc.Prepare();
                mc.Parameters.AddWithValue("@title", com3_tex);
                mc.Parameters.AddWithValue("@detail", com2_tex);
                mc.Parameters.AddWithValue("@contents", tex);
                mc.ExecuteNonQuery();
                Form7.getinstance.ShowDialog();
                
            }
            comboBox3.Text = "선택";
            comboBox2.Text = "선택";
            textBox3.Text = "";
            string query = "select contents from s_db where title=@title and detail=@detail";
            MySqlCommand command = new MySqlCommand(query, con);
            command.Parameters.AddWithValue("@title", com3_tex);
            command.Parameters.AddWithValue("@detail", com2_tex);
            MySqlDataReader reader = command.ExecuteReader();
            listBox3.Items.Clear();
            while(reader.Read())
            {
                string contents = reader.GetString("contents");
                listBox3.Items.Add(contents);
            }
            reader.Close();

        }

        private void button3_Click(object sender, EventArgs e) // 문장 추가
        {
            this.Height = 615;
            label2.Location = new Point(58, 106);
            comboBox3.Location = new Point(86,147);
            label4.Location = new Point(316,106);
            comboBox2.Location = new Point(333,147);
            listBox3.Location = new Point(62,192);
            textBox3.Location = new Point(62,472);
            button6.Location = new Point(241,515);

            listBox1.Hide();//상위항목 숨기기
            textBox1.Hide();
            button4.Hide();
            label1.Hide();
            //하위항목 숨기기
            label2.Show(); //문장추가에 쓸꺼
            listBox2.Hide(); //아닌거
            textBox2.Hide();
            button5.Hide();
            comboBox1.Hide();
            label4.Show(); //문장추가 보이기
            comboBox2.Show();
            listBox3.Show();
            textBox3.Show();
            button6.Show();
            comboBox3.Show();
            comboBox3.Text = "선택";
            comboBox2.Text = "선택";
            listBox3.Items.Clear();

            string query = "select * from s_title"; //콤보박스에 상위항목 보이기
            MySqlCommand command = new MySqlCommand(query, con);
            MySqlDataReader reader = command.ExecuteReader();
            comboBox3.Items.Clear();
            while (reader.Read())
            {
                string contents = reader.GetString("title");
                comboBox3.Items.Add(contents);
            }
            reader.Close();
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            string com3_tex = comboBox3.Text;
            string query = "select detail from s_detail where title=@title"; //리스트박스에 보여주기
            MySqlCommand command = new MySqlCommand(query, con);
            command.Parameters.AddWithValue("@title", com3_tex);
            MySqlDataReader reader = command.ExecuteReader();
            comboBox2.Items.Clear();
            while (reader.Read())
            {
                string contents = reader.GetString("detail");
                comboBox2.Items.Add(contents);
            }

            reader.Close();
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
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
