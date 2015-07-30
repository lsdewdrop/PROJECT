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
    public partial class Form3 : Form
    {
        DB db = DB.getInstance();
        MySqlConnection con;

        private static Form3 instance;

        public Form3()
        {
            InitializeComponent();
            instance = this;
            con = db.getDBConnection();
            
        }

        public static Form3 getinstance
        {
            get { return instance; }
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            this.Height = 120;
            this.Width = 509;
            button1.Location = new Point(27, 21);
            button2.Location = new Point(190, 21);
            button3.Location = new Point(350, 21);

            label1.Hide(); //상위항목 수정 숨기기
            comboBox1.Hide();
            pictureBox1.Hide();
            textBox1.Hide();
            button4.Hide();

            comboBox2.Hide();  //하위항목 숨기기
            comboBox3.Hide();
            textBox2.Hide();
            button5.Hide();
            

            pictureBox2.Hide(); //문장항목 숨기기
            button6.Hide(); 
            textBox3.Hide();
            comboBox4.Hide();
            comboBox5.Hide();

            Form7.getinstance.check = 1;
        }

        private void button1_Click(object sender, EventArgs e) //상위항목 버튼
        {
            this.Height = 318;
            label1.Location = new Point(23, 131);
            comboBox1.Location = new Point(44, 173);
            pictureBox1.Location = new Point(202, 178);
            textBox1.Location = new Point(234,173);
            button4.Location = new Point(401, 170);

            label1.Show(); //상위항목 수정 보이기
            comboBox1.Show();
            pictureBox1.Show();
            textBox1.Show();
            button4.Show();

            comboBox2.Hide();  //하위항목 숨기기
            comboBox3.Hide();
            textBox2.Hide();
            button5.Hide();

            //문장항목 숨기기
            comboBox4.Hide();
            button6.Hide();
            textBox3.Hide();
            pictureBox2.Hide();
            comboBox5.Hide();

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

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)  //상위항목 수정버튼
        {
            string tex1_tex=textBox1.Text;
            if(tex1_tex=="")
            {
                MessageBox.Show("텍스트를 입력해주세요.", "에러");
            }
            else
            {
                int check = 0;

                string query_double = "select * from s_title"; //상위항목 중복검사
                MySqlCommand command_double = new MySqlCommand(query_double, con);
                MySqlDataReader reader_double = command_double.ExecuteReader();
                while (reader_double.Read())
                {
                    string contents = reader_double.GetString("title");
                    if (tex1_tex == contents)
                    {
                        check = 1;
                    }

                }
                reader_double.Close();

                if (check == 0) //중복이 아닐때
                {
                    string query_title = "update s_title set title=@new_title where title=@old_title";
                    MySqlCommand mc_title = new MySqlCommand();
                    mc_title.Connection = con;
                    mc_title.CommandText = query_title;
                    mc_title.Prepare();
                    mc_title.Parameters.AddWithValue("@new_title", tex1_tex);
                    mc_title.Parameters.AddWithValue("@old_title", comboBox1.Text);
                    mc_title.ExecuteNonQuery();

                    string query_detail = "update s_detail set title=@new_title where title=@old_title";
                    MySqlCommand mc_detail = new MySqlCommand();
                    mc_detail.Connection = con;
                    mc_detail.CommandText = query_detail;
                    mc_detail.Prepare();
                    mc_detail.Parameters.AddWithValue("@new_title", tex1_tex);
                    mc_detail.Parameters.AddWithValue("@old_title", comboBox1.Text);
                    mc_detail.ExecuteNonQuery();

                    string query_db = "update s_db set title=@new_title where title=@old_title";
                    MySqlCommand mc_db = new MySqlCommand();
                    mc_db.Connection = con;
                    mc_db.CommandText = query_db;
                    mc_db.Prepare();
                    mc_db.Parameters.AddWithValue("@new_title", tex1_tex);
                    mc_db.Parameters.AddWithValue("@old_title", comboBox1.Text);
                    mc_db.ExecuteNonQuery();
                    Form7.getinstance.ShowDialog();
                }
                else //중복일 때
                {
                    MessageBox.Show("이미 있는 항목입니다.", "에러");
                }
            }
            string query = "select * from s_title"; // 수정이 끝나고 콤보박스에 상위항목 보이기
            MySqlCommand command = new MySqlCommand(query, con);
            MySqlDataReader reader = command.ExecuteReader();
            comboBox1.Items.Clear();
            while (reader.Read())
            {
                string contents = reader.GetString("title");
                comboBox1.Items.Add(contents);
            }
            reader.Close();
            comboBox1.Text = "";
            textBox1.Text = "";
            
        }

        private void button2_Click(object sender, EventArgs e)  //하위항목 수정
        {
            this.Height = 329;
            this.Width = 725;
            button1.Location = new Point(58,31);
            button2.Location = new Point(266,31);
            button3.Location = new Point(489,31);

            label1.Location = new Point(28,161);
            comboBox2.Location = new Point(32,211);
            comboBox3.Location = new Point(191,211);
            pictureBox1.Location = new Point(381, 217);
            textBox2.Location = new Point(451, 211);
            button5.Location = new Point(604, 208);

            textBox2.Size = new Size(106, 27);

            textBox2.ReadOnly = false;

            //상위항목 수정 숨기기
            comboBox1.Hide();
            textBox1.Hide();
            button4.Hide();
            //상위항목과 하위항목이 같이 쓰는거
            label1.Show(); //항목을 선택해주세요.
            pictureBox1.Show();

            comboBox2.Show();  //하위항목 보이기
            comboBox3.Show();
            textBox2.Show();
            button5.Show();

            //문장항목 숨기기
            comboBox4.Hide();
            pictureBox2.Hide();
            button6.Hide();
            textBox3.Hide();
            comboBox5.Hide();

            string query = "select * from s_title"; //콤보박스에 상위항목 보이기
            MySqlCommand command = new MySqlCommand(query, con);
            MySqlDataReader reader = command.ExecuteReader();
            comboBox2.Items.Clear();
            while (reader.Read())
            {
                string contents = reader.GetString("title");
                comboBox2.Items.Add(contents);
            }
            reader.Close();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string com2_tex = comboBox2.Text;
            string query = "select detail from s_detail where title=@title"; //콤보박스에 하위항목 보이기
            MySqlCommand command = new MySqlCommand(query, con);
            command.Parameters.AddWithValue("@title", com2_tex);
            MySqlDataReader reader = command.ExecuteReader();
            comboBox3.Items.Clear();
            while (reader.Read())
            {
                string contents = reader.GetString("detail");
                comboBox3.Items.Add(contents);
            }
            reader.Close();
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e) //하위항목 수정 버튼
        {
            if (comboBox3.Text == "")
            {
                MessageBox.Show("항목을 입력해주세요.", "에러");
            }
            else
            {
                string com2_tex = comboBox2.Text;
                string com3_tex = comboBox3.Text;
                string tex2_tex = textBox2.Text;

                int check = 0;

                string query_double = "select detail from s_detail where title=@title"; //하위항목 중복검사
                MySqlCommand command_double = new MySqlCommand(query_double, con);
                command_double.Parameters.AddWithValue("@title", com2_tex);
                MySqlDataReader reader_double = command_double.ExecuteReader();
                while (reader_double.Read())
                {
                    string contents = reader_double.GetString("detail");
                    if (tex2_tex == contents)
                    {
                        check = 1;
                    }

                }
                reader_double.Close();

                if (check == 0)  //중복일 때
                {
                    string query_detail = "update s_detail set detail=@new_detail where title=@title and detail=@old_detail";
                    MySqlCommand mc_detail = new MySqlCommand();
                    mc_detail.Connection = con;
                    mc_detail.CommandText = query_detail;
                    mc_detail.Prepare();
                    mc_detail.Parameters.AddWithValue("@new_detail", tex2_tex);
                    mc_detail.Parameters.AddWithValue("@title", com2_tex);
                    mc_detail.Parameters.AddWithValue("@old_detail", com3_tex);
                    mc_detail.ExecuteNonQuery();

                    string query_db = "update s_db set detail=@new_detail where title=@title and detail=@old_detail";
                    MySqlCommand mc_db = new MySqlCommand();
                    mc_db.Connection = con;
                    mc_db.CommandText = query_db;
                    mc_db.Prepare();
                    mc_db.Parameters.AddWithValue("@new_detail", tex2_tex);
                    mc_db.Parameters.AddWithValue("@title", com2_tex);
                    mc_db.Parameters.AddWithValue("@old_detail", com3_tex);
                    mc_db.ExecuteNonQuery();

                    Form7.getinstance.ShowDialog();

                    string query = "select detail from s_detail where title=@title"; //콤보박스에 하위항목 보이기
                    MySqlCommand command = new MySqlCommand(query, con);
                    command.Parameters.AddWithValue("@title", com2_tex);
                    MySqlDataReader reader = command.ExecuteReader();
                    comboBox3.Items.Clear();
                    while (reader.Read())
                    {
                        string contents = reader.GetString("detail");
                        comboBox3.Items.Add(contents);
                    }
                    reader.Close();
                }
                else //중복이 아닐 때
                {
                    MessageBox.Show("이미 있는 항목입니다.", "에러");
                }
            }
            comboBox2.Text = "";
            comboBox3.Text = "";
            textBox2.Text = "";
            
        }

        private void button3_Click(object sender, EventArgs e)  //문장수정
        {
            this.Height = 542;
            this.Width = 725;
            button1.Location = new Point(58, 31);
            button2.Location = new Point(266, 31);
            button3.Location = new Point(489, 31);

            label1.Location = new Point(257,124);
            comboBox5.Location = new Point(187,163);
            comboBox4.Location = new Point(396,163);
            textBox2.Location = new Point(58,245);
            pictureBox2.Location = new Point(354,300);
            textBox3.Location = new Point(58,348);
            button6.Location = new Point(332,398);

            //상위항목 수정 숨기기
            comboBox1.Hide();
            textBox1.Hide();
            button4.Hide();
            pictureBox1.Hide();
            //상위항목과 문장항목이 같이 쓰는거
            label1.Show(); //항목을 선택해주세요.
            comboBox4.Show();
            textBox2.Show();
            textBox2.ReadOnly = true;
            textBox2.Size = new Size(583, 27);
            textBox3.Size = new Size(583, 27);

            //하위항목 숨기기
            button5.Hide();
            comboBox3.Hide();
            comboBox2.Hide();

            //문장항목 보이기
            pictureBox2.Show();
            button6.Show();
            textBox3.Show();
            comboBox5.Show();

            string query = "select * from s_title"; //콤보박스에 상위항목 보이기
            MySqlCommand command = new MySqlCommand(query, con);
            MySqlDataReader reader = command.ExecuteReader();
            comboBox5.Items.Clear();
            while (reader.Read())
            {
                string contents = reader.GetString("title");
                comboBox5.Items.Add(contents);
            }
            reader.Close();


        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            if(textBox3.Text=="")
            {
                MessageBox.Show("항목을 선택해주세요.", "에러");
            }
            else
            {
                string com5_tex = comboBox5.Text;
                string com4_tex = comboBox4.Text;
                string tex2_tex = textBox2.Text;
                string tex3_tex = textBox3.Text;

                int check = 0;

                string query_double = "select contents from s_db where title=@title and detail=@detail"; //문장 중복검사
                MySqlCommand command_double = new MySqlCommand(query_double, con);
                command_double.Parameters.AddWithValue("@title", com5_tex);
                command_double.Parameters.AddWithValue("@detail", com4_tex);
                MySqlDataReader reader_double = command_double.ExecuteReader();
                while (reader_double.Read())
                {
                    string contents = reader_double.GetString("contents");
                    if (tex3_tex == contents)
                    {
                        check = 1;
                    }

                }
                reader_double.Close();

                if (check == 0)  //중복이 아닐 때
                {
                    string query_db = "update s_db set contents=@new_contents where title=@title and detail=@detail and contents=@old_contents";
                    MySqlCommand mc_db = new MySqlCommand();
                    mc_db.Connection = con;
                    mc_db.CommandText = query_db;
                    mc_db.Prepare();
                    mc_db.Parameters.AddWithValue("@new_contents", tex3_tex);
                    mc_db.Parameters.AddWithValue("@old_contents", tex2_tex);
                    mc_db.Parameters.AddWithValue("@title", com5_tex);
                    mc_db.Parameters.AddWithValue("@detail", com4_tex);
                    mc_db.ExecuteNonQuery();
                    Form7.getinstance.ShowDialog();
                }
                else //중복일 때
                {
                    MessageBox.Show("이미 있는 항목입니다.", "에러");
                }
            }
            comboBox5.Text = "";
            comboBox4.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            Form4.getinstance.ShowDialog();
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            string com5_tex = comboBox5.Text;
            string query = "select detail from s_detail where title=@title"; //콤보박스에 하위항목 보이기
            MySqlCommand command = new MySqlCommand(query, con);
            command.Parameters.AddWithValue("@title", com5_tex);
            MySqlDataReader reader = command.ExecuteReader();
            comboBox4.Items.Clear();
            while (reader.Read())
            {
                string contents = reader.GetString("detail");
                comboBox4.Items.Add(contents);
            }
            reader.Close();
        }

        private void Form3_FormClosed(object sender, FormClosedEventArgs e)
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
