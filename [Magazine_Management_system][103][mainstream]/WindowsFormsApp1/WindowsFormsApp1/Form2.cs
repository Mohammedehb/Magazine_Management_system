using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;

namespace WindowsFormsApp1
{
    public partial class Form2 : Form
    {
        private OracleConnection conn = new OracleConnection("Data source=orcl;User Id=scott;Password=tiger;");
        private OracleCommandBuilder builder;
        private OracleDataAdapter adapter;
        private DataTable dt;
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            conn.Open();
        }

        private void search_btn_Click(object sender, EventArgs e)
        {
            string searchValue = search_text.Text.Trim();

            string selectQuery = "select employee_id,name_magazine,number_magazine, sales_report from employess where name_magazine = :searchValue";

            adapter = new OracleDataAdapter(selectQuery, conn);
            adapter.SelectCommand.Parameters.Add(":searchValue", searchValue);
            builder = new OracleCommandBuilder(adapter);
            dt = new DataTable();

            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void Update_btn_Click(object sender, EventArgs e)
        {
            try
            {
                adapter.Update(dt);
                MessageBox.Show("Data saved successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}
