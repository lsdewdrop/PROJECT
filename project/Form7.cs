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
    public partial class Form7 : Form
    {
        public int check = 0;
        DB db = DB.getInstance();
        MySqlConnection con;

        private static Form7 instance;

        public Form7()
        {
            InitializeComponent();
            instance = this;
            con = db.getDBConnection();
        }

        public static Form7 getinstance
        {
            get { return instance; }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form7_Load(object sender, EventArgs e)
        {
            if(check==0)
            {
                label1.Text = "성공적으로 추가 되었습니다.";
            }
            else if(check==1)
            {
                label1.Text = "성공적으로 수정 되었습니다.";
            }
            else
            {
                label1.Text = "성공적으로 삭제 되었습니다.";
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        
    }
}
