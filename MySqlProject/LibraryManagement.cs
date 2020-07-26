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

namespace MySqlProject
{
    public partial class LibraryManagement : Form
    {
        string connectionString = @"Server=localhost;Database=csharp;Uid=root;";
        int bookID = 0;
        public LibraryManagement()
        {
            InitializeComponent();
            AutoComplete();
        }

        DataTable dbdataset;
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                MySqlConnection connection = new MySqlConnection("datasource=localhost;port=3306;username=root;password=");
                connection.Open();
                if (connection.State == ConnectionState.Open)
                {
                    label4.Text = "Connected";
                    label4.ForeColor = Color.Green;
                }
                else
                {
                    label4.Text = "Not Connected";
                    label4.ForeColor = Color.Red;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            Clear();
            GridFill();
        }

        /*DataTable dbdataset;*/

        void Clear()
        {
            txtBookName.Text = txtAuthor.Text = txtDescription.Text = txtSearch.Text = "";
            bookID = 0;
            btnSave.Text = "Save";
            btnDelete.Enabled = false;
            //txtBookName.Clear();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            using (MySqlConnection mysqlCon = new MySqlConnection(connectionString))
            {
                mysqlCon.Open();
                MySqlCommand mySqlCmd = new MySqlCommand("BookAddOrEdit",mysqlCon);
                mySqlCmd.CommandType = CommandType.StoredProcedure;
                mySqlCmd.Parameters.AddWithValue("_BookID",bookID);
                mySqlCmd.Parameters.AddWithValue("_BookName", txtBookName.Text.Trim());
                mySqlCmd.Parameters.AddWithValue("_Author", txtAuthor.Text.Trim());
                mySqlCmd.Parameters.AddWithValue("_Description", txtDescription.Text.Trim());
                mySqlCmd.ExecuteNonQuery();
                MessageBox.Show("Submitted Successfully");
                Clear();
                GridFill();
            }

        }

        void GridFill()
        {
            using (MySqlConnection mysqlCon = new MySqlConnection(connectionString))
            {
                mysqlCon.Open();
                MySqlDataAdapter sqlDa = new MySqlDataAdapter("BookViewAll", mysqlCon);
                sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
                DataTable dtblBook = new DataTable();
                sqlDa.Fill(dtblBook);
                dataGridView1.DataSource = dtblBook;
                dataGridView1.Columns[0].Visible = false;
            }
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            if(dataGridView1.CurrentRow.Index!=-1)
            {
                txtBookName.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                txtAuthor.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                txtDescription.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                bookID = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value.ToString());
                btnSave.Text = "Update";
                btnDelete.Enabled = Enabled;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            using (MySqlConnection mysqlCon = new MySqlConnection(connectionString))
            {
                mysqlCon.Open();
                MySqlDataAdapter sqlDa = new MySqlDataAdapter("BookSearchByValue", mysqlCon);
                sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
                sqlDa.SelectCommand.Parameters.AddWithValue("_SearchValue",txtSearch.Text);
                DataTable dtblBook = new DataTable();
                sqlDa.Fill(dtblBook);
                dataGridView1.DataSource = dtblBook;
                dataGridView1.Columns[0].Visible = false;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            using (MySqlConnection mysqlCon = new MySqlConnection(connectionString))
            {
                mysqlCon.Open();
                MySqlCommand mySqlCmd = new MySqlCommand("BookDeleteByID", mysqlCon);
                mySqlCmd.CommandType = CommandType.StoredProcedure;
                mySqlCmd.Parameters.AddWithValue("_BookID", bookID);
                mySqlCmd.ExecuteNonQuery();
                MessageBox.Show("Deleted Successfully");
                Clear();
                GridFill();
            }
        }


        void AutoComplete()
        {
            txtSearch.AutoCompleteMode = AutoCompleteMode.Suggest;
            txtSearch.AutoCompleteSource = AutoCompleteSource.CustomSource;
            AutoCompleteStringCollection obj = new AutoCompleteStringCollection();
            string constring = "datasource=localhost;port=3306;username=root;password=";
            string query = "select * from csharp.book";
            MySqlConnection conDataBase = new MySqlConnection(constring);
            MySqlCommand cmdDataBase = new MySqlCommand(query, conDataBase);
            MySqlDataReader myReader;
            try
            {
                conDataBase.Open();
                myReader = cmdDataBase.ExecuteReader();
                
                while(myReader.Read())
                {
                    string bName = myReader.GetString("BookName");
                    obj.Add(bName);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            txtSearch.AutoCompleteCustomSource = obj;
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            DataView DV = new DataView(dbdataset);
            DV.RowFilter = string.Format("BookName LIKE '%{0}%'", txtSearch.Text);
            dataGridView1.DataSource = DV;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        /*
private void button1_Click(object sender, EventArgs e)
{
  if (connection.State == ConnectionState.Open)
  {
      connection.Close();
      label4.Text = "Not Connected";
      label4.ForeColor = Color.Red;

  }
}

private void button2_Click(object sender, EventArgs e)
{
  if (connection.State == ConnectionState.Closed)
  {
      connection.Open();
      label4.Text = "Connected";
      label4.ForeColor = Color.Green;

  }
}
*/
    }
}
