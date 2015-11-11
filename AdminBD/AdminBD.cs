using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Data.OracleClient;


namespace AdminBD
{
    public class AdminBD
    {

        private SqlConnection dbConnection;

        private OracleConnection dbConnectionOra;

        private SqlCommand dbCommand;

        private OracleCommand dbCommandOra;

        private SqlDataReader dbDataReader;

        private OracleDataReader dbDataReaderOra;

        // 
        private DataTable dbDataTable;

        private DataSet dbDataSet;

        private SqlDataAdapter dbDataAdapter;

        private OracleDataAdapter dbDataAdapterOra;

        // 
        private string cadenaConexion = "";

        private string cadenaSelect;

        // 
        // 
        private string archivoDatos;

        private string nombreTabla = "";

        private string _nomBD;

        private string _serv;

        private string _us;

        private string _pwd;

        private int _tipoBD = 0;

        public AdminBD()
        {
        }

        public AdminBD(string nomBD, string serv, string us, string pwd, int tipoBD)
        {
            this.nomBD = nomBD;
            // Warning!!! Optional parameters not supported
            this.serv = serv;
            this.us = us;
            this.pwd = pwd;
            this.tipoBD = tipoBD;
        }

        public int tipoBD
        {
            get
            {
                return _tipoBD;
            }
            set
            {
                _tipoBD = value;
            }
        }

        public string us
        {
            get
            {
                return _us;
            }
            set
            {
                _us = value;
            }
        }

        public string pwd
        {
            get
            {
                return _pwd;
            }
            set
            {
                _pwd = value;
            }
        }

        public string serv
        {
            get
            {
                return _serv;
            }
            set
            {
                _serv = value;
            }
        }

        public string nomBD
        {
            get
            {
                return _nomBD;
            }
            set
            {
                _nomBD = value;
            }
        }

        public bool abrirBD()
        {
            if ((cadenaConexion == ""))
            {
                switch (tipoBD)
                {
                    case 0:
                        // "SQLServer"
                        cadenaConexion = ("data source="
                                    + (this.serv + (";" + ("initial catalog="
                                    + (this.nomBD + (";" + ("persist security info=True;" + ("user id="
                                    + (this.us + (";" + ("Password="
                                    + (this.pwd + ""))))))))))));
                        break;
                    case 1:
                        // "SQLServer"
                        cadenaConexion = ("data source="
                                    + (this.serv + (";" + ("initial catalog="
                                    + (this.nomBD + (";" + ("Persist Security Info=True;" + ("user id="
                                    + (this.us + (";" + ("Password="
                                    + (this.pwd + ""))))))))))));
                        break;
                    case 2:
                        // "Oracle"
                        cadenaConexion = ("data source="
                                    + (this.serv + (";" + ("user id="
                                    + (this.us + (";" + ("Password="
                                    + (this.pwd + (";" + "Integrated Security = no")))))))));
                        break;
                }
            }

            if (!Conectar())
            {
                return false;

            }

            return true;
        }

        public bool cerrarBD()
        {
            //  Cerrar la conexión
            try
            {
                switch (tipoBD)
                {
                    case 0:
                        // "SQLServer"
                        if (dbConnection != null && dbConnection.State == ConnectionState.Open)
                        {
                            dbConnection.Close();
                        }

                        break;
                    case 1:
                        // "SQLServer"
                        if (dbConnection != null && dbConnection.State == ConnectionState.Open)
                        {
                            dbConnection.Close();
                        }

                        break;
                    case 2:
                        // "Oracle"
                        if (dbConnectionOra != null && dbConnectionOra.State == ConnectionState.Open)
                        {
                            dbConnectionOra.Close();
                        }

                        break;
                }
            }
            catch (System.Exception End)
            {
                return false;
            }
            return true;
        }

