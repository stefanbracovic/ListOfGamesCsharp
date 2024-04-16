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


namespace ListaIgrica
{
    public partial class Form1 : Form
    {
        SqlConnection connection;
        static string imeZanr;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            populateGrid();
            populateFields();
            populateFields2();
            countFields();
        
        }



        public void populateFields()
        {
            string querry = "SELECT DISTINCT Zanr FROM InfoIgrica";
            using (connection = new SqlConnection(ConnectionHelp.connectionString))
            using (SqlDataAdapter adapter = new SqlDataAdapter(querry, connection))
            {

                try
                {
                    DataTable igriceTable = new DataTable();
                    adapter.Fill(igriceTable);


                    comboBox1.DisplayMember = "Zanr";
                    comboBox1.ValueMember = "Id";
                    comboBox1.DataSource = igriceTable;

                }
                catch
                {
                    MessageBox.Show("Vec postoji predstava pod tim parametrom");
                }


            }

        }

        public void populateFields2()
        {
            string querry = "SELECT DISTINCT Zanr FROM InfoIgrica";
            using (connection = new SqlConnection(ConnectionHelp.connectionString))
            using (SqlDataAdapter adapter = new SqlDataAdapter(querry, connection))
            {

                try
                {
                    DataTable igriceTable = new DataTable();
                    adapter.Fill(igriceTable);

                    comboBox2.DisplayMember = "Zanr";
                    comboBox2.ValueMember = "Id";
                    comboBox2.DataSource = igriceTable;
                }
                catch
                {
                    MessageBox.Show("Vec postoji predstava pod tim parametrom");
                }


            }

        }

        public void countFields()
        {
            string querry = "SELECT COUNT(Id) as broj FROM InfoIgrica";
            using (connection = new SqlConnection(ConnectionHelp.connectionString))
            using (SqlDataAdapter adapter = new SqlDataAdapter(querry, connection))
            {

                try
                {
                    DataTable igriceTable = new DataTable();
                    adapter.Fill(igriceTable);

                    textBox8.Text = igriceTable.Rows[0]["broj"].ToString();


                }
                catch
                {
                    MessageBox.Show("Vec postoji predstava pod tim parametrom");
                }


            }

        }

        public void countFieldsZanr()
        {
            imeZanr = comboBox2.Text;
           

            string querry = "SELECT COUNT(Id) as broj  FROM InfoIgrica WHERE InfoIgrica.Zanr='" +imeZanr+"'";
            using (connection = new SqlConnection(ConnectionHelp.connectionString))
            using (SqlDataAdapter adapter = new SqlDataAdapter(querry, connection))
         
            {

                try
                {
                    DataTable igriceTable = new DataTable();
                    adapter.Fill(igriceTable);

                  

                    textBox7.Text = igriceTable.Rows[0]["broj"].ToString();


                }
                catch
                {
                    MessageBox.Show("Vec postoji igrica pod tim parametrom");
                }


            }

        }

        public void AverageFields()
        {
            string querry = "SELECT AVG(cena) as prosek FROM InfoIgrica WHERE InfoIgrica.Zanr='" + imeZanr + "'";
            using (connection = new SqlConnection(ConnectionHelp.connectionString))
            using (SqlDataAdapter adapter = new SqlDataAdapter(querry, connection))
            {

                try
                {
                    DataTable igriceTable = new DataTable();
                    adapter.Fill(igriceTable);

                    textBox6.Text = igriceTable.Rows[0]["prosek"].ToString();


                }
                catch
                {
                    MessageBox.Show("Vec postoji predstava pod tim parametrom");
                }


            }

        }

        public void MaxFields()
        {
            string querry = "SELECT MAX(cena) as maksimum FROM InfoIgrica WHERE InfoIgrica.Zanr='" + imeZanr + "'";
            using (connection = new SqlConnection(ConnectionHelp.connectionString))
            using (SqlDataAdapter adapter = new SqlDataAdapter(querry, connection))
            {

                try
                {
                    DataTable igriceTable = new DataTable();
                    adapter.Fill(igriceTable);

                    textBox5.Text = igriceTable.Rows[0]["maksimum"].ToString();


                }
                catch
                {
                    MessageBox.Show("Vec postoji igrica pod tim parametrom");
                }


            }

        }


        public void populateGrid()
        {
            string querry = "SELECT * FROM InfoIgrica";


            using (connection = new SqlConnection(ConnectionHelp.connectionString))
            using (SqlCommand command = new SqlCommand(querry, connection))
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {



                DataTable igriceTable = new DataTable();
                adapter.Fill(igriceTable);

                dataGridView1.DataSource = igriceTable;

            }

        }


        public void insertElement()
        {
            string querry = "Insert into InfoIgrica (Id, Naziv, Zanr,GodinaIzdavanja,Cena) Values(@sif,@naziv,@zanr,@godina,@cena)";
            using (connection = new SqlConnection(ConnectionHelp.connectionString))
            using (SqlCommand command = new SqlCommand(querry, connection))
            {
                connection.Open();

                try
                {



                    command.Parameters.AddWithValue("@sif", Convert.ToInt32(textBox1.Text));
                    command.Parameters.AddWithValue("@naziv", textBox2.Text);
                    command.Parameters.AddWithValue("@zanr", comboBox1.Text);
                    command.Parameters.AddWithValue("@godina", textBox3.Text);
                    command.Parameters.AddWithValue("@cena", textBox4.Text);




                    command.ExecuteScalar();
                    MessageBox.Show("Uspesan unos ");
                }
                catch
                {

                    MessageBox.Show("Vec postoji igrica sa takvim Id ili proverite da ste lepo popunili polja");
                }

            }
        }


        public void deleteElement()
        {
            string querry = "DELETE FROM InfoIgrica WHERE InfoIgrica.Id=@sif";
            using (connection = new SqlConnection(ConnectionHelp.connectionString))
            using (SqlCommand command = new SqlCommand(querry, connection))
            {
                connection.Open();

                try
                {
                    command.Parameters.AddWithValue("@sif",textBox1.Text);

                    command.ExecuteScalar();
                    MessageBox.Show("Uspesno brisanje");
                }
                catch
                {
                    MessageBox.Show("Ne postoji igrica sa takvim Id ili proverite da ste lepo popunili polje Id");
                }

            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            insertElement();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            deleteElement();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

            countFieldsZanr();
            AverageFields();
            MaxFields();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
        }
    }
}
