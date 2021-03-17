using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Util;
using Dominio;
using Banco;
using System.Text;
using DIC;

/// <summary>
/// Summary description for PresencaAlunosDAO
/// </summary>

namespace DAO
{
    public class PresencaAlunosDAO
    {
        public PresencaAlunosDAO()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static bool ConfirmarPresenca(InscricaoDOM presenca)
        {
            try
            {
                StringBuilder sqlUpdate = new StringBuilder();
                sqlUpdate.Append("UPDATE " + InscricaoDIC.TABLE_INSCRICAO);
                sqlUpdate.Append(" SET ");

                sqlUpdate.Append(InscricaoDIC.COL_PRESENTE + " = "+StringSuporte.Formatar(presenca.presente.ToString()));

                sqlUpdate.Append(" WHERE ");
                sqlUpdate.Append(EventoDIC.COL_COD_EVENTO + " = " + presenca.codEvento.ToString() + " and ");
                sqlUpdate.Append(InscricaoDIC.COL_COD_ENSINO + " = " + presenca.codEnsino.ToString() + " and ");
                sqlUpdate.Append(InscricaoDIC.COL_MATRICULA + " = " + presenca.matricula.ToString() + " and ");
                sqlUpdate.Append(InscricaoDIC.COL_DATA + " = " + StringSuporte.Formatar(presenca.data) + " and ");
                sqlUpdate.Append(HorarioDIC.COL_COD_HORARIO + " = " + presenca.codHorario.ToString());

                clsObjetosBanco objbanco = new clsObjetosBanco();
                objbanco.CriarObjetosBanco(sqlUpdate.ToString());

                int confirmados = objbanco.command.ExecuteNonQuery();
                return (confirmados > 0);
            }
            catch (Exception e)
            {
                return false;
            }
        }


        public static ResultadoQuery AlunosdoEvento(int codEvento, DateTime dataEvento, int codHorario)
        {
            clsObjetosBanco objbanco = new clsObjetosBanco();
            StringBuilder sql = new StringBuilder();

            sql.Append(" SELECT " + InscricaoDIC.TABLE_INSCRICAO + "." + InscricaoDIC.COL_MATRICULA + ", ");
            sql.Append(InscricaoDIC.TABLE_INSCRICAO + "." + InscricaoDIC.COL_COD_ENSINO + ", ");
            sql.Append(AlunoDIC.TABLE_ALUNOG + "." + AlunoDIC.COL_NOME+", ");
            sql.Append(InscricaoDIC.TABLE_INSCRICAO + "." + InscricaoDIC.COL_PRESENTE+", ");
            sql.Append(InscricaoDIC.TABLE_INSCRICAO+"."+EventoDIC.COL_COD_EVENTO+", ");
            sql.Append(InscricaoDIC.TABLE_INSCRICAO + "." + HorarioDIC.COL_COD_HORARIO+", ");
            sql.Append(InscricaoDIC.TABLE_INSCRICAO + "." + InscricaoDIC.COL_DATA);
            sql.Append(" FROM " + AlunoDIC.TABLE_ALUNOG + "," + InscricaoDIC.TABLE_INSCRICAO);
            sql.Append(" WHERE " + InscricaoDIC.TABLE_INSCRICAO + "." + EventoDIC.COL_COD_EVENTO + " = " + codEvento.ToString() + " and ");
            sql.Append(InscricaoDIC.TABLE_INSCRICAO + "." + InscricaoDIC.COL_DATA + " = " + StringSuporte.Formatar(dataEvento) + " and ");
            //sql.Append(InscricaoDIC.TABLE_INSCRICAO + "." + HorarioDIC.COL_COD_HORARIO + " = " + codHorario.ToString() + " and ");
            sql.Append(InscricaoDIC.TABLE_INSCRICAO + "." + InscricaoDIC.COL_MATRICULA + " = " + AlunoDIC.TABLE_ALUNOG + "." + AlunoDIC.COL_MATRICULA);
            sql.Append(" UNION ");
            sql.Append(" SELECT " + InscricaoDIC.TABLE_INSCRICAO + "." + InscricaoDIC.COL_MATRICULA + ", ");
            sql.Append(InscricaoDIC.TABLE_INSCRICAO + "." + InscricaoDIC.COL_COD_ENSINO + ", ");
            sql.Append(AlunoDIC.TABLE_ALUNOT + "." + AlunoDIC.COL_NOME + ", ");
            sql.Append(InscricaoDIC.TABLE_INSCRICAO + "." + InscricaoDIC.COL_PRESENTE+", ");
            sql.Append(InscricaoDIC.TABLE_INSCRICAO + "." + EventoDIC.COL_COD_EVENTO + ", ");
            sql.Append(InscricaoDIC.TABLE_INSCRICAO + "." + HorarioDIC.COL_COD_HORARIO+", ");
            sql.Append(InscricaoDIC.TABLE_INSCRICAO + "." + InscricaoDIC.COL_DATA);
            sql.Append(" FROM " + AlunoDIC.TABLE_ALUNOT + "," + InscricaoDIC.TABLE_INSCRICAO);
            sql.Append(" WHERE " + InscricaoDIC.TABLE_INSCRICAO + "." + EventoDIC.COL_COD_EVENTO + " = " + codEvento.ToString() + " and ");
            sql.Append(InscricaoDIC.TABLE_INSCRICAO + "." + InscricaoDIC.COL_DATA + " = " + StringSuporte.Formatar(dataEvento) + " and ");
            //sql.Append(InscricaoDIC.TABLE_INSCRICAO + "." + HorarioDIC.COL_COD_HORARIO + " = " + codHorario.ToString() + " and ");
            sql.Append(InscricaoDIC.TABLE_INSCRICAO + "." + InscricaoDIC.COL_MATRICULA + " = " + AlunoDIC.TABLE_ALUNOT + "." + AlunoDIC.COL_MATRICULA);

            sql.Append(" ORDER BY 2");

            return objbanco.MontaDataSet(sql.ToString());
        }


