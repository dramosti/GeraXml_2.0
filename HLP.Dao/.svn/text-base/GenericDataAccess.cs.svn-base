using System;
using System.Data;
using System.Data.Common;
using System.Configuration;
using FirebirdSql.Data.FirebirdClient;
using System.Text;
using System.IO;
using System.Xml;


namespace HLP.Dao
{
   
    public class DataGeneric
    {
        // static constructor 
        public DataGeneric()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        /*
        public static DataSet GetDataSet(string sql, CommandType commandType)
        {
           
            FbDataAdapter adap = new FbDataAdapter(GetCommand(sql, commandType));
            DataSet dataSet = new DataSet();
            adap.Fill(dataSet);
            //definir tratamento e log de erros 
            adap.SelectCommand.Connection.Close();
            adap.SelectCommand.Connection.Dispose();
            adap.Dispose();

            return dataSet;
        }
         */
        
        public  FbCommand GetCommand(string sql, CommandType commandType)
        {
            FbCommand command = new FbCommand();
            command.CommandText = sql;
            command.CommandType = commandType;
            command.Connection = GetConnection();
            return command;
        }

        public static FbConnection GetConnection()
        {

            string ConnectionStrings = sMontaStringConexao(); //ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();


            FbConnection conn = new FbConnection(ConnectionStrings);
            try
            {
                conn.Open();
                
            }

            catch (FbException ex)
            {
                //logar erro de abertura de conexão
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return conn;
        }

        public FbConnection pGetConnection()
        {

            string ConnectionStrings = sMontaStringConexao(); //ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();


            FbConnection conn = new FbConnection(ConnectionStrings);
            try
            {
                conn.Open();
                return conn;
            }

            catch (FbException ex)
            {
                //logar erro de abertura de conexão
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;
        }

        public FbDataReader ExecuteReader(string sql, CommandType commandType)
        {
            FbCommand command = GetCommand(sql, commandType);
            //definir tratamento e log de erros 

            FbDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);

            return reader;
        }

        public void ExecuteQuery(string sql, CommandType commandType)
        {

            FbCommand command = GetCommand(sql, commandType);
            //definir tratamento e log de erros 
            command.ExecuteNonQuery();
        }

        public static void ExecuteQuery(FbCommand command)
        {
            command.ExecuteNonQuery();
        }
        public static FbDataReader ExecuteReader(FbCommand command)
        {
            //definir tratamento e log de erros 

            FbDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);

            return reader;
        }

        public static FbCommand GetCommand(string sql, CommandType commandType, FbConnection connection)
        {

            FbCommand command = new FbCommand();
            command.CommandText = sql;
            command.CommandType = commandType;
            command.Connection = connection;
            return command;
        }
        /*
        public static DataSet GetDataSet(string sql, CommandType commandType, int timeout)
        {

            FbDataAdapter adap = new FbDataAdapter(GetCommand(sql, commandType));
            DataSet dataSet = new DataSet();
            adap.SelectCommand.CommandTimeout = timeout;
            adap.Fill(dataSet);
            //definir tratamento e log de erros 
            adap.SelectCommand.Connection.Close();
            adap.SelectCommand.Connection.Dispose();
            adap.Dispose();
            return dataSet;
        }
         */

        public DataTable GetDataTable(string sql, CommandType commandType)
        {
            FbDataAdapter adap = new FbDataAdapter(GetCommand(sql, commandType));
            DataTable dataTable = new DataTable();
            adap.Fill(dataTable);
            //definir tratamento e log de erros 
            adap.SelectCommand.Connection.Close();
            adap.SelectCommand.Connection.Dispose();
            adap.Dispose();

            return dataTable;
        }

        public static DataTable GetDataTable(FbCommand command)
        {
            FbDataAdapter adap = new FbDataAdapter(command);
            DataTable dataTable = new DataTable();
            adap.Fill(dataTable);
            //definir tratamento e log de erros 
            adap.SelectCommand.Connection.Close();
            adap.SelectCommand.Connection.Dispose();
            adap.Dispose();

            return dataTable;
        }
        // execute a command and returns the results as a DataTable object
        public static DataTable ExecuteSelectCommand(DbCommand command)
        {
            // The DataTable to be returned 
            DataTable table;
            // Execute the command making sure the connection gets closed in the end
            try
            {                
                // Open the data connection 
                command.Connection.Open();
                // Execute the command and save the results in a DataTable
                DbDataReader reader = command.ExecuteReader();
                table = new DataTable();
                table.Load(reader);
                // Close the reader 
                reader.Close();
            }
            catch (Exception ex)
            {
                //Utilities.LogError(ex);
                throw ex;
            }
            finally
            {
                // Close the connection
                command.Connection.Close();
            }
            return table;
        }

