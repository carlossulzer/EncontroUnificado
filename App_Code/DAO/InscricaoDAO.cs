using System;
using System.Collections;
using System.Data;
using Util;
using Dominio;
using Banco;
using System.Text;
using DIC;
using System.Data.SqlClient;

namespace DAO
{
    public class InscricaoDAO
	{
        public InscricaoDAO()
		{
		}

        public static bool IncluirInscricao(InscricaoDOM inscricaoNova)
        {
            clsObjetosBanco objbanco = new clsObjetosBanco();

            StringBuilder sqlInsert = new StringBuilder();
            sqlInsert.Append("INSERT INTO " + InscricaoDIC.TABLE_INSCRICAO);
            sqlInsert.Append(" ( ");
            sqlInsert.Append(InscricaoDIC.ObterColunasdaTabela());
            sqlInsert.Append(" ) ");
            sqlInsert.Append(" VALUES ");
            sqlInsert.Append(" ( ");
            sqlInsert.Append(inscricaoNova.matricula.ToString()+", " );
            sqlInsert.Append(inscricaoNova.codEnsino.ToString()+", " );
            sqlInsert.Append(inscricaoNova.codEvento.ToString()+", ");
            sqlInsert.Append(StringSuporte.Formatar(inscricaoNova.data)+", ");
            sqlInsert.Append(inscricaoNova.codHorario.ToString() + ", ");
            sqlInsert.Append(StringSuporte.Formatar(inscricaoNova.presente));
            sqlInsert.Append(" ) ");
            int reg = objbanco.IncluirRegistro(sqlInsert.ToString());
            return (reg > 0);
        }

        public static bool ExcluirInscricao(InscricaoDOM inscricaoNova)
        {
            try
            {
                clsObjetosBanco objbanco = new clsObjetosBanco();

                StringBuilder sqlDelete = new StringBuilder();
                sqlDelete.Append("DELETE FROM " + InscricaoDIC.TABLE_INSCRICAO);
                sqlDelete.Append(" WHERE ");
                sqlDelete.Append(EventoDIC.COL_COD_EVENTO + " = " + inscricaoNova.codEvento.ToString() + " and ");
                sqlDelete.Append(InscricaoDIC.COL_COD_ENSINO + " = " + inscricaoNova.codEnsino.ToString() + " and ");
                sqlDelete.Append(InscricaoDIC.COL_MATRICULA + " = " + inscricaoNova.matricula.ToString() + " and ");
                sqlDelete.Append(InscricaoDIC.COL_DATA + " = " + StringSuporte.Formatar(inscricaoNova.data) + " and ");
                sqlDelete.Append(HorarioDIC.COL_COD_HORARIO + " = " + inscricaoNova.codHorario.ToString());

                objbanco.CriarObjetosBanco(sqlDelete.ToString());
                objbanco.command.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
        }

        private string ObterDadosInscricao()
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT "+ InscricaoDIC.ObterColunasdaTabela());
            sql.Append(" FROM " + InscricaoDIC.TABLE_INSCRICAO);

            return sql.ToString();
        }

