using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SendMail.Playground
{
    class Program
    {
        static void Main(string[] args)
        {
            string con =  @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=D:\test.xls;Extended Properties='Excel 8.0;HDR=Yes;'";

            using (OleDbConnection connection = new OleDbConnection(con))
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand("select * from [Nomes$]", connection);

                var adapter = new OleDbDataAdapter(command);
                var dt = new System.Data.DataTable();
                adapter.Fill(dt);                
            }
        }
    }
}