        bool Conectar()
        {
            bool bol = false;
            try
            {
                switch (tipoBD)
                {
                    case 0:
                        // "SQLServer"
                        if ((dbConnection == null))
                        {
                            bol = true;
                        }
                        else if ((dbConnection.State == ConnectionState.Closed))
                        {
                            bol = true;
                        }

                        if (bol)
                        {
                            dbConnection = new System.Data.SqlClient.SqlConnection(cadenaConexion);
                            dbConnection.Open();
                        }

                        break;
                    case 1:
                        // "SQLServer"
                        if ((dbConnection == null))
                        {
                            bol = true;
                        }
                        else if ((dbConnection.State == ConnectionState.Closed))
                        {
                            bol = true;
                        }

                        if (bol)
                        {
                            dbConnection = new System.Data.SqlClient.SqlConnection(cadenaConexion);
                            dbConnection.Open();
                        }

                        break;
                    case 2:
                        // "Oracle"
                        if ((dbConnectionOra == null))
                        {
                            bol = true;
                        }
                        else if ((dbConnectionOra.State == ConnectionState.Closed))
                        {
                            bol = true;
                        }

                        if (bol)
                        {
                            dbConnectionOra = new System.Data.OracleClient.OracleConnection(cadenaConexion);
                            dbConnectionOra.Open();
                        }

                        break;
                }
            }
            catch (Exception e)
            {
                // MessageBox.Show("Error al crear la conexi�n:" & vbCrLf & e.Message)
                return false;
            }

            return true;
        }



        public bool executeSQL(string sql)
        {
            try
            {
                SqlCommand command;
                switch (tipoBD)
                {
                    case 1:
                        // "SQLServer"
                        command = new SqlCommand(sql, dbConnection);
                        command.ExecuteNonQuery();
                        break;
                    case 0:
                        // "SQLServer"
                        command = new SqlCommand(sql, dbConnection);
                        command.ExecuteNonQuery();
                        break;
                    case 2:
                        // "Oracle"
                        OracleCommand commandora = new OracleCommand(sql, dbConnectionOra);
                        commandora.ExecuteNonQuery();
                        break;
                }
            }
            catch (Exception ex)
            {
                // MessageBox.Show("Error al crear la conexi�n:" & vbCrLf & e.Message)
                return false;
            }

            return true;
        }

        public object getDataReader(string sql)
        {
            try
            {
                SqlCommand command;
                SqlDataReader reader;
                switch (tipoBD)
                {
                    case 0:
                        // "SQLServer"
                        command = new SqlCommand(sql, dbConnection);
                        reader = command.ExecuteReader();
                        return reader;
                        break;
                    case 1:
                        // "SQLServer"
                        command = new SqlCommand(sql, dbConnection);
                        reader = command.ExecuteReader();
                        return reader;
                        break;
                    case 2:
                        // "Oracle"
                        OracleCommand commandora = new OracleCommand(sql, dbConnectionOra);
                        OracleDataReader readerora = commandora.ExecuteReader();
                        return readerora;
                        break;
                }
            }
            catch (Exception ex)
            {
                // MessageBox.Show("Error al crear la conexi�n:" & vbCrLf & e.Message)
                return null;
            }
            return null;

        }

