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
	public class PalestranteDAO
	{

        public PalestranteDAO()
		{
		}


        public static int IncluirPalestrante(PalestranteDOM palestranteNovo)
        {
            int reg = 0;
            if (!RegistroExiste(palestranteNovo.codEvento, palestranteNovo.codProfessor))
            {
                clsObjetosBanco objbanco = new clsObjetosBanco();

                StringBuilder sqlInsert = new StringBuilder();
                sqlInsert.Append("INSERT INTO " + PalestranteDIC.TABLE_PALESTRANTE);
                sqlInsert.Append(" ( ");
                sqlInsert.Append(PalestranteDIC.ObterColunasdaTabela());
                sqlInsert.Append(" ) ");
                sqlInsert.Append(" VALUES ");
                sqlInsert.Append(" ( ");
                sqlInsert.Append(palestranteNovo.codEvento.ToString()+", ");
                sqlInsert.Append(palestranteNovo.codProfessor.ToString());
                sqlInsert.Append(" ) ");
                reg = objbanco.IncluirRegistro(sqlInsert.ToString());
            }
            return reg;

        }


        public static string ExcluirPalestrante(PalestranteDOM palestranteExcluir)
        {
            try
            {
                clsObjetosBanco objbanco = new clsObjetosBanco();
                StringBuilder sqlDelete = new StringBuilder();
                sqlDelete.Append("DELETE FROM " + PalestranteDIC.TABLE_PALESTRANTE);
                sqlDelete.Append(" WHERE " + ProfessorDIC.COL_COD_PROFESSOR + " = " + palestranteExcluir.codProfessor.ToString() + " and ");
                sqlDelete.Append(EventoDIC.COL_COD_EVENTO + " = " + palestranteExcluir.codEvento.ToString());

                objbanco.CriarObjetosBanco(sqlDelete.ToString());
                objbanco.command.ExecuteNonQuery();
                return string.Empty;
            }
            catch
            {
                return "Erro ao excluir o palestrante.";
            }
        }

        public static bool RegistroExiste(int codEvento, int codProfessor)
        {
            clsObjetosBanco objbanco = new clsObjetosBanco();
            StringBuilder sqlExiste = new StringBuilder();
            sqlExiste.Append("SELECT COUNT(*) FROM " + PalestranteDIC.TABLE_PALESTRANTE);
            sqlExiste.Append(" WHERE " + ProfessorDIC.COL_COD_PROFESSOR + " = " + codProfessor.ToString()+" and ");
            sqlExiste.Append(EventoDIC.COL_COD_EVENTO + " = " + codEvento.ToString());
            objbanco.CriarObjetosBanco(sqlExiste.ToString());
            int existente = Conversor.ConverterParaInteiro(objbanco.command.ExecuteScalar().ToString());
            return (existente > 0);
        }

        public static bool SalvarPalestrante(int codEvento, DataSet Palestrante)
        {
            try
            {
                 // percorre a tabela temporária e verifica os palestrantes que foram excluído, e efetiva a exclusão
                DataSet dsDeletados = Palestrante.GetChanges(DataRowState.Deleted);
                if (dsDeletados != null)
                {
                    for (int i = 0; i <= dsDeletados.Tables[0].Rows.Count - 1; i++)
                    {
                        dsDeletados.Tables[0].Rows[i].RejectChanges();

                        PalestranteDOM exPalestrante = new PalestranteDOM();

                        exPalestrante.codEvento = Conversor.ConverterParaInteiro(dsDeletados.Tables[0].Rows[i]["cod_evento"].ToString());
                        exPalestrante.codProfessor = Conversor.ConverterParaInteiro(dsDeletados.Tables[0].Rows[i]["cod_professor"].ToString());

                        PalestranteDAO.ExcluirPalestrante(exPalestrante);
                    }
                }


                DataSet dsIncluidos = Palestrante.GetChanges(DataRowState.Added);

                // percorre todas as linhas da tabela e grava os palestrantes relacionados ao evento
                if (dsIncluidos != null)
                {
                    for (int i = 0; i <= dsIncluidos.Tables[0].Rows.Count - 1; i++)
                    {
                        PalestranteDOM incPalestrante = new PalestranteDOM();
                        incPalestrante.codEvento = codEvento;
                        incPalestrante.codProfessor = Conversor.ConverterParaInteiro(dsIncluidos.Tables[0].Rows[i]["cod_professor"].ToString());
                        PalestranteDAO.IncluirPalestrante(incPalestrante);
                    }
                }
                return true;
            }

            catch(Exception ex) 
            {
                throw new ApplicationException("Erro ao gravar dados do palestrante. "+ex.Message.ToString());
                return false;
            }
        }

		
	}
}