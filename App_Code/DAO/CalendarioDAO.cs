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
	public class CalendarioDAO
	{

        public CalendarioDAO()
		{
		}


        public static int IncluirCalendario(CalendarioDOM calendarioNovo)
        {
            int reg = 0;
            if (!RegistroExiste(calendarioNovo))
            {
                clsObjetosBanco objbanco = new clsObjetosBanco();

                StringBuilder sqlInsert = new StringBuilder();
                sqlInsert.Append("INSERT INTO " + CalendarioDIC.TABLE_CALENDARIO);
                sqlInsert.Append(" ( ");
                sqlInsert.Append(CalendarioDIC.ObterColunasdaTabela());
                sqlInsert.Append(" ) ");
                sqlInsert.Append(" VALUES ");
                sqlInsert.Append(" ( ");
                sqlInsert.Append(calendarioNovo.codSala.ToString() + ", ");
                sqlInsert.Append(calendarioNovo.codHorario.ToString() + ", ");
                sqlInsert.Append(calendarioNovo.codEvento.ToString() + ", ");
                sqlInsert.Append(StringSuporte.Formatar(calendarioNovo.data));
                sqlInsert.Append(" ) ");
                reg = objbanco.IncluirRegistro(sqlInsert.ToString());
            }
            return reg;
        }


        public static string ExcluirCalendario(CalendarioDOM calendarioExcluir)
        {
            try
            {
                clsObjetosBanco objbanco = new clsObjetosBanco();
                StringBuilder sqlDelete = new StringBuilder();
                sqlDelete.Append("DELETE FROM " + CalendarioDIC.TABLE_CALENDARIO);
                sqlDelete.Append(" WHERE " + CalendarioDIC.COL_COD_SALA + " = " + calendarioExcluir.codSala.ToString() + " and ");
                sqlDelete.Append(CalendarioDIC.COL_COD_HORARIO + " = " + calendarioExcluir.codHorario.ToString() + " and ");
                sqlDelete.Append(EventoDIC.COL_COD_EVENTO + " = " + calendarioExcluir.codEvento.ToString()+" and ");
                sqlDelete.Append( CalendarioDIC.COL_DATA + " = " + StringSuporte.Formatar(calendarioExcluir.data));
                objbanco.CriarObjetosBanco(sqlDelete.ToString());
                objbanco.command.ExecuteNonQuery();
                return string.Empty;
            }
            catch
            {
                return "Erro ao excluir o calendário.";
            }
        }

        public static bool RegistroExiste(CalendarioDOM calendarioExiste)
        {
            clsObjetosBanco objbanco = new clsObjetosBanco();
            StringBuilder sqlExiste = new StringBuilder();
            sqlExiste.Append("SELECT COUNT(*) FROM " + CalendarioDIC.TABLE_CALENDARIO);
            sqlExiste.Append(" WHERE " + CalendarioDIC.COL_COD_SALA+" = "+calendarioExiste.codSala.ToString()+" and ");
            sqlExiste.Append(CalendarioDIC.COL_COD_HORARIO+" = "+calendarioExiste.codHorario.ToString()+" and ");
            sqlExiste.Append(EventoDIC.COL_COD_EVENTO + " = " + calendarioExiste.codEvento.ToString()+" and ");
            sqlExiste.Append(CalendarioDIC.COL_DATA + " = " + StringSuporte.Formatar(calendarioExiste.data));
            objbanco.CriarObjetosBanco(sqlExiste.ToString());
            int existente = Conversor.ConverterParaInteiro(objbanco.command.ExecuteScalar().ToString());
            return (existente > 0);

        }

        public static bool SalvarCalendario(int codEvento, DataSet Calendario)
        {
            try
            {
                 // percorre a tabela temporária e verifica os calendários que foram excluído, e efetiva a exclusão
                DataSet dsDeletados = Calendario.GetChanges(DataRowState.Deleted);
                if (dsDeletados != null)
                {
                    for (int i = 0; i <= dsDeletados.Tables[0].Rows.Count - 1; i++)
                    {
                        dsDeletados.Tables[0].Rows[i].RejectChanges();

                        CalendarioDOM exCalendario = new CalendarioDOM();

                        exCalendario.codSala = Conversor.ConverterParaInteiro(dsDeletados.Tables[0].Rows[i][CalendarioDIC.COL_COD_SALA]);
                        exCalendario.codHorario = Conversor.ConverterParaInteiro(dsDeletados.Tables[0].Rows[i][CalendarioDIC.COL_COD_HORARIO]);
                        exCalendario.codEvento = Conversor.ConverterParaInteiro(dsDeletados.Tables[0].Rows[i][EventoDIC.COL_COD_EVENTO].ToString());
                        exCalendario.data = Conversor.ConverterParaDateTime(dsDeletados.Tables[0].Rows[i][CalendarioDIC.COL_DATA].ToString());

                        CalendarioDAO.ExcluirCalendario(exCalendario);
                    }
                }


                DataSet dsIncluidos = Calendario.GetChanges(DataRowState.Added);

                // percorre todas as linhas da tabela e grava os professores da banco relacionados ao evento
                if (dsIncluidos != null)
                {
                    for (int i = 0; i <= dsIncluidos.Tables[0].Rows.Count - 1; i++)
                    {
                        CalendarioDOM incCalendario = new CalendarioDOM();
                        incCalendario.codEvento = codEvento;
                        incCalendario.codSala = Conversor.ConverterParaInteiro(dsIncluidos.Tables[0].Rows[0][CalendarioDIC.COL_COD_SALA].ToString());
                        incCalendario.codHorario = Conversor.ConverterParaInteiro(dsIncluidos.Tables[0].Rows[0][CalendarioDIC.COL_COD_HORARIO].ToString());
                        incCalendario.data = Conversor.ConverterParaDateTime(dsIncluidos.Tables[0].Rows[i][CalendarioDIC.COL_DATA].ToString());
                        CalendarioDAO.IncluirCalendario(incCalendario);
                    }
                }
                return true;
            }

            catch(Exception ex) 
            {
                throw new ApplicationException("Erro ao gravar dados do calendário. "+ex.Message.ToString());
            }
        }
	}




}