        public static ResultadoQuery ListarInscricao()
        {
            clsObjetosBanco objbanco = new clsObjetosBanco();
            StringBuilder sql = new StringBuilder();
            
            sql.Append(" SELECT "+EventoDIC.TABLE_EVENTO+"."+EventoDIC.COL_COD_EVENTO+",");
            sql.Append(EventoDIC.TABLE_EVENTO+"."+EventoDIC.COL_TITULO+",");
            sql.Append(CalendarioDIC.TABLE_CALENDARIO+"."+CalendarioDIC.COL_COD_SALA+",");
            sql.Append(CalendarioDIC.TABLE_CALENDARIO+"."+CalendarioDIC.COL_COD_HORARIO+",");
            sql.Append(CalendarioDIC.TABLE_CALENDARIO+"."+CalendarioDIC.COL_DATA+",");
            sql.Append(TipoEventoDIC.TABLE_TIPO_EVENTO+"."+TipoEventoDIC.COL_DESCRICAO+" AS "+TipoEventoDIC.COL_TIPO_EVENTO_DESC+", ");
            sql.Append(HorarioDIC.TABLE_HORARIO + "." + HorarioDIC.COL_HORAINICIAL+"+' às '+" + HorarioDIC.TABLE_HORARIO+"."+HorarioDIC.COL_HORAFINAL+" AS "+HorarioDIC.COL_DESC_CONSULTA+", ");
            sql.Append("("+EventoDIC.TABLE_EVENTO + "." + EventoDIC.COL_NUM_VAGAS + " - (");
            sql.Append("SELECT COUNT(*) FROM " + InscricaoDIC.TABLE_INSCRICAO + " A1");
            sql.Append(" WHERE " + EventoDIC.TABLE_EVENTO+"."+ EventoDIC.COL_COD_EVENTO + " = A1." + EventoDIC.COL_COD_EVENTO + " and ");
            sql.Append(" A1." + HorarioDIC.COL_COD_HORARIO + " = " + CalendarioDIC.TABLE_CALENDARIO + "." + CalendarioDIC.COL_COD_HORARIO + " and ");
            sql.Append(" A1." + InscricaoDIC.COL_DATA + " = " + CalendarioDIC.TABLE_CALENDARIO + "." + CalendarioDIC.COL_DATA);
            sql.Append(")) AS " + InscricaoDIC.COL_VAGAS);
            sql.Append(" FROM " + EventoDIC.TABLE_EVENTO + ", " + CalendarioDIC.TABLE_CALENDARIO+", "+HorarioDIC.TABLE_HORARIO+", ");
            sql.Append(TipoEventoDIC.TABLE_TIPO_EVENTO);
            sql.Append(" WHERE ");
            sql.Append(EventoDIC.TABLE_EVENTO + "." + TipoEventoDIC.COL_COD_TIPO_EVENTO + " = " + TipoEventoDIC.TABLE_TIPO_EVENTO + "." + TipoEventoDIC.COL_COD_TIPO_EVENTO + " and "); 
            sql.Append(CalendarioDIC.TABLE_CALENDARIO + "." + HorarioDIC.COL_COD_HORARIO + " = " + HorarioDIC.TABLE_HORARIO + "." + HorarioDIC.COL_COD_HORARIO + " and ");
            sql.Append(EventoDIC.TABLE_EVENTO+"."+EventoDIC.COL_COD_EVENTO+" = " + CalendarioDIC.TABLE_CALENDARIO+"."+ EventoDIC.COL_COD_EVENTO+" and ");
            sql.Append("(" +EventoDIC.TABLE_EVENTO+"."+ EventoDIC.COL_NUM_VAGAS + " > ");
            sql.Append(" (SELECT COUNT(*) FROM " + InscricaoDIC.TABLE_INSCRICAO + " A2");
            sql.Append(" WHERE " +EventoDIC.TABLE_EVENTO+"."+EventoDIC.COL_COD_EVENTO + " = A2." + EventoDIC.COL_COD_EVENTO + " and ");
            sql.Append(" A2." + HorarioDIC.COL_COD_HORARIO + " = " + CalendarioDIC.TABLE_CALENDARIO + "." + CalendarioDIC.COL_COD_HORARIO + " and ");
            sql.Append(" A2." + InscricaoDIC.COL_DATA+" = " +CalendarioDIC.TABLE_CALENDARIO+"."+CalendarioDIC.COL_DATA+"))");
            sql.Append(" ORDER BY " + TipoEventoDIC.TABLE_TIPO_EVENTO + "." + TipoEventoDIC.COL_DESCRICAO + ", ");
            sql.Append(EventoDIC.TABLE_EVENTO + "." + EventoDIC.COL_TITULO+", ");
            sql.Append(CalendarioDIC.TABLE_CALENDARIO + "." + CalendarioDIC.COL_DATA+ ", ");
            sql.Append(CalendarioDIC.TABLE_CALENDARIO + "." + HorarioDIC.COL_COD_HORARIO);
            return objbanco.MontaDataSet(sql.ToString());
        }

        private static InscricaoDOM CriarObjetoInscricao(ResultadoQuery dadosInscricao)
        {
            return new InscricaoDOM(
                Conversor.ConverterParaInteiro(dadosInscricao[InscricaoDIC.COL_MATRICULA]),
                Conversor.ConverterParaInteiro(dadosInscricao[InscricaoDIC.COL_COD_ENSINO]),
                Conversor.ConverterParaInteiro(dadosInscricao[EventoDIC.COL_COD_EVENTO]),
                Conversor.ConverterParaDateTime(dadosInscricao[InscricaoDIC.COL_DATA]),
                Conversor.ConverterParaInteiro(dadosInscricao[HorarioDIC.COL_COD_HORARIO]),
                Conversor.ConverterParaBoolean(dadosInscricao[InscricaoDIC.COL_PRESENTE]));
        }

