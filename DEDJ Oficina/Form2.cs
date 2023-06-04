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

namespace DEDJ_Oficina
{
    public partial class Form2 : Form
    {
        private string connectionString = "Data Source=SETH;Initial Catalog=DEDJ Oficina;Integrated Security=True"; // Substitua pelos dados da sua conexão

        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string nome = txtUser.Text;
            string senha = txtPass.Text;

            if (VerificarCredenciais(nome, senha))
            {
                DialogResult = DialogResult.OK;

                MessageBox.Show("Login bem-sucedido!");
                // Faça aqui a navegação para a próxima tela, se necessário
            }
            else
            {
                MessageBox.Show("Usuário ou senha incorretos.");
            }
        }

        private bool VerificarCredenciais(string nome, string senha)
        {
            string query = "SELECT COUNT(*) FROM dbo.Cadastro WHERE Nome = @Nome AND Senha = @Senha"; // Substitua pelos dados da sua tabela

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Nome", nome);
                command.Parameters.AddWithValue("@Senha", senha);

                try
                {
                    connection.Open();
                    int count = Convert.ToInt32(command.ExecuteScalar());

                    return count > 0;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro: " + ex.Message);
                    return false;
                }
            }
        }
    }
}
