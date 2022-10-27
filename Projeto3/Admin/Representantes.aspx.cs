using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Projeto3
{
   public partial class Representantes : System.Web.UI.Page
   {
      // PUBLICA PARA TODOS OS MÉTODOS DESTA CLASSE
      Model.Representantes representante = new Model.Representantes();

      // INSTANCIA DA CLASSE DE ACESSO AO BANCO DE DADOS
      // DAO = Data Access Object (acesso ao banco de dados)
      DataServices.DataBase.DAO db = new DataServices.DataBase.DAO();
      protected void Page_Load(object sender, EventArgs e)
      {
            if(!IsPostBack)LoadGrid();
      }

      /// <summary>
      /// CARREGA O GRID COM OS DADOS DO BANCO DE DADOS
      /// </summary>
      protected void LoadGrid()
      {
         string comando = "SELECT RepresentanteId,Nome,NomeAcesso FROM Representantes ORDER BY Nome;";
         db.ConnectionString = App_Code.AppSettings.conectionDB();
         db.DataProviderName = DataServices.DataBase.DAO.ProviderName.OleDb;
         DataTable tb = (DataTable)db.Query(comando);

         ExibirRepresentantes.DataSource = tb;
         ExibirRepresentantes.DataBind();
         tb.Dispose();
      }

      /// <summary>
      /// LIMPA OS CONTROLES DO FORMULÁRIO
      /// </summary>
      protected void LimparControles()
      {
         RepresentanteId.Text = "";
         Nome.Text = "";
         NomeAcesso.Text = "";
         Senha.Text = "";
         Email.Text = "";
         Salvar.Text = "Inserir";
         Excluir.Visible = false;
      }

      protected void Salvar_Click(object sender, EventArgs e)
      {
         //1. validar as entradas
         if (Nome.Text.Trim() == "")
         {
            Mensagem.Text = "Digite seu nome";
         }
         else if (Email.Text.Trim() == "")
         {
            Mensagem.Text = "Digite seu e-mail";
         }
         else if (NomeAcesso.Text.Trim() == "")
         {
            Mensagem.Text = "Digite seu nome de acesso";
         }
         else if (Senha.Text.Trim() == "")
         {
            Mensagem.Text = "Digite a senha de acesso";
         }
         else if (PossoGravar(NomeAcesso.Text, RepresentanteId.Text) == false)
         {
            Mensagem.Text = "Este nome de acesso ja existe para outro usuário";
         }
         else
         {
            if (RepresentanteId.Text == "")
            {
               // INSERIR UM NOVO REGISTRO
               representante.Nome = Nome.Text;
               representante.Email = Email.Text;
               representante.NomeAcesso = NomeAcesso.Text;
               representante.Senha = Senha.Text;

               // 1. definir a conexão
               db.ConnectionString = App_Code.AppSettings.conectionDB();
               // 2. definir o banco de dados
               db.DataProviderName = DataServices.DataBase.DAO.ProviderName.OleDb;
               // 3. Insere o registro na tabela do banco de dados
               db.Insert(representante, "RepresentanteId");

               // SQL PARA INSERIR ESTE REGISTRO
               //string comando = "INSERT INTO Representantes(Nome,Email,NomeAcesso,Senha) VALUES ('" + Nome.Text + "','" + Email.Text + "','" + NomeAcesso.Text + "','" + Senha.Text + "');";
               // Executa o comando
               //db.Query(comando);
            }
            else
            {
               // EDITAR O REGISTRO 
               // EDITA O REGISTRO SELECIONADO
               representante.Nome = Nome.Text;
               representante.Email = Email.Text;
               representante.NomeAcesso = NomeAcesso.Text;
               representante.Senha = Senha.Text;
               // Insere o registro na tabela do banco de dados
               db.Update(representante, "RepresentanteId", RepresentanteId.Text);

               // EXEMPLO DO COMANDO SQL PARA EDITAR ESTE REGISTRO
               //string comando = "UPDATE Representantes SET Nome='" + Nome.Text + "',Email='" + Email.Text + "',NomeAcesso='" + NomeAcesso.Text + "',Senha='" + Senha.Text + "' WHERE RepresentanteId=" + RepresentanteId.Text;

               //db.Query(comando);
            }
            LimparControles();
            LoadGrid();
         }
      }

      /// <summary>
      /// Exclui um representante da tabela do banco de dados
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      protected void Excluir_Click(object sender, EventArgs e)
      {
         string comando = "DELETE FROM Representantes WHERE RepresentanteId=" + RepresentanteId.Text;
         db.ConnectionString = App_Code.AppSettings.conectionDB();
         db.DataProviderName = DataServices.DataBase.DAO.ProviderName.OleDb;
         db.Query(comando);
         LimparControles();
         LoadGrid();
      }

      /// <summary>
      /// Define se o nome de acesso pode ser ou não gravado para o usuário
      /// </summary>
      /// <param name="nomeAcesso">Nome de acesso</param>
      /// <param name="id">RepresentanteId</param>
      /// <returns></returns>
      protected bool PossoGravar(string nomeAcesso, string id)
      {
         string comando = "SELECT * FROM Representantes WHERE NomeAcesso='" + nomeAcesso + "';";
         db.ConnectionString = App_Code.AppSettings.conectionDB();
         db.DataProviderName = DataServices.DataBase.DAO.ProviderName.OleDb;

         DataTable tb = (DataTable)db.Query(comando);
         if (tb.Rows.Count == 0)
         {
            return true;
         }
         else
         {
            if (tb.Rows[0]["RepresentanteId"].ToString() == id)
            {
               return true;
            }
            else
            {
               return false;
            }
         }
      }

      protected void ExibirRepresentantes_SelectedIndexChanged(object sender, EventArgs e)
      {
         RepresentanteId.Text = ExibirRepresentantes.SelectedRow.Cells[1].Text;
         
         string comando = "SELECT * FROM Representantes WHERE RepresentanteId=" + RepresentanteId.Text;
         db.ConnectionString = App_Code.AppSettings.conectionDB();
         db.DataProviderName = DataServices.DataBase.DAO.ProviderName.OleDb;
         DataTable tb = (DataTable)db.Query(comando);

         Nome.Text = tb.Rows[0]["Nome"].ToString();
         Email.Text = tb.Rows[0]["Email"].ToString();
         NomeAcesso.Text = tb.Rows[0]["NomeAcesso"].ToString();
         Senha.Text = tb.Rows[0]["Senha"].ToString();

         Salvar.Text = "Editar";
         Excluir.Visible = true;
      }
   }
}