        public static ResultadoQuery InscricaoViewState()
        {
            clsObjetosBanco objbanco = new clsObjetosBanco();
            StringBuilder sql = new StringBuilder();

            sql.Append(" SELECT " + EventoDIC.COL_COD_EVENTO + ",");
            sql.Append(InscricaoDIC.COL_DATA+", ");
            sql.Append(HorarioDIC.COL_COD_HORARIO);
            sql.Append(" FROM " + InscricaoDIC.TABLE_INSCRICAO);
            sql.Append(" WHERE ");
            sql.Append(EventoDIC.COL_COD_EVENTO + " = 0 ");

            return objbanco.MontaDataSet(sql.ToString());
        }

        public static string SalvarInscricao(string matricula, string codEnsino, DataSet Inscricao) //, ref string CodEventoRet, ref string data, ref string CodHorario)
        {
            try
            {
                string msnErro = string.Empty;
                clsObjetosBanco objbanco = new clsObjetosBanco();

                 // percorre a tabela temporária e verifica os calendários que foram excluído, e efetiva a exclusão
                DataSet dsDeletados = Inscricao.GetChanges(DataRowState.Deleted);
                if (dsDeletados != null)
                {
                    for (int i = 0; i <= dsDeletados.Tables[0].Rows.Count - 1; i++)
                    {
                        dsDeletados.Tables[0].Rows[i].RejectChanges();
                    }
                }

                DataSet dsIncluidos = Inscricao.GetChanges(DataRowState.Added);

                // percorre todas as linhas da tabela e grava os professores da banco relacionados ao evento
                if (dsIncluidos != null)
                {
                    bool ocorreuErro = false;
                    SqlTransaction tx;

                    SqlConnection conn = (SqlConnection)objbanco.CriarConexao();
                    conn.Open();
                    tx = conn.BeginTransaction();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Transaction = tx;


                    if (InscricaoExiste(matricula))
                    {
                        msnErro = "Inscrição já gravada para este aluno.";
                    }
                    else
                    {
                        for (int i = 0; i <= dsIncluidos.Tables[0].Rows.Count - 1; i++)
                        {
                            InscricaoDOM incInscricao = new InscricaoDOM();
                            incInscricao.matricula = Conversor.ConverterParaInteiro(matricula);
                            incInscricao.codEnsino = Conversor.ConverterParaInteiro(codEnsino);
                            incInscricao.codEvento = Conversor.ConverterParaInteiro(dsIncluidos.Tables[0].Rows[i][EventoDIC.COL_COD_EVENTO].ToString());
                            incInscricao.data = Conversor.ConverterParaDateTime(dsIncluidos.Tables[0].Rows[i][CalendarioDIC.COL_DATA].ToString());
                            incInscricao.codHorario = Conversor.ConverterParaInteiro(dsIncluidos.Tables[0].Rows[i][CalendarioDIC.COL_COD_HORARIO].ToString());
                            incInscricao.presente   = false;

                            StringBuilder sqlVerifica = new StringBuilder();
                            sqlVerifica.Append("SELECT ");
                            sqlVerifica.Append("(" + EventoDIC.TABLE_EVENTO + "." + EventoDIC.COL_NUM_VAGAS + " - ");
                            sqlVerifica.Append(" (SELECT COUNT(*) FROM " + InscricaoDIC.TABLE_INSCRICAO + " A2");
                            sqlVerifica.Append(" WHERE " + EventoDIC.TABLE_EVENTO + "." + EventoDIC.COL_COD_EVENTO + " = " + EventoDIC.TABLE_EVENTO + "." + EventoDIC.COL_COD_EVENTO + " and ");
                            sqlVerifica.Append(" A2." + HorarioDIC.COL_COD_HORARIO + " = " + incInscricao.codHorario.ToString() + " and ");
                            sqlVerifica.Append(" A2." + InscricaoDIC.COL_DATA + " = " + StringSuporte.Formatar(incInscricao.data)+" and ");
                            sqlVerifica.Append("A2." + EventoDIC.COL_COD_EVENTO + " = " + incInscricao.codEvento.ToString()+ ")) AS " + EventoDIC.COL_NUM_VAGAS + ", ");
                            sqlVerifica.Append(EventoDIC.TABLE_EVENTO+"."+EventoDIC.COL_TITULO);
                            sqlVerifica.Append(" FROM " + EventoDIC.TABLE_EVENTO);
                            sqlVerifica.Append(" WHERE " + EventoDIC.TABLE_EVENTO + "." + EventoDIC.COL_COD_EVENTO + " = " + incInscricao.codEvento.ToString());

                            cmd.CommandText = sqlVerifica.ToString();
                            cmd.Connection = conn;

                            IDataReader resultado = cmd.ExecuteReader();

                            int qtdVagas = 0;
                            string erroEvento = string.Empty;
                            while (resultado.Read())
                            {
                                qtdVagas  = Conversor.ConverterParaInteiro(resultado[EventoDIC.COL_NUM_VAGAS].ToString());
                                erroEvento = resultado[EventoDIC.COL_TITULO].ToString();
                            }
                            
                            resultado.Close();

                            if (qtdVagas > 0)
                            {
                                StringBuilder sqlInsert = new StringBuilder();
                                sqlInsert.Append("INSERT INTO " + InscricaoDIC.TABLE_INSCRICAO);
                                sqlInsert.Append(" ( ");
                                sqlInsert.Append(InscricaoDIC.ObterColunasdaTabela());
                                sqlInsert.Append(" ) ");
                                sqlInsert.Append(" VALUES ");
                                sqlInsert.Append(" ( ");
                                sqlInsert.Append(incInscricao.matricula.ToString() + ", ");
                                sqlInsert.Append(incInscricao.codEnsino.ToString() + ", ");
                                sqlInsert.Append(incInscricao.codEvento.ToString() + ", ");
                                sqlInsert.Append(StringSuporte.Formatar(incInscricao.data) + ", ");
                                sqlInsert.Append(incInscricao.codHorario.ToString()+", ");
                                sqlInsert.Append(incInscricao.presente.ToString());
                                sqlInsert.Append(" ) ");

                                cmd.CommandText = sqlInsert.ToString();
                                cmd.Connection = conn;
                                try
                                {
                                    cmd.ExecuteNonQuery();
                                    ocorreuErro = false;
                                }
                                catch 
                                {
                                    ocorreuErro = true;
                                    msnErro = "Ocorreu um erro ao tentar gravar a inscrição. ";
                                    tx.Rollback();
                                    break;
                                }
                            }
                            else
                            {
                                msnErro = "VAGAS ESGOTADAS PARA O EVENTO ("+incInscricao.codEvento.ToString()+" - "+erroEvento+"). DESMARQUE ESTE EVENTO NA LISTA E CONFIRME NOVAMENTE." ;
                                ocorreuErro = true;
                                break;
                            }
                        }

                        if (!ocorreuErro)
                            tx.Commit();

                        conn.Close();
                    }
                }
                return msnErro;
            }

            catch(Exception ex) 
            {
                return "Erro ao gravar dados do calendário. "+ex.Message.ToString();
            }
        }

