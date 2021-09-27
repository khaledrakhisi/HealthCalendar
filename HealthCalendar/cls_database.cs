using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCalendar
{
    class cls_database
    {
        /// <summary>
        /// 
        /// </summary>        
        public string databaseFileName { get { return _databaseFileName; } }
        private string _databaseFileName;

        /// <summary>
        /// 
        /// </summary>
        public string connectionString { get { return _connectionString; } }
        private string _connectionString;


        private OleDbConnection OleDbConnection1 = null;
        private OleDbCommand OleDbCommand1 = null;
        private OleDbDataAdapter adapter = null;

        public cls_database(string sDBFileName)
        {
            _databaseFileName = sDBFileName;
            _connectionString = string.Format(@"Provider=Microsoft.ACE.Oledb.12.0;Data Source={0}", _databaseFileName);
            //Provider=Microsoft.ACE.Oledb.15.0

            OleDbConnection1 = new OleDbConnection(_connectionString);
            OleDbCommand1 = new OleDbCommand();
            
        }

        /// <summary>
        /// This method runs a select command.
        /// </summary>
        /// <param name="sTableName">The physical table name</param>
        /// <returns></returns>
        //public DataSet Select(string sTableName,string sWhere = "", string sSortColumnName = "", string sSortType = "ASC")
        //{
        //    StringBuilder sSql = new StringBuilder();
        //    sSql.Append("SELECT * FROM ");
        //    sSql.Append(sTableName);
        //    if (sWhere != "")
        //    {
        //        sSql.Append(" Where ");
        //        sSql.Append(sWhere);
        //        sSql.Append(" ");                
        //    }
        //    if (sSortColumnName != "")
        //    {
        //        sSql.Append(" Order by ");
        //        sSql.Append(sSortColumnName);
        //        sSql.Append(" ");
        //        sSql.Append(sSortType);
        //    }

        //    OleDbCommand1.CommandType = CommandType.Text;
        //    OleDbCommand1.CommandText = sSql.ToString();
        //    OleDbCommand1.Connection = OleDbConnection1;
        //    adapter = new OleDbDataAdapter(OleDbCommand1);
        //    using (DataSet DataSet1 = new DataSet())
        //    {
        //        adapter.Fill(DataSet1, sTableName);

        //        return DataSet1;
        //    }
        //}


        /// <summary>
        /// This method runs a select command.
        /// </summary>
        /// <param name="sTableName">The physical table name</param>
        /// <returns></returns>
        public DataTable Select(string sTableName, string sWhere = "", string sSortColumnName = "", string sSortType = "ASC", int nRecordsCount = -1)
        {
            try
            {
                StringBuilder sSql = new StringBuilder();
                sSql.Append("SELECT ");
                if (nRecordsCount != -1)
                {
                    sSql.Append(" TOP " + nRecordsCount.ToString());
                }
                sSql.Append(" * FROM ");
                sSql.Append(sTableName);
                if (sWhere != "")
                {
                    sSql.Append(" WHERE ");
                    sSql.Append(sWhere);
                    sSql.Append(" ");
                }
                if (sSortColumnName != "")
                {
                    sSql.Append(" ORDER BY ");
                    sSql.Append(sSortColumnName);
                    sSql.Append(" ");
                    sSql.Append(sSortType);
                }

                OleDbCommand1.CommandType = CommandType.Text;
                OleDbCommand1.CommandText = sSql.ToString();
                OleDbCommand1.Connection = OleDbConnection1;
                adapter = new OleDbDataAdapter(OleDbCommand1);
                using (DataTable DataTable1 = new DataTable())
                {
                    adapter.Fill(DataTable1);

                    return DataTable1;
                }
            }
            catch (Exception ex)
            {
                cls_utility.Log("Database Error. Select Method. " + ex.Message);
            }
            return null;
        }        


        public void UpdateData(DataSet dataset, string sTableName)
        {
            try
            {
                OleDbCommandBuilder commandBuilder = new OleDbCommandBuilder(adapter);

                adapter.UpdateCommand = commandBuilder.GetUpdateCommand();
                adapter.InsertCommand = commandBuilder.GetInsertCommand();
                adapter.Update(dataset, sTableName);
            }
            catch (Exception ex)
            {
                cls_utility.Log("Database Error. UpdatingData. " + ex.Message);
            }
        }

        public void UpdateData(DataTable datatable)
        {
            try
            {
                OleDbCommandBuilder commandBuilder = new OleDbCommandBuilder(adapter);
                commandBuilder.QuotePrefix = "[";
                commandBuilder.QuoteSuffix = "]";
                adapter.UpdateCommand = commandBuilder.GetUpdateCommand();
                adapter.InsertCommand = commandBuilder.GetInsertCommand();
                adapter.Update(datatable);
            }
            catch (Exception ex)
            {
                cls_utility.Log("Database Error. UpdatingData. " + ex.Message);
            }
        }
    }
}
