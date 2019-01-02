using Projeto.Util;
using Projeto.View.Consulta;
using Projeto.View.Paciente;
using Projeto.View.Problema;
using Projeto.View.Solucao;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projeto.View.Enfermeiro
{
    public partial class MenuEnfermeiro : Form
    {
        public MenuEnfermeiro()
        {
            InitializeComponent();
            if (!Session.stt)
            {
                Cadastrar_enfermeiro.Visible = false;
                Cadastrar_solucao.Visible = false;

                Consultar_enfermeiro.Visible = false;

                Excluir_enfermeiro.Visible = false;
                Excluir_problema.Visible = false;
                Excluir_solucao.Visible = false;

                Atualizar_enfermeiro.Visible = false;
                Atualizar_solucao.Visible = false;
                Atualizar_problema.Visible = false;
            }
        }

        private void Cadastar_consulta_click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<RealizacaoConsulta>().Count() <= 0)
            {
                RealizacaoConsulta form = new RealizacaoConsulta();
                form.Show();
            }
            else
            {
                MessageBox.Show("Formulário já está aberto!");
            }
        }

        private void Cadastrar_enfermeiro_click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<CadastroEnfermeiro>().Count() <= 0)
            {
                CadastroEnfermeiro form = new CadastroEnfermeiro();
                form.Show();
            }
            else
            {
                MessageBox.Show("Formulário já está aberto!");
            }
        }

        private void Cadastrar_paciente_click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<CadastroPaciente>().Count() <= 0)
            {
                CadastroPaciente form = new CadastroPaciente();
                form.Show();
            }
            else
            {
                MessageBox.Show("Formulário já está aberto!");
            }
        }

        private void Cadastrar_solucao_click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<CadastroSolucao>().Count() <= 0)
            {
                CadastroSolucao form = new CadastroSolucao();
                form.Show();
            }
            else
            {
                MessageBox.Show("Formulário já está aberto!");
            }
        }

        private void Consultar_enfermeiro_click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<ConsultaEnfermeiro>().Count() <= 0)
            {
                ConsultaEnfermeiro form = new ConsultaEnfermeiro();
                form.Show();
            }
            else
            {
                MessageBox.Show("Formulário já está aberto!");
            }
        }

        private void Consultar_paciente_click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<ConsultaPaciente>().Count() <= 0)
            {
                ConsultaPaciente form = new ConsultaPaciente();
                form.Show();
            }
            else
            {
                MessageBox.Show("Formulário já está aberto!");
            }
        }

        private void Consultar_problema_click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<ConsultaProblema>().Count() <= 0)
            {
                ConsultaProblema form = new ConsultaProblema();
                form.Show();
            }
            else
            {
                MessageBox.Show("Formulário já está aberto!");
            }
        }

        private void Consultar_solucao_click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<ConsultaSolucao>().Count() <= 0)
            {
                ConsultaSolucao form = new ConsultaSolucao();
                form.Show();
            }
            else
            {
                MessageBox.Show("Formulário já está aberto!");
            }
        }

        private void Excluir_enfermeiro_click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<Enfermeiro>().Count() <= 0)
            {
                Enfermeiro form = new Enfermeiro();
                form.Show();
            }
            else
            {
                MessageBox.Show("Formulário já está aberto!");
            }
        }

        private void Excluir_paciente_click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<ExclusaoPaciente>().Count() <= 0)
            {
                ExclusaoPaciente form = new ExclusaoPaciente();
                form.Show();
            }
            else
            {
                MessageBox.Show("Formulário já está aberto!");
            }
        }

        private void Excluir_solucao_click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<ExclusaoSolucao>().Count() <= 0)
            {
                ExclusaoSolucao form = new ExclusaoSolucao();
                form.Show();
            }
            else
            {
                MessageBox.Show("Formulário já está aberto!");
            }
        }

        private void Excluir_problema_click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<ExclusaoProblema>().Count() <= 0)
            {
                ExclusaoProblema form = new ExclusaoProblema();
                form.Show();
            }
            else
            {
                MessageBox.Show("Formulário já está aberto!");
            }
        }

        private void Atualizar_enfermeiro_click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<AtualizacaoEnfermeiro>().Count() <= 0)
            {
                AtualizacaoEnfermeiro form = new AtualizacaoEnfermeiro();
                form.Show();
            }
            else
            {
                MessageBox.Show("Formulário já está aberto!");
            }
        }

        private void Atualizar_paciente_click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<AtualizacaoPaciente>().Count() <= 0)
            {
                AtualizacaoPaciente form = new AtualizacaoPaciente();
                form.Show();
            }
            else
            {
                MessageBox.Show("Formulário já está aberto!");
            }
        }

        private void Atualizar_problema_click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<AtualizacaoProblema>().Count() <= 0)
            {
                AtualizacaoProblema form = new AtualizacaoProblema();
                form.Show();
            }
            else
            {
                MessageBox.Show("Formulário já está aberto!");
            }
        }

        private void Atualizar_solucao_click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<AtualizacaoSolucao>().Count() <= 0)
            {
                AtualizacaoSolucao form = new AtualizacaoSolucao();
                form.Show();
            }
            else
            {
                MessageBox.Show("Formulário já está aberto!");
            }
        }

        private void Atualizar_minhasenha_click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<SenhaEnfermeiro>().Count() <= 0)
            {
                SenhaEnfermeiro form = new SenhaEnfermeiro();
                form.Show();
            }
            else
            {
                MessageBox.Show("Formulário já está aberto!");
            }
        }

        private void Sair_fechar_Click(object sender, EventArgs e)
        {
            Sair.sair(this);
        }

        private void Sair_login_Click(object sender, EventArgs e)
        {
            LoginEnfermeiro form = new LoginEnfermeiro();
            form.Show();
            Close();
        }
    }
}