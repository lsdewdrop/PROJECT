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
    public partial class Form4 : Form
    {
        DB db = DB.getInstance();
        MySqlConnection con;

        private static Form4 instance;

        public Form4()
        {
            InitializeComponent();
            instance = this;
            con = db.getDBConnection();
        }

        public static Form4 getinstance
        {
            get { return instance; }
        }


        private void Form4_Load(object sender, EventArgs e)
        {
            string com5_tex = Form3.getinstance.comboBox5.Text;
            string com4_tex = Form3.getinstance.comboBox4.Text;

            string query = "select contents from s_db where title=@title and detail=@detail"; //리스트박스에 문장항목 보이기
            MySqlCommand command = new MySqlCommand(query, con);
            command.Parameters.AddWithValue("@title", com5_tex);
            command.Parameters.AddWithValue("@detail", com4_tex);
            MySqlDataReader reader = command.ExecuteReader();
            listBox1.Items.Clear();
            while (reader.Read())
            {
                string contents = reader.GetString("contents");
                listBox1.Items.Add(contents);
            }
            reader.Close();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Close();
            Form3.getinstance.textBox2.Text = Convert.ToString(listBox1.SelectedItem);
        }

        
    }
}
