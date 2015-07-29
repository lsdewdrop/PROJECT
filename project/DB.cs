using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace project
{
    class DB
    {
        private static DB instance;
        MySqlConnection con;

        private DB() 
        {
            string cs = @"server=data.khuhacker.com;userid=serverstudy;password=serverstudy!@#;database=serverstudy;charset=utf8";
            con = new MySqlConnection(cs);
            con.Open();
        }

        public static DB getInstance()
        {
            if (instance == null)
            {
                instance = new DB();
           
            }
            return instance;
        }

        public MySqlConnection getDBConnection()
        {
            return con;
        }
    }
}
