using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace DAO
{
    public class DataAccessHelper
    {
        SqlConnection cnn;
        SqlCommand cmd;
        String connString = @"Data Source=(local);Initial Catalog=StudentManagementDB;Integrated Security=True";

        public DataAccessHelper()
        {
            cnn = new SqlConnection(connString);
            cmd = cnn.CreateCommand();
        }
        public SqlConnection GetConnection()
        {
            try
            {
                SqlConnection conn = new SqlConnection(connString);
                return conn;
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
        public DataTable GetTable(String sql)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection con = GetConnection();
                SqlDataAdapter adapter = new SqlDataAdapter(sql, con);
                adapter.Fill(dt);
                return dt;
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return dt;
        }
      
        public bool ExecuteNonQuery(string strSQL,
            CommandType ct,
            params SqlParameter[] param)
        {
            //Co f la gia tri tra ve
            bool f = false;
            if (cnn.State == ConnectionState.Open)
                cnn.Close();
            cnn.Open();
            // cmd
            cmd.Parameters.Clear();
            cmd.CommandText = strSQL;
            cmd.CommandType = ct;
            // add parameters
            foreach (SqlParameter p in param)
                cmd.Parameters.Add(p);
            // run command
            try
            {
                cmd.ExecuteNonQuery();
                //Thuc thi tot
                f = true;
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                cnn.Close();
            }
            return f;
        }
    }
}