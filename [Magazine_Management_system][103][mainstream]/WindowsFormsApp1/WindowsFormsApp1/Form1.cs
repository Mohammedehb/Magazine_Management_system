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
    public partial class Form1 : Form
    {
        string ordb = "Data source=orcl;User Id=scott;Password=tiger;";
        OracleConnection conn;

        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            conn = new OracleConnection(ordb);
            conn.Open();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = "select customer_ID from customer";
            cmd.CommandType = CommandType.Text;
            OracleDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cmbCustomer_ID.Items.Add(dr[0]);
            }
            dr.Close();
        }

        private void cmbCustomer_ID_SelectedIndexChanged(object sender, EventArgs e)
        {
            OracleCommand c = new OracleCommand();
            c.Connection = conn;
            c.CommandText = "select customer_Name ,gender," +
                " account_bank , bank_balance ,user_account," +
                "name_magazine,View_feedback,Date_borrow " +
                "from customer " +
                "where customer_ID =: id";
            c.CommandType = CommandType.Text;
            c.Parameters.Add("id", cmbCustomer_ID.SelectedItem.ToString());
            OracleDataReader dr = c.ExecuteReader();
            if (dr.Read())
            {
                customer_Name.Text = dr[0].ToString();
                gender.Text = dr[1].ToString();
                account_bank.Text = dr[2].ToString();
                bank_balance.Text = dr[3].ToString();
                user_account.Text = dr[4].ToString();
                name_magazine.Text = dr[5].ToString();
                View_feedback.Text = dr[6].ToString();
                Date_borrow.Text = dr[7].ToString();
            }
            dr.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                using (OracleConnection connection = new OracleConnection(ordb))
                {
                    connection.Open();

                    // Get the current maximum customer ID from the database
                    string getMaxIdQuery = "SELECT MAX(customer_id) FROM customer";
                    using (OracleCommand getMaxIdCommand = new OracleCommand(getMaxIdQuery, connection))
                    {
                        string maxIdString = getMaxIdCommand.ExecuteScalar().ToString();
                        int maxId = string.IsNullOrEmpty(maxIdString) ? 0 : int.Parse(maxIdString);

                        // Generate a new customer ID by incrementing the maximum ID by 1
                        int newId = maxId + 1;

                        // Insert the new customer into the database
                        string query = "INSERT INTO customer (customer_id, customer_name, gender, account_bank, bank_balance, user_account, name_magazine, view_feedback, date_borrow) VALUES (:param1, :param2, :param3, :param4, :param5, :param6, :param7, :param8, :param9)";
                        using (OracleCommand command = new OracleCommand(query, connection))
                        {
                            command.Parameters.Add(new OracleParameter("param1", OracleDbType.Int32)).Value = newId;
                            command.Parameters.Add(new OracleParameter("param2", OracleDbType.Varchar2)).Value = customer_Name.Text;
                            command.Parameters.Add(new OracleParameter("param3", OracleDbType.Varchar2)).Value = gender.Text;
                            command.Parameters.Add(new OracleParameter("param4", OracleDbType.Varchar2)).Value = account_bank.Text;
                            command.Parameters.Add(new OracleParameter("param5", OracleDbType.Varchar2)).Value = bank_balance.Text;
                            command.Parameters.Add(new OracleParameter("param6", OracleDbType.Varchar2)).Value = user_account.Text;
                            command.Parameters.Add(new OracleParameter("param7", OracleDbType.Varchar2)).Value = name_magazine.Text;
                            command.Parameters.Add(new OracleParameter("param8", OracleDbType.Varchar2)).Value = View_feedback.Text;
                            command.Parameters.Add(new OracleParameter("param9", OracleDbType.Date)).Value = Date_borrow.Text;

                            int rowsInserted = command.ExecuteNonQuery();
                            MessageBox.Show(rowsInserted + " rows inserted.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void button2_Click_1(object sender, EventArgs e)
        {
            try
            {
                using (OracleConnection connection = new OracleConnection(ordb))

                 {
                        connection.Open();

                    // Call the viewfeedback procedure
                    OracleCommand command = connection.CreateCommand();
                    
                            command.CommandType = CommandType.StoredProcedure;
                            command.CommandText= "viewfeedback";
                            // Set the input parameter
                            command.Parameters.Add(new OracleParameter("cus_id ", OracleDbType.Int32,15)).Value = Convert.ToInt32(coustomer.Text);

                            // Set the output parameter
                            command.Parameters.Add(new OracleParameter("feedback", OracleDbType.Varchar2,15)).Direction = ParameterDirection.Output;

                            command.ExecuteNonQuery();

                            string feedback = command.Parameters["feedback"].Value.ToString();
                            MessageBox.Show($"feedback of magazines for {coustomer.Text}: {feedback}");

                    }
                }
            
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void emp_info_Click(object sender, EventArgs e)
        {

            try
            {
                using (OracleConnection connection = new OracleConnection(ordb))
                {
                    connection.Open();

                    using (OracleCommand command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "information";

                        command.Parameters.Add("id_emp", OracleDbType.Int32).Value = emp_id.Text;
                        command.Parameters.Add("name_emp", OracleDbType.Varchar2, 50).Direction = ParameterDirection.Output;
                        command.Parameters.Add("gender_emp", OracleDbType.Varchar2, 10).Direction = ParameterDirection.Output;
                        command.Parameters.Add("Phn_num", OracleDbType.Int32).Direction = ParameterDirection.Output;
                        command.Parameters.Add("addr", OracleDbType.Varchar2, 100).Direction = ParameterDirection.Output;
                        command.Parameters.Add("Dat_work", OracleDbType.Date).Direction = ParameterDirection.Output;

                        command.ExecuteNonQuery();

                        string name_emp = command.Parameters["name_emp"].Value.ToString();
                        string gender_emp = command.Parameters["gender_emp"].Value.ToString();
                        string Phn_num = command.Parameters["Phn_num"].Value.ToString();
                        string addr = command.Parameters["addr"].Value.ToString();
                        string Dat_work = command.Parameters["Dat_work"].Value.ToString();
                        emp_name.Text = name_emp;
                        emp_gender.Text = gender_emp;
                        emp_phone.Text = Phn_num;
                        emp_address.Text = addr;
                        emp_date_work.Text = Dat_work;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void emp_gender_Click(object sender, EventArgs e)
        {

        }

       
    }
}
