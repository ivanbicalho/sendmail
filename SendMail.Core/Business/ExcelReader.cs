using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SendMail.Core.Entity;

namespace SendMail.Core.Business
{
    public class ExcelReader
    {
        public IEnumerable<EmailEntity> Read(string localFile)
        {
            string connectionString = $"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={localFile};Extended Properties='Excel 8.0;HDR=Yes;'";

            var dt = new System.Data.DataTable();
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand("select * from [Nomes$]", connection);

                using (var adapter = new OleDbDataAdapter(command))
                {
                    adapter.Fill(dt);
                }
            }

            var emails = new List<EmailEntity>();
            foreach (DataRow row in dt.Rows)
            {
                if (IsValidRow(row))
                {
                    emails.Add(new EmailEntity()
                    {
                        Name = row[0].ToString(),
                        Email = row[1].ToString()
                    });
                }
            }

            return emails;
        }

        private bool IsValidRow(DataRow row)
        {
            return !row.IsNull(0)
                && !row.IsNull(1)
                && !string.IsNullOrWhiteSpace(row[0].ToString())
                && !string.IsNullOrWhiteSpace(row[1].ToString());
        }
    }
}