        // execute an update, delete, or insert command 
        // and return the number of affected rows
        public int ExecuteNonQuery(DbCommand command)
        {
            // The number of affected rows 
            int affectedRows = -1;
            // Execute the command making sure the connection gets closed in the end
            try
            {
                // Open the connection of the command
                command.Connection.Open();
                // Execute the command and get the number of affected rows
                affectedRows = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                // Log eventual errors and rethrow them
                //Utilities.LogError(ex);
                throw ex;
            }
            finally
            {
                // Close the connection
                command.Connection.Close();
            }
            // return the number of affected rows
            return affectedRows;
        }

        public  string ExecuteScalar(DbCommand command)
        {
            // The number of affected rows 
            string strReturnedValue = "-1";
            // Execute the command making sure the connection gets closed in the end
            try
            {
                // Open the connection of the command
                if (command.Connection.State != ConnectionState.Open)
                {
                    command.Connection.Open();
                }
                // Execute the command and get the number of affected rows
                strReturnedValue = System.Convert.ToString(command.ExecuteScalar());
            }
            catch (Exception ex)
            {
                // Log eventual errors and rethrow them
                //Utilities.LogError(ex);
                throw ex;
            }
            finally
            {
                // Close the connection
                command.Connection.Close();
            }
            // return the number of affected rows
            return strReturnedValue;
        }


        // creates and prepares a new DbCommand object on a new connection
        

        public static FbCommand CreateCommand(CommandType command)
        {            
            // Obtain the database provider name            
            string dataProviderName = "FirebirdSql.Data.FirebirdClient";
            // Obtain the database connection string
            string connectionString = sMontaStringConexao();
            // Create a new data provider factory
            DbProviderFactory factory = DbProviderFactories.GetFactory(dataProviderName);
            // Obtain a database specific connection object
            DbConnection conn = factory.CreateConnection();
            // Set the connection string
            conn.ConnectionString = connectionString;
            // Create a database specific command object

            FbCommand comm = (FbCommand)conn.CreateCommand();
            // Set the command type to stored procedure
            comm.CommandType = command;
            // Return the initialized command object
            return comm;
        }

        public static string sMontaStringConexao()
        {
            Globais MontaStringConexao = new Globais();
            StringBuilder sbConexao = new StringBuilder();

            sbConexao.Append("User =");
            sbConexao.Append(MontaStringConexao.LeRegWin("Usuario"));
            sbConexao.Append(";");
            sbConexao.Append("Password=");
            sbConexao.Append(MontaStringConexao.LeRegWin("Senha"));
            sbConexao.Append(";");
            sbConexao.Append("Database=");
            string sdatabase = MontaStringConexao.LeRegWin("BancoDados");
            sbConexao.Append(sdatabase);
            sbConexao.Append(";");
            sbConexao.Append("DataSource=");
            sbConexao.Append(MontaStringConexao.LeRegWin("Servidor"));
            sbConexao.Append(";");
            sbConexao.Append("Port=3050;Dialect=3; Charset=NONE;Role=;Connection lifetime=15;Pooling=true; MinPoolSize=0;MaxPoolSize=50;Packet Size=8192;ServerType=0;");


            return (string)sbConexao.ToString();
        }

    }
    public class Globais
    {

        public string LeRegWin(string NomeChave)
        {
            string Retorno = "";
            /*
            RegistryKey Registro = Registry.CurrentConfig.OpenSubKey("HLP\\", true);
            foreach (string subKeyName in Registro.GetSubKeyNames())
            {
                using (RegistryKey
                    tempKey = Registro.OpenSubKey(subKeyName))
                {
                    foreach (string valueName in tempKey.GetValueNames())
                    {
                        Retorno = tempKey.GetValue(valueName).ToString();
                        if (subKeyName == NomeChave)
                        {
                            break;
                        }
                    }
                }
                if (subKeyName == NomeChave)
                {
                    break;
                }

            }
             */
            string path = "HLPNfeConfig.xml";

            if (File.Exists(path))
            {
                XmlTextReader reader = new XmlTextReader(path);
                while (reader.Read())
                {
                    if ((reader.NodeType != XmlNodeType.Element) || !(reader.Name == "nfe_configuracoes"))
                    {
                        continue;
                    }
                    while (reader.Read())
                    {
                        if (reader.NodeType == XmlNodeType.Element)
                        {
                            //Claudinei - o.s. 23507 - 08/06/2009
                            if (reader.Name == NomeChave)
                            {
                                reader.Read();
                                Retorno = reader.Value;
                                continue;
                            }


                            //Fim - Claudinei - o.s. 23507 - 08/06/2009
                        }
                    }
                }
                reader.Close();
            }
            //}


            return Retorno;
        }
    }

}