//using Maticsoft.DBUtility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.Services;
using System.Xml;
using System.Xml.Linq;
using System.Linq;
using System.Text;

namespace ik3DtWs
{
    /// <summary>
    /// IAccount 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]

    public class Service2 : System.Web.Services.WebService
    {


        ///<summary>
        ///科目维护接口
        ///<\summary>
        /// <param name="sToken"></param>
        /// <param name="sTop"></param>
        /// <param name="sWhere"></param>
        /// <param name="sOrder"></param>
        /// <returns></returns>
        [WebMethod(Description = "科目明细维护接口:sActionType:ADD 新增 UPD 修改。科目编码不允许修改")]
        public string T_Account(string uid, string pwd, string sActionType, string xml)
        //public string T_Account(string uid, string pwd, string xml)
        {
            if (uid != "k3WrAdmin" || pwd != "kd147258")
            {
                return "你的id或psw错误";
            }
            string sReturn = string.Empty;

            //XElement xe = XElement.Load(xml);
            //XElement xe = XElement.Load(@"D:\mldotNet\ik3DtWs\T_Dept.xml");
            XElement xe = XElement.Parse(xml);
            IEnumerable<XElement> elements = from ele in xe.Elements("T_Account")
                                             select ele;
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
            XmlElement xmlElem = doc.DocumentElement;//获取根节点
            
            //showInfoByElements(elements);

            List<accountModel> modelList = new List<accountModel>();
            foreach (var ele in elements)
            {
                
                accountModel model = new accountModel();
                if (!string.IsNullOrEmpty(ele.Element("FNumber").Value))
                {
                    model.Number = ele.Element("FNumber").Value;
                }
                if (!string.IsNullOrEmpty(ele.Element("FName").Value))
                {
                    model.Name = ele.Element("FName").Value;
                }
                if (!string.IsNullOrEmpty(ele.Element("FParentNumber").Value))
                {
                    model.ParentNumber = ele.Element("FParentNumber").Value;
                }
                //if (!string.IsNullOrEmpty(ele.Element("FParentName").Value))
                //{
                //    model.ParentName = ele.Element("FParentName").Value;
                //}
                if (!string.IsNullOrEmpty(ele.Element("FGroupID").Value))
                {
                    model.GroupID = ele.Element("FGroupID").Value;
                }
                if (!string.IsNullOrEmpty(ele.Element("FDC").Value))
                {
                    model.DC = ele.Element("FDC").Value;
                }
                if (!string.IsNullOrEmpty(ele.Element("FCurrency").Value))
                {
                    model.Currency = ele.Element("FCurrency").Value;
                }
                if (!string.IsNullOrEmpty(ele.Element("FAdjustRate").Value))
                {
                    model.AdjustRate = ele.Element("FAdjustRate").Value;
                }
                if (!string.IsNullOrEmpty(ele.Element("FContact").Value))
                {
                    model.Contact = ele.Element("FContact").Value;
                }
                if (!string.IsNullOrEmpty(ele.Element("FQuantities").Value))
                {
                    model.Quantities = ele.Element("FQuantities").Value;
                }
                if (!string.IsNullOrEmpty(ele.Element("FIsCash").Value))
                {
                    model.IsCash = ele.Element("FIsCash").Value;
                }
                if (!string.IsNullOrEmpty(ele.Element("FIsBank").Value))
                {
                    model.IsBank = ele.Element("FIsBank").Value;
                }
                if (!string.IsNullOrEmpty(ele.Element("FJournal").Value))
                {
                    model.Journal = ele.Element("FJournal").Value;
                }
                if (!string.IsNullOrEmpty(ele.Element("FAcctint").Value))
                {
                    model.Acctint = ele.Element("FAcctint").Value;
                }
                if (!string.IsNullOrEmpty(ele.Element("FIsCashFlow").Value))
                {
                    model.IsCashFlow = ele.Element("FIsCashFlow").Value;
                }
                if (!string.IsNullOrEmpty(ele.Element("FIsBudget").Value))
                {
                    model.IsBudget = ele.Element("FIsBudget").Value;
                }
                if (!string.IsNullOrEmpty(ele.Element("FControlSystem").Value))
                {
                    model.ControlSystem = ele.Element("FControlSystem").Value;
                }
                if (!string.IsNullOrEmpty(ele.Element("FDetailID").Value))
                {
                    model.DetailID = ele.Element("FDetailID").Value;
                }
                if (((xmlElem.GetElementsByTagName("FDeleted") as XmlNodeList).Count ==elements.Count()))
                {
                    if (!string.IsNullOrEmpty(ele.Element("FDeleted").Value))
                    {
                        model.Deleted = ele.Element("FDeleted").Value;
                    }
                }

                modelList.Add(model);
            }

            DataSet ods = new DataSet();
            //switch (sActionType)
            //{=nll
            //case "ADD":
            foreach (var account in modelList)
            {
                try
                {
                    SqlParameter[] oPara ={
                                        new SqlParameter("@FNAME",SqlDbType.VarChar), //接收传入的编码值
                                        new SqlParameter("@FGROUPID",SqlDbType.VarChar),
                                        new SqlParameter("@FNUMBER",SqlDbType.VarChar),
                                        new SqlParameter("@FPARENT",SqlDbType.VarChar)//接收传入的fparentnumber
                                            };
                    oPara[0].Value = account.Name;
                    oPara[1].Value = account.GroupID; //此处@FPARENT是@FPARENTNumber
                    oPara[2].Value = account.Number;

                    #region 检查要插入的科目名称是否已存在，存在则修改原有科目
                    DataSet exist = new DataSet();
                    string sSqlsc = @"  Select * From t_Account Where FNumber=@FNumber";
                    exist = DbHelperSQL.Query(sSqlsc, oPara);
                    if (exist != null && exist.Tables[0].Rows.Count > 0)
                    {

                        //sReturn = sReturn + "<status>-1</status>" + "<message>" + "名称为" + account.Name + "的科目已存在" + "</message>";
                        try
                        {
                            SqlParameter[] oParaU ={
                                        new SqlParameter("@FNumber",SqlDbType.VarChar), //接收传入的编码值
                                        new SqlParameter("@FParent",SqlDbType.VarChar)
                                            };
                            oParaU[0].Value = account.Number;
                            oParaU[1].Value = account.ParentNumber; //此处@FPARENT是@FPARENTNumber

                            DataSet existU = new DataSet();
                            //查询要修改的科目是否存在，存在则可以修改，
                            string sSqlscU = @"  Select * From t_Account Where FNumber=@FNumber";
                            existU = DbHelperSQL.Query(sSqlscU, oParaU);
                            if (existU != null && existU.Tables[0].Rows.Count > 0)
                            {
                                SqlParameter[] oParaUEditNeed ={
                                           new SqlParameter("@FName",SqlDbType.VarChar), //接收传入的名称值
                                           new SqlParameter("@FNumber",SqlDbType.VarChar),
                                           new SqlParameter("@FAccountID",SqlDbType.Int),
                                           new SqlParameter("@FGroupID",SqlDbType.Int),//接收生成的fitemid
                                           new SqlParameter("@FDC",SqlDbType.SmallInt),
                                           new SqlParameter("@FCurrency",SqlDbType.VarChar),
                                           new SqlParameter("@FAdjustRate",SqlDbType.VarChar),
                                           new SqlParameter("@FContact",SqlDbType.VarChar),
                                           new SqlParameter("@FQuantities",SqlDbType.VarChar),
                                           new SqlParameter("@FIsCash",SqlDbType.VarChar),
                                           new SqlParameter("@FIsBank",SqlDbType.VarChar),
                                           new SqlParameter("@FJournal",SqlDbType.VarChar),
                                           new SqlParameter("@FAcctint",SqlDbType.VarChar),
                                           new SqlParameter("@FIsCashFlow",SqlDbType.VarChar),
                                           new SqlParameter("@FIsBudget",SqlDbType.VarChar),
                                           new SqlParameter("@FControlSystem",SqlDbType.VarChar),
                                           new SqlParameter("@FDetailID",SqlDbType.Int),
                                           new SqlParameter("@FPeriod",SqlDbType.VarChar),
                                           new SqlParameter("@FDeleted",SqlDbType.Int)
                                           
                                           
                                                    };
                                oParaUEditNeed[0].Value = account.Name;
                                oParaUEditNeed[2].Value = Convert.ToInt32(existU.Tables[0].Rows[0]["FAccountID"]);

                                //groupid赋值
                                oParaUEditNeed[3].Value = account.GroupID;

                                //余额方向1- 借方   -1 - 贷方，填值1或-1
                                oParaUEditNeed[4].Value = account.DC;

                                //FCurrency赋值
                                oParaUEditNeed[5].Value = account.Currency;
                                sSqlscU = @"SELECT FCurrencyID FROM t_Currency WHERE FNumber=@FCurrency";
                                oParaUEditNeed[5].Value = DbHelperSQL.GetSingle(sSqlscU, oParaUEditNeed);

                                oParaUEditNeed[6].Value = account.AdjustRate;
                                oParaUEditNeed[7].Value = account.Contact;
                                oParaUEditNeed[8].Value = account.Quantities;
                                oParaUEditNeed[9].Value = account.IsCash;
                                oParaUEditNeed[10].Value = account.IsBank;
                                oParaUEditNeed[11].Value = account.Journal;
                                oParaUEditNeed[12].Value = account.Acctint;
                                oParaUEditNeed[13].Value = account.IsCashFlow;
                                oParaUEditNeed[14].Value = account.IsBudget;
                                oParaUEditNeed[15].Value = account.ControlSystem;
                                oParaUEditNeed[16].Value = account.DetailID;
                                if (account.Deleted!=null)
                                {
                                    oParaUEditNeed[18].Value = account.Deleted;
                                }
                                else
                                    oParaUEditNeed[18].Value = 0;

                                #region 转游标了
                                ////SELECT fnumber FROM t_Account WHERE FNumber = '2369' and FAccountID<>1286
                                //string sSqlEdit = @"Create table #ObjectItemID(ItemID INT NOT NULL,FUsedFlag INT DEFAULT(2),FDescription VARCHAR(400) NULL)";
                                //sSqlEdit = sSqlEdit + @" INSERT INTO #ObjectItemID (ItemID) VALUES(@FAccountID)";
                                //DbHelperSQL.ExecuteSql(sSqlEdit, oParaUEditNeed);

                                //sSqlEdit = @"select t1.FSQLColumnName as FColumn,t2.FSQLTableName as FTable,'核算项目_'+ t2.FName as FDescription from t_ItemPropDesc t1,t_ItemClass t2,sysobjects t3 where t1.FItemClassID = t2.FItemClassID And t2.FSQLTableName=t3.name And t1.FSrcTable ='t_Account'";
                                //sSqlEdit = sSqlEdit + @" select FTable,FColumn,FDescription as FDescription from t_BASE_ObjectInUsed where FObjectType=0 and (isnull(FItemClassID,0)=0 or isnull(FItemClassID,0) = -1)";
                                //ods = DbHelperSQL.Query(sSqlEdit, oParaUEditNeed);
                                //for (int j = 0; j < ods.Tables.Count; j++)//循环两个表，查看是否已被使用
                                //{
                                //    for (int k = 0; k < ods.Tables[j].Rows.Count; k++)//循环表里面的分录
                                //    {

                                //        SqlParameter[] oParaUUse = {
                                //                                new SqlParameter("FColumn",SqlDbType.VarChar),
                                //                                new SqlParameter("FTable",SqlDbType.VarChar),
                                //                                new SqlParameter("FDescription",SqlDbType.VarChar),
                                //                                new SqlParameter("Count",SqlDbType.VarChar)
                                //                            };
                                //        oParaUUse[0].Value = ods.Tables[j].Rows[k]["FColumn"].ToString();
                                //        string table = ods.Tables[j].Rows[k]["FTable"].ToString();
                                //        oParaUUse[2].Value = ods.Tables[j].Rows[k]["FDescription"].ToString();
                                //        string sSqlinUse = @" UPDATE #ObjectItemID SET FUsedFlag = 1,FDescription=@FDescription FROM #ObjectItemID t1," + table + "  WHERE t1.ItemID=@FColumn AND FUsedFlag <> 1";
                                //        DbHelperSQL.ExecuteSql(sSqlinUse, oParaUUse);
                                //    }
                                //}
                                #endregion

                                #region ver14.1
                                StringBuilder sSqlEdit = new StringBuilder();

                                string table = existU.Tables[0].Rows[0]["FAccountID"].ToString();
                                sSqlEdit.Append(" 	Create table #ObjectItemID(ItemID INT NOT NULL,FUsedFlag INT DEFAULT(2),FDescription VARCHAR(400) NULL)				 ");
                                sSqlEdit.Append(" 	INSERT INTO #ObjectItemID (ItemID) VALUES(" + table + ")				 ");
                                sSqlEdit.Append(" 					 ");
                                sSqlEdit.Append(" 	declare objecttable cursor scroll for				 ");
                                sSqlEdit.Append(" 		select t1.FSQLColumnName as FColumn,t2.FSQLTableName as FTable,'核算项目_'+ t2.FName as FDescription from t_ItemPropDesc t1,t_ItemClass t2,sysobjects t3 where t1.FItemClassID = t2.FItemClassID And t2.FSQLTableName=t3.name And t1.FSrcTable ='t_Account'			 ");
                                sSqlEdit.Append(" 		union			 ");
                                sSqlEdit.Append(" 		select FColumn,FTable,FDescription as FDescription from t_BASE_ObjectInUsed where FObjectType=0 and (isnull(FItemClassID,0)=0 or isnull(FItemClassID,0) = -1)			 ");
                                sSqlEdit.Append(" 	open objecttable				 ");
                                sSqlEdit.Append(" 		declare @Column varchar(100)			 ");
                                sSqlEdit.Append(" 		declare @Table varchar(100)			 ");
                                sSqlEdit.Append(" 		declare @Description varchar(100)			 ");
                                sSqlEdit.Append(" 		declare @sql varchar(255)			 ");
                                sSqlEdit.Append(" 		while @@FETCH_STATUS=0			 ");
                                sSqlEdit.Append(" 		begin			 ");
                                sSqlEdit.Append(" 			set @sql='UPDATE #ObjectItemID SET FUsedFlag = 1,FDescription='''+@Description+''' FROM #ObjectItemID t1,'+@Table+' WHERE t1.ItemID= '+@Column+' AND FUsedFlag <> 1'		 ");
                                sSqlEdit.Append(" 			PRINT @sql		 ");
                                sSqlEdit.Append(" 			exec(@sql)		 ");
                                sSqlEdit.Append(" 			fetch next from objecttable into  @Column,@Table,@Description		 ");
                                sSqlEdit.Append(" 		end			 ");
                                sSqlEdit.Append(" 	CLOSE objecttable				 ");
                                sSqlEdit.Append(" 	DEALLOCATE objecttable				 ");
                                sSqlEdit.Append(" 	select count(1) from #ObjectItemID where FUsedFlag=1				 ");
                                sSqlEdit.Append(" 	Drop table #ObjectItemID				 ");


                                int count = 0;
                                count = Convert.ToInt32(DbHelperSQL.GetSingle(sSqlEdit.ToString()));
                                if (count > 0)
                                {
                                    //sReturn = sReturn + "<status>-1</status>" + "<message>" + "编号为" + account.Number + "的科目已使用，不可修改" + "</message>";
                                    //更新account名称数据
                                    string accountIDCN = existU.Tables[0].Rows[0]["FAccountID"].ToString();
                                    string sSqlUpdCN = " exec sp_executesql N'UPDATE t_Account";
                                    sSqlUpdCN = sSqlUpdCN + " SET FName=@P2 WHERE FAccountID=" + accountIDCN + "' ";
                                    sSqlUpdCN = sSqlUpdCN + " ,N'@P2 varchar(80)' ";
                                    sSqlUpdCN = sSqlUpdCN + " ,@FName ";
                                    sSqlUpdCN = sSqlUpdCN + " exec sp_executesql N'Update t_Account Set FFullName = '''' Where FNumber Like @P1',N'@P1 varchar(40)','@FNumber.%'";
                                    DbHelperSQL.ExecuteSql(sSqlUpdCN, oParaUEditNeed);
                                    sReturn = sReturn + "<status>0</status>" + "<message>" + "编号为" + account.Number + "的科目已使用。科目修改名称成功，其它项不可修改" + "</message>";
                                    //sReturn = sReturn + "<status>0</status>" + "<message>" + account.Number + "科目修改成功" + "</message>";
                                    continue;
                                }
                                #endregion

                                #region ver10.3
//                                //判断account是否被使用过
//                                DateTime now = new DateTime();
//                                oParaUEditNeed[17].Value = now.ToString("yyyyMMdd");
//                                string sqlused = @" Select FAccountID from t_balance 
//                                                    Where FYear*100+FPeriod<=@FPeriod and FAccountID=@FAccountID and 
//                                                    (FBeginBalanceFor<>0 or FYtdDebitFor<>0 or FYtdCreditFor<>0 or FBeginBalance<>0 or 
//                                                    FYtdDebit<>0 or FYtdCredit<>0) Union all Select FAccountID From t_ProfitAndLoss 
//                                                    Where FYear*100+FPeriod<=@FPeriod and FAccountID=@FAccountID 
//                                                    And (FAmountFor<>0 or FYtdAmountFor<>0 or FAmount<>0 or FYtdAmount<>0)  
//                                                    Union all Select FAccountID From t_QuantityBalance Where FYear*100+FPeriod<=@FPeriod and FAccountID=@FAccountID 
//                                                    And (FBeginQty<>0 or FYtdDebitQty<>0 or FYtdCreditQty<>0 or FDebitQty<>0 or FCreditQty<>0)  
//                                                    Union all Select FAccountID from t_Voucher v inner join t_voucherentry e on v.Fvoucherid=e.FVoucherid 
//                                                    where FYear*100+FPeriod<=@FPeriod and FAccountID=@FAccountID";

//                                if (DbHelperSQL.ExecuteSql(sqlused, oParaUEditNeed) > 0)
//                                {
//                                    sReturn = sReturn + "<status>-1</status>" + "<message>" + "编号为" + account.Number + "的科目已使用，不可修改" + "</message>";
//                                    continue;
//                                }
                                #endregion

                                //更新account数据
                                string accountID = existU.Tables[0].Rows[0]["FAccountID"].ToString();
                                string sSqlUpd = " exec sp_executesql N'UPDATE t_Account";
                                sSqlUpd = sSqlUpd + " SET FName=@P2,FDC=@P3,FHelperCode=@P4,FCurrencyID=@P5,FAdjustRate=@P6,FContact=@P7,FQuantities=@P8,FUnitGroupID=@P9,FMeasureUnitID=@P10,FDetailID=@P11,FIsCashFlow=@P12 ";
                                sSqlUpd = sSqlUpd + " ,FIsBusi=@P13,FAcnt=@P14,FAcntType=@P15,FInterest=@P16,FAcctint=@P17,FintRate=@P18,FLastIntDate=@P19,FTradeNum=@P20,FControl=@P21,FViewMsg=@P22,FMessage=@P23,FDelete=@P24,FControlSystem=@P25 WHERE FAccountID=" + accountID + "' ";
                                sSqlUpd = sSqlUpd + " ,N'@P2 varchar(80),@P3 int,@P4 varchar(40),@P5 int,@P6 bit,@P7 bit,@P8 bit,@P9 int,@P10 int,@P11 int,@P12 bit,@P13 bit,@P14 bit,@P15 int,@P16 bit,@P17 bit,@P18 float,@P19 datetime,@P20 varchar(40),@P21 int,@P22 bit,@P23 varchar(100),@P24 int,@P25 int' ";
                                sSqlUpd = sSqlUpd + " ,@FName,@FDC,NULL,@FCurrency,@FAdjustRate,@FContact,@FQuantities,0,0,@FDetailID,@FIsCashFlow,0,0,0,0,@FAcctint,0,NULL,NULL,0,0,NULL,@FDeleted,0";
                                sSqlUpd = sSqlUpd + " exec sp_executesql N'Update t_Account Set FFullName = '''' Where FNumber Like @P1',N'@P1 varchar(40)','@FNumber.%'";
                                DbHelperSQL.ExecuteSql(sSqlUpd, oParaUEditNeed);
                                sReturn = sReturn + "<status>0</status>" + "<message>" + account.Number + "科目修改成功" + "</message>";
                            }
                            else
                            {
                                sReturn = sReturn + "<status>-1</status>" + "<message>" + "编号为" + account.Number + "的科目不存在" + "</message>";

                            }


                        }
                        catch (Exception ex)
                        {

                            sReturn = sReturn + "<status>-1</status>" + "<message>" + "编号为" + account.Number + "修改出错" + ex.Message.ToString() + "</message>";
                        }
                        continue;
                    }
                    #endregion

                    //检查要插入的科目类别是否存在，存在则检查要插入的科目编码是否存在
                    string sSqldetailse = " Select FGroupID From t_AcctGroup Where FGroupID=@FGROUPID";
                    ods = DbHelperSQL.Query(sSqldetailse, oPara);
                    if (ods != null && ods.Tables[0].Rows.Count > 0)
                    {
                        sSqldetailse = @" exec sp_executesql N'SELECT FAccountID FROM t_Account WHERE FNumber=@P1',N'@P1 varchar(40)',@FNUMBER";
                        ods = DbHelperSQL.Query(sSqldetailse, oPara);
                        if (ods != null && ods.Tables[0].Rows.Count == 0)
                        {
                            SqlParameter[] oParaInsertNeed ={
                                           new SqlParameter("@FName",SqlDbType.VarChar), //接收传入的名称值
                                           new SqlParameter("@FNumber",SqlDbType.VarChar),
                                           new SqlParameter("@FParent",SqlDbType.VarChar),
                                           new SqlParameter("@FGroupID",SqlDbType.Int),//接收生成的fitemid
                                           new SqlParameter("@FDC",SqlDbType.SmallInt),
                                           new SqlParameter("@FCurrency",SqlDbType.VarChar),
                                           new SqlParameter("@FAdjustRate",SqlDbType.VarChar),
                                           new SqlParameter("@FContact",SqlDbType.VarChar),
                                           new SqlParameter("@FQuantities",SqlDbType.VarChar),
                                           new SqlParameter("@FIsCash",SqlDbType.VarChar),
                                           new SqlParameter("@FIsBank",SqlDbType.VarChar),
                                           new SqlParameter("@FJournal",SqlDbType.VarChar),
                                           new SqlParameter("@FAcctint",SqlDbType.VarChar),
                                           new SqlParameter("@FIsCashFlow",SqlDbType.VarChar),
                                           new SqlParameter("@FIsBudget",SqlDbType.VarChar),
                                           new SqlParameter("@FControlSystem",SqlDbType.VarChar),
                                           new SqlParameter("@FDetailID",SqlDbType.Int),
                                           new SqlParameter("@FLevel",SqlDbType.Int),
                                           new SqlParameter("@FAccountID",SqlDbType.Int),
                                           new SqlParameter("@FRootID",SqlDbType.Int),
                                                    };
                            oParaInsertNeed[0].Value = account.Name;
                            oParaInsertNeed[1].Value = account.Number;

                            //parentid赋值
                            if (string.IsNullOrWhiteSpace(account.ParentNumber))
                            {
                                oParaInsertNeed[2].Value = 0;
                                oParaInsertNeed[17].Value = 1;
                            }
                            else
                            {
                                oParaInsertNeed[2].Value = account.ParentNumber;//这里@parent是parentnumber
                                sSqldetailse = @"select * from t_Account where FNumber=@FParent";
                                ods = DbHelperSQL.Query(sSqldetailse, oParaInsertNeed);
                                if (ods != null && ods.Tables[0].Rows.Count == 0)
                                {
                                    sReturn = sReturn + "<status>-1</status>" + "<message>" + "编码为" + account.Number + "的科目的上级科目" + account.ParentNumber + "不存在" + "</message>";
                                    continue;
                                }
                                oParaInsertNeed[2].Value = ods.Tables[0].Rows[0]["FAccountID"].ToString();//此处parent是parentid
                                oParaInsertNeed[17].Value = Convert.ToInt32(ods.Tables[0].Rows[0]["FLevel"]) + 1;
                            }

                            //oParaInsertNeed[17].Value = Convert.ToInt32(ods.Tables[0].Rows[0]["FLevel"]) + 1;

                            //groupid赋值
                            oParaInsertNeed[3].Value = account.GroupID;

                            //余额方向1- 借方   -1 - 贷方，填值1或-1
                            oParaInsertNeed[4].Value = account.DC;

                            //FCurrency赋值
                            oParaInsertNeed[5].Value = account.Currency;
                            sSqldetailse = @"SELECT FCurrencyID FROM t_Currency WHERE FNumber=@FCurrency";
                            oParaInsertNeed[5].Value = DbHelperSQL.GetSingle(sSqldetailse, oParaInsertNeed);

                            //Frootid赋值
                            string RootNum;
                            int Dot = account.Number.ToString().IndexOf(".");
                            if (Dot > -1)
                            {
                                RootNum = account.Number.ToString().Substring(0, Dot);

                            }
                            else
                            {
                                RootNum = account.Number;
                            }

                            oParaInsertNeed[6].Value = account.AdjustRate;
                            oParaInsertNeed[7].Value = account.Contact;
                            oParaInsertNeed[8].Value = account.Quantities;
                            oParaInsertNeed[9].Value = account.IsCash;
                            oParaInsertNeed[10].Value = account.IsBank;
                            oParaInsertNeed[11].Value = account.Journal;
                            oParaInsertNeed[12].Value = account.Acctint;
                            oParaInsertNeed[13].Value = account.IsCashFlow;
                            oParaInsertNeed[14].Value = account.IsBudget;
                            oParaInsertNeed[15].Value = account.ControlSystem;
                            oParaInsertNeed[16].Value = account.DetailID;


                            string sSqlInsert = @" exec sp_executesql N'INSERT INTO t_Account (FNumber
                                                                                                      ,FName
                                                                                                      ,FGroupID
                                                                                                      ,FDC
                                                                                                      ,FHelperCode
                                                                                                      ,FCurrencyID
                                                                                                      ,FAdjustRate
                                                                                                      ,FIsCash
                                                                                                      ,FIsBank
                                                                                                      ,FJournal
                                                                                                      ,FContact
                                                                                                      ,FQuantities
                                                                                                      ,FUnitGroupID
                                                                                                      ,FMeasureUnitID
                                                                                                      ,FDetailID
                                                                                                      ,FIsCashFlow
                                                                                                      ,FAcnt
                                                                                                      ,FInterest
                                                                                                      ,FIsAcnt
                                                                                                      ,FAcctint
                                                                                                      ,FLevel
                                                                                                      ,FDetail
                                                                                                      ,FParentID
                                                                                                      ,FIsBudget)
                                                                                                      VALUES (@P1,@P2,@P3,@P4,@P5,@P6,@P7,@P8,@P9,@P10,@P11,@P12,@P13,@P14,@P15,@P16,@P17,@P18,@P19,@P20,@P21,@P22,@P23,@P24)'
                                                                                                      ,N'@P1 varchar(40),@P2 varchar(80),@P3 int,@P4 int,@P5 varchar(40),@P6 int,@P7 bit,@P8 bit,@P9 bit,@P10 bit,@P11 bit,@P12 bit,@P13 int,@P14 int,@P15 int,@P16 bit,@P17 bit,@P18 bit,@P19 bit,@P20 bit,@P21 smallint,@P22 bit,@P23 int,@P24 int'
                                                                                                      ,@FNumber,@FName,@FGroupID,@FDC,NULL,@FCurrency,@FAdjustRate,@FIsCash,@FIsBank,@FJournal,@FContact
                                                                                                      ,@FQuantities,0,0,@FDetailID,@FIsCashFlow,0,0,0,@FAcctint,@FLevel,1,@FParent,@FIsBudget";
                            DbHelperSQL.ExecuteSql(sSqlInsert, oParaInsertNeed);
                            sSqldetailse = @" exec sp_executesql N'SELECT FAccountID FROM t_Account WHERE FNumber=@P1',N'@P1 varchar(40)',@FNumber";

                            oParaInsertNeed[18].Value = DbHelperSQL.GetSingle(sSqldetailse, oParaInsertNeed);//获得系统生成的FAccountID
                            string rootID = "select FaccountID from t_account where fnumber='" + RootNum + "'";
                            rootID = DbHelperSQL.GetSingle(rootID).ToString();
                            sSqlInsert = @" Update t_Account Set FRootID=" + rootID + " Where FAccountID=@FAccountID";
                            sSqlInsert = sSqlInsert + @" Update t_Account Set FDetail=0 Where FAccountID=@FParent";
                            DbHelperSQL.ExecuteSql(sSqlInsert, oParaInsertNeed);

                            sSqlInsert = "Delete from Access_t_Account where FItemID=@FAccountID";
                            sSqlInsert = sSqlInsert + @"  Insert into Access_t_Account(FItemID,FParentIDX,FDataAccessView,FDataAccessEdit,FDataAccessDelete)
                                                                                        Values(@FAccountID
                                                                                                ,0
                                                                                                ,convert(varbinary(7200),REPLICATE(char(255),100)),convert(varbinary(7200),REPLICATE(char(255),100)),convert(varbinary(7200),REPLICATE(char(255),100)))";
                            sSqlInsert = sSqlInsert + @" Delete from Access_t_AccountR where FItemID=@FAccountID
                                                                 Insert into Access_t_AccountR(FItemID,FParentIDX,FDataAccessView,FDataAccessEdit,FDataAccessDelete)
                                                                 Values(@FAccountID,0,convert(varbinary(7200),REPLICATE(char(255),100)),convert(varbinary(7200),REPLICATE(char(255),100)),convert(varbinary(7200),REPLICATE(char(255),100)))";
                            DbHelperSQL.ExecuteSql(sSqlInsert, oParaInsertNeed);
                            sReturn = sReturn + "<status>0</status>" + "<message>" + account.Number + "科目插入成功" + "</message>";


                        }
                        else
                        {
                            sReturn = sReturn + "<status>-1</status>" + "<message>" + "要插入的科目" + account.Number + "已存在" + "</message>";
                        }
                    }
                    else
                    {
                        sReturn = sReturn + "<status>-1</status>" + "<message>" + "要插入的科目" + account.Number + "的科目类别不存在" + "</message>";
                    }
                }
                catch (Exception ex)
                {
                    sReturn = sReturn + "<status>-1</status>" + "<message>" + "编号为" + account.Number + "插入出错" + ex.Message.ToString() + "</message>";
                }
                //}

                //break;
                #region 原来的修改代码

                //                case "UPD":
                //                    foreach (var account in modelList)
                //                    {
                //                        try
                //                        {
                //                            SqlParameter[] oPara ={
                //                                        new SqlParameter("@FNumber",SqlDbType.VarChar), //接收传入的编码值
                //                                        new SqlParameter("@FParent",SqlDbType.VarChar)
                //                                            };
                //                            oPara[0].Value = account.Number;
                //                            oPara[1].Value = account.ParentNumber; //此处@FPARENT是@FPARENTNumber

                //                            DataSet exist = new DataSet();
                //                            //查询要修改的科目是否存在，存在则可以修改，
                //                            string sSqlsc = @"  Select * From t_Account Where FNumber=@FNumber";
                //                            exist = DbHelperSQL.Query(sSqlsc, oPara);
                //                            if (exist != null && exist.Tables[0].Rows.Count > 0)
                //                            {
                //                                SqlParameter[] oParaEditNeed ={
                //                                           new SqlParameter("@FName",SqlDbType.VarChar), //接收传入的名称值
                //                                           new SqlParameter("@FNumber",SqlDbType.VarChar),
                //                                           new SqlParameter("@FAccountID",SqlDbType.Int),
                //                                           new SqlParameter("@FGroupID",SqlDbType.Int),//接收生成的fitemid
                //                                           new SqlParameter("@FDC",SqlDbType.SmallInt),
                //                                           new SqlParameter("@FCurrency",SqlDbType.VarChar),
                //                                           new SqlParameter("@FAdjustRate",SqlDbType.VarChar),
                //                                           new SqlParameter("@FContact",SqlDbType.VarChar),
                //                                           new SqlParameter("@FQuantities",SqlDbType.VarChar),
                //                                           new SqlParameter("@FIsCash",SqlDbType.VarChar),
                //                                           new SqlParameter("@FIsBank",SqlDbType.VarChar),
                //                                           new SqlParameter("@FJournal",SqlDbType.VarChar),
                //                                           new SqlParameter("@FAcctint",SqlDbType.VarChar),
                //                                           new SqlParameter("@FIsCashFlow",SqlDbType.VarChar),
                //                                           new SqlParameter("@FIsBudget",SqlDbType.VarChar),
                //                                           new SqlParameter("@FControlSystem",SqlDbType.VarChar),
                //                                           new SqlParameter("@FDetailID",SqlDbType.VarChar),
                //                                           new SqlParameter("@FPeriod",SqlDbType.VarChar),


                //                                                    };
                //                                oParaEditNeed[0].Value = account.Name;
                //                                oParaEditNeed[2].Value = Convert.ToInt32(exist.Tables[0].Rows[0]["FAccountID"]);

                //                                //groupid赋值
                //                                oParaEditNeed[3].Value = account.GroupID;

                //                                //余额方向1- 借方   -1 - 贷方，填值1或-1
                //                                oParaEditNeed[4].Value = account.DC;

                //                                //FCurrency赋值
                //                                oParaEditNeed[5].Value = account.Currency;
                //                                sSqlsc = @"SELECT FCurrencyID FROM t_Currency WHERE FNumber=@FCurrency";
                //                                oParaEditNeed[5].Value = DbHelperSQL.GetSingle(sSqlsc, oParaEditNeed);

                //                                oParaEditNeed[6].Value = account.AdjustRate;
                //                                oParaEditNeed[7].Value = account.Contact;
                //                                oParaEditNeed[8].Value = account.Quantities;
                //                                oParaEditNeed[9].Value = account.IsCash;
                //                                oParaEditNeed[10].Value = account.IsBank;
                //                                oParaEditNeed[11].Value = account.Journal;
                //                                oParaEditNeed[12].Value = account.Acctint;
                //                                oParaEditNeed[13].Value = account.IsCashFlow;
                //                                oParaEditNeed[14].Value = account.IsBudget;
                //                                oParaEditNeed[15].Value = account.ControlSystem;
                //                                oParaEditNeed[16].Value = account.DetailID;


                //                                #region 转游标了
                //                                ////SELECT fnumber FROM t_Account WHERE FNumber = '2369' and FAccountID<>1286
                //                                //string sSqlEdit = @"Create table #ObjectItemID(ItemID INT NOT NULL,FUsedFlag INT DEFAULT(2),FDescription VARCHAR(400) NULL)";
                //                                //sSqlEdit=sSqlEdit+@" INSERT INTO #ObjectItemID (ItemID) VALUES(@FAccountID)";
                //                                //DbHelperSQL.ExecuteSql(sSqlEdit, oParaEditNeed);

                //                                //sSqlEdit = @"select t1.FSQLColumnName as FColumn,t2.FSQLTableName as FTable,'核算项目_'+ t2.FName as FDescription from t_ItemPropDesc t1,t_ItemClass t2,sysobjects t3 where t1.FItemClassID = t2.FItemClassID And t2.FSQLTableName=t3.name And t1.FSrcTable ='t_Account'";
                //                                //sSqlEdit = sSqlEdit + @" select FTable,FColumn,FDescription as FDescription from t_BASE_ObjectInUsed where FObjectType=0 and (isnull(FItemClassID,0)=0 or isnull(FItemClassID,0) = -1)";
                //                                //ods = DbHelperSQL.Query(sSqlEdit, oParaEditNeed);
                //                                //for (int j = 0; j < ods.Tables.Count; j++)//循环两个表，查看是否已被使用
                //                                //{
                //                                //    for (int k = 0; k < ods.Tables[j].Rows.Count; k++)//循环表里面的分录
                //                                //    {

                //                                //        SqlParameter[] oParaUse = {
                //                                //                                new SqlParameter("FColumn",SqlDbType.VarChar),
                //                                //                                new SqlParameter("FTable",SqlDbType.VarChar),
                //                                //                                new SqlParameter("FDescription",SqlDbType.VarChar),
                //                                //                                new SqlParameter("Count",SqlDbType.VarChar)
                //                                //                            };
                //                                //        oParaUse[0].Value = ods.Tables[j].Rows[k]["FColumn"].ToString();
                //                                //        string table = ods.Tables[j].Rows[k]["FTable"].ToString();
                //                                //        oParaUse[2].Value = ods.Tables[j].Rows[k]["FDescription"].ToString();
                //                                //        string sSqlinUse = @" UPDATE #ObjectItemID SET FUsedFlag = 1,FDescription=@FDescription FROM #ObjectItemID t1,"+table+"  WHERE t1.ItemID=@FColumn AND FUsedFlag <> 1";
                //                                //        DbHelperSQL.ExecuteSql(sSqlinUse, oParaUse);
                //                                //    }
                //                                //}
                //                                #endregion

                //                                #region ver14.1
                //                                //StringBuilder sSqlEdit = new StringBuilder();

                //                                //string table = exist.Tables[0].Rows[0]["FAccountID"].ToString();
                //                                //sSqlEdit.Append(" 	Create table #ObjectItemID(ItemID INT NOT NULL,FUsedFlag INT DEFAULT(2),FDescription VARCHAR(400) NULL)				 ");
                //                                //sSqlEdit.Append(" 	INSERT INTO #ObjectItemID (ItemID) VALUES(" + table + ")				 ");
                //                                //sSqlEdit.Append(" 					 ");
                //                                //sSqlEdit.Append(" 	declare objecttable cursor scroll for				 ");
                //                                //sSqlEdit.Append(" 		select t1.FSQLColumnName as FColumn,t2.FSQLTableName as FTable,'核算项目_'+ t2.FName as FDescription from t_ItemPropDesc t1,t_ItemClass t2,sysobjects t3 where t1.FItemClassID = t2.FItemClassID And t2.FSQLTableName=t3.name And t1.FSrcTable ='t_Account'			 ");
                //                                //sSqlEdit.Append(" 		union			 ");
                //                                //sSqlEdit.Append(" 		select FColumn,FTable,FDescription as FDescription from t_BASE_ObjectInUsed where FObjectType=0 and (isnull(FItemClassID,0)=0 or isnull(FItemClassID,0) = -1)			 ");
                //                                //sSqlEdit.Append(" 	open objecttable				 ");
                //                                //sSqlEdit.Append(" 		declare @Column varchar(100)			 ");
                //                                //sSqlEdit.Append(" 		declare @Table varchar(100)			 ");
                //                                //sSqlEdit.Append(" 		declare @Description varchar(100)			 ");
                //                                //sSqlEdit.Append(" 		declare @sql varchar(255)			 ");
                //                                //sSqlEdit.Append(" 		while @@FETCH_STATUS=0			 ");
                //                                //sSqlEdit.Append(" 		begin			 ");
                //                                //sSqlEdit.Append(" 			set @sql='UPDATE #ObjectItemID SET FUsedFlag = 1,FDescription='''+@Description+''' FROM #ObjectItemID t1,'+@Table+' WHERE t1.ItemID= '+@Column+' AND FUsedFlag <> 1'		 ");
                //                                //sSqlEdit.Append(" 			PRINT @sql		 ");
                //                                //sSqlEdit.Append(" 			exec(@sql)		 ");
                //                                //sSqlEdit.Append(" 			fetch next from objecttable into  @Column,@Table,@Description		 ");
                //                                //sSqlEdit.Append(" 		end			 ");
                //                                //sSqlEdit.Append(" 	CLOSE objecttable				 ");
                //                                //sSqlEdit.Append(" 	DEALLOCATE objecttable				 ");
                //                                //sSqlEdit.Append(" 	select count(1) from #ObjectItemID where FUsedFlag=1				 ");
                //                                //sSqlEdit.Append(" 	Drop table #ObjectItemID				 ");


                //                                //int count = 0;
                //                                //count = Convert.ToInt32(DbHelperSQL.GetSingle(sSqlEdit.ToString()));
                //                                //if (count > 0)
                //                                //{
                //                                //    sReturn = sReturn + "<status>-1</status>" + "<message>" + "编号为" + account.Number + "的科目已使用，需要手动修改" + "</message>";
                //                                //    continue;
                //                                //}
                //                                #endregion

                //                                //判断account是否被使用过
                //                                DateTime now = new DateTime();
                //                                oParaEditNeed[16].Value = now.ToString("yyyyMMdd");
                //                                string sqlused = @" Select FAccountID from t_balance 
                //                                                    Where FYear*100+FPeriod<=@FPeriod and FAccountID=@FAccountID and 
                //                                                    (FBeginBalanceFor<>0 or FYtdDebitFor<>0 or FYtdCreditFor<>0 or FBeginBalance<>0 or 
                //                                                    FYtdDebit<>0 or FYtdCredit<>0) Union all Select FAccountID From t_ProfitAndLoss 
                //                                                    Where FYear*100+FPeriod<=@FPeriod and FAccountID=@FAccountID 
                //                                                    And (FAmountFor<>0 or FYtdAmountFor<>0 or FAmount<>0 or FYtdAmount<>0)  
                //                                                    Union all Select FAccountID From t_QuantityBalance Where FYear*100+FPeriod<=@FPeriod and FAccountID=@FAccountID 
                //                                                    And (FBeginQty<>0 or FYtdDebitQty<>0 or FYtdCreditQty<>0 or FDebitQty<>0 or FCreditQty<>0)  
                //                                                    Union all Select FAccountID from t_Voucher v inner join t_voucherentry e on v.Fvoucherid=e.FVoucherid 
                //                                                    where FYear*100+FPeriod<=@FPeriod and FAccountID=@FAccountID";

                //                                if (DbHelperSQL.ExecuteSql(sqlused, oParaEditNeed) > 0)
                //                                {
                //                                    sReturn = sReturn + "<status>-1</status>" + "<message>" + "编号为" + account.Number + "的科目已使用，不可修改" + "</message>";
                //                                    continue;
                //                                }

                //                                //更新account数据
                //                                string accountID = exist.Tables[0].Rows[0]["FAccountID"].ToString();
                //                                string sSqlUpd = " exec sp_executesql N'UPDATE t_Account";
                //                                sSqlUpd = sSqlUpd + " SET FName=@P2,FDC=@P3,FHelperCode=@P4,FCurrencyID=@P5,FAdjustRate=@P6,FContact=@P7,FQuantities=@P8,FUnitGroupID=@P9,FMeasureUnitID=@P10,FDetailID=@P11,FIsCashFlow=@P12 ";
                //                                sSqlUpd = sSqlUpd + " ,FIsBusi=@P13,FAcnt=@P14,FAcntType=@P15,FInterest=@P16,FAcctint=@P17,FintRate=@P18,FLastIntDate=@P19,FTradeNum=@P20,FControl=@P21,FViewMsg=@P22,FMessage=@P23,FDelete=@P24,FControlSystem=@P25 WHERE FAccountID=" + accountID + "' ";
                //                                sSqlUpd = sSqlUpd + " ,N'@P2 varchar(80),@P3 int,@P4 varchar(40),@P5 int,@P6 bit,@P7 bit,@P8 bit,@P9 int,@P10 int,@P11 int,@P12 bit,@P13 bit,@P14 bit,@P15 int,@P16 bit,@P17 bit,@P18 float,@P19 datetime,@P20 varchar(40),@P21 int,@P22 bit,@P23 varchar(100),@P24 int,@P25 int' ";
                //                                sSqlUpd = sSqlUpd + " ,@FName,@FDC,NULL,@FCurrency,@FAdjustRate,@FContact,@FQuantities,0,0,@FDetailID,@FIsCashFlow,0,0,0,0,@FAcctint,0,NULL,NULL,0,0,NULL,0,0";
                //                                sSqlUpd = sSqlUpd + " exec sp_executesql N'Update t_Account Set FFullName = '''' Where FNumber Like @P1',N'@P1 varchar(40)','@FNumber.%'";
                //                                DbHelperSQL.ExecuteSql(sSqlUpd, oParaEditNeed);
                //                                sReturn = sReturn + "<status>0</status>" + "<message>" + account.Number + "科目修改成功" + "</message>";
                //                            }
                //                            else
                //                            {
                //                                sReturn = sReturn + "<status>-1</status>" + "<message>" + "编号为" + account.Number + "的科目不存在" + "</message>";

                //                            }


                //                        }
                //                        catch (Exception ex)
                //                        {

                //                            sReturn = sReturn + "<status>-1</status>" + "<message>" + "编号为" + account.Number + "插入出错" + ex.Message.ToString() + "</message>";
                //                        }
                //                    }
                //                    break;
                #endregion

            }


            return "<?xml version=\"1.0\" encoding=\"utf-8\"?> <xml>" + sReturn + "</xml>";

        }


    }
}
