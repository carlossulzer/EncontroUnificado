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
	public class AlunosDAO
	{

        public AlunosDAO()
		{
		}

        public static ResultadoQuery DropDownAlunoGraduacao()
        {
            clsObjetosBanco objbanco = new clsObjetosBanco();
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT " + AlunoDIC.TABLE_ALUNOG+"."+AlunoDIC.COL_MATRICULA + ", ");
            sql.Append(AlunoDIC.TABLE_ALUNOG+"."+AlunoDIC.COL_NOME);
            sql.Append(" FROM " + AlunoDIC.TABLE_ALUNOG);
            sql.Append(" ORDER BY " + AlunoDIC.TABLE_ALUNOG + "." + AlunoDIC.COL_NOME);

            return objbanco.MontaDataSet(sql.ToString());
        }

        public static ResultadoQuery DropDownAlunoTecnologo()
        {
            clsObjetosBanco objbanco = new clsObjetosBanco();
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT " + AlunoDIC.TABLE_ALUNOT + "." + AlunoDIC.COL_MATRICULA + ", ");
            sql.Append(AlunoDIC.TABLE_ALUNOT + "." + AlunoDIC.COL_NOME);
            sql.Append(" FROM " + AlunoDIC.TABLE_ALUNOT);
            sql.Append(" ORDER BY " + AlunoDIC.TABLE_ALUNOT + "." + AlunoDIC.COL_NOME);

            return objbanco.MontaDataSet(sql.ToString());
        }


        public static ResultadoQuery ObterDadosAluno(string matricula, string codEnsino )
        {
            clsObjetosBanco objbanco = new clsObjetosBanco();
            StringBuilder sql = new StringBuilder();

            string tabelaAluno = string.Empty;
            string tabelaCurso = string.Empty;
            if (codEnsino.Equals("1"))
            {
                tabelaAluno = AlunoDIC.TABLE_ALUNOG;
                tabelaCurso = CursoDIC.TABLE_CURSOG;
            }
            else if (codEnsino.Equals("2"))
            {
                tabelaAluno = AlunoDIC.TABLE_ALUNOT;
                tabelaCurso = CursoDIC.TABLE_CURSOT;
            }
            else
            {
                tabelaAluno = AlunoDIC.TABLE_ALUNOT;
                tabelaCurso = CursoDIC.TABLE_CURSOT;
                matricula = "0";
            }


            sql.Append("SELECT " + tabelaAluno+ "." + AlunoDIC.COL_MATRICULA + ", ");
            sql.Append(tabelaAluno+ "." + AlunoDIC.COL_NOME+", ");
            sql.Append(tabelaAluno + "." + AlunoDIC.COL_LETRA + ", ");
            sql.Append(tabelaAluno + "." + AlunoDIC.COL_SERIE + ", ");
            sql.Append(tabelaCurso + "." + CursoDIC.COL_CURSO);
            sql.Append(" FROM " + tabelaAluno);
            sql.Append(" LEFT OUTER JOIN " + tabelaCurso + " ON (" + tabelaCurso+"."+ CursoDIC.COL_COD_CURSO + " = " + tabelaAluno+"."+CursoDIC.COL_COD_CURSO+ ")");
            sql.Append(" WHERE " + tabelaAluno + "." + AlunoDIC.COL_MATRICULA + " = " + matricula);
            sql.Append(" ORDER BY " + tabelaAluno+ "." + AlunoDIC.COL_NOME);

            return objbanco.MontaDataSet(sql.ToString());
        }

        public static bool VerificaSenha(string matricula, string senha, string codEnsino)
        {
            clsObjetosBanco objbanco = new clsObjetosBanco();

            string tabelaAluno = string.Empty;
            if (codEnsino.Equals("1"))
                tabelaAluno = AlunoUsuarioDIC.TABLE_ALUNOUSUARIOG;
            else if (codEnsino.Equals("2"))
                tabelaAluno = AlunoUsuarioDIC.TABLE_ALUNOUSUARIOT;

            StringBuilder sqlExiste = new StringBuilder();
            sqlExiste.Append("SELECT COUNT(*) FROM " + tabelaAluno);
            sqlExiste.Append(" WHERE " + tabelaAluno+"."+AlunoUsuarioDIC.COL_MATRICULA + " = " + StringSuporte.Plic(matricula.ToString().Trim())+" and ");
            sqlExiste.Append(tabelaAluno + "." + AlunoUsuarioDIC.COL_SENHA + " = " + StringSuporte.Plic(senha.ToString().Trim()));
            objbanco.CriarObjetosBanco(sqlExiste.ToString());

            int resultado = (int)objbanco.command.ExecuteScalar();

            return (resultado > 0);
        }
	}
}