        // Public Function getDataSet(ByVal sql As String) As DataSet
        //     Dim command As New SqlCommand(sql, dbConnection)
        //     Dim reader As SqlDataReader = command.ExecuteReader()
        //     Return reader
        // End Function
        public ArrayList getColRegString(string sql)
        {
            ArrayList colGen = new ArrayList();
            ArrayList arr;
            object reader = this.getDataReader(sql);
            try
            {
                if (tipoBD == 0 || tipoBD == 1)
                { //Sql Server
                    while (((SqlDataReader)reader).Read())
                    {
                        arr = new ArrayList();
                        int cont2 = 0;
                        while (((SqlDataReader)reader).FieldCount > cont2)
                        {
                            arr.Add(((SqlDataReader)reader).GetSqlValue(cont2).ToString());
                            cont2 = (cont2 + 1);
                        }

                        colGen.Add(arr);
                    }
                    if (!((SqlDataReader)reader).IsClosed)
                    {
                        ((SqlDataReader)reader).Close();
                    }
                }
                else if (tipoBD == 2)
                {//Oracle
                    while (((OracleDataReader)reader).Read())
                    {
                        arr = new ArrayList();
                        int cont2 = 0;
                        while (((OracleDataReader)reader).FieldCount > cont2)
                        {
                            arr.Add(((OracleDataReader)reader).GetOracleValue(cont2).ToString());
                            cont2 = (cont2 + 1);
                        }

                        colGen.Add(arr);
                    }
                    if (!((OracleDataReader)reader).IsClosed)
                    {
                        ((OracleDataReader)reader).Close();
                    }
                }

            }
            catch (Exception e)
            {
                //  Always call Close when done reading.
                return null;
            }

            return colGen;
        }

        public object getColRegResultSet(string sql)
        {
            return this.getDataReader(sql);
        }

        public void addParametroProcedimiento(string nom, string val, string tip, int tam)
        {
            switch (tipoBD)
            {
                case 1:
                case 0:
                    // "SQLServer"
                    this.addParProcedimiento(nom, val, tip, tam);
                    break;
                case 2:
                    // "Oracle"
                    this.addParProcedimientoOra(nom, val, tip, tam);
                    break;
            }
        }

        public void addParametroOutProcedimiento(string nom, object val, string tip, int tam)
        {
            switch (tipoBD)
            {
                case 1:
                case 0:
                    // "SQLServer"
                    // addParProcedimiento(nom, val, tip, tam)
                    break;
                case 2:
                    // "Oracle"
                    this.addParOutProcedimientoOra(nom, val, tip, tam);
                    break;
            }
        }

        private void addParProcedimientoOra(string nom, string val, string tip, int tam)
        {
            switch (tip)
            {
                case "VARCHAR":
                    dbCommandOra.Parameters.Add(new OracleParameter(nom, OracleType.VarChar)).Value = val;
                    // dbCommandOra.Parameters.Add(nom, OracleType.VarChar, tam).Value = val
                    break;
                case "VARCHAR2":
                    dbCommandOra.Parameters.Add(new OracleParameter(nom, OracleType.VarChar)).Value = val;
                    // dbCommandOra.Parameters.Add(nom, OracleType.VarChar, tam).Value = val
                    break;
                case "CHAR":
                    dbCommandOra.Parameters.Add(new OracleParameter(nom, OracleType.Char)).Value = val;
                    // dbCommandOra.Parameters.Add(nom, OracleType.Char, tam).Value = val
                    break;
                case "NUMBERDEC":
                    dbCommandOra.Parameters.Add(new OracleParameter(nom, OracleType.Number)).Value = this.strToDouble(val);
                    // dbCommandOra.Parameters.Add(nom, OracleType.Number, tam).Value = val
                    break;
                case "NUMBER":
                    dbCommandOra.Parameters.Add(new OracleParameter(nom, OracleType.Number)).Value = this.strToInt(val);
                    // dbCommandOra.Parameters.Add(nom, OracleType.Number, tam).Value = val
                    break;
                case "INTEGER":
                    dbCommandOra.Parameters.Add(new OracleParameter(nom, OracleType.Int32)).Value = this.strToInt(val);
                    // dbCommandOra.Parameters.Add(nom, OracleType.Int32, tam).Value = val
                    break;
                case "DATE":
                    dbCommandOra.Parameters.Add(new OracleParameter(nom, OracleType.DateTime)).Value = this.strToDate(val);
                    // dbCommandOra.Parameters.Add(nom, OracleType.DateTime, tam).Value = val
                    break;
                case "BYTE":
                    dbCommandOra.Parameters.Add(new OracleParameter(nom, OracleType.Byte)).Value = byte.Parse(val);
                    break;
                case "RESULT":
                    dbCommandOra.Parameters.Add(new OracleParameter(nom, OracleType.Cursor)).Direction = ParameterDirection.Output;
                    break;
            }
        }

