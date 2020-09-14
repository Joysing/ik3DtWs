//using Maticsoft.DBUtility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Xml;
using System.Xml.Linq;
using System.Text.RegularExpressions;

namespace ik3DtWs
{
    /// <summary>
    /// IDepartment 的摘要说明
    /// </summary>

    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class IDepartment : System.Web.Services.WebService
    {

        /// <summary>
        /// 部门维护接口
        /// </summary>
        /// <param name="sToken"></param>
        /// <param name="sTop"></param>
        /// <param name="sWhere"></param>
        /// <param name="sOrder"></param>
        /// <returns></returns>
        //        [WebMethod(Description = "部门维护测试接口:sActionType:ADD 新增 UPD 修改。部门编码不允许修改\r\n若返回空值则可能是xml出错")]
        //        public string T_DeptGroup(string sActionType, string xml, string id, string psw)
        //        {
        //            if (id != "k3WrAdmin" || psw != "kd147258")
        //            {
        //                return "你的id或psw错误";
        //            }
        //            string sReturn = string.Empty;

        //            //XElement xe = XElement.Load(xml);
        //            XElement xe = XElement.Parse(xml);
        //            IEnumerable<XElement> elements = from ele in xe.Elements("recortList")
        //                                             select ele;


        //            //showInfoByElements(elements);
        //            # region oa部门接入
        //            //List<deptModel> modelList = new List<deptModel>();
        //            //foreach (var ele in elements)
        //            //{
        //            //    deptModel model = new deptModel();

        //            //    if (!string.IsNullOrEmpty(ele.Element("FNumber").Value))
        //            //    {
        //            //        model.Number = ele.Element("FNumber").Value;
        //            //    }
        //            //    if (!string.IsNullOrEmpty(ele.Element("FName").Value))
        //            //    {
        //            //        model.Name = ele.Element("FName").Value;
        //            //    }
        //            //    if (!string.IsNullOrEmpty(ele.Element("FParentNumber").Value))
        //            //    {
        //            //        model.ParentNumber = ele.Element("FParentNumber").Value;
        //            //    }
        //            //    //if (!string.IsNullOrEmpty(ele.Element("FParentName").Value))
        //            //    //{
        //            //    //    model.ParentName = ele.Element("FParentName").Value;
        //            //    //}

        //            //    //model.Number = ele.Element("FNumber").Value;
        //            //    //model.Name = ele.Element("FName").Value;
        //            //    //model.ParentNumber = ele.Element("FParentNumber").Value;
        //            //    //model.ParentName = ele.Element("FParentName").Value;

        //            //    modelList.Add(model);
        //            //}
        //            # endregion oa部门接入

        //            List<oaBm> oaBmList = new List<oaBm>();
        //            foreach (var ele in elements)
        //            {
        //                oaBm Bm = new oaBm();
        //                if (!string.IsNullOrEmpty(ele.Element("fDeptBH").Value))
        //                {
        //                    Bm.DeptBH = ele.Element("fDeptBH").Value;
        //                }
        //                if (!string.IsNullOrEmpty(ele.Element("fDeptName").Value))
        //                {
        //                    Bm.DeptName = ele.Element("fDeptName").Value;
        //                }
        //                if (!string.IsNullOrEmpty(ele.Element("fBMJC").Value))
        //                {
        //                    Bm.BMJC = ele.Element("fBMJC").Value;
        //                }
        //                if (!string.IsNullOrEmpty(ele.Element("fZZ").Value))
        //                {
        //                    Bm.ZZ = ele.Element("fZZ").Value;
        //                }
        //                if (!string.IsNullOrEmpty(ele.Element("fLevel").Value))
        //                {
        //                    Bm.Level = ele.Element("fLevel").Value;
        //                }
        //                if (!string.IsNullOrEmpty(ele.Element("fEnable").Value))
        //                {
        //                    Bm.Enable = ele.Element("fEnable").Value;
        //                }
        //                oaBmList.Add(Bm);
        //            }

        //            DataSet ods = new DataSet();
        //            switch (sActionType)
        //            {
        //                case "ADD":
        //                    foreach (var department in oaBmList)
        //                    {
        //                        try
        //                        {
        //                            //查询要插入的编码是否存在
        //                            string sSql = " SELECT FItemID FROM t_Item";
        //                            SqlParameter[] oPara ={
        //                                        new SqlParameter("@FNUMBER",SqlDbType.VarChar), //接收传入的编码值
        //                                        new SqlParameter("@FNAME",SqlDbType.VarChar), //接收传入的名称值
        //                                        new SqlParameter("@UUID",SqlDbType.VarChar),//接收生成的uuid
        //                                        new SqlParameter("@FITEMID",SqlDbType.VarChar),//接收生成的fitemid
        //                                        new SqlParameter("@FPARENT",SqlDbType.Int)//接收传入的fparentid
        //                                            };


        //                            //传入参数赋值
        //                            oPara[0].Value = department.DeptBH;
        //                            oPara[1].Value = department.DeptName;
        //                            ods = DbHelperSQL.Query(sSql + " where fnumber=@FNUMBER and FItemClassID=2", oPara);
        //                            //如果查询成功，且没有传入的物料编码，则新建这个item
        //                            if (ods != null && ods.Tables[0].Rows.Count == 0)
        //                            {
        //                                string sSqlse = "Select newid() as UUID ";
        //                                ods = DbHelperSQL.Query(sSqlse, oPara);  //获得uuid
        //                                oPara[2].Value = ods.Tables[0].Rows[0]["UUID"].ToString();//uuid赋值
        //                                ////oPara[4].Value = department.ParentNumber;//@FPARENT现在保存的是FPARENTNUMBER
        //                                //sSqlse = " select FITEMID from t_Item where FItemClassID=2 and FNumber='" + department.ParentNumber + "'";
        //                                //if (DbHelperSQL.GetSingle(sSqlse) != null)
        //                                //{
        //                                //    oPara[4].Value = DbHelperSQL.GetSingle(sSqlse).ToString();//从这里开始@FPARENT保存的是@FPARENTID
        //                                //}
        //                                //else oPara[4].Value = 0;

        //                                //插入t_item表
        //                                string sSqlInsert = @" INSERT INTO t_Item (FItemClassID,FParentID,FLevel,FName,FNumber,FShortNumber,FFullNumber,FDetail,UUID,FDeleted) 
        //                                                    VALUES (2,0,1,@FNAME,@FNUMBER,@FNUMBER,@FNUMBER,0,@UUID,0)";//插入item表
        //                                DbHelperSQL.ExecuteSql(sSqlInsert, oPara);
        //                                sSqlInsert = @" select fitemid from t_Item where FNumber=@FNUMBER and FItemClassID=2";
        //                                oPara[3].Value = DbHelperSQL.GetSingle(sSqlInsert, oPara);//获得系统生成的fitemid
        //                                sSqlInsert = " Delete from Access_t_Department where FItemID=@FITEMID";
        //                                sSqlInsert = sSqlInsert + @"  Insert into Access_t_Department(FItemID
        //                                                                                      ,FParentIDX
        //                                                                                      ,FDataAccessView
        //                                                                                      ,FDataAccessEdit
        //                                                                                      ,FDataAccessDelete) 
        //                                                                                Values(@FITEMID
        //                                                                                      ,0
        //                                                                                      ,convert(varbinary(7200)
        //                                                                                      ,REPLICATE(char(255),100))
        //                                                                                      ,convert(varbinary(7200)
        //                                                                                      ,REPLICATE(char(255),100))
        //                                                                                      ,convert(varbinary(7200)
        //                                                                                      ,REPLICATE(char(255),100)))";
        //                                sSqlInsert = sSqlInsert + @"  update t_Item set FName=FName where FItemID=@FITEMID and FItemClassID=2";
        //                                DbHelperSQL.ExecuteSql(sSqlInsert, oPara);//插入组表，并更新ffullname

        //                            }
        //                            else
        //                            {
        //                                sReturn = sReturn + "<status>-1</status>" + "<message>" + "编号为" + department.DeptBH + "的部门已存在" + "</message>";
        //                            }

        //                        }
        //                        catch (Exception)
        //                        {
        //                            sReturn = sReturn + "<status>-1</status>" + "<message>" + "编号为" + department.DeptBH + "的部门参数有误" + "</message>";
        //                        }
        //                    }
        //                    break;

        //                //修改部门信息
        //                case "UPD":
        //                    foreach (var department in oaBmList)
        //                    {
        //                        try
        //                        {
        //                            SqlParameter[] oParaEdit = {
        //                                                   new SqlParameter("@FNUMBER",SqlDbType.VarChar),//接收传入的编号
        //                                                   new SqlParameter("@FITEMID",SqlDbType.Int),//接收搜索出来的fitemid
        //                                                   new SqlParameter("@FNAME",SqlDbType.VarChar)//接收传入的fname
        //                                               };
        //                            oParaEdit[0].Value = department.DeptBH;

        //                            //查询要修改的部门的fitemid
        //                            string sSqlEditse = "select * from t_Item where FItemClassID=2 and FNumber=@FNUMBER";
        //                            ods = DbHelperSQL.Query(sSqlEditse, oParaEdit);

        //                            //如果存在，则更新
        //                            if (ods != null && ods.Tables[0].Rows.Count > 0)
        //                            {
        //                                oParaEdit[1].Value = ods.Tables[0].Rows[0]["FITEMID"].ToString();
        //                                oParaEdit[2].Value = department.DeptName;
        //                                //sSqlEditse = "SELECT FItemID FROM t_Item WHERE (FItemClassID=2 AND FNumber=@FNUMBER) AND FItemID<>@FITEMID";

        //                                oParaEdit[0].Value = department.DeptBH + ".%";
        //                                string sSqlEdit = @" UPDATE t_Item SET FName=@FNAME WHERE FItemID=@FITEMID";
        //                                sSqlEdit = sSqlEdit + @" Update t_Item Set FFullName = '''' Where FNumber Like @FNUMBER AND FItemClassID = 2";
        //                                DbHelperSQL.ExecuteSql(sSqlEdit, oParaEdit);
        //                                sReturn = sReturn + "<status>0</status>" + "<message>" + department.DeptBH + "部门修改成功" + "</message>";
        //                            }
        //                            else
        //                            {
        //                                sReturn = sReturn + "<status>-1</status>" + "<message>" + "编号为" + department.DeptBH + "的部门不存在" + "</message>";
        //                            }
        //                        }
        //                        catch (Exception ex)
        //                        {

        //                            sReturn = sReturn + "<status>-1</status>" + "<message>" + "编号为" + department.DeptBH + "插入出错" + ex.Message.ToString() + "</message>";
        //                        }
        //                    }
        //                    break;

        //            }


        //            return "<?xml version=\"1.0\" encoding=\"utf-8\"?> <xml>" + sReturn + "</xml>";
        //        }

        /// <summary>
        /// 部门明细维护接口
        /// </summary>
        /// <param name="sToken"></param>
        /// <param name="sTop"></param>
        /// <param name="sWhere"></param>
        /// <param name="sOrder"></param>
        /// <returns></returns>
        [WebMethod(Description = "部门编码不允许修改")]
        public string T_Dept(string xml)
        {
            //if (id != "k3WrAdmin" || psw != "kd147258")
            //{
            //    return "你的id或psw错误";
            //}
            string sReturn = string.Empty;


            //XElement xe = XElement.Load(xml);
            //XElement xe = XElement.Load(@"D:\mldotNet\ik3DtWs\T_Dept.xml");

            XElement xe;
            try
            {
                xe = XElement.Parse(xml.Trim());
            }
            catch (Exception ex)
            {

                return "<status>-1</status>" + "<message>" + "xml格式出错，无法解析。" + ex.ToString() + "</message>";
            }
            if (!xe.Element("resultRequest").Element("resultCode").Value.Equals("000"))
            { return "<status>-1</status>" + "<message>" + "接口获取部门信息失败，无法同步！！" + "</message>"; }
            IEnumerable<XElement> elements = from ele in xe.Elements("recortList")
                                             orderby ele.Element("fDeptBH").Value
                                             select ele;
            //showInfoByElements(elements);
            #region oa部门接入
            List<oaBm> oaBmList = new List<oaBm>();
            foreach (var ele in elements)
            {
                oaBm Bm = new oaBm();
                if (!string.IsNullOrEmpty(ele.Element("fDeptBH").Value))
                {
                    Bm.DeptBH = ele.Element("fDeptBH").Value;
                }
                else
                { continue; }
                if (!string.IsNullOrEmpty(ele.Element("fDeptName").Value))
                {
                    Bm.DeptName = ele.Element("fDeptName").Value;
                }
                else
                { continue; }
                if (!string.IsNullOrEmpty(ele.Element("fBMJC").Value))
                {
                    Bm.BMJC = ele.Element("fBMJC").Value;
                }
                if (!string.IsNullOrEmpty(ele.Element("fZZ").Value))
                {
                    Bm.ZZ = ele.Element("fZZ").Value;
                }
                if (!string.IsNullOrEmpty(ele.Element("fZZBH").Value))
                {
                    Bm.ZZBH = ele.Element("fZZBH").Value;
                }
                if (!string.IsNullOrEmpty(ele.Element("fLevel").Value))
                {
                    Bm.Level = ele.Element("fLevel").Value;
                }
                if (!string.IsNullOrEmpty(ele.Element("fEnable").Value))
                {
                    Bm.Enable = ele.Element("fEnable").Value;
                }
                if (!string.IsNullOrEmpty(ele.Element("fParentDeptID").Value))
                {
                    Bm.ParentDeptID = ele.Element("fParentDeptID").Value;
                }
                if (!string.IsNullOrEmpty(ele.Element("fParentDeptName").Value))
                {
                    Bm.ParentDeptName = ele.Element("fParentDeptName").Value;
                }

                oaBmList.Add(Bm);
            }


            //List<deptModel> modelList = new List<deptModel>();
            //foreach (var ele in elements)
            //{
            //    deptModel model = new deptModel();
            //    if (!string.IsNullOrEmpty(ele.Element("FNumber").Value))
            //    {
            //        model.Number = ele.Element("FNumber").Value;
            //    }
            //    if (!string.IsNullOrEmpty(ele.Element("FName").Value))
            //    {
            //        model.Name = ele.Element("FName").Value;
            //    }
            //    if (!string.IsNullOrEmpty(ele.Element("FParentNumber").Value))
            //    {
            //        model.ParentNumber = ele.Element("FParentNumber").Value;
            //    }
            //    //if (!string.IsNullOrEmpty(ele.Element("FParentName").Value))
            //    //{
            //    //    model.ParentName = ele.Element("FParentName").Value;
            //    ////}
            //    //if (!string.IsNullOrEmpty(ele.Element("FDProperty").Value))
            //    //{
            //    //    model.Property = ele.Element("FDProperty").Value;
            //    //}
            //    //if (!string.IsNullOrEmpty(ele.Element("FCostAccountType").Value))
            //    //{
            //    //    model.CostAccountType = ele.Element("FCostAccountType").Value;
            //    //}
            //    if (!string.IsNullOrEmpty(ele.Element("FNote").Value))
            //    {
            //        model.Note = ele.Element("FNote").Value;
            //    }

            //model.Number = ele.Element("FNumber").Value;
            //model.Name = ele.Element("FName").Value;
            //model.ParentNumber = ele.Element("FParentNumber").Value;
            //model.Property = ele.Element("FDProperty").Value;
            //model.CostAccountType = ele.Element("FCostAccountType").Value;
            //model.Note = ele.Element("FNote").Value;

            //    modelList.Add(model);
            //}
            #endregion

            DataSet ods = new DataSet();
            //switch (sActionType)
            //{
            //    case "ADD":
            foreach (var Bm in oaBmList)
            {
                try
                {
                    SqlParameter[] oPara ={
                                        new SqlParameter("@FNUMBER",SqlDbType.VarChar), //接收传入的编码值
                                        new SqlParameter("@FNAME",SqlDbType.VarChar), //接收传入的编码值
                                        new SqlParameter("@FPARENT",SqlDbType.VarChar),//接收传入的fparentnumber
                                        new SqlParameter("@FZZ",SqlDbType.VarChar),//组织名称
                                        new SqlParameter("@FZZUUID",SqlDbType.VarChar),//组织UUID
                                        new SqlParameter("@FZZBH",SqlDbType.VarChar),//组织编号
                                        new SqlParameter("@FZZID",SqlDbType.VarChar)
                                            };

                    oPara[0].Value = Bm.DeptBH;
                    oPara[1].Value = Bm.BMJC;
                    oPara[2].Value = Bm.ParentDeptName; //此处@FPARENT是@FPARENTNumber
                    oPara[3].Value = Bm.ZZ;
                    oPara[5].Value = Bm.ZZBH;

                    string zzExist = "select FItemID from t_Item where FItemClassID=2 and FNumber=@FZZBH and FName=@FZZ";
                    object exsit=DbHelperSQL.GetSingle(zzExist, oPara);
                    if (exsit==null)
                    {

                        string sSqlse = "Select newid() as UUID ";
                        oPara[4].Value = DbHelperSQL.GetSingle(sSqlse).ToString();
                        string sSqlInsert = @" INSERT INTO t_Item (FItemClassID,FParentID,FLevel,FName,FNumber,FShortNumber,FFullNumber,FDetail,UUID,FDeleted) 
                                                           VALUES (2,0,1,@FZZ,@FZZBH,@FZZBH,@FZZBH,0,@FZZUUID,0)";//插入item表
                        DbHelperSQL.ExecuteSql(sSqlInsert, oPara);
                        sSqlInsert = @" select fitemid from t_Item where FNumber=@FZZBH and FItemClassID=2";
                        oPara[6].Value = DbHelperSQL.GetSingle(sSqlInsert, oPara);//获得系统生成的fitemid
                        sSqlInsert = " Delete from Access_t_Department where FItemID=@FZZID";
                        sSqlInsert = sSqlInsert + @"  Insert into Access_t_Department(FItemID
                                                                                     ,FParentIDX
                                                                                     ,FDataAccessView
                                                                                     ,FDataAccessEdit
                                                                                     ,FDataAccessDelete) 
                                                                               Values(@FZZID
                                                                                     ,0
                                                                                     ,convert(varbinary(7200)
                                                                                     ,REPLICATE(char(255),100))
                                                                                     ,convert(varbinary(7200)
                                                                                     ,REPLICATE(char(255),100))
                                                                                     ,convert(varbinary(7200)
                                                                                     ,REPLICATE(char(255),100)))";
                        sSqlInsert = sSqlInsert + @"  update t_Item set FName=FName where FItemID=@FZZID and FItemClassID=2";
                        DbHelperSQL.ExecuteSql(sSqlInsert, oPara);//插入组表，并更新ffullname
 
                    }

                    ////检查部门是否存在，存在则退出这次循环
                    //object exist;
                    //string sSqlsc = @"  select FItemID from t_Item where (FNumber=@FNUMBER and FName=@FNAME) and FItemClassID=2";
                    //exist = DbHelperSQL.GetSingle(sSqlsc, oPara);
                    //if (exist!=null)
                    //{
                    //    sReturn = sReturn + "<status>-1</status>" + "<message>" + "编号为" + Bm.DeptBH + "的部门已存在" + "</message>";
                    //    continue;
                    //}

                    //////查询传入的部门的父级部门否明细，是就弹出提示
                    //string sSqldetailse = " select 1 from t_Item where FDetail=1 and FItemID=" +;
                    //object isDetail =DbHelperSQL.GetSingle(sSqldetailse, oPara);
                    //if (!isDetail.Equals(1))
                    //{
                    //    sReturn = sReturn + oPara[0].Value + "对不起，请手动在部门明细下新增部门明细\r\n";
                    //    continue;
                    //}
                    ////若父级不是明细，开始新增流程
                    //else
                    //{
                    //查询要新增的部门明细编号是否存在，若不存在，则新增
                    string sSqldetailse = "SELECT FItemID FROM t_Item WHERE FItemClassID=2 AND FNumber=@FNUMBER";
                    ods = DbHelperSQL.Query(sSqldetailse, oPara);
                    #region if 1
                    if (ods != null && ods.Tables[0].Rows.Count == 0)
                    {

                        SqlParameter[] oParaInsertNeed ={
                                        new SqlParameter("@FNAME",SqlDbType.VarChar), //接收传入的名称值
                                        new SqlParameter("@UUID",SqlDbType.VarChar),//接收生成的uuid
                                        new SqlParameter("@FITEMID",SqlDbType.Int),//接收生成的fitemid
                                        new SqlParameter("@FLEVEL",SqlDbType.SmallInt),
                                        new SqlParameter("@FSHORTNUMBER",SqlDbType.VarChar),
                                        new SqlParameter("@FNUMBER",SqlDbType.VarChar), //接收传入的编码值
                                        new SqlParameter("@FPARENT",SqlDbType.Int),//接收传入的fparentnumber
                                        //new SqlParameter("@FDPROPERTY",SqlDbType.VarChar),
                                        new SqlParameter("@FNOTE",SqlDbType.VarChar),
                                        //new SqlParameter("@FCOSTACCOUNTTYPE",SqlDbType.Int)
                                            };
                        
                        
                        oParaInsertNeed[0].Value = Bm.BMJC;
                        //oParaInsertNeed[3].Value = Bm.Level;//给部门层级赋值
                        oParaInsertNeed[5].Value = Bm.DeptBH;

                        ////获取新部门明细的短编码fshortnumber
                        string ParentNumber;
                        int lastDot = Bm.DeptBH.ToString().LastIndexOf(".");
                        if (lastDot > -1)
                        {
                            oParaInsertNeed[4].Value = Bm.DeptBH.ToString().Substring(lastDot + 1);
                            ParentNumber = Bm.DeptBH.ToString().Substring(0, lastDot);

                        }
                        else
                        {
                            oParaInsertNeed[4].Value = Bm.DeptBH;
                            ParentNumber = "*";
                        }

                        //获取上级部门短编号和层级
                        string sPShortNum;
                        int fPLevel=0;
                        if (ParentNumber.LastIndexOf(".") > -1) { sPShortNum = ParentNumber.Substring(ParentNumber.LastIndexOf(".")+1); }
                        else sPShortNum = ParentNumber;
                        //查询父级是否明细
                        sSqldetailse = "Select FItemID,FName, FLevel, FDetail,FParentID  from t_Item where FItemClassID =2 And FNumber='" + ParentNumber + "'";
                        DataSet parentData = new DataSet();
                        parentData = DbHelperSQL.Query(sSqldetailse);
                        if (parentData != null && parentData.Tables[0].Rows.Count == 0)
                        {
                            sReturn = sReturn + "<status>-1</status>" + "<message>" + "编号为" + Bm.DeptBH + "的父级部门不存在" + "</message>";
                            continue;
                        }
                        else
                        {
                            //若父级编码是明细，改掉父级的资料，新建父级组
                            string fName = parentData.Tables[0].Rows[0]["FName"].ToString();
                            string fPaItemID = parentData.Tables[0].Rows[0]["FItemID"].ToString();
                            string fPaDetail = parentData.Tables[0].Rows[0]["FDetail"].ToString();
                            fPLevel = Regex.Matches(ParentNumber, @"\.").Count;
                            //fPLevel = Convert.ToInt32(parentData.Tables[0].Rows[0]["FLevel"]);
                            if (!string.IsNullOrEmpty(fPaDetail) && fPaDetail.Equals("True"))
                            {
                                StringBuilder FPaUP = new StringBuilder();
                                //FPaUP.Append(" UPDATE t_Item SET FNumber=@FNUMBER,FName=@FNAME,FShortNumber ='" + ParentNumber + "',FFullNumber=@FNUMBER WHERE FItemID=@FPARENT ");
                                //DbHelperSQL.ExecuteSql(FPaUP.ToString(), oParaInsertNeed);
                                string sFShortNum = oParaInsertNeed[4].Value.ToString();
                                oParaInsertNeed[6].Value = parentData.Tables[0].Rows[0]["FItemID"];
                                FPaUP.Append(" UPDATE t_Item SET FNumber=@FNUMBER,FName=@FNAME,FShortNumber =@FSHORTNUMBER,FFullNumber=@FNUMBER WHERE FItemID=@FPARENT ");
                                DbHelperSQL.ExecuteSql(FPaUP.ToString(), oParaInsertNeed);
                                DeptGroup(fName, ParentNumber, fPaItemID, fPLevel, sFShortNum);
                                sReturn = sReturn + "<status>0</status>" + "<message>" + Bm.DeptBH + "部门插入成功" + "</message>";
                                continue;
                            }

                        }

                        //给部门层级赋值
                        oParaInsertNeed[3].Value = Convert.ToInt32(parentData.Tables[0].Rows[0]["FLevel"])+1;
                        //uuid赋值
                        string sSqlse = "Select newid() as UUID ";
                        ods = DbHelperSQL.Query(sSqlse);
                        oParaInsertNeed[1].Value = ods.Tables[0].Rows[0]["UUID"].ToString();
                        oParaInsertNeed[6].Value = parentData.Tables[0].Rows[0]["FItemID"];



                        #region 插入item表
                        string sSqlDetailInsert = @"INSERT INTO t_Item (FItemClassID
                                                                        ,FParentID
                                                                        ,FLevel
                                                                        ,FName
                                                                        ,FNumber
                                                                        ,FShortNumber
                                                                        ,FFullNumber
                                                                        ,FDetail
                                                                        ,UUID
                                                                        ,FDeleted) 
                                                                        VALUES (2
                                                                        ,@FPARENT
                                                                        ,@FLEVEL
                                                                        ,@FNAME
                                                                        ,@FNUMBER
                                                                        ,@FSHORTNUMBER
                                                                        ,@FNUMBER
                                                                        ,1
                                                                        ,@UUID
                                                                        ,0)";
                        #endregion
                        DbHelperSQL.ExecuteSql(sSqlDetailInsert, oParaInsertNeed);
                        sSqlDetailInsert = @" select fitemid from t_Item where FNumber=@FNUMBER and FItemClassID=2";
                        object sfitemId = DbHelperSQL.GetSingle(sSqlDetailInsert, oParaInsertNeed);
                        if (sfitemId == null)
                        {
                            sReturn = sReturn + "<status>-1</status>" + "<message>" + "编号为" + Bm.DeptBH + "插入失败，" + "</message>";
                            continue;
                        }
                        oParaInsertNeed[2].Value = DbHelperSQL.GetSingle(sSqlDetailInsert, oParaInsertNeed);//获得系统生成的fitemid

                        #region 插入部门、Access_t_Department表
                        sSqlDetailInsert = @" INSERT INTO t_Department (FManager
                                                                        ,FPhone
                                                                        ,FFax
                                                                        ,FDProperty
                                                                        ,FIsCreditMgr
                                                                        ,FAcctID
                                                                        ,FNote
                                                                        ,FCostAccountType
                                                                        ,FOtherARAcctID
                                                                        ,FPreARAcctID
                                                                        ,FOtherAPAcctID
                                                                        ,FPreAPAcctID
                                                                        ,FShortNumber
                                                                        ,FNumber
                                                                        ,FName
                                                                        ,FParentID
                                                                        ,FItemID)
                                                                 VALUES (0
                                                                        ,NULL
                                                                        ,NULL
                                                                        ,1071
                                                                        ,0
                                                                        ,0
                                                                        ,NULL
                                                                        ,363
                                                                        ,0
                                                                        ,0
                                                                        ,0
                                                                        ,0
                                                                        ,@FSHORTNUMBER
                                                                        ,@FNUMBER
                                                                        ,@FNAME
                                                                        ,@FPARENT
                                                                        ,@FITEMID)";

                        sSqlDetailInsert = sSqlDetailInsert + @" Delete from Access_t_Department where FItemID=@FITEMID";
                        sSqlDetailInsert = sSqlDetailInsert + @"  Insert into Access_t_Department(FItemID
                                                                                      ,FParentIDX
                                                                                      ,FDataAccessView
                                                                                      ,FDataAccessEdit
                                                                                      ,FDataAccessDelete) 
                                                                                Values(@FITEMID
                                                                                      ,0
                                                                                      ,convert(varbinary(7200)
                                                                                      ,REPLICATE(char(255),100))
                                                                                      ,convert(varbinary(7200)
                                                                                      ,REPLICATE(char(255),100))
                                                                                      ,convert(varbinary(7200)
                                                                                      ,REPLICATE(char(255),100)))";

                        //更新fullname
                        sSqlDetailInsert = sSqlDetailInsert + @"  update t_Item set FName=FName where FItemID=@FITEMID and FItemClassID=2";
                        #endregion
                        //int result = 
                        DbHelperSQL.ExecuteSql(sSqlDetailInsert, oParaInsertNeed);//插入组表，并更新ffullname
                        //if (result >0) { 
                        sReturn = sReturn + "<status>0</status>" + "<message>" + Bm.DeptBH + "部门插入成功" + "</message>";
                        //}


                    }
                    else
                    {

                        try
                        {
                            SqlParameter[] oParaEdit = {
                                                   new SqlParameter("@FNUMBER",SqlDbType.VarChar),//接收传入的编号
                                                   new SqlParameter("@FPNUMBER",SqlDbType.VarChar),
                                                   new SqlParameter("@FPITEMID",SqlDbType.VarChar),
                                                   new SqlParameter("@FPSNUMBER",SqlDbType.VarChar)
                                               };
                            oParaEdit[0].Value = Bm.DeptBH;
                            //string ParentNumber;
                            int lastDot = Bm.DeptBH.ToString().LastIndexOf(".");
                            if (lastDot > -1)
                            {
                                oParaEdit[3].Value = Bm.DeptBH.ToString().Substring(lastDot + 1);
                                oParaEdit[1].Value = Bm.DeptBH.ToString().Substring(0, lastDot);

                            }
                            else
                            {
                                oParaEdit[3].Value = Bm.DeptBH;
                                oParaEdit[1].Value = "*";
                            }
                            //oParaEdit[3].Value = Bm.ParentNumber;//此处@parent为parentnumber


                            //查询要修改的部门明细的父级id
                            string sSqlEditse = @"Select * From (Select t1.* From t_Item t1  
                                                WHERE (t1.FDeleteD=1 Or t1.FDeleteD=0)  
                                                And t1.FItemClassID = 2 And t1.FItemClassID = 2) i  
                                                Where 1=1  And (FNumber = @FPNUMBER)";
                            ods = DbHelperSQL.Query(sSqlEditse, oParaEdit);

                            //如果父级id存在，查询他是否部门明细
                            if (ods != null && ods.Tables[0].Rows.Count > 0)
                            {

                                oParaEdit[2].Value = ods.Tables[0].Rows[0]["FITEMID"].ToString();//给item赋值
                                //oParaEdit[2].Value = Bm.DeptName;//给部门名称赋值

                                sSqlEditse = @" select 1 from t_Department where FItemID = @FPITEMID";//此处@FPARENT为parentid
                                ods = DbHelperSQL.Query(sSqlEditse, oParaEdit);
                                //若父级id是部门明细，则部门不存在
                                if (ods != null && ods.Tables[0].Rows.Count > 0)
                                {
                                    sReturn = sReturn + "<status>-1</status>" + "<message>" + "编号为" + Bm.DeptBH + "的部门信息有误，请检查数据表（修改，父级是明细）" + "</message>";
                                    continue;
                                }
                                else
                                {
                                    //若父级id不存在，查询部门的fitemid
                                    sSqlEditse = "select * from t_Item where FNumber=@FNUMBER and FItemClassID=2";
                                    ods = DbHelperSQL.Query(sSqlEditse, oParaEdit);
                                    SqlParameter[] oParaEditNeed ={
                                        new SqlParameter("@FNAME",SqlDbType.VarChar), //接收传入的名称值
                                        new SqlParameter("@FITEMID",SqlDbType.Int),//接收生成的fitemid
                                        new SqlParameter("@FNUMBER",SqlDbType.VarChar), //接收传入的编码值
                                        new SqlParameter("@FDPROPERTY",SqlDbType.VarChar),//接收传入的部门属性值
                                        new SqlParameter("@FNOTE",SqlDbType.VarChar),//接收传入的备注值
                                        new SqlParameter("@FDELETE",SqlDbType.Int),//接收传入的是否使用
                                        //new SqlParameter("@FCOSTACCOUNTTYPE",SqlDbType.Int)//接收传入的成本核算类型值
                                            };
                                    oParaEditNeed[1].Value = Convert.ToInt32(ods.Tables[0].Rows[0]["FITEMID"]);
                                    oParaEditNeed[0].Value = Bm.BMJC;
                                    //oParaEditNeed[3].Value = Bm.Property;
                                    //oParaEditNeed[4].Value = Bm.Note;
                                    if (Bm.Enable == "1")
                                        oParaEditNeed[5].Value = 0;
                                    else
                                        oParaEditNeed[5].Value = 1;

                                    ////FCostAccountTypess赋值
                                    //string csat = Bm.CostAccountType;
                                    //sSqlEditse = "/*dialect*/select * from t_SubMessage where FID='" + csat + "'";
                                    //ods = DbHelperSQL.Query(sSqlEditse);
                                    //oParaEditNeed[5].Value = ods.Tables[0].Rows[0]["FINTERID"].ToString();

                                    //update改变的部门明细
                                    oParaEditNeed[2].Value = Bm.DeptBH + ".%";
                                    string sSqlEdit = @" UPDATE t_Item SET FName=@FNAME WHERE FItemID=@FITEMID";
                                    sSqlEdit = sSqlEdit + @" Update t_Item Set FFullName = '''',FDeleted=@FDELETE Where FNumber=@FNUMBER AND FItemClassID = 2";
                                    sSqlEdit = sSqlEdit + @" UPDATE t_Department SET FNote=@FNOTE
                                                                                ,FName=@FNAME,FDeleted=@FDELETE  
                                                                                WHERE FItemID = @FITEMID";
                                    int result = DbHelperSQL.ExecuteSql(sSqlEdit, oParaEditNeed);

                                    if (result > 0)
                                    {
                                        sReturn = sReturn + "<status>0</status>" + "<message>" + Bm.DeptBH + "部门修改成功" + "</message>";
                                    }

                                }
                            }
                            else
                            {
                                sReturn = sReturn + "<status>-1</status>" + "<message>" + "编号为" + Bm.DeptBH + "的部门信息有误，请检查数据表（修改，父级不存在）" + "</message>";
                            }
                        }
                        catch (Exception ex)
                        {

                            sReturn = sReturn + "<status>-1</status>" + "<message>" + "编号为" + Bm.DeptBH + "修改出错" + ex.Message.ToString() + "</message>";


                        }
                    }
                    #endregion
                    //}

                }
                catch (Exception ex)
                {
                    sReturn = sReturn + "<status>-1</status>" + "<message>" + "编号为" + Bm.DeptBH + "的部门参数有误" + ex.Message.ToString()+ "</message>";
                    int maxDept = Convert.ToInt32(DbHelperSQL.GetSingle("select MAX(fitemid) from t_Item where FItemClassID=2 "));
                    int maxDeptDetail = Convert.ToInt32(DbHelperSQL.GetSingle("select MAX(fitemid) from t_Department "));
                    if (maxDept > maxDeptDetail)
                    {
                        DbHelperSQL.ExecuteSql("delete  from t_Item where FItemID='" + maxDept + "'");
                    }
                }

            }

            // break;
            #region
            //                case "UPD":
            //                    foreach (var Bm in oaBmList)
            //                    {
            //                        try
            //                        {
            //                            SqlParameter[] oParaEdit = {
            //                                                   new SqlParameter("@FNUMBER",SqlDbType.VarChar),//接收传入的编号
            //                                                   new SqlParameter("@FPARENT",SqlDbType.VarChar)
            //                                               };
            //                            oParaEdit[0].Value = Bm.DeptBH;
            //                            //oParaEdit[3].Value = Bm.ParentNumber;//此处@parent为parentnumber

            //                            //查询要修改的部门明细的父级id
            //                            string sSqlEditse = @"Select * From (Select t1.* From t_Item t1  
            //                                                WHERE (t1.FDeleteD=1 Or t1.FDeleteD=0)  
            //                                                And t1.FItemClassID = 2 And t1.FItemClassID = 2) i  
            //                                                Where 1=1  And (FNumber = @FPARENT)";
            //                            ods = DbHelperSQL.Query(sSqlEditse, oParaEdit);

            //                            //如果父级id存在，查询他是否部门明细
            //                            if (ods != null && ods.Tables[0].Rows.Count > 0)
            //                            {

            //                                oParaEdit[3].Value = ods.Tables[0].Rows[0]["FITEMID"].ToString();//给item赋值
            //                                oParaEdit[2].Value = Bm.DeptName;//给部门名称赋值

            //                                sSqlEditse = @" select * from t_Department where FItemID = @FPARENT";//此处@FPARENT为parentid
            //                                ods = DbHelperSQL.Query(sSqlEditse, oParaEdit);
            //                                //若父级id是部门明细，则部门不存在
            //                                if (ods != null && ods.Tables[0].Rows.Count > 0)
            //                                {
            //                                    sReturn = sReturn + "<status>-1</status>" + "<message>" + "编号为" + Bm.DeptBH + "的部门不存在" + "</message>";
            //                                    continue;
            //                                }
            //                                else
            //                                {
            //                                    //若父级id不存在，查询部门的fitemid
            //                                    sSqlEditse = "select * from t_Item where FNumber=@FNUMBER and FItemClassID=2";
            //                                    ods = DbHelperSQL.Query(sSqlEditse, oParaEdit);
            //                                    SqlParameter[] oParaEditNeed ={
            //                                        new SqlParameter("@FNAME",SqlDbType.VarChar), //接收传入的名称值
            //                                        new SqlParameter("@FITEMID",SqlDbType.Int),//接收生成的fitemid
            //                                        new SqlParameter("@FNUMBER",SqlDbType.VarChar), //接收传入的编码值
            //                                        new SqlParameter("@FDPROPERTY",SqlDbType.VarChar),//接收传入的部门属性值
            //                                        new SqlParameter("@FNOTE",SqlDbType.VarChar),//接收传入的备注值
            //                                        //new SqlParameter("@FCOSTACCOUNTTYPE",SqlDbType.Int)//接收传入的成本核算类型值
            //                                            };
            //                                    oParaEditNeed[1].Value = Convert.ToInt32(ods.Tables[0].Rows[0]["FITEMID"]);
            //                                    oParaEditNeed[0].Value = Bm.DeptBH;
            //                                    //oParaEditNeed[3].Value = Bm.Property;
            //                                    //oParaEditNeed[4].Value = Bm.Note;


            //                                    ////FCostAccountTypess赋值
            //                                    //string csat = Bm.CostAccountType;
            //                                    //sSqlEditse = "/*dialect*/select * from t_SubMessage where FID='" + csat + "'";
            //                                    //ods = DbHelperSQL.Query(sSqlEditse);
            //                                    //oParaEditNeed[5].Value = ods.Tables[0].Rows[0]["FINTERID"].ToString();

            //                                    //update改变的部门明细
            //                                    oParaEditNeed[2].Value = Bm.DeptBH + ".%";
            //                                    string sSqlEdit = @" UPDATE t_Item SET FName=@FNAME WHERE FItemID=@FITEMID";
            //                                    sSqlEdit = sSqlEdit + @" Update t_Item Set FFullName = '''' Where FNumber Like @FNUMBER AND FItemClassID = 2";
            //                                    sSqlEdit = sSqlEdit + @" UPDATE t_Department SET FNote=@FNOTE
            //                                                                                ,FName=@FNAME  
            //                                                                                WHERE FItemID = @FITEMID";
            //                                    DbHelperSQL.ExecuteSql(sSqlEdit, oParaEditNeed);


            //                                    sReturn = sReturn + "<status>0</status>" + "<message>" + Bm.DeptBH + "部门修改成功" + "</message>";

            //                                }
            //                            }
            //                            else
            //                            {
            //                                sReturn = sReturn + "<status>-1</status>" + "<message>" + "编号为" + Bm.DeptBH + "的部门不存在" + "</message>";
            //                            }
            //                        }
            //                        catch (Exception ex)
            //                        {

            //                            sReturn = sReturn + "<status>-1</status>" + "<message>" + "编号为" + Bm.DeptBH + "修改出错" + ex.Message.ToString() + "</message>";
            //                        }
            //                    }
            //                    break;

            //}

            #endregion
            return "<?xml version=\"1.0\" encoding=\"utf-8\"?> <xml>" + sReturn + "</xml>";
        }

        #region dataset对比
        //        /// <summary>
        //        /// 部门明细维护接口
        //        /// </summary>
        //        /// <param name="sToken"></param>
        //        /// <param name="sTop"></param>
        //        /// <param name="sWhere"></param>
        //        /// <param name="sOrder"></param>
        //        /// <returns></returns>
        //        [WebMethod(Description = "部门明细维护接口:sActionType:ADD 新增 UPD 修改。部门编码不允许修改")]
        //        public int T_DeptQuery(string xml)
        //        {
        //            DataSet allDept = new DataSet();
        //            allDept = DbHelperSQL.Query(@"select t1.FNumber,t1.FName,t2.FNumber FParentNumber,t2.FName FParentName from t_Item t1
        //                                left join t_Item t2 on t1.FParentID=t2.FItemID
        //                                where t1.FItemClassID=2 ");
        //            DataSet oaDept = new DataSet();
        //            oaDept = GetDataSetByXml(xml);


        //            return 1;
        //        }

        //        protected static DataSet GetDataSetByXml(string xmlData)
        //        {
        //            try
        //            {
        //                DataSet ds = new DataSet();

        //                using (StringReader xmlSR = new StringReader(xmlData))
        //                {

        //                    ds.ReadXml(xmlSR, XmlReadMode.InferTypedSchema); //忽视任何内联架构，从数据推断出强类型架构并加载数据。如果无法推断，则解释成字符串数据
        //                    if (ds.Tables.Count > 0)
        //                    {
        //                        return ds;
        //                    }
        //                }
        //                return null;
        //            }
        //            catch (Exception)
        //            {
        //                return null;
        //            }
        //        }
        #endregion

        /// <summary>
        /// 把上级资料转移到下级
        /// </summary>
        /// <param name="sDeptName">上级部门名称</param>
        /// <param name="sDeptNumber">上级部门编号</param>
        /// <param name="sDeptID">上级部门id</param>
        /// <param name="fLevel">上级部门等级</param>
        /// <param name="sShortNum">部门短编号</param>
        public void DeptGroup(string sDeptName, string sDeptNumber, string sDeptID, int fLevel, string sShortNum)
        {
            DataSet ods = new DataSet();
            SqlParameter[] oPara ={
                                      new SqlParameter("@FNUMBER",SqlDbType.VarChar), //接收传入的编码值
                                      new SqlParameter("@FNAME",SqlDbType.VarChar), //接收传入的名称值
                                      new SqlParameter("@UUID",SqlDbType.VarChar),//接收生成的uuid
                                      new SqlParameter("@FITEMID",SqlDbType.VarChar),//接收生成的fitemid
                                      new SqlParameter("@FPARENT",SqlDbType.Int),//接收传入的fparentid
                                      new SqlParameter("@FLEVEL",SqlDbType.Int),
                                      new SqlParameter("@FSHORTNUMBER",SqlDbType.VarChar),
                                      new SqlParameter("@FPPID",SqlDbType.VarChar)//上级部门的上级部门
                                  };

            //传入参数赋值
            oPara[0].Value = sDeptNumber;//上级部门编号
            oPara[1].Value = sDeptName;//上级部门名称
            oPara[4].Value = sDeptID;//上级部门id
            oPara[5].Value = fLevel+1;//上级部门等级
            oPara[6].Value = sShortNum;//上级部门短编号
            string sSqlse = "Select newid() as UUID ";
            oPara[2].Value = DbHelperSQL.GetSingle(sSqlse, oPara).ToString();  //uuid
           // oPara[2].Value = ods.Tables[0].Rows[0]["UUID"].ToString();//uuid赋值
            oPara[7].Value = DbHelperSQL.GetSingle("select FParentID from t_Department where FNumber=@FNUMBER", oPara).ToString();
            ods = DbHelperSQL.Query("select FNumber,FName,FShortNumber from t_Item where FItemClassID=2 and FItemID=@FPARENT",oPara);
            string Number = ods.Tables[0].Rows[0]["FNumber"].ToString();
            string Name = ods.Tables[0].Rows[0]["FName"].ToString();
            string SNumber = ods.Tables[0].Rows[0]["FShortNumber"].ToString();

            //上级部门插入t_item表
            string sSqlInsert = @" INSERT INTO t_Item (FItemClassID,FParentID,FLevel,FName,FNumber,FShortNumber,FFullNumber,FDetail,UUID,FDeleted) 
                                                                VALUES (2,@FPPID,@FLEVEL,@FNAME,@FNUMBER,@FSHORTNUMBER,@FNUMBER,0,@UUID,0)";//插入item表
            DbHelperSQL.ExecuteSql(sSqlInsert, oPara);
            sSqlInsert = @" select fitemid from t_Item where FNumber=@FNUMBER and FItemClassID=2";
            oPara[3].Value = DbHelperSQL.GetSingle(sSqlInsert, oPara);//获得系统生成的fitemid
           
            sSqlInsert = " Delete from Access_t_Department where FItemID=@FITEMID";
//            sSqlInsert = sSqlInsert + @"  Insert into Access_t_Department(FItemID
//                                                                         ,FParentIDX
//                                                                         ,FDataAccessView
//                                                                         ,FDataAccessEdit
//                                                                         ,FDataAccessDelete) 
//                                                                  Values(@FITEMID
//                                                                         ,0
//                                                                         ,convert(varbinary(7200)
//                                                                         ,REPLICATE(char(255),100))
//                                                                         ,convert(varbinary(7200)
//                                                                         ,REPLICATE(char(255),100))
//                                                                         ,convert(varbinary(7200)
//                                                                         ,REPLICATE(char(255),100)))";
//            sSqlInsert = sSqlInsert + @"  update t_Item set FName=FName where FItemID=@FITEMID and FItemClassID=2";
//            DbHelperSQL.ExecuteSql(sSqlInsert, oPara);
            //新部门继承上级部门数据
            sSqlInsert = @" UPDATE t_Item SET FLevel=FLevel+1,FDetail=-1,FParentID=@FITEMID WHERE FItemID=@FPARENT ";
            sSqlInsert = sSqlInsert + @" UPDATE t_Department SET FParentID=@FITEMID,FNumber='"+Number+"',FName='"+Name+"' ,FShortNumber ='"+SNumber+"' WHERE FItemID=@FPARENT ";
            DbHelperSQL.ExecuteSql(sSqlInsert, oPara);
            sSqlInsert = @" Insert Access_t_Department(FItemID,FParentIDX,FDataAccessView,FDataAccessEdit,FDataAccessDelete)
                           Select @FITEMID as FItemID,@FPPID as FParentIDX,FDataAccessView,FDataAccessEdit,FDataAccessDelete from Access_t_Department
                           where FItemID=@FPARENT ";
            sSqlInsert = sSqlInsert + @" Update Access_t_Department Set FParentIDX=@FITEMID where FItemID=@FPARENT ";
            sSqlInsert = sSqlInsert + @" update t_Item set FName=FName where FItemClassID=2 ";
            DbHelperSQL.ExecuteSql(sSqlInsert, oPara);//插入组表，并更新ffullname


        }
    }

}