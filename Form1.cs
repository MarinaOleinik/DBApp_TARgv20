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
            pictureBox1.Image = Image.FromFile("../../Images/about.png");
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
                pictureBox1.Image = Image.FromFile("../../Images/about.png");
            }
            else
            {
                filebox.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                pictureBox1.Image = Image.FromFile("../../Images/" + dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString());
            }

        }
        private void ClearData()
        {
            toodebox.Clear();
            kogusbox.Clear();
            hindbox.Clear();
            filebox.Clear();
            pictureBox1.Image = Image.FromFile("../../Images/about.png");
        }
        private void addbtn_Click(object sender, EventArgs e)
        {
            if (toodebox.Text!="" && kogusbox.Text!="" && hindbox.Text!="" )
            {
                try
                {
                    con.Open();
                    command = new SqlCommand("INSERT INTO Tooded(Nimetus, Kogus, Hind, Pilt) VALUES(@toode,@kogus,@hind,@pilt)", con);
                    command.Parameters.AddWithValue("@toode", toodebox.Text);
                    command.Parameters.AddWithValue("@kogus", kogusbox.Text);
                    command.Parameters.AddWithValue("@hind", hindbox.Text);
                    //string file = "file.jpg";
                    command.Parameters.AddWithValue("@pilt", filebox.Text);
                    command.ExecuteNonQuery();
                    con.Close();
                    Data();
                    ClearData();
                    MessageBox.Show("Andmed on lisatud");
                }
                catch (Exception)
                {
                    MessageBox.Show("Andmebaasiga on viga!");
                }
            }
            else
            {
                MessageBox.Show("Andmed vaja sisestada!");
            }
            
        }
        private void updatebtn_Click(object sender, EventArgs e)
        {
            if (toodebox.Text != "" && kogusbox.Text != "" && hindbox.Text != "")
            {
                try
                {
                    con.Open();
                    command = new SqlCommand("UPDATE Tooded SET Nimetus=@toode,Kogus=@kogus,Hind=@hind,Pilt=@pilt WHERE Id=@id", con);
                    command.Parameters.AddWithValue("@id", Id);
                    command.Parameters.AddWithValue("@toode", toodebox.Text);
                    command.Parameters.AddWithValue("@kogus", kogusbox.Text);
                    command.Parameters.AddWithValue("@hind", hindbox.Text.Replace(",","."));
                    //string file = "file.jpg";
                    //command.Parameters.AddWithValue("@pilt", filebox.Text);
                    command.ExecuteNonQuery();
                    con.Close();
                    Data();
                    ClearData();
                    MessageBox.Show("Uuendatud");
                }
                catch (Exception)
                {
                    MessageBox.Show("Andmebaasiga on viga!");
                }
            }
            else
            {
                MessageBox.Show("Andmed on vaja valida!");
            }

        }

        private void delbtn_Click(object sender, EventArgs e)
        {
            if (Id!=0)
            {
                string andmed ="Andmed "+ toodebox.Text+" on kustutatud";
                command = new SqlCommand("DELETE Tooded WHERE Id=@id",con);
                con.Open();
                command.Parameters.AddWithValue("@id", Id);
                command.ExecuteNonQuery();
                con.Close();
                Data();
                ClearData();   
                MessageBox.Show(andmed);
            }

        }
    }
}