        private void addParOutProcedimientoOra(string nom, object val, string tip, int tam)
        {
            switch (tip)
            {
                case "VARCHAR":
                    dbCommandOra.Parameters.Add(new OracleParameter(nom, OracleType.VarChar)).Direction = ParameterDirection.Output;
                    // dbCommandOra.Parameters.Add(nom, OracleType.VarChar, tam).Value = val
                    break;
                case "VARCHAR2":
                    dbCommandOra.Parameters.Add(new OracleParameter(nom, OracleType.VarChar)).Direction = ParameterDirection.Output;
                    // dbCommandOra.Parameters.Add(nom, OracleType.VarChar, tam).Value = val
                    break;
                case "CHAR":
                    dbCommandOra.Parameters.Add(new OracleParameter(nom, OracleType.Char)).Direction = ParameterDirection.Output;
                    // dbCommandOra.Parameters.Add(nom, OracleType.Char, tam).Value = val
                    break;
                case "NUMBERDEC":
                    dbCommandOra.Parameters.Add(new OracleParameter(nom, OracleType.Number)).Direction = ParameterDirection.Output;
                    // dbCommandOra.Parameters.Add(nom, OracleType.Number, tam).Value = val
                    break;
                case "NUMBER":
                    dbCommandOra.Parameters.Add(new OracleParameter(nom, OracleType.Number)).Direction = ParameterDirection.Output;
                    // dbCommandOra.Parameters.Add(nom, OracleType.Number, tam).Value = val
                    break;
                case "INTEGER":
                    dbCommandOra.Parameters.Add(new OracleParameter(nom, OracleType.Int32)).Direction = ParameterDirection.Output;
                    // dbCommandOra.Parameters.Add(nom, OracleType.Int32, tam).Value = val
                    break;
                case "DATE":
                    dbCommandOra.Parameters.Add(new OracleParameter(nom, OracleType.DateTime)).Direction = ParameterDirection.Output;
                    // dbCommandOra.Parameters.Add(nom, OracleType.DateTime, tam).Value = val
                    break;
                case "BYTE":
                    dbCommandOra.Parameters.Add(new OracleParameter(nom, OracleType.Byte)).Direction = ParameterDirection.Output;
                    // dbCommandOra.Parameters.Add(nom, OracleType.Byte, tam).Value = val
                    break;
                case "RESULT":
                    dbCommandOra.Parameters.Add(new OracleParameter(nom, OracleType.Cursor)).Direction = ParameterDirection.Output;
                    break;
            }
        }

        private int strToInt(string val)
        {
            try
            {
                return int.Parse(val);
            }
            catch (Exception ex)
            {
                return 0;
            }

        }

        private double strToDouble(string val)
        {
            try
            {
                return Convert.ToDouble(val);
            }
            catch (Exception ex)
            {
                return 0;
            }

        }

        private DateTime strToDate(string val)
        {
            DateTime dat = new DateTime();
            try
            {
                DateTime.TryParse(val, out dat);
                return dat;
            }
            catch (Exception e)
            {
                return dat;
            }

        }