        public static string SalvarPresencadoAluno(DataSet Presenca)
        {
            string mensErro =  "Erro ao gravar presença dos alunos.";
            try
            {
                // percorre a tabela temporária e verifica os integrante que foram excluído, e efetiva a exclusão
                DataSet dsDeletados = Presenca.GetChanges(DataRowState.Deleted);
                if (dsDeletados != null)
                {
                    bool alunosExcluidos = true;
                    for (int i = 0; i <= dsDeletados.Tables[0].Rows.Count - 1; i++)
                    {
                        dsDeletados.Tables[0].Rows[i].RejectChanges();

                        InscricaoDOM exAluno = new InscricaoDOM();

                        exAluno.codEvento = Conversor.ConverterParaInteiro(dsDeletados.Tables[0].Rows[i][EventoDIC.COL_COD_EVENTO].ToString());
                        exAluno.matricula = Conversor.ConverterParaInteiro(dsDeletados.Tables[0].Rows[i][InscricaoDIC.COL_MATRICULA].ToString());
                        exAluno.codEnsino = Conversor.ConverterParaInteiro(dsDeletados.Tables[0].Rows[i][InscricaoDIC.COL_COD_ENSINO].ToString());
                        exAluno.data      = Conversor.ConverterParaDateTime(dsDeletados.Tables[0].Rows[i][InscricaoDIC.COL_DATA].ToString());
                        exAluno.codHorario = Conversor.ConverterParaInteiro(dsDeletados.Tables[0].Rows[i][HorarioDIC.COL_COD_HORARIO].ToString());

                        alunosExcluidos = InscricaoDAO.ExcluirInscricao(exAluno);
                        if (alunosExcluidos.Equals(false))
                            return mensErro;
                    }
                }

                DataSet dsAlterados = Presenca.GetChanges(DataRowState.Modified);
                // percorre todas as linhas da tabela e grava os professores da banco relacionados ao evento
                if (dsAlterados != null)
                {
                    bool alunosAlterados = true;
                    for (int i = 0; i <= dsAlterados.Tables[0].Rows.Count - 1; i++)
                    {
                        InscricaoDOM altAluno = new InscricaoDOM();

                        altAluno.codEvento = Conversor.ConverterParaInteiro(dsAlterados.Tables[0].Rows[i][EventoDIC.COL_COD_EVENTO].ToString());
                        altAluno.matricula = Conversor.ConverterParaInteiro(dsAlterados.Tables[0].Rows[i][InscricaoDIC.COL_MATRICULA].ToString());
                        altAluno.codEnsino = Conversor.ConverterParaInteiro(dsAlterados.Tables[0].Rows[i][InscricaoDIC.COL_COD_ENSINO].ToString());
                        altAluno.data = Conversor.ConverterParaDateTime(dsAlterados.Tables[0].Rows[i][InscricaoDIC.COL_DATA].ToString());
                        altAluno.codHorario = Conversor.ConverterParaInteiro(dsAlterados.Tables[0].Rows[i][HorarioDIC.COL_COD_HORARIO].ToString());
                        altAluno.presente = Conversor.ConverterParaBoolean(dsAlterados.Tables[0].Rows[i][InscricaoDIC.COL_PRESENTE].ToString());
                        alunosAlterados = ConfirmarPresenca(altAluno);
                         if (alunosAlterados.Equals(false))
                            return mensErro;
                   }
                }

                DataSet dsIncluidos = Presenca.GetChanges(DataRowState.Added);
                // percorre todas as linhas da tabela e grava os professores da banco relacionados ao evento
                if (dsIncluidos != null)
                {
                    bool alunosIncluidos = false;
                    for (int i = 0; i <= dsIncluidos.Tables[0].Rows.Count - 1; i++)
                    {
                        InscricaoDOM incAluno = new InscricaoDOM();

                        incAluno.codEvento = Conversor.ConverterParaInteiro(dsIncluidos.Tables[0].Rows[i][EventoDIC.COL_COD_EVENTO].ToString());
                        incAluno.matricula = Conversor.ConverterParaInteiro(dsIncluidos.Tables[0].Rows[i][InscricaoDIC.COL_MATRICULA].ToString());
                        incAluno.codEnsino = Conversor.ConverterParaInteiro(dsIncluidos.Tables[0].Rows[i][InscricaoDIC.COL_COD_ENSINO].ToString());
                        incAluno.data = Conversor.ConverterParaDateTime(dsIncluidos.Tables[0].Rows[i][InscricaoDIC.COL_DATA].ToString());
                        incAluno.codHorario = Conversor.ConverterParaInteiro(dsIncluidos.Tables[0].Rows[i][HorarioDIC.COL_COD_HORARIO].ToString());
                        incAluno.presente = Conversor.ConverterParaBoolean(dsIncluidos.Tables[0].Rows[i][InscricaoDIC.COL_PRESENTE].ToString()) ;
                        alunosIncluidos = InscricaoDAO.IncluirInscricao(incAluno);
                        //if (alunosIncluidos.Equals(false))
                        //    return mensErro;
                    }
                }
                return string.Empty;
            }

            catch(Exception ex) 
            {
                return mensErro+" - "+ex.Message.ToString();
            }
        }



    }
}