using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Xml;
using System.Xml.Linq;

namespace ik3DtWs
{
    /// <summary>
    /// IProject 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class IProject : System.Web.Services.WebService
    {
        ///<summary>
        ///项目维护接口
        ///<\summary>
        /// <param name="sToken"></param>
        /// <param name="sTop"></param>
        /// <param name="sWhere"></param>
        /// <param name="sOrder"></param>
        /// <returns></returns>
        [WebMethod(Description = "项目维护接口:sActionType:ADD 新增 UPD 修改。项目编码编码不允许修改")]
        public string T_Project(string uid, string pwd, string sActionType, string xml)
        {
            if (uid != "k3WrAdmin" || pwd != "kd147258")
            {
                return "你的id或psw错误" + xml.Trim();
            }
            string sReturn = string.Empty;

            //XElement xe = XElement.Load(xml);
            //XElement xe = XElement.Load(@"D:\mldotNet\ik3DtWs\T_Dept.xml");
            XElement xe = XElement.Parse(xml);
            IEnumerable<XElement> elements = from ele in xe.Elements("T_Project")
                                             select ele;
            //showInfoByElements(elements);
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
            XmlElement xmlElem = doc.DocumentElement;//获取根节点

            List<hsxmModel> modelList = new List<hsxmModel>();

            foreach (var ele in elements)
            {
                hsxmModel model = new hsxmModel();
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
                if (!string.IsNullOrEmpty(ele.Element("FParentName").Value))
                {
                    model.ParentName = ele.Element("FParentName").Value;
                }
                if (((xmlElem.GetElementsByTagName("FDeleted") as XmlNodeList).Count == elements.Count()))
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
            //{
            //case "ADD":
            foreach (var project in modelList)
            {
                try
                {
                    SqlParameter[] oPara ={
                                        new SqlParameter("@FNAME",SqlDbType.VarChar), //接收传入的编码值
                                        new SqlParameter("@FNUMBER",SqlDbType.VarChar),
                                        new SqlParameter("@FPARENT",SqlDbType.VarChar),//接收传入的fparentnumber
                                        new SqlParameter("@FITEMCLASSID",SqlDbType.Int),
                                        new SqlParameter("@UUID",SqlDbType.VarChar),
                                        new SqlParameter("@FITEMID",SqlDbType.Int),

                                            };
                    oPara[0].Value = project.Name;
                    oPara[1].Value = project.Number;
                    if (string.IsNullOrWhiteSpace(project.ParentNumber))
                    {
                        oPara[2].Value = "*";
                    }
                    oPara[2].Value = project.ParentNumber; //此处@FPARENT是@FPARENTNumber




                    ////查询项目的fitemclassid
                    //DataSet scID = new DataSet();
                    //string sSqlsc = @"  select FItemClassID from t_ItemClass where FName='项目'";
                    //scID = DbHelperSQL.Query(sSqlsc, oPara);
                    //oPara[3].Value = scID.Tables[0].Rows[0]["FItemClassID"];
                    oPara[3].Value = "3001";



                    #region
                    ////检查项目明细的父级编号是否存在，不存在则跳过这次循环循环
                    //string exist = " Select * From (Select t1.* From t_Item t1  WHERE (t1.FDeleteD=1 Or t1.FDeleteD=0)  And t1.FItemClassID = @FITEMCLASSID And t1.FItemClassID =@FITEMCLASSID) i  Where 1=1  And (FNumber = @FPARENT)";
                    //ods = DbHelperSQL.Query(exist, oPara);
                    //if (ods != null && ods.Tables[0].Rows.Count == 0)
                    //{
                    //    sReturn = sReturn + "<status>-1</status>" + "<message>" + "要插入的项目" + project.Number + "的父级项目不存在" + "</message>";
                    //    continue;
                    //}
                    //oPara[2].Value = ods.Tables[0].Rows[0]["FItemID"].ToString();


                    //核算项目的父级项目如果存在，则查询父级项目是否明细，是明细要检验是否有过业务
                    //oPara[2].Value = ods.Tables[0].Rows[0]["FItemID"].ToString();
                    //oPara[6].Value = Convert.ToInt32(ods.Tables[0].Rows[0]["FLevel"]) + 1;
                    //int parentDetail = Convert.ToInt16(ods.Tables[0].Rows[0]["FDetail"]);
                    //if (parentDetail == 1)
                    //{
                    //    string detail = "";
                    //    detail = detail + " 	declare @id varchar(100)				  ";
                    //    detail = detail + " 	declare @tablename varchar(100)				  ";
                    //    detail = detail + " 	declare @fieldname varchar(100)				  ";
                    //    detail = detail + " 	declare @Description varchar(100)				  ";
                    //    detail = detail + " 	declare @primarykey varchar(100)				  ";
                    //    detail = detail + " 	declare @classtypekey varchar(100)				  ";
                    //    detail = detail + " 	declare @ptablename varchar(100)				  ";
                    //    detail = detail + " 	declare @Column varchar(100)				  ";
                    //    detail = detail + " 	declare @Table varchar(255)				  ";
                    //    detail = detail + " 	declare @Description1 varchar(100)				  ";
                    //    detail = detail + " 	declare @sql varchar(2000)				  ";
                    //    detail = detail + " 	declare @sql1 varchar(2000)				  ";
                    //    detail = detail + " 	Create table #ObjectItemID(ItemID INT NOT NULL,FUsedFlag INT DEFAULT(2),FDescription VARCHAR(400) NULL)				  ";
                    //    detail = detail + " 	INSERT INTO #ObjectItemID (ItemID) VALUES(2621)				  ";
                    //    detail = detail + " 	declare feeCursor cursor scroll for				  ";
                    //    detail = detail + " 		select distinct a.fclasstypeid as fid,a.ftablename,a.ffieldname,b.fname_CHS			  ";
                    //    detail = detail + " 	 as FDescription,b.fprimarykey,b.fclasstypekey,b.ftablename as fptablename				  ";
                    //    detail = detail + " 	 from icclasstableinfo a 				  ";
                    //    detail = detail + " 	 inner join icclasstype b on a.fclasstypeid=b.fid 				  ";
                    //    detail = detail + " 	 inner join (select name as fname from sysobjects where xtype='u' or xtype='v') c on a.ftablename=c.fname 				  ";
                    //    detail = detail + " 	 inner join (select name as fname from sysobjects where xtype='u' or xtype='v') d on a.ftablename=d.fname 				  ";
                    //    detail = detail + " 	 where ((a.flookupclassid=3006 and a.fsourcetype=1 and a.fctltype=1 and a.fkeyword='') 				  ";
                    //    detail = detail + " 	 or (a.fsourcetype=7 and a.fctltype=1 and a.fkeyword='')) 				  ";
                    //    detail = detail + " 	 and fbilltypeid in (1,3) and fclasstypeid>=1000000				  ";
                    //    detail = detail + " 	open feeCursor				  ";
                    //    detail = detail + " 	while @@FETCH_STATUS=0				  ";
                    //    detail = detail + " 	begin				  ";
                    //    detail = detail + " 		if(@tablename=@ptablename)			  ";
                    //    detail = detail + " 		begin			  ";
                    //    detail = detail + " 		set @sql=' SELECT DISTINCT '+@fieldname+' INTO #UsedItem FROM '+@tablename+' T2 			  ";
                    //    detail = detail + " 				INNER JOIN #ObjectItemID T1 ON T1.ItemID = T2.'+@fieldname+'	  ";
                    //    detail = detail + " 				WHERE T2.'+@classtypekey+'='+@id+' AND ISNULL(T1.FUsedFlag,0)<>1	  ";
                    //    detail = detail + " 				UPDATE #ObjectItemID SET FUsedFlag = 1,FDescription='''+@Description+'''	  ";
                    //    detail = detail + " 				WHERE EXISTS(SELECT 1 FROM #UsedItem WHERE ItemID= '+@fieldname+')	  ";
                    //    detail = detail + " 				DROP TABLE #UsedItem'	  ";
                    //    detail = detail + " 		end			  ";
                    //    detail = detail + " 		else			  ";
                    //    detail = detail + " 		begin			  ";
                    //    detail = detail + " 		set @sql='SELECT DISTINCT T2.'+@fieldname+' INTO #UsedItem 			  ";
                    //    detail = detail + " 				FROM '+@tablename+' T2 WHERE EXISTS(SELECT 1 FROM '+@ptablename+' T1 WHERE T1.'+@classtypekey+' = '+@id+' 	  ";
                    //    detail = detail + " 				AND T1.'+@primarykey+'= T2.'+@primarykey+')	  ";
                    //    detail = detail + " 				 AND EXISTS(SELECT 1 FROM #ObjectItemID WHERE ISNULL(FUsedFlag,0)<>1 AND ItemID = T2.'+@fieldname+')	  ";
                    //    detail = detail + " 				UPDATE #ObjectItemID SET FUsedFlag = 1,FDescription='''+@Description+'''	  ";
                    //    detail = detail + " 				WHERE EXISTS(SELECT 1 FROM #UsedItem WHERE ItemID= '+@fieldname+')	  ";
                    //    detail = detail + " 				DROP TABLE #UsedItem'	  ";
                    //    detail = detail + " 		end			  ";
                    //    detail = detail + " 		print(@sql)			  ";
                    //    detail = detail + " 		exec(@sql)			  ";
                    //    detail = detail + " 		fetch next from feeCursor into  @id,@tablename,@fieldname,@Description,@primarykey,@classtypekey,@ptablename			  ";
                    //    detail = detail + " 	end				  ";
                    //    detail = detail + " 	CLOSE feeCursor				  ";
                    //    detail = detail + " 	DEALLOCATE feeCursor				  ";
                    //    detail = detail + " 					  ";
                    //    detail = detail + " 	declare feeCursorO cursor  FORWARD_ONLY for--LOCAL FAST_FORDARD				  ";
                    //    detail = detail + " 	select FTable,FColumn,FDescription as FDescription from t_BASE_ObjectInUsed where FObjectType=3 and (isnull(FItemClassID,0)=3006 or isnull(FItemClassID,0) = -1)				  ";
                    //    detail = detail + " 					  ";
                    //    detail = detail + " 	open feeCursorO				  ";
                    //    detail = detail + " 	while @@FETCH_STATUS=0				  ";
                    //    detail = detail + " 	begin				  ";
                    //    detail = detail + " 		set @sql1='UPDATE #ObjectItemID SET FUsedFlag = 1,FDescription='''+@Description1+''' FROM #ObjectItemID t1,'+@Table+'  WHERE t1.ItemID= '+@Column+' AND FUsedFlag <> 1'			  ";
                    //    detail = detail + " 		PRINT @sql1			  ";
                    //    detail = detail + " 		exec(@sql1)			  ";
                    //    detail = detail + " 		fetch next from feeCursorO into  @Table,@Column,@Description1			  ";
                    //    detail = detail + " 	end				  ";
                    //    detail = detail + " 	CLOSE feeCursorO				  ";
                    //    detail = detail + " 	DEALLOCATE feeCursorO				  ";
                    //    detail = detail + " 	select count(1) from #ObjectItemID where FUsedFlag=1				  ";
                    //    detail = detail + " 	Drop table #ObjectItemID				  ";


                    //    int count = 0;
                    //    count = Convert.ToInt32(DbHelperSQL.GetSingle(detail, oPara));
                    //    if (count > 0)
                    //    {
                    //        sReturn = sReturn + "<status>-1</status>" + "<message>" + "编号为" + project.Number + "的科目已被使用" + "</message>";
                    //        continue;
                    //    }
                    //}
                    #endregion
                    string sSqlsc;
                    sSqlsc = " exec sp_executesql N'SELECT FItemID FROM t_Item WHERE FItemClassID=@P1 AND FNumber=@P2',N'@P1 int,@P2 varchar(80)','3001',@FNUMBER";
                    ods = DbHelperSQL.Query(sSqlsc, oPara);
                    //查询项目的id，如果不存在，则新增,存在则修改
                    if (ods != null && ods.Tables[0].Rows.Count > 0)
                    {

                        try
                        {
                            SqlParameter[] oParaE ={
                                        new SqlParameter("@FNumber",SqlDbType.VarChar), //接收传入的编码值
                                        new SqlParameter("@FName",SqlDbType.VarChar),
                                        new SqlParameter("@FParent",SqlDbType.VarChar),
                                        new SqlParameter("@FItemID",SqlDbType.VarChar),
                                        new SqlParameter("@FShortNumber",SqlDbType.VarChar),
                                        new SqlParameter("@FDeleted",SqlDbType.Int),
                                            };
                            oParaE[0].Value = project.Number;
                            oParaE[1].Value = project.Name;
                            oParaE[2].Value = project.ParentNumber; //此处@FPARENT是@FPARENTNumber
                            if (project.Deleted != null)
                            {
                                oParaE[5].Value = project.Deleted;
                            }
                            else
                                oParaE[5].Value = 0;
                            DataSet exist = new DataSet();

                            #region 父级
                            //                            int lastDot = project.Number.ToString().LastIndexOf(".");
                            //                            if (lastDot != -1)
                            //                            {

                            //                                oPara[4].Value = project.Number.ToString().Substring(lastDot + 1);
                            //                                //查询要修改的项目的父级项目是否存在，存在则可以修改，
                            //                                string sSqlsc = @"  Select * From (Select t1.* From t_Item t1  
                            //                                                WHERE (t1.FDeleteD=1 Or t1.FDeleteD=0)  And t1.FItemClassID = 3006 
                            //                                                And t1.FItemClassID = 3006) i  Where 1=1  And (FNumber = @FParent)";
                            //                                exist = DbHelperSQL.Query(sSqlsc, oPara);

                            //                                if (exist != null && exist.Tables[0].Rows.Count == 0)
                            //                                {
                            //                                    //
                            //                                    sReturn = sReturn + "<status>-1</status>" + "<message>" + "要修改的项目" + project.Number + "的父级项目不存在" + "</message>";
                            //                                    continue;
                            //                                }
                            //                                //获取要修改项目的父级fitemid
                            //                                oPara[2].Value = exist.Tables[0].Rows[0]["FItemID"];
                            //                            }
                            #endregion

                            //获取要修改对象的itemid
                            string sSqlGetId = "select FItemID from t_Item where FItemClassID='3001' and FNumber=@FNumber";
                            ods = DbHelperSQL.Query(sSqlGetId, oParaE);
                            if (ods != null && ods.Tables[0].Rows.Count == 0)
                            {
                                sReturn = sReturn + "<status>-1</status>" + "<message>" + "要修改的项目" + project.Number + "不存在" + "</message>";
                                continue;
                            }
                            oParaE[3].Value = ods.Tables[0].Rows[0]["FItemID"].ToString();
                            string paNumber = project.Number;
                            if (project.Deleted == "1")
                            {
                                oParaE[5].Value = project.Deleted;
                                string sSqlEnable = " UPDATE t_Item SET FDeleted=1 WHERE FItemID=@FItemID";
                                DbHelperSQL.ExecuteSql(sSqlEnable, oParaE);
                                sReturn = sReturn + "<status>0</status>" + "<message>" + "编号为" + project.Number + "的项目禁用成功" + "</message>";
                                continue;
                            }
                            string sSqlEdit = " UPDATE t_Item SET FName=@FName WHERE FItemID=@FItemID";
                            sSqlEdit = sSqlEdit + " Update t_Item Set FFullName = '''' Where FNumber Like '" + paNumber + ".%' AND FItemClassID = 3001";

                            DbHelperSQL.ExecuteSql(sSqlEdit, oParaE);
                            sReturn = sReturn + "<status>0</status>" + "<message>" + "编号为" + project.Number + "的项目修改成功" + "</message>";
                            //return "<?xml version=\"1.0\" encoding=\"utf-8\"?> <xml>" + sReturn + "</xml>";
                        }
                        catch (Exception ex)
                        {

                            sReturn = sReturn + "<status>-1</status>" + "<message>" + "编号为" + project.Number + "的项目修改出错" + ex.Message.ToString() + "</message>";
                        }

                        continue;
                    }

                    //给uuid赋值
                    sSqlsc = " Select newid() as UUID";
                    ods = DbHelperSQL.Query(sSqlsc);
                    oPara[4].Value = ods.Tables[0].Rows[0]["UUID"].ToString();

                    #region
                    ////获取新项目明细的短编码fshortnumber
                    //int lastDot = project.Number.ToString().LastIndexOf(".");
                    //if (lastDot > -1)
                    //{
                    //    oPara[7].Value = project.Number.ToString().Substring(lastDot + 1);
                    //}
                    //else
                    //{
                    //    oPara[7].Value = project.ParentNumber;
                    //}
                    #endregion

                    //插入item表
                    string sSqlInsert = @" exec sp_executesql N'INSERT INTO t_Item (FItemClassID
                                                                                           ,FParentID
                                                                                           ,FLevel
                                                                                           ,FName
                                                                                           ,FNumber
                                                                                           ,FShortNumber
                                                                                           ,FFullNumber
                                                                                           ,FDetail
                                                                                           ,UUID
                                                                                           ,FDeleted) 
                                                                                            VALUES (@P1,@P2,@P3,@P4,@P5,@P6,@P7,@P8,@P9,@P10)',N'@P1 int,@P2 int,@P3 smallint,@P4 varchar(80),@P5 varchar(80),@P6 varchar(80),@P7 varchar(80),@P8 bit,@P9 varchar(38),@P10 smallint'
                                                                                           ,3001
                                                                                           ,0
                                                                                           ,1
                                                                                           ,@FNAME
                                                                                           ,@FNUMBER
                                                                                           ,@FNUMBER
                                                                                           ,@FNUMBER
                                                                                           ,1
                                                                                           ,@UUID
                                                                                           ,0";
                    DbHelperSQL.ExecuteSql(sSqlInsert, oPara);

                    sSqlInsert = " exec sp_executesql N'SELECT FItemID FROM t_Item WHERE FItemClassID=@P1 AND FNumber=@P2',N'@P1 int,@P2 varchar(80)',@FITEMCLASSID,@FNUMBER";
                    oPara[5].Value = DbHelperSQL.GetSingle(sSqlInsert, oPara);

                    //插入Access_t_ItemDefine表
                    sSqlInsert = " Delete from Access_t_ItemDefine where FItemID=@FITEMID";
                    sSqlInsert = sSqlInsert + @"  Insert into Access_t_ItemDefine(FItemID,FParentIDX,FDataAccessView,FDataAccessEdit,FDataAccessDelete)
                                                      Values(@FITEMID,0,convert(varbinary(7200),REPLICATE(char(255),100)),convert(varbinary(7200),REPLICATE(char(255),100)),convert(varbinary(7200),REPLICATE(char(255),100)))";
                    sSqlInsert = sSqlInsert + "  update t_Item set FName=FName where FItemID=@FITEMID and FItemClassID=@FITEMCLASSID";
                    sSqlInsert = sSqlInsert + "  UPDATE t_Item SET FDetail=0 WHERE FItemID=0";
                    DbHelperSQL.ExecuteSql(sSqlInsert, oPara);



                    sReturn = sReturn + "<status>0</status>" + "<message>" + "编号为" + project.Number + "的项目插入成功" + "</message>";

                    //return "<?xml version=\"1.0\" encoding=\"utf-8\"?> <xml>" + sReturn + "</xml>";


                }
                catch (Exception ex)
                {
                    sReturn = sReturn + "<status>-1</status>" + "<message>" + "编号为" + project.Number + "的项目插入出错" + ex.Message.ToString() + "</message>";
                }
            }

            //break;
            #region
            //case "UPD":
            //foreach (var project in modelList)
            //{
            //    try
            //    {
            //        SqlParameter[] oPara ={
            //                    new SqlParameter("@FNumber",SqlDbType.VarChar), //接收传入的编码值
            //                    new SqlParameter("@FName",SqlDbType.VarChar),
            //                    new SqlParameter("@FParent",SqlDbType.VarChar),
            //                    new SqlParameter("@FItemID",SqlDbType.VarChar),
            //                    new SqlParameter("@FShortNumber",SqlDbType.VarChar)
            //                        };
            //        oPara[0].Value = project.Number;
            //        oPara[1].Value = project.Name;
            //        oPara[2].Value = project.ParentNumber; //此处@FPARENT是@FPARENTNumber


            //        DataSet exist = new DataSet();

            //        #region 父级
            //        //                            int lastDot = project.Number.ToString().LastIndexOf(".");
            //        //                            if (lastDot != -1)
            //        //                            {

            //        //                                oPara[4].Value = project.Number.ToString().Substring(lastDot + 1);
            //        //                                //查询要修改的项目的父级项目是否存在，存在则可以修改，
            //        //                                string sSqlsc = @"  Select * From (Select t1.* From t_Item t1  
            //        //                                                WHERE (t1.FDeleteD=1 Or t1.FDeleteD=0)  And t1.FItemClassID = 3006 
            //        //                                                And t1.FItemClassID = 3006) i  Where 1=1  And (FNumber = @FParent)";
            //        //                                exist = DbHelperSQL.Query(sSqlsc, oPara);

            //        //                                if (exist != null && exist.Tables[0].Rows.Count == 0)
            //        //                                {
            //        //                                    //
            //        //                                    sReturn = sReturn + "<status>-1</status>" + "<message>" + "要修改的项目" + project.Number + "的父级项目不存在" + "</message>";
            //        //                                    continue;
            //        //                                }
            //        //                                //获取要修改项目的父级fitemid
            //        //                                oPara[2].Value = exist.Tables[0].Rows[0]["FItemID"];
            //        //                            }
            //        #endregion

            //        //获取要修改对象的itemid
            //        string sSqlGetId = "select FItemID from t_Item where FItemClassID='3001' and FNumber=@FNumber";
            //        ods = DbHelperSQL.Query(sSqlGetId, oPara);
            //        if (ods != null && ods.Tables[0].Rows.Count == 0)
            //        {
            //            sReturn = sReturn + "<status>-1</status>" + "<message>" + "要修改的项目" + project.Number + "不存在" + "</message>";
            //            continue;
            //        }
            //        oPara[3].Value = ods.Tables[0].Rows[0]["FItemID"].ToString();
            //        string paNumber = project.Number;

            //        string sSqlEdit = " UPDATE t_Item SET FName=@FName WHERE FItemID=@FItemID";
            //        sSqlEdit = sSqlEdit + " Update t_Item Set FFullName = '''' Where FNumber Like '" + paNumber + ".%' AND FItemClassID = 3001";

            //        DbHelperSQL.ExecuteSql(sSqlEdit, oPara);
            //        sReturn = sReturn + "<status>0</status>" + "<message>" + "编号为" + project.Number + "的项目修改成功" + "</message>";
            //        return "<?xml version=\"1.0\" encoding=\"utf-8\"?> <xml>" + sReturn + "</xml>";
            //    }
            //    catch (Exception ex)
            //    {

            //        sReturn = sReturn + "<status>-1</status>" + "<message>" + "编号为" + project.Number + "的项目插入出错" + ex.Message.ToString() + "</message>";
            //    }
            //}
            //break;
            #endregion

            //}


            return "<?xml version=\"1.0\" encoding=\"utf-8\"?> <xml>" + sReturn + "</xml>";

        }


    }
}