        private void addParProcedimiento(string nom, string val, string tip, int tam)
        {
            switch (tip)
            {
                case "VARCHAR":
                    dbCommand.Parameters.Add(nom, SqlDbType.VarChar).Value = val;
                    // dbCommand.Parameters.Add(nom, SqlDbType.VarChar, tam).Value = val
                    break;
                case "CHAR":
                    dbCommand.Parameters.Add(nom, SqlDbType.Char).Value = val;
                    // dbCommand.Parameters.Add(nom, SqlDbType.Char, tam).Value = val
                    break;
                case "NUMBER":
                    dbCommand.Parameters.Add(nom, SqlDbType.Int).Value = this.strToInt(val);
                    // dbCommand.Parameters.Add(nom, SqlDbType.Int, tam).Value = val
                    break;
                case "INTEGER":
                    dbCommand.Parameters.Add(nom, SqlDbType.Int).Value = this.strToInt(val);
                    // dbCommand.Parameters.Add(nom, OracleType.Int, tam).Value = val
                    break;
                case "DATE":
                    dbCommand.Parameters.Add(nom, SqlDbType.DateTime).Value = this.strToDate(val);
                    // dbCommand.Parameters.Add(nom, SqlDbType.DateTime, tam).Value = val
                    break;
                case "BYTE":
                    dbCommand.Parameters.Add(nom, SqlDbType.Bit).Value = byte.Parse(val);
                    break;
            }
        }

        public int ejecutaProcedimiento()
        {
            switch (tipoBD)
            {
                case 0:
                    // "SQLServer"
                    return dbCommand.ExecuteNonQuery();
                    break;
                case 1:
                    // "SQLServer"
                    return dbCommand.ExecuteNonQuery();
                    break;
                case 2:
                    // "Oracle"
                    return dbCommandOra.ExecuteNonQuery();
                    break;
            }
            return -1;
        }

        public ArrayList ejecutaProcedimientoRes()
        {
            ArrayList colGen = new ArrayList();
            ArrayList arr;
            object reader = this.getResultProcedimiento();
            try
            {
                if (tipoBD == 0 || tipoBD == 1)
                {
                    while (((SqlDataReader)reader).Read())
                    {
                        arr = new ArrayList();
                        int cont2 = 0;
                        while ((((SqlDataReader)reader).FieldCount > cont2))
                        {
                            arr.Add(((SqlDataReader)reader).GetString(cont2));
                            cont2 = (cont2 + 1);
                        }

                        colGen.Add(arr);
                    }
                    if (!((SqlDataReader)reader).IsClosed)
                    {
                        ((SqlDataReader)reader).Close();
                    }
                }
                else if (tipoBD == 0 || tipoBD == 1)
                {
                    while (((OracleDataReader)reader).Read())
                    {
                        arr = new ArrayList();
                        int cont2 = 0;
                        while ((((OracleDataReader)reader).FieldCount > cont2))
                        {
                            arr.Add(((OracleDataReader)reader).GetString(cont2));
                            cont2 = (cont2 + 1);
                        }

                        colGen.Add(arr);
                    }
                    if (!((OracleDataReader)reader).IsClosed)
                    {
                        ((OracleDataReader)reader).Close();
                    }
                }

            }
            catch (Exception e)
            {
                //  Always call Close when done reading.
            }

            return colGen;
        }

        public object ejecutaProcedimientoResultSet()
        {
            return this.getResultProcedimiento();
        }

        public object getResultProcedimiento()
        {
            try
            {
                SqlDataReader reader;
                switch (tipoBD)
                {
                    case 0:
                        // "SQLServer"
                        reader = dbCommand.ExecuteReader();
                        return reader;
                        break;
                    case 1:
                        // "SQLServer"
                        reader = dbCommand.ExecuteReader();
                        return reader;
                        break;
                    case 2:
                        // "Oracle"
                        OracleDataReader readerora = dbCommandOra.ExecuteReader();
                        return readerora;
                        break;
                }
            }
            catch (Exception ex)
            {
                // MessageBox.Show("Error al crear la conexi�n:" & vbCrLf & e.Message)
                return null;
            }
            return null;

        }

