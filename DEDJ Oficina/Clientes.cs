using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DEDJ_Oficina
{
    public partial class Clientes : Form
    {
        public Clientes()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string nome = txtNome.Text;
            string telefone = txtTelefone.Text;
            string endereco = txtEndereco.Text;
            string modelo = txtModelo.Text;
            string placa = txtPlaca.Text;
            string cor = txtCor.Text;

            string connectionString = "Data Source=SETH;Initial Catalog=DEDJ Oficina;Integrated Security=True";

            using (SqlConnection Connection = new SqlConnection(connectionString))
            {
                Connection.Open();

                string query = "INSERT INTO dbo.Clientes (Nome, Telefone, Endereco) VALUES (@nome, @Telefone, @Endereco); " +
                               "INSERT INTO dbo.Veiculo (Modelo, Placa, Cor) VALUES (@Modelo, @Placa, @Cor);";

                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    command.Parameters.AddWithValue("@Nome", nome);
                    command.Parameters.AddWithValue("@Telefone", telefone);
                    command.Parameters.AddWithValue("@Endereco", endereco);
                    command.Parameters.AddWithValue("@Modelo", modelo);
                    command.Parameters.AddWithValue("@Placa", placa);
                    command.Parameters.AddWithValue("@Cor", cor);

                    command.ExecuteNonQuery();
                }
            }

            MessageBox.Show("Client Cadastrado com Sucesso!");

            txtNome.Clear();
            txtTelefone.Clear();
            txtEndereco.Clear();
            txtModelo.Clear();
            txtPlaca.Clear();
            txtCor.Clear();

        }
    }
}
