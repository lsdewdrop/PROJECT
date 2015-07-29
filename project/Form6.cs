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
    public partial class Form6 : Form
    {
        DB db = DB.getInstance();
        MySqlConnection con;

        private static Form6 instance;

        public Form6()
        {
            InitializeComponent();
            instance = this;
            con = db.getDBConnection();
        }

        public static Form6 getinstance
        {
            get { return instance; }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Form5.getinstance.check == 0)  //상위 삭제일때
            {
                string title = Form5.getinstance.comboBox1.Text;

                string query_title = "delete from s_title where title=@title";
                MySqlCommand command = new MySqlCommand();
                command.Connection = con;
                command.CommandText = query_title;
                command.Prepare();
                command.Parameters.AddWithValue("@title", title);
                command.ExecuteNonQuery();

                string query_detail = "delete from s_detail where title=@title";
                command.CommandText = query_detail;
                command.Prepare();
                command.ExecuteNonQuery();

                string query_db = "delete from s_db where title=@title";
                command.CommandText = query_db;
                command.Prepare();
                command.ExecuteNonQuery();

                string query = "select * from s_title";
                MySqlCommand command_re = new MySqlCommand(query, con);
                MySqlDataReader reader = command_re.ExecuteReader();
                Form5.getinstance.comboBox1.Items.Clear();
                while (reader.Read())
                {
                    string title_re = reader.GetString("title");
                    Form5.getinstance.comboBox1.Items.Add(title_re);
                }
                reader.Close();

                Form5.getinstance.comboBox1.Text = "";
            }
            else if(Form5.getinstance.check==1)  //하위 삭제일때
            {
                string title = Form5.getinstance.comboBox1.Text;
                string detail = Form5.getinstance.comboBox2.Text;

                MySqlCommand command = new MySqlCommand();
                command.Connection = con;

                string query_detail = "delete from s_detail where title=@title and detail=@detail";
                command.CommandText = query_detail;
                command.Prepare();
                command.Parameters.AddWithValue("@title", title);
                command.Parameters.AddWithValue("@detail", detail);
                command.ExecuteNonQuery();

                string query_db = "delete from s_db where title=@title and detail=@detail";
                command.CommandText = query_db;
                command.Prepare();
                command.ExecuteNonQuery();

                Form5.getinstance.comboBox1.Text = "";
                Form5.getinstance.comboBox2.Text = "";
            }
            else //문장 삭제일때
            {
                string title = Form5.getinstance.comboBox4.Text;
                string detail = Form5.getinstance.comboBox3.Text;
                string contents = Convert.ToString(Form5.getinstance.listBox1.SelectedItem);

                string query = "delete from s_db where title=@title and detail=@detail and contents=@contents";
                MySqlCommand command = new MySqlCommand();
                command.Connection = con;

                command.CommandText = query;
                command.Prepare();
                command.Parameters.AddWithValue("@title", title);
                command.Parameters.AddWithValue("@detail", detail);
                command.Parameters.AddWithValue("@contents", contents);
                command.ExecuteNonQuery();

                Form5.getinstance.comboBox4.Text = "";
                Form5.getinstance.comboBox3.Text = "";

                string query_contents = "select contents from s_db where title=@title and detail=@detail";
                MySqlCommand command_con = new MySqlCommand(query_contents, con);
                command_con.Parameters.AddWithValue("@title", title);
                command_con.Parameters.AddWithValue("@detail", detail);
                MySqlDataReader reader = command_con.ExecuteReader();
                Form5.getinstance.listBox1.Items.Clear();
                while(reader.Read())
                {
                    string contents_list = reader.GetString("contents");
                    Form5.getinstance.listBox1.Items.Add(contents_list);
                }
                reader.Close();
            }
            this.Close();
            Form7.getinstance.ShowDialog();

            string query_list = "select * from s_title"; //리스트박스에 상위항목 보이기
            MySqlCommand command_list = new MySqlCommand(query_list, con);
            MySqlDataReader reader_list = command_list.ExecuteReader();
            Form1.getinstance.listBox2.Items.Clear();
            while (reader_list.Read())
            {
                string contents = reader_list.GetString("title");
                Form1.getinstance.listBox2.Items.Add(contents);
            }
            reader_list.Close();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form5.getinstance.comboBox1.Text = "";
            this.Close();
        }
    }
}