        public void procedimiento(string proc)
        {
            try
            {
                switch (tipoBD)
                {
                    case 0:
                        // "SQLServer"
                        dbCommand = new SqlCommand();
                        dbCommand.CommandType = CommandType.StoredProcedure;
                        dbCommand.CommandText = proc;
                        dbCommand.Connection = dbConnection;
                        break;
                    case 1:
                        // "SQLServer"
                        dbCommand = new SqlCommand();
                        dbCommand.CommandType = CommandType.StoredProcedure;
                        dbCommand.CommandText = proc;
                        dbCommand.Connection = dbConnection;
                        break;
                    case 2:
                        // "Oracle"
                        dbCommandOra = new OracleCommand();
                        dbCommandOra.CommandType = CommandType.StoredProcedure;
                        dbCommandOra.CommandText = proc;
                        dbCommandOra.Connection = dbConnectionOra;
                        break;
                }
            }
            catch (Exception e)
            {
                // MessageBox.Show("Error al crear la conexi�n:" & vbCrLf & e.Message)
                return;
            }

            return;
        }

        public bool ejecutaTransaccion(ArrayList querys)
        {

            return true;
        }

        public ArrayList getRegistros(object obj)
        {
            return null;
        }

        public int getNumColumnasTabla(string nomTabla)
        {
            if ((nomTabla == ""))
            {
                nomTabla = nombreTabla;
                // Warning!!! Optional parameters not supported
            }

            return dbDataSet.Tables[nomTabla].Columns.Count;
        }

        public ArrayList getColReg(string sql)
        {
            ArrayList colGen = new ArrayList();
            ArrayList arr;
            try
            {
                if (tipoBD == 0 || tipoBD == 1)
                {
                    SqlCommand command = new SqlCommand(sql, dbConnection);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        arr = new ArrayList();
                        int cont2 = 0;
                        while (!(reader.FieldCount > cont2))
                        {
                            arr.Add(reader[cont2]);
                            cont2 = (cont2 + 1);
                        }

                        colGen.Add(arr);
                    }
                    reader.Close();
                }
                else if (tipoBD == 2)
                {
                    OracleCommand command = new OracleCommand();
                    OracleDataReader reader = dbCommandOra.ExecuteReader();
                    while (reader.Read())
                    {
                        arr = new ArrayList();
                        int cont2 = 0;
                        while (!(reader.FieldCount > cont2))
                        {
                            arr.Add(reader[cont2]);
                            cont2 = (cont2 + 1);
                        }

                        colGen.Add(arr);
                    }
                    reader.Close();

                }

            }
            catch (Exception e)
            {
                //  Always call Close when done reading.
            }

            return colGen;
        }

        public ArrayList getNombresColumnas(string nomTabla)
        {
            System.Data.DataColumn columna;
            // Warning!!! Optional parameters not supported
            int i;
            int j;
            ArrayList nomCol = new ArrayList();
            // 
            if ((nomTabla == ""))
            {
                nomTabla = nombreTabla;
            }

            j = (dbDataSet.Tables[nomTabla].Columns.Count - 1);
            for (i = 0; (i <= j); i++)
            {
                columna = dbDataSet.Tables[nomTabla].Columns[i];
                ArrayList colum = new ArrayList();
                colum.Add(columna.ColumnName);
                colum.Add("" + columna.DataType);
            }

            return nomCol;
        }

        public ArrayList getNombresTablas(string nombreBase)
        {
            ArrayList nomTablas = new ArrayList();
            // Warning!!! Optional parameters not supported
            System.Data.DataTable dataTable = new System.Data.DataTable();
            int i;
            // 
            System.Data.SqlClient.SqlDataAdapter schemaDA = new System.Data.SqlClient.SqlDataAdapter(("SELECT * FROM INFORMATION_SCHEMA.TABLES " + ("WHERE TABLE_TYPE = \'BASE TABLE\' " + "ORDER BY TABLE_TYPE")), dbConnection);
            // 
            schemaDA.Fill(dataTable);
            i = (dataTable.Rows.Count - 1);
            if ((i > -1))
            {
                for (i = 0; i <= (dataTable.Rows.Count - 1); i++)
                {
                    nomTablas.Add(dataTable.Rows[i]["TABLE_NAME"].ToString());
                }

            }

            //  
            return nomTablas;
        }
    }
}
