using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
//using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using System.Data.SqlClient;
using System.Collections;
using System.Data;
using System.Data.SqlTypes;
using Maticsoft.DBUtility;


namespace linkTimer
{
    /// <summary>
    /// 数据访问类
    /// </summary>
    public class DbHelperSQL
    {

        private static log4net.ILog mo_Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #region ���Τ�k
        /// <summary>
        ///  �����Y����쪺�̤j��
        /// </summary>
        /// <param name="FieldName"></param>
        /// <param name="TableName"></param>
        /// <returns></returns> 
        public static int GetMaxID(string FieldName, string TableName)
        {
            string strSql = "select max(" + FieldName + ")+1 from " + TableName;
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql);
            object obj = db.ExecuteScalar(dbCommand);
            if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
            {
                return 1;
            }
            else
            {
                return int.Parse(obj.ToString());
            }
        }
        /// <summary>
        /// �˴��@�ӰO���O�_�s�b(SQL�y�y�覡)
        /// </summary>
        /// <param name="strSql"></param>
        /// <returns></returns>
        public static bool Exists(string strSql)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql);
            object obj = db.ExecuteScalar(dbCommand);
            int cmdresult;
            if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
            {
                cmdresult = 0;
            }
            else
            {
                cmdresult = int.Parse(obj.ToString());
            }
            if (cmdresult == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        /// <summary>
        /// 是否存在(SqlParameter语句参数)
       /// </summary>
       /// <param name="strSql"></param>
       /// <param name="cmdParms"></param>
       /// <returns></returns>
        public static bool Exists(string strSql, params SqlParameter[] cmdParms)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql);
            BuildDBParameter(db, dbCommand, cmdParms);
            object obj = db.ExecuteScalar(dbCommand);
            int cmdresult;
            if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
            {
                cmdresult = 0;
            }
            else
            {
                cmdresult = int.Parse(obj.ToString());
            }
            if (cmdresult == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        /// <summary>
        /// 建立db参数
        /// </summary>
        /// <param name="db"></param>
        /// <param name="dbCommand"></param>
        /// <param name="cmdParms"></param>
        public static void BuildDBParameter(Database db, DbCommand dbCommand, params IDataParameter[] cmdParms)
        {
            foreach (IDataParameter sp in cmdParms)
            {
                //begin wind add by 2008/08/31 �P�wDateTime不是null�����p
                if (sp.DbType.Equals(DbType.DateTime))
                {
                    //�P�_�O�_��null
                    if (sp.Value != null)
                    {
                        if (sp.Value.Equals(DateTime.MinValue))//Modify By Lanny
                        {
                            db.AddInParameter(dbCommand, sp.ParameterName, sp.DbType, SqlDateTime.Null);
                        }
                        else
                        {
                            db.AddInParameter(dbCommand, sp.ParameterName, sp.DbType, sp.Value);
                        }
                    }
                    else
                    {
                        db.AddInParameter(dbCommand, sp.ParameterName, sp.DbType, sp.Value);
                    }

                }//end
                else
                {
                    db.AddInParameter(dbCommand, sp.ParameterName, sp.DbType, sp.Value);
                }
            }
        }
        #endregion

        #region  执行带参数的SQL语句

        /// <summary>
        /// 执行SQL语句，返回影响的记录数
        /// </summary>
        /// <param name="strSql">SQL语句</param>
        /// <returns>影响的记录数</returns>
        public static int ExecuteSql(string strSql)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetSqlStringCommand(strSql);
                return db.ExecuteNonQuery(dbCommand);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
        /// <summary>
        /// ����SQL�y�y�A��^�v�T���O����(�����ɶ��d�ߪ��y�y�A�]�m���ݮɶ��קK�d�߶W��)
       /// </summary>
       /// <param name="strSql"></param>
       /// <param name="Times"></param>
       /// <returns></returns>
        public static int ExecuteSqlByTime(string strSql, int Times)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql);
            dbCommand.CommandTimeout = Times;
            return db.ExecuteNonQuery(dbCommand);
        }
 	    /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
 	    /// </summary>
        /// <param name="SQLStringList">多条SQL语句</param>
        public static void ExecuteSqlParaTran(ArrayList SQLStringList)
        {

            Database db = DatabaseFactory.CreateDatabase();
            using (DbConnection dbconn = db.CreateConnection())
            {
                dbconn.Open();
                DbTransaction dbtran = dbconn.BeginTransaction();
                try
                {
                    //����y�y
                    for (int n = 0; n < SQLStringList.Count; n++)
                    {
                        string strsql = SQLStringList[n].ToString();
                        if (strsql.Trim().Length > 1)
                        {
                            DbCommand dbCommand = db.GetSqlStringCommand(strsql);
                            db.ExecuteNonQuery(dbCommand, dbtran);
                        }
                    }
                    //����s�x�L�{
                    dbtran.Commit();
                }
                catch(Exception ex)
                {
                    dbtran.Rollback();
                    throw new Exception(ex.ToString());
                }
                finally
                {
                    dbconn.Close();
                }
            }
        }
        /// <summary>
        /// 执行一条计算查询结果语句，返回查询结果（object）。
        /// </summary>
        /// <param name="strSql">计算查询结果语句</param>
        /// <returns>查询结果（object）</returns>
        public static object GetSingle(string strSql)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetSqlStringCommand(strSql);
                object obj = db.ExecuteScalar(dbCommand);
                if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                {
                    return null;
                }
                else
                {
                    return obj;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
        /// <summary>
        /// ����d�语句�A��^SqlDataReader ( �`�N�G�ϥΫ�@�w�n��SqlDataReader�i��Close )
        /// </summary>
        /// <param name="strSql">�d�语句</param>
        /// <returns>SqlDataReader</returns>
        public static SqlDataReader ExecuteReader(string strSql)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql);
            SqlDataReader dr = (SqlDataReader)db.ExecuteReader(dbCommand);
            return dr;

        }
        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="strSql">查询语句</param>
        /// <returns>DataSet</returns>
        public static DataSet Query(string strSql)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetSqlStringCommand(strSql);
                return db.ExecuteDataSet(dbCommand);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

        }
        /// <summary>
        /// (�����ɶ��d�ߪ��y�y�A�]�m���ݮɶ��קK�d�߶W��)
        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="Times"></param>
        /// <returns></returns>
        public static DataSet Query(string strSql, int Times)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql);
            dbCommand.CommandTimeout = Times;
            return db.ExecuteDataSet(dbCommand);
        }
        #endregion

        #region ����@�ӯS�����a�Ѽƪ��y�y
        /// <summary>
        /// 执行带一个存储过程参数的的SQL语句。
        /// </summary>
        /// <param name="strSql">SQL语句</param>
        /// <param name="content">参数内容,比如一个字段是格式复杂的文章，有特殊符号，可以通过这个方式添加[</param>
        /// <returns>影响的记录数</returns>
        public static int ExecuteSql(string strSql, string content)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql);
            db.AddInParameter(dbCommand, "@content", DbType.String, content);
            return db.ExecuteNonQuery(dbCommand);
        }
        /// <summary>
        /// ����a�@�Ӧs�x�L�{�Ѽƪ���SQL�y�y�C
        /// </summary>
        /// <param name="strSql">SQL�y�y</param>
        /// <param name="content">�ѼƤ��e,��p�@�����O�榡�������峹�A���S��Ÿ��A�i�H�q�L�o�Ӥ覡�K�[</param>
        /// <returns>��^�y�y�ت��d�ߵ��G</returns>
        public static object ExecuteSqlGet(string strSql, string content)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql);
            db.AddInParameter(dbCommand, "@content", DbType.String, content);
            object obj = db.ExecuteNonQuery(dbCommand);
            if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
            {
                return null;
            }
            else
            {
                return obj;
            }
        }
        /// <summary>
        /// �V��Ʈw�ش��J�Ϲ��榡�����(�M�W�����p�������t�@�ع��)
        /// </summary>
        /// <param name="strSql">SQL�y�y</param>
        /// <param name="fs">�Ϲ��줸��,��Ʈw�����������image�����p</param>
        /// <returns>�v�T���O����</returns>
        public static int ExecuteSqlInsertImg(string strSql, byte[] fs)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql);
            db.AddInParameter(dbCommand, "@fs", DbType.Byte, fs);
            return db.ExecuteNonQuery(dbCommand);
        }
        #endregion

        #region ����a�Ѽƪ�SQL�y�y
        /// <summary>
        /// ����SQL�y�y�A��^�v�T���O����
        /// </summary>
        /// <param name="strSql">SQL�y�y</param>
        /// <param name="cmdParms"></param>
        /// <returns>�v�T���O����</returns>
        public static int ExecuteSql(string strSql, params SqlParameter[] cmdParms)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetSqlStringCommand(strSql);
                BuildDBParameter(db, dbCommand, cmdParms);
                return db.ExecuteNonQuery(dbCommand);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
        /// <summary>
        /// ����h��SQL�y�y�A��{��Ʈw�ư�,��Ҧ�SQL�y�y��쫢�ƪ��
        /// </summary>
        /// <param name="SQLStringList">SQL�y�y�����ƪ�]key��SqlParameter[]�Avalue�O�ӻy�y��sql�y�y�^</param>
        public static void ExecuteSqlParaTran(Hashtable SQLStringList)
        {
            Database db = DatabaseFactory.CreateDatabase();
            using (DbConnection dbconn = db.CreateConnection())
            {
                dbconn.Open();
                DbTransaction dbtran = dbconn.BeginTransaction();
                try
                {
                    //����y�y
                    foreach (DictionaryEntry myDE in SQLStringList)
                    {
                        string strsql = myDE.Value.ToString();
                        SqlParameter[] cmdParms = (SqlParameter[])myDE.Key;
                        if (strsql.Trim().Length > 1)
                        {
                            DbCommand dbCommand = db.GetSqlStringCommand(strsql);
                            BuildDBParameter(db, dbCommand, cmdParms);
                            db.ExecuteNonQuery(dbCommand, dbtran);
                        }
                    }
                    dbtran.Commit();
                }
                catch (Exception ex)
                {
                    dbtran.Rollback();
                    throw new Exception(ex.ToString());
                }
                finally
                {
                    dbconn.Close();
                }
            }
        }
        /// <summary>
        /// ����h��SQL�y�y�A��{��Ʈw�ư�,��Ҧ�SQL�y�y��쫢�ƪ��
        /// </summary>
        /// <param name="SQLStringList">SQL�y�y�����ƪ�</param>
        ///<param name="SpOrSql">�]key��SqlParameter[]�Aif(SpOrSql) value�O�ӻy�y��sql�y�y else value�O�ӻy�y��sp�W�١^</param>
        public static void ExecuteSqlParaTran(Hashtable SQLStringList,bool SpOrSql)
        {
            Database db = DatabaseFactory.CreateDatabase();
            using (DbConnection dbconn = db.CreateConnection())
            {
                dbconn.Open();
                DbTransaction dbtran = dbconn.BeginTransaction();
                try
                {
                    //����y�y
                    foreach (DictionaryEntry myDE in SQLStringList)
                    {
                        string strsqlorsp = myDE.Value.ToString();
                        SqlParameter[] cmdParms = (SqlParameter[])myDE.Key;
                        if (strsqlorsp.Trim().Length > 1)
                        {
                            DbCommand dbCommand;
                            if (SpOrSql)
                                dbCommand = db.GetSqlStringCommand(strsqlorsp);
                            else
                                dbCommand = db.GetStoredProcCommand(strsqlorsp);
                            BuildDBParameter(db, dbCommand, cmdParms);
                            db.ExecuteNonQuery(dbCommand, dbtran);
                        }
                    }
                    dbtran.Commit();
                }
                catch (Exception ex)
                {
                    dbtran.Rollback();
                    throw new Exception(ex.ToString());
                }
                finally
                {
                    dbconn.Close();
                }
            }
        }
        /// <summary>
        /// ����h��SQL�y�y�A��{��Ʈw�ư�,��Ҧ�SQL�y�y���ArrayList��
        /// </summary>
        /// <param name="SQLStringList">SQL�y�y(SQLStringList��SQL�y�y�ASqlpara�O�ӻy�y��SqlParameter[]�^</param>
        public static void ExecuteSqlParaTran(ArrayList SQLStringList, ArrayList Sqlpara)
        {
            Database db = DatabaseFactory.CreateDatabase();
            using (DbConnection dbconn = db.CreateConnection())
            {
                dbconn.Open();
                DbTransaction dbtran = dbconn.BeginTransaction();
                try
                {
                    //�j��
                    for (int i = 0; i < SQLStringList.Count; i++)
                    {
                        if (SQLStringList[i] != null)
                        {
                            string strsql = SQLStringList[i].ToString();
                            SqlParameter[] cmdParms = (SqlParameter[])Sqlpara[i];
                            DbCommand dbCommand = null;
                            if (strsql.Length > 3 && strsql.Substring(0, 3).Equals("dbo"))
                            {
                                dbCommand = db.GetStoredProcCommand(strsql);
                            }
                            else
                            {
                                dbCommand = db.GetSqlStringCommand(strsql);
                            }
                            BuildDBParameter(db, dbCommand, cmdParms);
                            db.ExecuteNonQuery(dbCommand, dbtran);
                        }
                    }
                    dbtran.Commit();
                }
                catch(Exception ex)
                {
                    dbtran.Rollback();
                    throw new Exception(ex.ToString());
                }
                finally
                {
                    dbconn.Close();
                }
            }
        }
        /// <summary>
        /// 执行一条计算查询结果语句，返回查询结果（object）。
        /// </summary>
        /// <param name="strSql">计算查询结果语句</param>
        /// <param name="cmdParms"></param>
        /// <returns>查询结果（object）</returns>
        public static object GetSingle(string strSql, params SqlParameter[] cmdParms)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetSqlStringCommand(strSql);
                BuildDBParameter(db, dbCommand, cmdParms);
                object obj = db.ExecuteScalar(dbCommand);
                if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                {
                    return null;
                }
                else
                {
                    return obj;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
        /// <summary>
        /// ����d�语句�A��^SqlDataReader ( �`�N�G�ϥΫ�@�w�n��SqlDataReader�i��Close )
        /// </summary>
        /// <param name="strSql">�d�߻语句</param>
        /// <param name="cmdParms"></param>
        /// <returns>SqlDataReader</returns>
        public static SqlDataReader ExecuteReader(string strSql, params SqlParameter[] cmdParms)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetSqlStringCommand(strSql);
                BuildDBParameter(db, dbCommand, cmdParms);
                SqlDataReader dr = (SqlDataReader)db.ExecuteReader(dbCommand);
                return dr;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

        }
        /// <summary>
        /// ����d�语句�A��^DataSet
        /// </summary>
        /// <param name="strSql">�d�语句</param>
        /// <param name="cmdParms"></param>
        /// <returns>DataSet</returns>
        public static DataSet Query(string strSql, params SqlParameter[] cmdParms)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetSqlStringCommand(strSql);
                BuildDBParameter(db, dbCommand, cmdParms);
                return db.ExecuteDataSet(dbCommand);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="conn"></param>
        /// <param name="trans"></param>
        /// <param name="cmdText"></param>
        /// <param name="cmdParms"></param>
        private static void PrepareCommand(SqlCommand cmd, SqlConnection conn, SqlTransaction trans, string cmdText, SqlParameter[] cmdParms)
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            if (trans != null)
                cmd.Transaction = trans;
            cmd.CommandType = CommandType.Text;//cmdType;
            if (cmdParms != null)
            {
                foreach (SqlParameter parameter in cmdParms)
                {
                    if ((parameter.Direction == ParameterDirection.InputOutput || parameter.Direction == ParameterDirection.Input) &&
                        (parameter.Value == null))
                    {
                        parameter.Value = DBNull.Value;
                    }
                    cmd.Parameters.Add(parameter);
                }
            }
        }
        #endregion

        #region �s�x�L�{�ާ@

        /// <summary>
        /// ����s�x�L�{�A��^�v�T�����
        /// </summary>
        /// <param name="storedProcName"></param>
        /// <returns></returns>
        public static int RunProcedure(string storedProcName)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand(storedProcName);
            return db.ExecuteNonQuery(dbCommand);
        }    
        /// <summary>
        /// ����s�x�L�{�A��^�v�T�����
        /// </summary>
        /// <param name="storedProcName">�s�x�L�{�W</param>
        /// <param name="parameters">�s�x�L�{�Ѽ�</param>
        /// <returns></returns>
        public static int RunProcedure(string storedProcName, IDataParameter[] parameters)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand(storedProcName);
            dbCommand.CommandTimeout = 0;
            BuildDBParameter(db, dbCommand, parameters);
            return db.ExecuteNonQuery(dbCommand);
        }
        /// <summary>
        /// ����s�x�L�{�A��^��X�Ѽƪ��ȩM�v�T�����
        /// </summary>
        /// <param name="storedProcName">�s�x�L�{�W</param>
        /// <param name="InParameters">�s�x�L�{�Ѽ�</param>
        /// <param name="OutParameter">��X�ѼƦW��</param>
        /// <param name="rowsAffected">�v�T�����</param>
        /// <returns></returns>
        public static object RunProcedure(string storedProcName, IDataParameter[] InParameters, SqlParameter OutParameter, ref int rowsAffected)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand(storedProcName);
            BuildDBParameter(db, dbCommand, (IDataParameter[])InParameters);
            db.AddOutParameter(dbCommand, OutParameter.ParameterName, OutParameter.DbType, OutParameter.Size);
            rowsAffected = db.ExecuteNonQuery(dbCommand);
            return db.GetParameterValue(dbCommand, "@" + OutParameter.ParameterName);  //�o���X�Ѽƪ���
        }
        /// <summary>
        /// ����s�x�L�{�A��^SqlDataReader ( �`�N�G�ϥΫ�@�w�n��SqlDataReader�i��Close )
        /// </summary>
        /// <param name="storedProcName">�s�x�L�{�W</param>
        /// <param name="parameters">�s�x�L�{�Ѽ�</param>
        /// <returns>SqlDataReader</returns>
        public static SqlDataReader RunProcedure(string storedProcName, IDataParameter[] parameters,int i_Times)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand(storedProcName, parameters);
            dbCommand.CommandTimeout = i_Times;
            BuildDBParameter(db, dbCommand, parameters);
            return (SqlDataReader)db.ExecuteReader(dbCommand);
        }
        /// <summary>
        /// ����s�x�L�{�A��^DataSet
        /// </summary>
        /// <param name="storedProcName">�s�x�L�{�W</param>
        /// <param name="parameters">�s�x�L�{�Ѽ�</param>
        /// <param name="tableName"> DataSet���G������W</param>
        /// <returns>DataSet</returns>
        public static DataSet RunProcedure(string storedProcName, IDataParameter[] parameters, string tableName)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand(storedProcName);
            dbCommand.CommandTimeout = 0;
            BuildDBParameter(db, dbCommand, parameters);
            return db.ExecuteDataSet(dbCommand);
        }
        /// <summary>
        /// ����s�x�L�{�A��^DataSet(�]�w���ݮɶ�)
        /// </summary>
        /// <param name="storedProcName"></param>
        /// <param name="parameters"></param>
        /// <param name="tableName"></param>
        /// <param name="Times"></param>
        /// <returns></returns>
        public static DataSet RunProcedure(string storedProcName, IDataParameter[] parameters, string tableName, int Times)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand(storedProcName);
            dbCommand.CommandTimeout = Times;
            BuildDBParameter(db, dbCommand, parameters);
            return db.ExecuteDataSet(dbCommand);
        }
        /// <summary>
        /// �c��SqlCommand ����(�ΨӪ�^�@�ӵ��G���A�Ӥ��O�@�Ӿ�ƭ�)
        /// </summary>
        /// <param name="connection">��Ʈw�s��</param>
        /// <param name="storedProcName">�s�x�L�{�W</param>
        /// <param name="parameters">�s�x�L�{�Ѽ�</param>
        /// <returns>SqlCommand</returns>
        private static SqlCommand BuildQueryCommand(SqlConnection connection, string storedProcName, IDataParameter[] parameters)
        {
            SqlCommand command = new SqlCommand(storedProcName, connection);
            command.CommandType = CommandType.StoredProcedure;
            foreach (SqlParameter parameter in parameters)
            {
                if (parameter != null)
                {
                    // �ˬd�����t�Ȫ���X�Ѽ�,�N����t�HDBNull.Value.
                    if ((parameter.Direction == ParameterDirection.InputOutput || parameter.Direction == ParameterDirection.Input) &&
                        (parameter.Value == null))
                    {
                        parameter.Value = DBNull.Value;
                    }
                    command.Parameters.Add(parameter);
                }
            }
            return command;
        }
        /// <summary>
        /// �Ы�SqlCommand ������(�ΨӪ�^�@�Ӿ�ƭ�)	
        /// </summary>
        /// <param name="connection">�s�x�L�{�W</param>
        /// <param name="storedProcName">�s�x�L�{�Ѽ�</param>
        /// <param name="parameters">SqlCommand ������</param>
        /// <returns></returns>
        private static SqlCommand BuildIntCommand(SqlConnection connection, string storedProcName, IDataParameter[] parameters)
        {
            SqlCommand command = BuildQueryCommand(connection, storedProcName, parameters);
            command.Parameters.Add(new SqlParameter("ReturnValue",
                SqlDbType.Int, 4, ParameterDirection.ReturnValue,
                false, 0, 0, string.Empty, DataRowVersion.Default, null));
            return command;
        }
        #endregion	
    }
}
