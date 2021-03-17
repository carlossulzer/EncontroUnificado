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
	public class IntegranteDAO
	{

        public IntegranteDAO()
		{
		}


        public static int IncluirIntegrante(IntegranteDOM integranterNovo)
        {
            int reg = 0;
            if (!RegistroExiste(integranterNovo.matricula, integranterNovo.codEnsino, integranterNovo.codEvento))
            {
                clsObjetosBanco objbanco = new clsObjetosBanco();

                StringBuilder sqlInsert = new StringBuilder();
                sqlInsert.Append("INSERT INTO " + IntegranteDIC.TABLE_INTEGRANTE);
                sqlInsert.Append(" ( ");
                sqlInsert.Append(IntegranteDIC.ObterColunasdaTabela());
                sqlInsert.Append(" ) ");
                sqlInsert.Append(" VALUES ");
                sqlInsert.Append(" ( ");
                sqlInsert.Append(integranterNovo.matricula.ToString()+", ");
                sqlInsert.Append(integranterNovo.codEnsino.ToString()+", ");
                sqlInsert.Append(integranterNovo.codEvento.ToString());
                sqlInsert.Append(" ) ");
                reg = objbanco.IncluirRegistro(sqlInsert.ToString());
            }
            return reg;

        }


        public static string ExcluirIntegrante(IntegranteDOM integranteExcluir)
        {
            try
            {
                clsObjetosBanco objbanco = new clsObjetosBanco();
                StringBuilder sqlDelete = new StringBuilder();
                sqlDelete.Append("DELETE FROM " + IntegranteDIC.TABLE_INTEGRANTE);
                sqlDelete.Append(" WHERE " + IntegranteDIC.COL_MATRICULA + " = " + integranteExcluir.matricula.ToString() + " and ");
                sqlDelete.Append(IntegranteDIC.COL_COD_ENSINO + " = " + integranteExcluir.codEnsino.ToString() + " and ");
                sqlDelete.Append(EventoDIC.COL_COD_EVENTO + " = " + integranteExcluir.codEvento.ToString());

                objbanco.CriarObjetosBanco(sqlDelete.ToString());
                objbanco.command.ExecuteNonQuery();
                return string.Empty;
            }
            catch
            {
                return "Erro ao excluir o integrante.";
            }
        }

        public static bool RegistroExiste(int matricula, int codEnsino, int codEvento )
        {
            clsObjetosBanco objbanco = new clsObjetosBanco();
            StringBuilder sqlExiste = new StringBuilder();
            sqlExiste.Append("SELECT COUNT(*) FROM " + IntegranteDIC.TABLE_INTEGRANTE);

            sqlExiste.Append(" WHERE " + IntegranteDIC.COL_MATRICULA + " = " + matricula.ToString() + " and ");
            sqlExiste.Append(IntegranteDIC.COL_COD_ENSINO + " = " + codEnsino.ToString() + " and ");
            sqlExiste.Append(EventoDIC.COL_COD_EVENTO + " = " + codEvento.ToString());

            objbanco.CriarObjetosBanco(sqlExiste.ToString());
            int existente = Conversor.ConverterParaInteiro(objbanco.command.ExecuteScalar().ToString());
            return (existente > 0);
        }

        public static bool SalvarIntegrante(int codEvento, DataSet Integrante)
        {
            try
            {
                 // percorre a tabela temporária e verifica os integrante que foram excluído, e efetiva a exclusão
                DataSet dsDeletados = Integrante.GetChanges(DataRowState.Deleted);
                if (dsDeletados != null)
                {
                    for (int i = 0; i <= dsDeletados.Tables[0].Rows.Count - 1; i++)
                    {
                        dsDeletados.Tables[0].Rows[i].RejectChanges();

                        IntegranteDOM exIntegrante = new IntegranteDOM();

                        exIntegrante.matricula = Conversor.ConverterParaInteiro(dsDeletados.Tables[0].Rows[i]["matricula"].ToString());
                        exIntegrante.codEnsino = Conversor.ConverterParaInteiro(dsDeletados.Tables[0].Rows[i]["cod_ensino"].ToString());
                        exIntegrante.codEvento = Conversor.ConverterParaInteiro(dsDeletados.Tables[0].Rows[i]["cod_evento"].ToString());

                        IntegranteDAO.ExcluirIntegrante(exIntegrante);
                    }
                }


                DataSet dsIncluidos = Integrante.GetChanges(DataRowState.Added);

                // percorre todas as linhas da tabela e grava os professores da banco relacionados ao evento
                if (dsIncluidos != null)
                {
                    for (int i = 0; i <= dsIncluidos.Tables[0].Rows.Count - 1; i++)
                    {
                        IntegranteDOM incIntegrante = new IntegranteDOM();
                        incIntegrante.codEvento = codEvento;
                        incIntegrante.matricula = Conversor.ConverterParaInteiro(dsIncluidos.Tables[0].Rows[i]["matricula"].ToString());
                        incIntegrante.codEnsino = Conversor.ConverterParaInteiro(dsIncluidos.Tables[0].Rows[i]["cod_ensino"].ToString());
                        IntegranteDAO.IncluirIntegrante(incIntegrante);
                    }
                }
                return true;
            }

            catch(Exception ex) 
            {
                throw new ApplicationException("Erro ao gravar dados do integrante. "+ex.Message.ToString());
                return false;
            }
        }

		
	}
}