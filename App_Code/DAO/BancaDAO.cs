using System;
using System.Collections;
using System.Data;
using Util;
using Dominio;
using Banco;
using System.Text;
using DIC;

namespace DAO
{
	public class BancaDAO
	{

        public BancaDAO()
		{
		}


        public static int IncluirBanca(BancaDOM bancaNova)
        {
            int reg = 0;
            if (! RegistroExiste(bancaNova.codEvento,bancaNova.codProfessor))
            {
                clsObjetosBanco objbanco = new clsObjetosBanco();

                StringBuilder sqlInsert = new StringBuilder();
                sqlInsert.Append("INSERT INTO " + BancaDIC.TABLE_BANCA);
                sqlInsert.Append(" ( ");
                sqlInsert.Append(BancaDIC.ObterColunasdaTabela());
                sqlInsert.Append(" ) ");
                sqlInsert.Append(" VALUES ");
                sqlInsert.Append(" ( ");
                sqlInsert.Append(bancaNova.codEvento.ToString()+", ");
                sqlInsert.Append(bancaNova.codProfessor.ToString());
                sqlInsert.Append(" ) ");
                reg = objbanco.IncluirRegistro(sqlInsert.ToString());
            }
            return reg;
        }


        public static string ExcluirBanca(BancaDOM bancaExcluir)
        {
            try
            {
                clsObjetosBanco objbanco = new clsObjetosBanco();
                StringBuilder sqlDelete = new StringBuilder();
                sqlDelete.Append("DELETE FROM " + BancaDIC.TABLE_BANCA);
                sqlDelete.Append(" WHERE " + EventoDIC.COL_COD_EVENTO + " = " + bancaExcluir.codEvento.ToString()+" and ");
                sqlDelete.Append( ProfessorDIC.COL_COD_PROFESSOR + " = " + bancaExcluir.codProfessor.ToString());
                objbanco.CriarObjetosBanco(sqlDelete.ToString());
                objbanco.command.ExecuteNonQuery();
                return string.Empty;
            }
            catch
            {
                return "Erro ao excluir a banca.";
            }
        }

        public static bool RegistroExiste(int codEvento, int codProfessor)
        {
            clsObjetosBanco objbanco = new clsObjetosBanco();
            StringBuilder sqlExiste = new StringBuilder();
            sqlExiste.Append("SELECT COUNT(*) FROM " + BancaDIC.TABLE_BANCA);
            sqlExiste.Append(" WHERE " + EventoDIC.COL_COD_EVENTO + " = " + codEvento.ToString()+" and ");
            sqlExiste.Append(ProfessorDIC.COL_COD_PROFESSOR + " = " + codProfessor.ToString());
            objbanco.CriarObjetosBanco(sqlExiste.ToString());
            int existente = Conversor.ConverterParaInteiro(objbanco.command.ExecuteScalar().ToString());
            return (existente > 0);

        }

        public static bool SalvarBanca(int codEvento, DataSet Banca)
        {
            try
            {
                 // percorre a tabela temporária e verifica os professores da banca que foram excluído e efetiva a exclusão
                DataSet dsDeletados = Banca.GetChanges(DataRowState.Deleted);
                if (dsDeletados != null)
                {
                    for (int i = 0; i <= dsDeletados.Tables[0].Rows.Count - 1; i++)
                    {
                        dsDeletados.Tables[0].Rows[i].RejectChanges();

                        BancaDOM exBanca = new BancaDOM();

                        exBanca.codEvento    = Conversor.ConverterParaInteiro(dsDeletados.Tables[0].Rows[i]["cod_evento"].ToString());
                        exBanca.codProfessor = Conversor.ConverterParaInteiro(dsDeletados.Tables[0].Rows[i]["cod_professor"].ToString());

                        BancaDAO.ExcluirBanca(exBanca);
                    }
                }


                DataSet dsIncluidos = Banca.GetChanges(DataRowState.Added);

                // percorre todas as linhas da tabela e grava os professores da banco relacionados ao evento
                if (dsIncluidos != null)
                {
                    for (int i = 0; i <= dsIncluidos.Tables[0].Rows.Count - 1; i++)
                    {
                        BancaDOM incBanca = new BancaDOM();
                        incBanca.codEvento = codEvento;
                        incBanca.codProfessor = Conversor.ConverterParaInteiro(dsIncluidos.Tables[0].Rows[i]["cod_professor"].ToString());
                        BancaDAO.IncluirBanca(incBanca);
                    }
                }
                return true;
            }

            catch(Exception ex) 
            {
                throw new ApplicationException("Erro ao gravar dados da banca. "+ex.Message.ToString());
                //return false;
            }
        }

		
	}
}