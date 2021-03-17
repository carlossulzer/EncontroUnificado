
using System;
using System.Collections;
using System.Data;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Data.OracleClient;
using System.Configuration;
using System.Data.Common;

/// <summary>
/// Summary description for clsObjetosBanco.
/// </summary>

namespace Banco
{
    public class clsObjetosBanco
    {

        string cString;
        IDbConnection cn;
        IDbCommand cmd;
        IDataAdapter da;
        IDataReader dr;

        public clsObjetosBanco()
        {
            //o constructor da classe faz a leitura do tipo de banco
            cString = ConfigurationManager.ConnectionStrings["EncontroUnifConnectionString"].ConnectionString;
            string setProvider = ConfigurationManager.ConnectionStrings["EncontroUnifConnectionString"].ProviderName;

            if (setProvider == "System.Data.OleDB")
                iBanco = (eTipoBanco)1;
            else if (setProvider == "System.Data.SqlClient")
                iBanco = (eTipoBanco)2;
            else if (setProvider == "System.Data.OracleClient")
                iBanco = (eTipoBanco)3;

        }

        //Abaixo encontra-se a variável e o enum que determinam o tipo de banco

        eTipoBanco iBanco;
        public enum eTipoBanco
        {
            OLEDB = 1,
            SQL = 2,
            ORACLE = 3
        }

        // Devolve o objeto de conexão armazenado nesta instância
        public IDbConnection conexao
        {
            get { return (cn); }
        }

        // Devolve o objeto command armazenado nesta instância
        public IDbCommand command
        {
            get { return (cmd); }
        }

        // Devolve o objeto adapter armazenado nesta instância
        public IDataAdapter DataAdapter
        {
            get { return (da); }
        }

        // Devolve o objeto DataRedaer armazenado nesta instância
        public IDataReader DataReader
        {
            get { return (dr); }
        }

        // Gera uma conexão conforme especificado no web.config
        public IDbConnection CriarConexao()
        {
            switch (iBanco)
            {
                case eTipoBanco.OLEDB:
                    OleDbConnection objole = new OleDbConnection();
                    objole.ConnectionString = cString;
                    return (objole);
                case eTipoBanco.SQL:
                    SqlConnection objsql = new SqlConnection();
                    objsql.ConnectionString = cString;
                    return (objsql);
                case eTipoBanco.ORACLE:
                    OracleConnection objora = new OracleConnection();
                    objora.ConnectionString = cString;
                    return (objora);
                default:
                    throw new ApplicationException("Erro de configuração na configuração do tipo de banco");
            }
        }

        // Cria um Command utilizando o banco especificado
        public IDbCommand CriarCommand()
        {
            switch (iBanco)
            {
                case eTipoBanco.OLEDB:
                    return (new OleDbCommand());
                case eTipoBanco.SQL:
                    return (new SqlCommand());
                case eTipoBanco.ORACLE:
                    return (new OracleCommand());
                default:
                    throw new ApplicationException("Erro de configuração na configuração do tipo de banco");
            }
        }

        // Cria um DataAdapter utilizando o banco especificado
        public IDbDataAdapter CriarAdapter(bool bInterligarCommand)
        {
            switch (iBanco)
            {
                case eTipoBanco.OLEDB:
                    if (!bInterligarCommand)
                        return (new OleDbDataAdapter());
                    else
                        return new OleDbDataAdapter(cmd.CommandText, cmd.Connection.ConnectionString);
                case eTipoBanco.SQL:
                    if (!bInterligarCommand)
                        return (new SqlDataAdapter());
                    else
                        return new SqlDataAdapter(cmd.CommandText, cmd.Connection.ConnectionString);
                case eTipoBanco.ORACLE:
                    if (!bInterligarCommand)
                        return (new OracleDataAdapter());
                    else
                        return new OracleDataAdapter(cmd.CommandText, cmd.Connection.ConnectionString);
                default:
                    throw new ApplicationException("Erro de configuração na configuração do tipo de banco");
            }
        }

        // Criação de todos os objetos de banco para um acesso simples e os interliga
        public void CriarObjetosBanco(string Sql)
        {
            cn = CriarConexao();
            cn.Open();
            cmd = CriarCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = Sql;
            cmd.Connection = cn;
        }

        // Cria um DataSet Genérico
        public ResultadoQuery MontaDataSet(string sql)
        {
            try
            {
                CriarObjetosBanco(sql);
                DataSet ds = new DataSet();
                IDataAdapter da = CriarAdapter(true);
                da.Fill(ds);
                return new ResultadoQuery(ds);
            }
            catch (Exception e)
            {
                throw new ApplicationException("Erro ao tentar obter dados."+e.Message.ToString()); 
            }
        }


    
        public IDataReader MontaDataReader(string Sql)
        {
            CriarObjetosBanco(Sql);

            switch (iBanco)
            {
                case eTipoBanco.OLEDB:
                    dr = (OleDbDataReader)cmd.ExecuteReader();
                    return dr;
                case eTipoBanco.SQL:
                    dr = (SqlDataReader)cmd.ExecuteReader();
                    return dr;
                case eTipoBanco.ORACLE:
                    dr = (OracleDataReader)cmd.ExecuteReader();
                    return dr;
                default:
                    throw new ApplicationException("Não é possível consultar os dados.");
            }
        }

        private object ObterValor(string sql)
        {
            CriarObjetosBanco(sql);

            object retorno = cmd.ExecuteScalar();
            if (retorno == null)
                retorno = String.Empty;
            return retorno;
        }


        public int ObterValorInteiro(string sql)
        {
            return Convert.ToInt32("0" + ObterValor(sql));
        }

        public decimal ObterValorDecimal(string sql)
        {
            return Convert.ToDecimal("0" + ObterValor(sql));
        }

        public string ObterValorString(string sql)
        {
            return ObterValor(sql).ToString();
        }

        public int IncluirRegistro(string sql)
        {
            return ObterValorInteiro(sql);
        }


    }
}