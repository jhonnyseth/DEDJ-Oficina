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
    public partial class Form1 : Form
    {
        private string connectionString = "Data Source=SETH;Initial Catalog=DEDJ Oficina;Integrated Security=True"; // Substitua pelos dados da sua conexão

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            AtualizarDataGridViewClientes();
        }

        private void btnCriar_Click(object sender, EventArgs e)
        {
            string nome = txtNome.Text;
            string email = txtEmail.Text;
            string senha = txtSenha.Text;

            if (CriarCliente(nome, email, senha))
            {
                MessageBox.Show("Cliente criado com sucesso!");
                LimparCampos();
                AtualizarDataGridViewClientes();
            }
            else
            {
                MessageBox.Show("Erro ao criar o cliente.");
            }
        }

        private bool CriarCliente(string nome, string email, string senha)
        {
            string query = "INSERT INTO dbo.Cadastro (Nome, Email, Senha) VALUES (@Nome, @Email, @Senha)"; // Substitua pelos dados da sua tabela

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Nome", nome);
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@Senha", senha);

                try
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro: " + ex.Message);
                    return false;
                }
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (dgvClientes.SelectedRows.Count > 0)
            {
                int idCliente = Convert.ToInt32(dgvClientes.SelectedRows[0].Cells["ClienteID"].Value);

                if (ExcluirCliente(idCliente))
                {
                    MessageBox.Show("Cliente excluído com sucesso!");
                    AtualizarDataGridViewClientes();
                }
                else
                {
                    MessageBox.Show("Erro ao excluir o cliente.");
                }
            }
            else
            {
                MessageBox.Show("Selecione um cliente para excluir.");
            }
        }

        private bool ExcluirCliente(int idCliente)
        {
            string query = "DELETE FROM dbo.Cadastro WHERE ClienteID = @ClienteID"; // Substitua pelos dados da sua tabela

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ClienteID", idCliente);

                try
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro: " + ex.Message);
                    return false;
                }
            }
        }

        private void LimparCampos()
        {
            txtNome.Text = "";
            txtEmail.Text = "";
            txtSenha.Text = "";
        }

        private void AtualizarDataGridViewClientes()
        {
            string query = "SELECT ClienteID, Nome, Email, Senha FROM dbo.Cadastro"; // Substitua pelos dados da sua tabela

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable dataTable = new DataTable();

                try
                {
                    adapter.Fill(dataTable);
                    dgvClientes.DataSource = dataTable;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro: " + ex.Message);
                }
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}