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
    /// IVoucher 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class IVoucher : System.Web.Services.WebService
    {
        ///<summary>
        ///凭证维护接口
        ///<\summary>
        /// <param name="sToken"></param>
        /// <param name="sTop"></param>
        /// <param name="sWhere"></param>
        /// <param name="sOrder"></param>
        /// <returns></returns>
        [WebMethod(Description = "凭证维护接口:包含新增凭证功能")]
        public string T_Voucher(string uid, string pwd, string xml)
        {
            if (uid != "k3WrAdmin" || pwd != "kd147258")
            {
                return "你的id或psw错误";
            }
            string sReturn = string.Empty;

            //XElement xe = XElement.Load(xml);
            //XElement xe = XElement.Load(@"D:\mldotNet\ik3DtWs\T_Dept.xml");
            XElement xe = XElement.Parse(xml.Trim());
            IEnumerable<XElement> elements = from ele in xe.Elements("T_Voucher")
                                             select ele;
            //showInfoByElements(elements);

            List<voucherModel> modelList = new List<voucherModel>();

            try
            {
                //凭证数据模型
                foreach (var ele in elements)
                {
                    voucherModel model = new voucherModel();
                    if (!string.IsNullOrEmpty(ele.Element("FNumber").Value))
                    {
                        model.Number = ele.Element("FNumber").Value;
                    }
                    if (!string.IsNullOrEmpty(ele.Element("FDebitTotal").Value))
                    {
                        model.DebitTotal = ele.Element("FDebitTotal").Value;
                    }
                    if (!string.IsNullOrEmpty(ele.Element("FDate").Value))
                    {
                        model.Date = ele.Element("FDate").Value;
                    }
                    //if (!string.IsNullOrEmpty(ele.Element("FFootNote").Value))
                    //{
                    //    model.FootNote = ele.Element("FFootNote").Value;
                    //}
                    if (!string.IsNullOrEmpty(ele.Element("FGroupID").Value))
                    {
                        model.GroupID = ele.Element("FGroupID").Value;
                    }
                    if (!string.IsNullOrEmpty(ele.Element("FPeriod").Value))
                    {
                        model.Period = ele.Element("FPeriod").Value;
                    }
                    if (!string.IsNullOrEmpty(ele.Element("FPreparerID").Value))
                    {
                        model.PreparerID = ele.Element("FPreparerID").Value;
                    }
                    if (!string.IsNullOrEmpty(ele.Element("FReference").Value))
                    {
                        model.Reference = ele.Element("FReference").Value;
                    }
                    if (!string.IsNullOrEmpty(ele.Element("FSerialNum").Value))
                    {
                        model.SerialNum = ele.Element("FSerialNum").Value;
                    }
                    if (!string.IsNullOrEmpty(ele.Element("FTransDate").Value))
                    {
                        model.TransDate = ele.Element("FTransDate").Value;
                    }
                    if (!string.IsNullOrEmpty(ele.Element("FYear").Value))
                    {
                        model.Year = ele.Element("FYear").Value;
                    }
                    if (!string.IsNullOrEmpty(ele.Element("FExplanation").Value))
                    {
                        model.Explanation = ele.Element("FExplanation").Value;
                    }
                    if (!string.IsNullOrEmpty(ele.Element("FHandler").Value))
                    {
                        model.Handler = ele.Element("FHandler").Value;
                    }
                    if (!string.IsNullOrEmpty(ele.Element("FParameter").Value))
                    {
                        model.Parameter = ele.Element("FParameter").Value;
                    }

                    IEnumerable<XElement> entrys = from eleEn in ele.Element("T_VoucherEntrys").Elements("T_VoucherEntry")
                                                   select eleEn;
                    //分录数据模型
                    foreach (var eleEn in entrys)
                    {
                        voucherEnModel EnModel = new voucherEnModel();
                        if (!string.IsNullOrEmpty(eleEn.Element("FAccountName").Value))
                        {
                            EnModel.AccountName = eleEn.Element("FAccountName").Value;
                        }
                        if (!string.IsNullOrEmpty(eleEn.Element("FAccountNum").Value))
                        {
                            EnModel.AccountNum = eleEn.Element("FAccountNum").Value;
                        }
                        if (!string.IsNullOrEmpty(eleEn.Element("FAmountFor").Value))
                        {
                            EnModel.AmountFor = eleEn.Element("FAmountFor").Value;
                        }
                        if (!string.IsNullOrEmpty(eleEn.Element("FEntryID").Value))
                        {
                            EnModel.EntryID = eleEn.Element("FEntryID").Value;
                        }
                        if (!string.IsNullOrEmpty(eleEn.Element("FDC").Value))
                        {
                            EnModel.DC = eleEn.Element("FDC").Value;
                        }
                        if (!string.IsNullOrEmpty(eleEn.Element("FExplanation").Value))
                        {
                            EnModel.Explanation = eleEn.Element("FExplanation").Value;
                        }
                        if (!string.IsNullOrEmpty(eleEn.Element("FTransNo").Value))
                        {
                            EnModel.TransNo = eleEn.Element("FTransNo").Value;
                        }
                        if (!string.IsNullOrEmpty(eleEn.Element("FSettleNo").Value))
                        {
                            EnModel.SettleNo = eleEn.Element("FSettleNo").Value;
                        }
                        if (!string.IsNullOrEmpty(eleEn.Element("FDeptNum").Value))
                        {
                            EnModel.DeptNum = eleEn.Element("FDeptNum").Value;
                        }
                        if (!string.IsNullOrEmpty(eleEn.Element("FDeptName").Value))
                        {
                            EnModel.DeptName = eleEn.Element("FDeptName").Value;
                        }
                        if (!string.IsNullOrEmpty(eleEn.Element("FProjNum").Value))
                        {
                            EnModel.ProjNum = eleEn.Element("FProjNum").Value;
                        }
                        if (!string.IsNullOrEmpty(eleEn.Element("FProjName").Value))
                        {
                            EnModel.ProjName = eleEn.Element("FProjName").Value;
                        }


                        model.Entrys.Add(EnModel);

                    }
                    modelList.Add(model);
                }
            }
            catch (Exception ex)
            {

                sReturn = sReturn + "<status>011</status>" + "<message>凭证输入信息有误，输入数据缺失" + ex.ToString() + "</message>";
                return "<?xml version=\"1.0\" encoding=\"utf-8\"?> <xml>" + sReturn + "</xml>";
            }

            DataSet ods = new DataSet();
            foreach (var voucher in modelList)
            {
                string voucherid = null;
                try
                {

                    //查询凭证是否存在
                    //若存在则跳出本次循环
                    string sSql = " select * from t_Voucher where FNumber=@FNumber and FGroupID=@FGroupID and FYear=@FYear and FPeriod=@FPeriod";
                    SqlParameter[] oParaCZ = {
                                        new SqlParameter("@FYear",SqlDbType.Int), 
                                        new SqlParameter("@FPeriod",SqlDbType.Int), 
                                        new SqlParameter("@FGroupID",SqlDbType.Int), 
                                        new SqlParameter("@FNumber",SqlDbType.Int),
                                                    };
                    oParaCZ[0].Value = voucher.Year;
                    oParaCZ[1].Value = voucher.Period;
                    oParaCZ[2].Value = voucher.GroupID;
                    //oParaCZ[3].Value = voucher.Number;
                    if (voucher.Number != null) { oParaCZ[3].Value = voucher.Number; }
                    int maxNum = Convert.ToInt32(DbHelperSQL.GetSingle("select isnull(MAX(FNumber),0) maxNum from t_voucher where FYear=@FYear and FPeriod=@FPeriod and FGroupID=@FGroupID", oParaCZ)) + 1;
                    oParaCZ[3].Value = maxNum;
                    ods = DbHelperSQL.Query(sSql, oParaCZ);
                    if (ods != null && ods.Tables[0].Rows.Count > 0)
                    {
                        sReturn = sReturn + "<status>010</status>" + "<message>" + "凭证日期为" + voucher.Date + "的凭证已存在" + "</message>";
                        continue;
                    }
                    string checkPeriod = "select FValue  from t_SystemProfile where FKey='cn_current_period' or FKey='cn_current_year'";
                    ods = DbHelperSQL.Query(checkPeriod);
                    //if (ods != null && ods.Tables[0].Rows.Count > 0)
                    //{
                    //    if (!ods.Tables[0].Rows[0]["FValue"].ToString().Equals(oParaCZ[0].Value.ToString()))
                    //    {
                    //        sReturn = sReturn + "<status>011</status>" + "<message>" + "凭证年限不等于当前k3凭证年限" + voucher.Year + "</message>";
                    //        continue;
                    //    }
                    //    if (!ods.Tables[0].Rows[0]["FValue"].ToString().Equals(oParaCZ[1].Value.ToString()))
                    //    {
                    //        sReturn = sReturn + "<status>012</status>" + "<message>" + "凭证期间不等于当前k3凭证期间" + voucher.Period + "</message>";
                    //        continue;
                    //    }

                    //}



                    //插入凭证头表
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("exec sp_executesql N'INSERT INTO t_Voucher (");
                    strSql.Append("FDate,FTransDate,FYear,FPeriod,FGroupID,FNumber,FReference,FExplanation,FAttachments,FEntryCount");
                    strSql.Append(",FDebitTotal,FCreditTotal,FInternalInd,FChecked,FPosted,FPreparerID,FCheckerID,FPosterID,FCashierID");
                    strSql.Append(",FHandler,FObjectName,FParameter,FSerialNum,FTranType,FOwnerGroupID)");
                    strSql.Append(" VALUES (@P1,@P2,@P3,@P4,@P5,@P6,@P7,@P8,@P9,@P10,@P11,@P12,@P13,@P14,@P15,@P16,@P17,@P18,@P19,@P20,@P21,@P22,@P23,@P24,@P25)'");
                    strSql.Append(",N'@P1 datetime,@P2 datetime,@P3 int,@P4 int,@P5 int,@P6 int,@P7 varchar(255),@P8 varchar(255),@P9 int,@P10 int,@P11 money,@P12 money,@P13 varchar(10),@P14 bit,@P15 bit,@P16 int,@P17 int,@P18 int,@P19 int,@P20 varchar(50),@P21 varchar(100),@P22 varchar(100),@P23 int,@P24 int,@P25 int'");
                    strSql.Append(",@FDate,@FTransDate,@FYear,@FPeriod,@FGroupID,@FNumber,@FReference,@FExplanation,0,@FEntryCount");
                    strSql.Append(",@FDebitTotal,@FDebitTotal,NULL,0,0,@FPreparerID,-1,-1,-1");
                    strSql.Append(",@FHandler,NULL,@FParameter,@FSerialNum,0,1");
                    SqlParameter[] oPara ={
                                        new SqlParameter("@FDate",SqlDbType.DateTime),
                                        new SqlParameter("@FTransDate",SqlDbType.DateTime),
                                        new SqlParameter("@FYear",SqlDbType.Int), 
                                        new SqlParameter("@FPeriod",SqlDbType.Int), 
                                        new SqlParameter("@FGroupID",SqlDbType.Int), 
                                        new SqlParameter("@FNumber",SqlDbType.Int),
                                        new SqlParameter("@FReference",SqlDbType.VarChar),
                                        new SqlParameter("@FExplanation",SqlDbType.VarChar),
                                        new SqlParameter("@FEntryCount",SqlDbType.Int),
                                        new SqlParameter("@FDebitTotal",SqlDbType.Money),
                                        new SqlParameter("@FSerialNum",SqlDbType.Int),
                                        new SqlParameter("@FFootNote",SqlDbType.VarChar),
                                        new SqlParameter("@FVoucherID",SqlDbType.Int),
                                        new SqlParameter("@FPreparerID",SqlDbType.Int),
                                        new SqlParameter("@FHandler",SqlDbType.VarChar),
                                        new SqlParameter("@FParameter",SqlDbType.VarChar),
                                            };
                    oPara[0].Value = voucher.Date;
                    oPara[1].Value = voucher.TransDate;
                    oPara[2].Value = Convert.ToInt32(voucher.Year);
                    oPara[3].Value = Convert.ToInt32(voucher.Period);
                    oPara[4].Value = Convert.ToInt32(voucher.GroupID);
                    oPara[5].Value = maxNum;
                    oPara[6].Value = voucher.Reference;
                    oPara[7].Value = voucher.Explanation;
                    oPara[8].Value = voucher.Entrys.Count;
                    oPara[9].Value = Convert.ToDecimal(voucher.DebitTotal);
                    object getMaxSerialNum = DbHelperSQL.GetSingle(" select isnull(MAX(FSerialNum),0) from t_Voucher");
                    oPara[10].Value = Convert.ToInt32(getMaxSerialNum) + 1;
                    //oPara[11].Value = voucher.FootNote;
                    oPara[14].Value = voucher.Handler;
                    oPara[15].Value = voucher.Parameter;
                    string getPrepare = "select Fuserid from t_User where FName='" + voucher.PreparerID + "'";
                    if (DbHelperSQL.GetSingle(getPrepare) != null)
                    {
                        oPara[13].Value = DbHelperSQL.GetSingle(getPrepare);
                    }
                    else
                    {
                        sReturn = sReturn + "<status>001</status>" + "<message>" + "凭证日期为" + voucher.Date + "的制单人" + voucher.PreparerID + "在K3系统中不存在，请联系管理员在K3中添加。</message>";
                        continue;
                    }

                    int vouHead = DbHelperSQL.ExecuteSql(strSql.ToString(), oPara);
                    //获取生成的FVoucherID
                    strSql = new StringBuilder();
                    strSql.Append("exec sp_executesql N'SELECT FVoucherID FROM t_Voucher WHERE FYear=@P1 AND FPeriod=@P2 AND FGroupID=@P3 AND FNumber=@P4',N'@P1 int,@P2 int,@P3 int,@P4 int',@FYear,@FPeriod,@FGroupID,@FNumber");
                    oPara[12].Value = DbHelperSQL.GetSingle(strSql.ToString(), oPara);
                    voucherid = DbHelperSQL.GetSingle(strSql.ToString(), oPara).ToString();
                    #region 插入凭证分录表
                    bool flag = false;//记录凭证分录是否有错，默认没错

                    foreach (var voucherEntry in voucher.Entrys)
                    {
                        StringBuilder strSqld = new StringBuilder();
                        string checkDetail = "select FDetailID from t_Account where  FDetail<>0 and FNumber='" + voucherEntry.AccountNum + "'";
                        int checkd = Convert.ToInt32(DbHelperSQL.GetSingle(checkDetail));

                        string checkDE = " select FDetailID from t_ItemDetail where F2=isnull(@FDeptID,0) and F3001=isnull(@FProjectID,0)";
                        strSql = new StringBuilder();
                        //strSql.Append(" select FDetailID from t_ItemDetail where F2=@FDeptID and F3001=@FProjectID");
                        strSql.Append("insert into t_ItemDetail (FDetailCount,F2,F3001) values(@FDetailCount,isnull(@FDeptID,0),isnull(@FProjectID,0))");
                        strSql.Append(" select MAX(FDetailID) from t_ItemDetail");
                        SqlParameter[] oParaDetail ={
                                       new SqlParameter("@FDetailCount",SqlDbType.Int),
                                       new SqlParameter("@FDeptID",SqlDbType.Int),
                                       new SqlParameter("@FProjectID",SqlDbType.Int),
                                       new SqlParameter("@FDetailID",SqlDbType.Int)
                                                   };
                        //oParaDetail[0].Value = oPara[8].Value;
                        //查询部门内码
                        if (!string.IsNullOrEmpty(voucherEntry.DeptNum))
                        {
                            string qDept = "select FItemID from t_Department where FNumber='" + voucherEntry.DeptNum + "' and FName='" + voucherEntry.DeptName + "'";
                            oParaDetail[1].Value = DbHelperSQL.GetSingle(qDept);
                            if (oParaDetail[1].Value == null)
                            {
                                sReturn = sReturn + "<status>002</status>" + "<message>" + "凭证日期为" + voucher.Date + "的凭证输入部门" + voucherEntry.DeptNum + "(" + voucherEntry.DeptName + "），并非明细部门，或在K3系统中不存在，请联系管理员添加。</message>";
                                flag = true;
                                break;
                            }
                        }

                        //查询项目内码
                        if (!string.IsNullOrEmpty(voucherEntry.ProjNum))
                        {
                            string qProj = "select FItemID from t_Item where FItemClassID=3001 and FNumber='" + voucherEntry.ProjNum + "'";
                            oParaDetail[2].Value = DbHelperSQL.GetSingle(qProj);
                            if (oParaDetail[2].Value == null)
                            {
                                sReturn = sReturn + "<status>003</status>" + "<message>" + "凭证日期为" + voucher.Date + "的凭证输入项目" + voucherEntry.ProjNum + "(" + voucherEntry.ProjName + "），在K3系统中不存在，请联系管理员添加。</message>";
                                flag = true;
                                break;
                            }
                        }
                        var eDetail=DbHelperSQL.GetSingle(checkDE,oParaDetail);
                        
                        if (eDetail==null)
                        {
                            if (checkd == 0)
                            {
                                oParaDetail[3].Value = 0;
                            }
                            else if (checkd == 6)
                            {
                                //oParaDetail[0].Value = oPara[8].Value;
                                //插入t_itemDetailv表
                                oParaDetail[0].Value = 1;

                                oParaDetail[3].Value = DbHelperSQL.GetSingle(strSql.ToString(), oParaDetail);
                                if (oParaDetail[1].Value != null)
                                {
                                    strSqld.Append(" insert into t_ItemDetailV(FDetailID,FItemClassID,FItemID) values(@FDetailID,2,@FDeptID)");
                                }

                            }
                            else if (checkd == 13)
                            {
                                oParaDetail[0].Value = 2;

                                //插入t_itemDetailv表
                                oParaDetail[3].Value = DbHelperSQL.GetSingle(strSql.ToString(), oParaDetail);
                                if (oParaDetail[1].Value != null)
                                {
                                    strSqld.Append(" insert into t_ItemDetailV(FDetailID,FItemClassID,FItemID) values(@FDetailID,2,@FDeptID)");
                                }
                                if (oParaDetail[2].Value != null)
                                {
                                    strSqld.Append(" insert into t_ItemDetailV(FDetailID,FItemClassID,FItemID) values(@FDetailID,3001,@FProjectID)");
                                }
                            }
                        }
                        else oParaDetail[3].Value = Convert.ToInt32(eDetail);

                        if (!string.IsNullOrWhiteSpace(strSqld.ToString()))
                        {
                            ods = DbHelperSQL.Query(strSqld.ToString(), oParaDetail);
                        }

                        //插入凭证分录语句
                        strSql = new StringBuilder();
                        strSql.Append("exec sp_executesql N'INSERT INTO t_VoucherEntry (");
                        strSql.Append("FVoucherID,FEntryID,FExplanation,FAccountID,FCurrencyID,FExchangeRate,FDC,FAmountFor,FAmount,FQuantity,FMeasureUnitID,FUnitPrice,FInternalInd,FAccountID2,FSettleTypeID,FSettleNo,FCashFlowItem,FTaskID,FResourceID,FTransNo,FDetailID)");
                        strSql.Append(" VALUES (@P1,@P2,@P3,@P4,@P5,@P7,@P8,@P9,@P10,@P11,@P12,@P13,@P14,@P15,@P16,@P17,@P18,@P19,@P20,@P21,@P22)'");
                        strSql.Append(",N'@P1 int,@P2 int,@P3 varchar(255),@P4 int,@P5 int,@P7 float,@P8 int,@P9 money,@P10 money,@P11 float,@P12 int,@P13 float,@P14 varchar(10),@P15 int,@P16 int,@P17 varchar(40),@P18 int,@P19 int,@P20 int,@P21 varchar(255),@P22 int'");
                        strSql.Append(",@FVoucherID,@FEntryID,@FExplanation,@FAccountID,1,1,@FDC,@FAmountFor,@FAmountFor,0,0,0,NULL,1000,0,@FSettleNo,0,0,0,@FTransNo,@FDetailID");
                        SqlParameter[] oParaEntrys = {
                                            new SqlParameter("@FVoucherID",SqlDbType.Int),
                                            new SqlParameter("@FEntryID",SqlDbType.Int),
                                            new SqlParameter("@FExplanation",SqlDbType.VarChar),
                                            new SqlParameter("@FAccountID",SqlDbType.Int),
                                            new SqlParameter("@FAmountFor",SqlDbType.Decimal),
                                            new SqlParameter("@FDetailID",SqlDbType.Int),
                                            new SqlParameter("@FDC",SqlDbType.Int),
                                            new SqlParameter("@FCurrencyID",SqlDbType.Int),
                                            new SqlParameter("@FExchangeRate",SqlDbType.Int),
                                            new SqlParameter("@FTransNo",SqlDbType.VarChar),
                                            new SqlParameter("@FSettleNo",SqlDbType.VarChar),
                                                          };
                        oParaEntrys[0].Value = oPara[12].Value;
                        oParaEntrys[1].Value = voucherEntry.EntryID;
                        oParaEntrys[2].Value = voucherEntry.Explanation;
                        oParaEntrys[4].Value = voucherEntry.AmountFor;
                        oParaEntrys[5].Value = oParaDetail[3].Value;
                        oParaEntrys[6].Value = voucherEntry.DC;
                        oParaEntrys[9].Value = voucherEntry.TransNo;
                        oParaEntrys[10].Value = voucherEntry.SettleNo;


                        //获取fAccountid和FCurrencyID
                        StringBuilder sSqlEntry = new StringBuilder();
                        sSqlEntry.Append("select  FAccountID,FDetailID from t_Account where FDetail<>0 and FNumber='" + voucherEntry.AccountNum + "'");

                        DataSet acc = DbHelperSQL.Query(sSqlEntry.ToString());
                        if (acc == null || acc.Tables[0].Rows.Count == 0)
                        {
                            sReturn = sReturn + "<status>005</status>" + "<message>" + "凭证日期为" + voucher.Date + "的凭证输入费用项目" + voucherEntry.AccountNum + "(" + voucherEntry.AccountName + ")" + "在K3系统中不存在或者非明细项目，请联系管理员在K3中添加该项目</message>";
                            flag = true;
                            break;
                        }
                        //此处FAccountID是FAccountID
                        oParaEntrys[3].Value = acc.Tables[0].Rows[0]["FAccountID"].ToString();
                        if (Convert.ToInt32(acc.Tables[0].Rows[0]["FDetailID"]) == 0)
                        {
                            oParaEntrys[5].Value = 0;
                        }
                        else if (Convert.ToInt32(acc.Tables[0].Rows[0]["FDetailID"]) == 6)
                        {

                            if (oParaDetail[1].Value == null)
                            {
                                sReturn = sReturn + "<status>005</status>" + "<message>" + "凭证日期为" + voucher.Date + "的凭证输入费用项目" + voucherEntry.AccountNum + "(" + voucherEntry.AccountName + ")" + "的部门核算项目缺失，请检查传入参数</message>";
                                flag = true;
                                break;
                            }

                        }
                        else if (Convert.ToInt32(acc.Tables[0].Rows[0]["FDetailID"]) == 13)
                        {
                            if (oParaDetail[1].Value == null)
                            {
                                sReturn = sReturn + "<status>015</status>" + "<message>" + "凭证日期为" + voucher.Date + "的凭证输入费用项目" + voucherEntry.AccountNum + "(" + voucherEntry.AccountName + ")" + "的部门核算项目缺失，请检查传入参数</message>";
                                flag = true;
                                break;
                            }
                            if (oParaDetail[2].Value == null)
                            {
                                sReturn = sReturn + "<status>016</status>" + "<message>" + "凭证日期为" + voucher.Date + "的凭证输入费用项目" + voucherEntry.AccountNum + "(" + voucherEntry.AccountName + ")" + "的项目核算项目缺失，请检查传入参数</message>";
                                flag = true;
                                break;
                            }

                        }
                        ods = DbHelperSQL.Query(strSql.ToString(), oParaEntrys);
                        if (ods == null)
                        {
                            sReturn = sReturn + "<status>006</status>" + "<message>" + "凭证日期为" + voucher.Date + "参数缺失" + "</message>";
                            flag = true;
                            break;
                        }
                    }
                    #endregion

                    //如果分录插入错误，删除之前插入错误的凭证信息。并停止插入此凭证
                    if (flag)
                    {
                        //int maxVoucher = Convert.ToInt32(DbHelperSQL.GetSingle("select MAX(FVoucherID) from t_Voucher "));
                        //int maxVoucherEntry = Convert.ToInt32(DbHelperSQL.GetSingle("select MAX(FVoucherID) from t_VoucherEntry "));
                        //if (maxVoucher > maxVoucherEntry)
                        DbHelperSQL.ExecuteSql("delete from t_Voucher where FVoucherID='" + oPara[12].Value.ToString() + "'");
                        DbHelperSQL.ExecuteSql("delete from t_VoucherEntry where FVoucherID='" + oPara[12].Value.ToString() + "'");
                        sReturn = sReturn + "error1";
                        continue;
                    }

                    //调整凭证中对应科目关系
                    StringBuilder sSqlEnUd = new StringBuilder();
                    //sSqlEnUd.Append(" 	update t_VoucherEntry set FAccountID2=(select top 1 FAccountID from t_VoucherEntry where FAmountFor=(select MAX(FAmountFor) from t_VoucherEntry where FVoucherID=@FVoucherID and FDC=1) and FVoucherID=@FVoucherID and FDC=1) where FDC=0 and FVoucherID=@FVoucherID ");
                    //sSqlEnUd.Append(" 	update t_VoucherEntry set FAccountID2=(select top 1 FAccountID from t_VoucherEntry where FAmountFor=(select MAX(FAmountFor) from t_VoucherEntry where FVoucherID=@FVoucherID and FDC=0) and FVoucherID=@FVoucherID and FDC=0) where FDC=1 and FVoucherID=@FVoucherID ");

                    //sSqlEnUd.Append(" 	update t_VoucherEntry set FAccountID2=(select distinct FAccountID from t_VoucherEntry where FAmountFor=(select MAX(FAmountFor) from t_VoucherEntry where FVoucherID=@FVoucherID and FDC=1) and FVoucherID=@FVoucherID and FDC=1) where FDC=0 and FVoucherID=@FVoucherID ");
                    //sSqlEnUd.Append(" 	update t_VoucherEntry set FAccountID2=(select distinct FAccountID from t_VoucherEntry where FAmountFor=(select MAX(FAmountFor) from t_VoucherEntry where FVoucherID=@FVoucherID and FDC=0) and FVoucherID=@FVoucherID and FDC=0) where FDC=1 and FVoucherID=@FVoucherID ");

                    #region ver14.1 update凭证分录表中的对应科目
                    sSqlEnUd.Append(" 	update t_VoucherEntry set FSideEntryID=-1 where FVoucherID=@FVoucherID							 ");
                    sSqlEnUd.Append(" 	 Select FVoucherID,FEntryID,FDC,FAmount,FAccountID,FSideEntryID  into #VoucherEntry1846375 From t_VoucherEntry where FVoucherID=@FVoucherID									 ");
                    sSqlEnUd.Append(" 	Select FDC from #VoucherEntry1846375 group by FDC									 ");
                    sSqlEnUd.Append(" 	  update a set a.FSideEntryID=b.FSideEntryID  from  t_VoucherEntry a inner join (									 ");
                    sSqlEnUd.Append(" 	 select self.FVoucherID,self.FEntryID,FSideEntryID=min(side.FEntryID)                    									 ");
                    sSqlEnUd.Append(" 	 from #VoucherEntry1846375 self                    									 ");
                    sSqlEnUd.Append(" 	 inner join #VoucherEntry1846375 side on self.FVoucherID=side.FVoucherID                                									 ");
                    sSqlEnUd.Append(" 	 and self.FEntryID<>side.FEntryID                                									 ");
                    sSqlEnUd.Append(" 	 and self.FDC <> side.FDC and self.FAmount = side.FAmount                                 									 ");
                    sSqlEnUd.Append(" 	 and self.FSideEntryID=-1                    									 ");
                    sSqlEnUd.Append(" 	 group by self.FVoucherID,self.FEntryID            									 ");
                    sSqlEnUd.Append(" 	 ) b on a.FVoucherID=b.FVoucherID and a.FEntryID=b.FEntryID and a.FSideEntryID=-1									 ");
                    sSqlEnUd.Append(" 	  update a set a.FSideEntryID=b.FSideEntryID  from  t_VoucherEntry a inner join (              									 ");
                    sSqlEnUd.Append(" 	 select self.FVoucherID,self.FEntryID,FSideEntryID=min(side.FEntryID)                    									 ");
                    sSqlEnUd.Append(" 	 from #VoucherEntry1846375 self                    									 ");
                    sSqlEnUd.Append(" 	 inner join #VoucherEntry1846375 side on self.FVoucherID=side.FVoucherID                                									 ");
                    sSqlEnUd.Append(" 	 and self.FEntryID<>side.FEntryID                                									 ");
                    sSqlEnUd.Append(" 	 and self.FDC <> side.FDC and self.FAmount <> side.FAmount                                 									 ");
                    sSqlEnUd.Append(" 	 and self.FSideEntryID=-1                    									 ");
                    sSqlEnUd.Append(" 	 group by self.FVoucherID,self.FEntryID            									 ");
                    sSqlEnUd.Append(" 	 ) b on a.FVoucherID=b.FVoucherID and a.FEntryID=b.FEntryID  and a.FSideEntryID=-1									 ");
                    sSqlEnUd.Append(" 	 update tve set tve.FAccountID2=ve.FAccountID  from t_VoucherEntry tve  inner join #VoucherEntry1846375 ve on ve.FVoucherID=tve.FVoucherID and tve.FSideEntryID=ve.FEntryID and tve.FSideEntryID<>-1									 ");
                    sSqlEnUd.Append(" 	 Drop table #VoucherEntry1846375									 ");
                    #endregion

                    int a = DbHelperSQL.ExecuteSql(sSqlEnUd.ToString(), oPara);
                    if (!(a == 0))
                    {
                        sReturn = sReturn + "<status>0</status>" + "<message>" + "凭证日期为" + voucher.Date + "插入成功" + "</message>";
                    }
                    else
                    {
                        sReturn = sReturn + "<status>018</status>" + "<message>" + "凭证日期为" + voucher.Date + "凭证分录插入失败！" + "</message>";
                        continue;
                    }

                }
                catch (Exception ex)
                {
                    //数据不完整或数据输入格式有误
                    sReturn = sReturn + "<status>007</status>" + "<message>" + ex.ToString() + "</message>";
                    //int maxVoucher = Convert.ToInt32(DbHelperSQL.GetSingle("select MAX(FVoucherID) from t_Voucher "));
                    //int maxVoucherEntry = Convert.ToInt32(DbHelperSQL.GetSingle("select MAX(FVoucherID) from t_VoucherEntry "));
                    //if (maxVoucher > maxVoucherEntry)
                    //{
                    DbHelperSQL.ExecuteSql("delete from t_Voucher where FVoucherID='" + voucherid + "'");
                    DbHelperSQL.ExecuteSql("delete from t_VoucherEntry where FVoucherID='" + voucherid + "'");

                    //}

                }

            }
            return "<?xml version=\"1.0\" encoding=\"utf-8\"?> <xml>" + sReturn + "</xml>";

        }


        ///<summary>
        ///凭证查询接口
        ///<\summary>
        /// <param name="sToken"></param>
        /// <param name="sTop"></param>
        /// <param name="sWhere"></param>
        /// <param name="sOrder"></param>
        /// <returns></returns>
        [WebMethod(Description = "凭证查询接口:包含查询凭证功能 ")]
        public string T_VoucherQuery(string uid, string pwd, string conInfo)
        {
            if (uid != "k3WrAdmin" || pwd != "kd147258")
            {
                return "<resultRequest><status>-1</status><message>你的id或psw错误！！！</message></resultRequest>";
            }
            StringBuilder sReturn = new StringBuilder();
            sReturn.Append("<root><resultRequest><status>0</status>");
            sReturn.Append("<massage>查询成功</massage></resultRequest>");
            sReturn.Append("<recortList>");
            XElement xe = XElement.Parse(conInfo);
            IEnumerable<XElement> elements = from ele in xe.Elements("T_VoucherSelect")
                                             select ele;

            //showInfoByElements(elements);
            XDocument xdoc = new XDocument();
            //创建声明对象  
            XDeclaration xDeclaration = new XDeclaration("1.0", "utf-8", "yes");
            xdoc.Declaration = xDeclaration;    //指定XML声明对象
            //string path = @"d:\website";

            List<VSModel> modelList = new List<VSModel>();
            foreach (var ele in elements)
            {
                VSModel model = new VSModel();
                if (!string.IsNullOrEmpty(ele.Element("FYear").Value))
                {
                    model.Year = ele.Element("FYear").Value;
                }
                if (!string.IsNullOrEmpty(ele.Element("FPeriod").Value))
                {
                    model.Period = ele.Element("FPeriod").Value;
                }
                if (!string.IsNullOrEmpty(ele.Element("FDeptNum").Value))
                {
                    model.DeptNum = ele.Element("FDeptNum").Value;
                }
                if (!string.IsNullOrEmpty(ele.Element("FDeptName").Value))
                {
                    model.DeptName = ele.Element("FDeptName").Value;
                }
                if (!string.IsNullOrEmpty(ele.Element("MaxFNumber").Value))
                {
                    model.MaxNumber = ele.Element("MaxFNumber").Value;
                }
                if (!string.IsNullOrEmpty(ele.Element("MinFNumber").Value))
                {
                    model.MinNumber = ele.Element("MinFNumber").Value;
                }
                modelList.Add(model);
            }
            DataSet ods = new DataSet();

            try
            {
                //foreach (var select in modelList)
                //{
                StringBuilder strSql = new StringBuilder();
                strSql.Append(@" select distinct t1.FVoucherID, CONVERT(varchar(100),t1.FDate, 120) FDate,CONVERT(varchar(100),t1.FTransDate, 120) FTransDate,t1.FYear,t1.FPeriod,t1.FGroupID,t1.FNumber,FReference=isnull(t1.FReference,''),FExplanation=isnull(t1.FExplanation,''),t4.FName FPreparerID,t1.FDebitTotal,FHandler=isnull(t1.FHandler,''),FParameter=isnull(t1.FParameter,''),t1.FFootNote
                                    from t_Voucher t1 
                                    join t_VoucherEntry t2 on t1.FVoucherID=t2.FVoucherID 
                                    join t_ItemDetailV t3 on t2.FDetailID=t3.FDetailID 
                                    join t_user t4 on t4.FUserID=t1.FPreparerID
                                    where FYear=ISNULL(@FYear,FYear) and FPeriod=isnull(@FPeriod,FPeriod) and t3.FItemID=ISNULL(@FItemId,t3.FItemID) 
                                    and (FNumber<=ISNULL(@MaxFnumber,FNumber) and FNumber>=ISNULL(@MinFnumber,1))");
                SqlParameter[] opara ={
                                     new SqlParameter("@FYear",SqlDbType.Int),
                                     new SqlParameter("@FPeriod",SqlDbType.Int),
                                     new SqlParameter("@FDeptNum",SqlDbType.VarChar),
                                     new SqlParameter("@FDeptName",SqlDbType.VarChar),
                                     new SqlParameter("@FItemId",SqlDbType.Int),
                                     new SqlParameter("@MaxFNumber",SqlDbType.Int),
                                     new SqlParameter("@MinFNumber",SqlDbType.Int)
                                     };
                opara[0].Value = modelList[0].Year;
                opara[1].Value = modelList[0].Period;
                opara[2].Value = modelList[0].DeptNum;
                opara[3].Value = modelList[0].DeptName;
                opara[5].Value = modelList[0].MaxNumber;
                opara[6].Value = modelList[0].MinNumber;

                StringBuilder sSql = new StringBuilder();
                sSql.Append("select FItemId from t_Department where FNumber=@FDeptNum and FName like '%" + opara[3].Value + "%'");
                opara[4].Value = DbHelperSQL.GetSingle(sSql.ToString(), opara);
                ods = DbHelperSQL.Query(strSql.ToString(), opara);
                if (ods != null && ods.Tables[0].Rows.Count == 0)
                {
                    return "<resultRequest><status>008</status><message>此次查询无返回值！！！</message></resultRequest>";
                }

                //sReturn.Append (StopwatchTest(xdoc,ods,opara));
                //sReturn.Append(CreateVoucher4(xdoc, ods));
                sReturn.Append(CreateVoucher5(xdoc, ods, opara));
                //sReturn.Append(StopwatchTest(xdoc, ods,opara));
                sReturn.Append("</recortList></root>");
                //}
                return sReturn.ToString();
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        protected string StopwatchTest(XDocument xdoc, DataSet ods)
        {
            System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
            stopwatch.Start(); //  开始监视代码

            //_________________要执行的函数______________________
            //CreateVoucher1(xdoc, ods);
            CreateVoucher4(xdoc, ods);
            //ConvertDataSetToXML(ods);

            stopwatch.Stop(); //  停止监视
            TimeSpan timeSpan = stopwatch.Elapsed; //  获取总时间
            double hours = timeSpan.TotalHours; // 小时
            double minutes = timeSpan.TotalMinutes;  // 分钟
            double seconds = timeSpan.TotalSeconds;  //  秒数
            double milliseconds = timeSpan.TotalMilliseconds;  //  毫秒数
            return seconds.ToString();
        }

        protected string StopwatchTest(XDocument xdoc, DataSet ods, SqlParameter[] opara)
        {
            System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
            stopwatch.Start(); //  开始监视代码

            //_________________要执行的函数______________________
            //CreateVoucher1(xdoc, ods);
            CreateVoucher5(xdoc, ods, opara);
            //ConvertDataSetToXML(ods);

            stopwatch.Stop(); //  停止监视
            TimeSpan timeSpan = stopwatch.Elapsed; //  获取总时间
            double hours = timeSpan.TotalHours; // 小时
            double minutes = timeSpan.TotalMinutes;  // 分钟
            double seconds = timeSpan.TotalSeconds;  //  秒数
            double milliseconds = timeSpan.TotalMilliseconds;  //  毫秒数
            return seconds.ToString();
        }


        /// <summary>  
        /// 新增一个凭证xml
        /// </summary>  
        public string CreateVoucher(XDocument VoucherDoc, DataSet ods)
        {
            //创建T_Vouchers节点  
            XElement vouchers = new XElement("T_Vouchers");
            foreach (DataRow voRow in ods.Tables[0].Rows)
            {
                var FVoucherID = voRow["FVoucherID"].ToString();
                StringBuilder sEnQuery = new StringBuilder(@" select distinct t1.FVoucherID,t2.FNumber FAccountNum,t2.FName FAccountName,t1.FEntryID,t1.FAmountFor,t1.FDC,t3.FNumber FCurrencyNum,t3.FName FCurrencyName,t1.FExchangeRate,t1.FExplanation,t1.FDetailID
                                                            from t_VoucherEntry t1 
                                                            join t_Account t2 on t1.FAccountID=t2.FAccountID
                                                            join t_Currency t3 on t1.FCurrencyID=t3.FCurrencyID
                                                            where t1.FVoucherID=" + FVoucherID);
                DataSet odsEQ = new DataSet();
                odsEQ = DbHelperSQL.Query(sEnQuery.ToString());
                //创建t_voucher节点  
                XElement Voucher = new XElement("T_Voucher");
                ////添加子节点
                //Voucher.Add(new XElement("FVoucherID", voRow["FVoucherID"].ToString()));
                //Voucher.Add(new XElement("FDate", voRow["FDate"].ToString()));
                //Voucher.Add(new XElement("FTransDate", voRow["FTransDate"].ToString()));
                //Voucher.Add(new XElement("FYear", voRow["FYear"].ToString()));
                //Voucher.Add(new XElement("FPeriod", voRow["FPeriod"].ToString()));
                //Voucher.Add(new XElement("FGroupID", voRow["FGroupID"].ToString()));
                //Voucher.Add(new XElement("FNumber", voRow["FNumber"].ToString()));
                //Voucher.Add(new XElement("FDebitTotal", voRow["FDebitTotal"].ToString()));
                //Voucher.Add(new XElement("FPreparerID", voRow["FPreparerID"].ToString()));
                //Voucher.Add(new XElement("FReference", voRow["FReference"].ToString()));
                //Voucher.Add(new XElement("FExplanation", voRow["FExplanation"].ToString()));
                ////Voucher.Add(new XElement("FSerialNum", voRow["FSerialNum"]));
                //Voucher.Add(new XElement("FParameter", voRow["FParameter"].ToString()));
                //Voucher.Add(new XElement("FFootNote", voRow["FFootNote"].ToString()));

                //添加子节点
                Voucher.Add(new XElement("FVoucherID", FVoucherID));
                Voucher.Add(new XElement("FDate", voRow["FDate"]));
                Voucher.Add(new XElement("FTransDate", voRow["FTransDate"]));
                Voucher.Add(new XElement("FYear", voRow["FYear"]));
                Voucher.Add(new XElement("FPeriod", voRow["FPeriod"]));
                Voucher.Add(new XElement("FGroupID", voRow["FGroupID"]));
                Voucher.Add(new XElement("FNumber", voRow["FNumber"]));
                Voucher.Add(new XElement("FDebitTotal", voRow["FDebitTotal"]));
                Voucher.Add(new XElement("FPreparerID", voRow["FPreparerID"]));
                Voucher.Add(new XElement("FReference", voRow["FReference"]));
                Voucher.Add(new XElement("FExplanation", voRow["FExplanation"]));
                //Voucher.Add(new XElement("FSerialNum", voRow["FSerialNum"]));
                Voucher.Add(new XElement("FParameter", voRow["FParameter"]));
                Voucher.Add(new XElement("FFootNote", voRow["FFootNote"]));

                XElement voucherEntrys = new XElement("T_VoucherEntrys");
                Voucher.Add(voucherEntrys);
                foreach (DataRow voEnRow in odsEQ.Tables[0].Rows)
                {
                    XElement voucherEntry = new XElement("T_VoucherEntry");
                    voucherEntry.Add(new XElement("FVoucherID", voRow["FVoucherID"]));
                    voucherEntry.Add(new XElement("FAccountNum", voEnRow["FAccountNum"]));
                    voucherEntry.Add(new XElement("FAccountName", voEnRow["FAccountName"]));
                    voucherEntry.Add(new XElement("FAmountFor", voEnRow["FAmountFor"]));
                    voucherEntry.Add(new XElement("FDC", voEnRow["FDC"]));
                    voucherEntry.Add(new XElement("FCurrencyNum", voEnRow["FCurrencyNum"]));
                    voucherEntry.Add(new XElement("FCurrencyName", voEnRow["FCurrencyName"]));
                    voucherEntry.Add(new XElement("FExchangeRate", voEnRow["FExchangeRate"]));
                    if (voEnRow["FExplanation"] != null)
                    {
                        voucherEntry.Add(new XElement("FExplanation", voEnRow["FExplanation"]));
                    }
                    else voucherEntry.Add(new XElement("FExplanation", ""));
                    voucherEntry.Add(new XElement("FEntryID", voEnRow["FEntryID"]));

                    DataSet odsr = new DataSet();
                    string strSqlQ = @"select FNumber,FName from t_Department  t1 
                            join t_ItemDetailV t2 on t1.FItemID=t2.FItemID
                            join t_VoucherEntry t3 on t2.FDetailID=t3.FDetailID and t3.FDetailID=" + voEnRow["FDetailID"];
                    odsr = DbHelperSQL.Query(strSqlQ);
                    if (odsr != null && odsr.Tables[0].Rows.Count > 0)
                    {
                        voucherEntry.Add(new XElement("FDeptNum", odsr.Tables[0].Rows[0]["FNumber"]));
                        voucherEntry.Add(new XElement("FDeptName", odsr.Tables[0].Rows[0]["FName"]));
                    }

                    strSqlQ = @"select FNumber,FName from t_Item  t1 
                            join t_ItemDetailV t2 on t1.FItemID=t2.FItemID
                            join t_VoucherEntry t3 on t2.FDetailID=t3.FDetailID and t3.FDetailID=" + voEnRow["FDetailID"] + @"
                            where t1.FItemClassID=3001";
                    odsr = DbHelperSQL.Query(strSqlQ);
                    if (odsr != null && odsr.Tables[0].Rows.Count > 0)
                    {
                        voucherEntry.Add(new XElement("FProjNum", odsr.Tables[0].Rows[0]["FNumber"]));
                        voucherEntry.Add(new XElement("FProjName", odsr.Tables[0].Rows[0]["FName"]));
                    }
                    voucherEntrys.Add(voucherEntry);
                }


                //将T_voucher节点添加到T_vouchers节点中  
                vouchers.Add(Voucher);
            }
            //保存文件  
            VoucherDoc.Add(vouchers);
            return VoucherDoc.ToString();
        }


        public string CreateVoucher1(XDocument VoucherDoc, DataSet ods)
        {
            ////创建T_Vouchers节点  
            //XElement vouchers = new XElement("T_Vouchers");
            MemoryStream stream = null;
            XmlTextWriter writer = null;

            stream = new MemoryStream();
            //从stream装载到XmlTextReader
            writer = new XmlTextWriter(stream, Encoding.Unicode);
            writer.Formatting = Formatting.Indented;
            writer.WriteStartElement("T_Vouchers");

            foreach (DataRow voRow in ods.Tables[0].Rows)
            {
                var FVoucherID = voRow["FVoucherID"].ToString();
                StringBuilder sEnQuery = new StringBuilder(@" select distinct t1.FVoucherID,t2.FNumber FAccountNum,t2.FName FAccountName,t1.FEntryID,t1.FAmountFor,t1.FDC,t3.FNumber FCurrencyNum,t3.FName FCurrencyName,t1.FExchangeRate,t1.FExplanation,t1.FDetailID
                                                            from t_VoucherEntry t1 
                                                            join t_Account t2 on t1.FAccountID=t2.FAccountID
                                                            join t_Currency t3 on t1.FCurrencyID=t3.FCurrencyID
                                                            where t1.FVoucherID=" + FVoucherID);
                DataSet odsEQ = new DataSet();
                odsEQ = DbHelperSQL.Query(sEnQuery.ToString());
                ////创建t_voucher节点  
                //XElement Voucher = new XElement("T_Voucher");
                writer.WriteStartElement("T_Voucher");
                //添加子节点
                writer.WriteStartElement("FVoucherID", voRow["FVoucherID"].ToString());
                writer.WriteStartElement("FDate", voRow["FDate"].ToString());
                writer.WriteStartElement("FTransDate", voRow["FTransDate"].ToString());
                writer.WriteStartElement("FYear", voRow["FYear"].ToString());
                writer.WriteStartElement("FPeriod", voRow["FPeriod"].ToString());
                writer.WriteStartElement("FGroupID", voRow["FGroupID"].ToString());
                writer.WriteStartElement("FNumber", voRow["FNumber"].ToString());
                writer.WriteStartElement("FDebitTotal", voRow["FDebitTotal"].ToString());
                writer.WriteStartElement("FPreparerID", voRow["FPreparerID"].ToString());
                writer.WriteStartElement("FReference", voRow["FReference"].ToString());
                writer.WriteStartElement("FExplanation", voRow["FExplanation"].ToString());
                //writer.WriteStartElement("FSerialNum", voRow["FSerialNum"].ToString());
                writer.WriteStartElement("FParameter", voRow["FParameter"].ToString());
                writer.WriteStartElement("FFootNote", voRow["FFootNote"].ToString());
                writer.WriteStartElement("T_VoucherEntrys");

                //XElement voucherEntrys = new XElement("T_VoucherEntrys");
                //Voucher.Add(voucherEntrys);
                foreach (DataRow voEnRow in odsEQ.Tables[0].Rows)
                {
                    writer.WriteStartElement("T_VoucherEntry");
                    //XElement voucherEntry = new XElement("T_VoucherEntry");
                    writer.WriteElementString("FVoucherID", voRow["FVoucherID"].ToString());
                    writer.WriteElementString("FAccountNum", voEnRow["FAccountNum"].ToString());
                    writer.WriteElementString("FAccountName", voEnRow["FAccountName"].ToString());
                    writer.WriteElementString("FAmountFor", voEnRow["FAmountFor"].ToString());
                    writer.WriteElementString("FDC", voEnRow["FDC"].ToString());
                    writer.WriteElementString("FCurrencyNum", voEnRow["FCurrencyNum"].ToString());
                    writer.WriteElementString("FCurrencyName", voEnRow["FCurrencyName"].ToString());
                    writer.WriteElementString("FExplanation", voEnRow["FExplanation"].ToString());
                    writer.WriteElementString("FEntryID", voEnRow["FEntryID"].ToString());

                    string strSqlQ;
                    DataSet odsr = new DataSet();
                    strSqlQ = @"select FNumber,FName from t_Department  t1 
                            join t_ItemDetailV t2 on t1.FItemID=t2.FItemID
                            join t_VoucherEntry t3 on t2.FDetailID=t3.FDetailID and t3.FDetailID=" + voEnRow["FDetailID"];
                    odsr = DbHelperSQL.Query(strSqlQ);
                    if (odsr != null && odsr.Tables[0].Rows.Count > 0)
                    {
                        writer.WriteElementString("FDeptNum", odsr.Tables[0].Rows[0]["FNumber"].ToString());
                        writer.WriteElementString("FDeptName", odsr.Tables[0].Rows[0]["FName"].ToString());

                    }

                    strSqlQ = @"select FNumber FProjNum,FName FProjName from t_Item  t1 
                            join t_ItemDetailV t2 on t1.FItemID=t2.FItemID
                            join t_VoucherEntry t3 on t2.FDetailID=t3.FDetailID and t3.FDetailID=" + voEnRow["FDetailID"] + @"
                            where t1.FItemClassID=3001";
                    odsr = DbHelperSQL.Query(strSqlQ);
                    if (odsr != null && odsr.Tables[0].Rows.Count > 0)
                    {
                        writer.WriteElementString("FProjNum", odsr.Tables[0].Rows[0]["FProjNum"].ToString());
                        writer.WriteElementString("FProjName", odsr.Tables[0].Rows[0]["FProjName"].ToString());
                    }
                    writer.WriteEndElement();
                }

                writer.WriteEndElement();
            }
            writer.WriteFullEndElement();
            //保存文件  
            int count = (int)stream.Length;
            byte[] arr = new byte[count];
            stream.Seek(0, SeekOrigin.Begin);
            //stream.Seek(0, SeekOrigin.Current);
            stream.Read(arr, 0, count);


            UnicodeEncoding utf = new UnicodeEncoding();
            writer.Close();
            return utf.GetString(arr);

        }

        /// <summary>  
        /// 新增一个凭证xml
        /// </summary>  
        public string CreateVoucher3(XDocument VoucherDoc, DataSet ods)
        {
            string sReturn = ConvertDataSetToXML(ods);
            sReturn = sReturn.Replace("NewDataSet", "T_Vouchers");
            sReturn = sReturn.Replace("Table", "T_Voucher");
            string Entry = "";
            foreach (DataRow voRow in ods.Tables[0].Rows)
            {
                var FVoucherID = voRow["FVoucherID"].ToString();
                StringBuilder sEnQuery = new StringBuilder(@" select distinct t1.FVoucherID,t2.FNumber FAccountNum,t2.FName FAccountName,t1.FEntryID,t1.FAmountFor,t1.FDC,t3.FNumber FCurrencyNum,t3.FName FCurrencyName,t1.FExplanation,t5.FNumber FDeptNum,t5.FName FDeptName,t1.FDetailID,t6.FNumber FProjNum,t6.FName FProjName
                                                              from t_VoucherEntry t1 
                                                              join t_Account t2 on t1.FAccountID=t2.FAccountID
                                                              join t_Currency t3 on t1.FCurrencyID=t3.FCurrencyID
                                                              join t_ItemDetailV t4 on t1.FDetailID=t4.FDetailID
                                                              left join t_Department t5 on t4.FItemID=t5.FItemID
                                                              join t_Item t6 on t4.FItemID=t6.FItemID and t6.FItemClassID=3001
                                                              where t1.FVoucherID=" + FVoucherID);
                DataSet odsEQ = new DataSet();
                odsEQ = DbHelperSQL.Query(sEnQuery.ToString());
                if (odsEQ != null && odsEQ.Tables[0].Rows.Count == 0)
                {
                    sEnQuery = new StringBuilder(@" select distinct t1.FVoucherID,t2.FNumber FAccountNum,t2.FName FAccountName,t1.FEntryID,t1.FAmountFor,t1.FDC,t3.FNumber FCurrencyNum,t3.FName FCurrencyName,t1.FExplanation,t1.FDetailID
                                                            from t_VoucherEntry t1 
                                                            join t_Account t2 on t1.FAccountID=t2.FAccountID
                                                            join t_Currency t3 on t1.FCurrencyID=t3.FCurrencyID
                                                            where t1.FVoucherID=" + FVoucherID);
                    odsEQ = DbHelperSQL.Query(sEnQuery.ToString());
                }

                Entry = Entry + ConvertDataSetToXML(odsEQ);
                Entry = Entry.Replace("NewDataSet", "T_VoucherEntrys");
                Entry = Entry.Replace("Table", "T_VoucherEntry");
            }
            return sReturn + Entry;
        }

        /// <summary>  
        /// 新增一个凭证xml
        /// </summary>  
        public string CreateVoucher4(XDocument VoucherDoc, DataSet ods)
        {
            StringBuilder sR = new StringBuilder();
            string sReturn = ConvertDataSetToXML(ods);

            sReturn = sReturn.Replace("<NewDataSet>", "");
            sReturn = sReturn.Replace("</NewDataSet>", "");
            sReturn = sReturn.Replace("Table", "T_Voucher");
            sR.Append(sReturn);
            StringBuilder entry = new StringBuilder();
            foreach (DataRow voRow in ods.Tables[0].Rows)
            {
                var FVoucherID = voRow["FVoucherID"].ToString();
                StringBuilder sEnQuery = new StringBuilder(@" select distinct t1.FVoucherID,t2.FNumber FAccountNum,t2.FName FAccountName,t1.FEntryID,t1.FAmountFor,t1.FDC,t3.FNumber FCurrencyNum,t3.FName FCurrencyName,t1.FExplanation,t5.FNumber FDeptNum,t5.FName FDeptName,t6.FNumber FProjNum,t6.FName FProjName
                                                              from t_VoucherEntry t1 
                                                              join t_Account t2 on t1.FAccountID=t2.FAccountID
                                                              join t_Currency t3 on t1.FCurrencyID=t3.FCurrencyID
                                                              join t_ItemDetail t4 on t1.FDetailID=t4.FDetailID
                                                              left join t_Department t5 on t4.f2=t5.FItemID
                                                              left join t_Item t6 on t4.f3001=t6.FItemID and t6.FItemClassID=3001
                                                              where t1.FVoucherID=" + FVoucherID);
                DataSet odsEQ = new DataSet();
                odsEQ = DbHelperSQL.Query(sEnQuery.ToString());
                if (odsEQ != null && odsEQ.Tables[0].Rows.Count == 0)
                {
                    sEnQuery = new StringBuilder(@" select distinct t1.FVoucherID,t2.FNumber FAccountNum,t2.FName FAccountName,t1.FEntryID,t1.FAmountFor,t1.FDC,t3.FNumber FCurrencyNum,t3.FName FCurrencyName,t1.FExplanation
                                                            from t_VoucherEntry t1 
                                                            join t_Account t2 on t1.FAccountID=t2.FAccountID
                                                            join t_Currency t3 on t1.FCurrencyID=t3.FCurrencyID
                                                            where t1.FVoucherID=" + FVoucherID);
                    odsEQ = DbHelperSQL.Query(sEnQuery.ToString());
                }
                entry.Append(ConvertDataSetToXML(odsEQ));
            }
            entry = entry.Replace("<NewDataSet>", "");
            entry = entry.Replace("</NewDataSet>", "");
            entry = entry.Replace("Table", "T_VoucherEntry");
            sR.Append(entry);
            return sR.ToString();
        }

        /// <summary>  
        /// 新增一个凭证xml
        /// </summary>  
        public string CreateVoucher5(XDocument VoucherDoc, DataSet ods, SqlParameter[] opara)
        {
            StringBuilder sR = new StringBuilder();
            string sReturn = ConvertDataSetToXML(ods);
            //string sReturn = ods.GetXml();

            sReturn = sReturn.Replace("<NewDataSet>", "");
            sReturn = sReturn.Replace("</NewDataSet>", "");
            sReturn = sReturn.Replace("Table", "T_Voucher");
            sR.Append(sReturn);
            StringBuilder entry = new StringBuilder();
            StringBuilder sEnQuery = new StringBuilder();
            sEnQuery.Append(" 	select  t1.FVoucherID,t2.FNumber FAccountNum,t2.FName FAccountName,t1.FEntryID,FTransNo=isnull(t1.FTransNo,''),FSettleNo=isnull(t1.FSettleNo,''),t1.FAmountFor,t1.FDC,t3.FNumber FCurrencyNum,t3.FName FCurrencyName,FExplanation=isnull(t1.FExplanation,''), FDeptNum=isnull(t6.FNumber,''),FDeptName=isnull(t6.FName,''), FProjNum=isnull(t7.FNumber,''),FProjName=isnull(t7.FName ,'') ");
            sEnQuery.Append(" 	from t_VoucherEntry t1 						 ");
            sEnQuery.Append(" 	join t_Account t2 on t1.FAccountID=t2.FAccountID						 ");
            sEnQuery.Append(" 	join t_Currency t3 on t1.FCurrencyID=t3.FCurrencyID						 ");
            sEnQuery.Append(" 	join (						 ");
            sEnQuery.Append(" 	select distinct t1.FVoucherID FVoucherID						 ");
            sEnQuery.Append(" 	from t_Voucher t1 						 ");
            sEnQuery.Append(" 	join t_VoucherEntry t2 on t1.FVoucherID=t2.FVoucherID 						 ");
            sEnQuery.Append(" 	join t_ItemDetail t3 on t2.FDetailID=t3.FDetailID						 ");
            sEnQuery.Append(" 	where FYear=ISNULL(@FYear,FYear) and FPeriod=isnull(@FPeriod,FPeriod) and t3.F2=ISNULL(@FItemId,t3.f2) ) t4 on t1.FVoucherID=t4.FVoucherID						 ");
            sEnQuery.Append(" 	left join t_ItemDetail t5 on t1.FDetailID=t5.FDetailID						 ");
            sEnQuery.Append(" 	left join t_Department t6 on t5.f2=t6.FItemID						 ");
            sEnQuery.Append(" 	left join t_Item t7 on t5.f3001=t7.FItemID and t7.FItemClassID=3001						 ");

            DataSet odsEQ = new DataSet();
            odsEQ = DbHelperSQL.Query(sEnQuery.ToString(), opara);

            //entry.Append(odsEQ.GetXml());
            entry.Append(ConvertDataSetToXML(odsEQ));
            entry = entry.Replace("<NewDataSet>", "");
            entry = entry.Replace("</NewDataSet>", "");
            entry = entry.Replace("Table", "T_VoucherEntry");
            sR.Append(entry);
            return sR.ToString();
        }

        public static string ConvertDataSetToXML(DataSet xmlDS)
        {
            MemoryStream stream = null;
            XmlTextWriter writer = null;

            try
            {
                stream = new MemoryStream();
                //从stream装载到XmlTextReader
                writer = new XmlTextWriter(stream, Encoding.Unicode);

                //用WriteXml方法写入文件.
                xmlDS.WriteXml(writer);
                //xmlDS.GetXml();
                int count = (int)stream.Length;
                byte[] arr = new byte[count];
                stream.Seek(0, SeekOrigin.Begin);
                stream.Read(arr, 0, count);

                UnicodeEncoding utf = new UnicodeEncoding();
                return utf.GetString(arr);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (writer != null) writer.Close();
            }
        }

    }
}
