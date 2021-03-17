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
	public class OrientadorDAO
	{

        public OrientadorDAO()
		{
		}


        public static int IncluirOrientador(OrientadorDOM orientadorNovo)
        {
            int reg = 0;
            if (!RegistroExiste(orientadorNovo.codEvento, orientadorNovo.codProfessor))
            {
                clsObjetosBanco objbanco = new clsObjetosBanco();

                StringBuilder sqlInsert = new StringBuilder();
                sqlInsert.Append("INSERT INTO " + OrientadorDIC.TABLE_ORIENTADOR);
                sqlInsert.Append(" ( ");
                sqlInsert.Append(OrientadorDIC.ObterColunasdaTabela());
                sqlInsert.Append(" ) ");
                sqlInsert.Append(" VALUES ");
                sqlInsert.Append(" ( ");
                sqlInsert.Append(orientadorNovo.codEvento.ToString()+", ");
                sqlInsert.Append(orientadorNovo.codProfessor.ToString());
                sqlInsert.Append(" ) ");
                reg = objbanco.IncluirRegistro(sqlInsert.ToString());
            }
            return reg;

        }


        public static string ExcluirOrientador(OrientadorDOM orientadorExcluir)
        {
            try
            {
                clsObjetosBanco objbanco = new clsObjetosBanco();
                StringBuilder sqlDelete = new StringBuilder();
                sqlDelete.Append("DELETE FROM " + OrientadorDIC.TABLE_ORIENTADOR);
                sqlDelete.Append(" WHERE " + ProfessorDIC.COL_COD_PROFESSOR + " = " + orientadorExcluir.codProfessor.ToString() + " and ");
                sqlDelete.Append(EventoDIC.COL_COD_EVENTO + " = " + orientadorExcluir.codEvento.ToString());

                objbanco.CriarObjetosBanco(sqlDelete.ToString());
                objbanco.command.ExecuteNonQuery();
                return string.Empty;
            }
            catch
            {
                return "Erro ao excluir o orientador.";
            }
        }

        public static bool RegistroExiste(int codEvento, int codProfessor)
        {
            clsObjetosBanco objbanco = new clsObjetosBanco();
            StringBuilder sqlExiste = new StringBuilder();
            sqlExiste.Append("SELECT COUNT(*) FROM " + OrientadorDIC.TABLE_ORIENTADOR);
            sqlExiste.Append(" WHERE " + ProfessorDIC.COL_COD_PROFESSOR + " = " + codProfessor.ToString()+" and ");
            sqlExiste.Append(EventoDIC.COL_COD_EVENTO + " = " + codEvento.ToString());
            objbanco.CriarObjetosBanco(sqlExiste.ToString());
            int existente = Conversor.ConverterParaInteiro(objbanco.command.ExecuteScalar().ToString());
            return (existente > 0);
        }

        public static bool SalvarOrientador(int codEvento, DataSet Orientador)
        {
            try
            {
                 // percorre a tabela temporária e verifica os orientadores que foram excluído, e efetiva a exclusão
                DataSet dsDeletados = Orientador.GetChanges(DataRowState.Deleted);
                if (dsDeletados != null)
                {
                    for (int i = 0; i <= dsDeletados.Tables[0].Rows.Count - 1; i++)
                    {
                        dsDeletados.Tables[0].Rows[i].RejectChanges();

                        OrientadorDOM exOrientador = new OrientadorDOM();

                        exOrientador.codEvento = Conversor.ConverterParaInteiro(dsDeletados.Tables[0].Rows[i]["cod_evento"].ToString());
                        exOrientador.codProfessor = Conversor.ConverterParaInteiro(dsDeletados.Tables[0].Rows[i]["cod_professor"].ToString());

                        OrientadorDAO.ExcluirOrientador(exOrientador);
                    }
                }


                DataSet dsIncluidos = Orientador.GetChanges(DataRowState.Added);

                // percorre todas as linhas da tabela e grava os professores da banco relacionados ao evento
                if (dsIncluidos != null)
                {
                    for (int i = 0; i <= dsIncluidos.Tables[0].Rows.Count - 1; i++)
                    {
                        OrientadorDOM incOrientador = new OrientadorDOM();
                        incOrientador.codEvento = codEvento;
                        incOrientador.codProfessor = Conversor.ConverterParaInteiro(dsIncluidos.Tables[0].Rows[i]["cod_professor"].ToString());
                        OrientadorDAO.IncluirOrientador(incOrientador);
                    }
                }
                return true;
            }

            catch(Exception ex) 
            {
                throw new ApplicationException("Erro ao gravar dados do orientador. "+ex.Message.ToString());
                return false;
            }
        }

		
	}
}