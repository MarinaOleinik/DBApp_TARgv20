using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DBApp_TARgv20
{
    public partial class Form1 : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename=|DataDirectory|\AppData\Database.mdf;Integrated Security = True");
        SqlDataAdapter adapter;
        SqlCommand command;
        public Form1()
        {
            InitializeComponent();
            Data();
        }
        private void Data()
        {
            con.Open();
            DataTable table = new DataTable();
            adapter = new SqlDataAdapter("SELECT * FROM Tooded", con);
            adapter.Fill(table);
            dataGridView1.DataSource = table;
            con.Close();
        }
        int Id = 0;
        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            Id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
            toodebox.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            kogusbox.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            hindbox.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            
            if (dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString()=="")
            {
                filebox.Text = "none";
            }
            else
            {
                filebox.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            }

        }

        private void addbtn_Click(object sender, EventArgs e)
        {

            con.Open();
            command = new SqlCommand("INSERT INTO Tooded(Nimetus, Kogus, Hind, Pilt) VALUES(@toode,@kogus,@hind,@pilt)",con);
            command.Parameters.AddWithValue("@toode", toodebox.Text);
            command.Parameters.AddWithValue("@kogus", kogusbox.Text);
            command.Parameters.AddWithValue("@hind", hindbox.Text);
            //string file = "file.jpg";
            command.Parameters.AddWithValue("@pilt", filebox.Text);
            command.ExecuteNonQuery();
            con.Close();
            Data();
        }
    }
}