        public static bool InscricaoExiste(string matricula)
        {
            StringBuilder sqlExiste = new StringBuilder();
            sqlExiste.Append(" SELECT COUNT(*) FROM " + InscricaoDIC.TABLE_INSCRICAO);
            sqlExiste.Append(" WHERE " + InscricaoDIC.COL_MATRICULA + " = " + matricula);

            clsObjetosBanco objbanco = new clsObjetosBanco();
            objbanco.CriarObjetosBanco(sqlExiste.ToString());

            int resultado = (int)objbanco.command.ExecuteScalar();
            return (resultado > 0);
        }

        public static ResultadoQuery ImprimirInscricao(string matricula, string codEnsino)
        {
            clsObjetosBanco objbanco = new clsObjetosBanco();
            StringBuilder sql = new StringBuilder();

            sql.Append(" SELECT " );
            sql.Append(    EventoDIC.TABLE_EVENTO + "." + EventoDIC.COL_TITULO + ",");
            sql.Append(    CalendarioDIC.TABLE_CALENDARIO + "." + CalendarioDIC.COL_DATA + ",");
            sql.Append(    TipoEventoDIC.TABLE_TIPO_EVENTO + "." + TipoEventoDIC.COL_DESCRICAO + " AS " + TipoEventoDIC.COL_TIPO_EVENTO_DESC + ", ");
            sql.Append(    HorarioDIC.TABLE_HORARIO + "." + HorarioDIC.COL_HORAINICIAL + "+' às '+" + HorarioDIC.TABLE_HORARIO + "." + HorarioDIC.COL_HORAFINAL + " AS " + HorarioDIC.COL_DESC_CONSULTA + ", ");
            sql.Append(    "'SALA :'+" +SalaDIC.TABLE_SALA+"."+SalaDIC.COL_DESCRICAO + "+' - BLOCO:'+" + SalaDIC.TABLE_SALA+"."+ SalaDIC.COL_BLOCO + "+' - '+" + SalaDIC.TABLE_SALA+"."+SalaDIC.COL_ANDAR + "+'º ANDAR' AS " + SalaDIC.COL_DESC_CONSULTA);
            sql.Append(" FROM " + InscricaoDIC.TABLE_INSCRICAO+", "+EventoDIC.TABLE_EVENTO + ", " + CalendarioDIC.TABLE_CALENDARIO + ", ");
            sql.Append(     HorarioDIC.TABLE_HORARIO + ", "+ SalaDIC.TABLE_SALA+", "+TipoEventoDIC.TABLE_TIPO_EVENTO);
            sql.Append(" WHERE ");
            sql.Append(EventoDIC.TABLE_EVENTO + "."   + TipoEventoDIC.COL_COD_TIPO_EVENTO + " = " + TipoEventoDIC.TABLE_TIPO_EVENTO + "." + TipoEventoDIC.COL_COD_TIPO_EVENTO + " and ");
            sql.Append(InscricaoDIC.TABLE_INSCRICAO   + "." + EventoDIC.COL_COD_EVENTO + " = " + EventoDIC.TABLE_EVENTO + "." + EventoDIC.COL_COD_EVENTO + " and ");
            sql.Append(CalendarioDIC.TABLE_CALENDARIO + "." + EventoDIC.COL_COD_EVENTO + " = " + InscricaoDIC.TABLE_INSCRICAO + "." + EventoDIC.COL_COD_EVENTO + " and ");
            sql.Append(CalendarioDIC.TABLE_CALENDARIO + "." + HorarioDIC.COL_COD_HORARIO + " = " + InscricaoDIC.TABLE_INSCRICAO + "." + HorarioDIC.COL_COD_HORARIO + " and ");
            sql.Append(CalendarioDIC.TABLE_CALENDARIO + "." + CalendarioDIC.COL_DATA + " = " + InscricaoDIC.TABLE_INSCRICAO + "." + InscricaoDIC.COL_DATA + " and ");
            sql.Append(CalendarioDIC.TABLE_CALENDARIO + "." + SalaDIC.COL_COD_SALA + " = " + SalaDIC.TABLE_SALA + "." + SalaDIC.COL_COD_SALA + " and ");
            sql.Append(InscricaoDIC.TABLE_INSCRICAO   + "." + HorarioDIC.COL_COD_HORARIO + " = " + HorarioDIC.TABLE_HORARIO + "." + HorarioDIC.COL_COD_HORARIO + " and ");
            sql.Append(InscricaoDIC.TABLE_INSCRICAO   + "." + InscricaoDIC.COL_MATRICULA + " = " + matricula + " and ");
            sql.Append(InscricaoDIC.TABLE_INSCRICAO   + "." + InscricaoDIC.COL_COD_ENSINO + " = " + codEnsino);

            return objbanco.MontaDataSet(sql.ToString());
        }

        public static ResultadoQuery DropDownDatadaInscricao()
        {
            clsObjetosBanco objbanco = new clsObjetosBanco();
            StringBuilder sql = new StringBuilder();
            sql.Append(" SELECT " ); 
            sql.Append(" RIGHT(CAST((DATEPART(day,data)+100) AS CHAR(3)),2)+'/'+RIGHT(CAST((DATEPART(month,data)+100) AS CHAR(3)),2)+'/'+CAST(datepart(YEAR, data) AS varchar(4)) AS "+InscricaoDIC.COL_DATA);
            sql.Append(" FROM " + InscricaoDIC.TABLE_INSCRICAO );
            sql.Append(" GROUP BY " + InscricaoDIC.COL_DATA);
            sql.Append(" ORDER BY " + InscricaoDIC.COL_DATA);

            return objbanco.MontaDataSet(sql.ToString());
        }

    }
}