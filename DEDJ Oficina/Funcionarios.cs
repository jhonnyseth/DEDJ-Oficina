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
    public partial class Funcionarios : Form
    {
        private string connectionString = "Data Source=SETH;Initial Catalog=DEDJ Oficina;Integrated Security=True"; // Substitua pelos dados da sua conexão

        public Funcionarios()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            AtualizarDataGridViewFuncionarios();
        }

        private void btnCriar_Click(object sender, EventArgs e)
        {
            string nome = txtNome.Text;
            string senha = txtSenha.Text;

            if (CriarFuncionarios(nome, senha))
            {
                MessageBox.Show("Funcionario Cadastrado com Sucesso!");
                LimparCampos();
                AtualizarDataGridViewFuncionarios();
            }
            else
            {
                MessageBox.Show("Erro ao criar o cadastro.");
            }
        }

        private bool CriarFuncionarios(string nome, string senha)
        {
            string query = "INSERT INTO dbo.Funcionarios (Nome, Senha) VALUES (@Nome, @Senha)"; // Substitua pelos dados da sua tabela

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Nome", nome);
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
            if (dgvFuncionarios.SelectedRows.Count > 0)
            {
                int idFuncionario = Convert.ToInt32(dgvFuncionarios.SelectedRows[0].Cells["FuncionarioID"].Value);

                if (ExcluirFuncionarios(idFuncionario))
                {
                    MessageBox.Show("Funcionario excluído com sucesso!");
                    AtualizarDataGridViewFuncionarios();
                }
                else
                {
                    MessageBox.Show("Erro ao excluir o Funcionario.");
                }
            }
            else
            {
                MessageBox.Show("Selecione um Funcionario para excluir.");
            }
        }

        private bool ExcluirFuncionarios(int idFuncionario)
        {
            string query = "DELETE FROM dbo.Funcionarios WHERE FuncionarioID = @FuncionarioID"; // Substitua pelos dados da sua tabela

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@FuncionarioID", idFuncionario);

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
            txtSenha.Text = "";
        }

        private void AtualizarDataGridViewFuncionarios()
        {
            string query = "SELECT FuncionarioID, Nome, Senha FROM dbo.Funcionarios"; // Substitua pelos dados da sua tabela

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable dataTable = new DataTable();

                try
                {
                    adapter.Fill(dataTable);
                    dgvFuncionarios.DataSource = dataTable;
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