using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ik3DtWs
{
    public class voucherModel
    {
        public voucherModel()
        {
        }


        /// <summary>
        /// 对应的凭证日期
        /// </summary>                    

        private string FDate;

        public string Date
        {
            get { return FDate; }
            set { FDate = value; }
        }

        /// <summary>
        /// 对应的会计年度
        /// </summary>                    

        private string FYear;

        public string Year
        {
            get { return FYear; }
            set { FYear = value; }
        }

        /// <summary>
        /// 对应的会计期间
        /// </summary>                    

        private string FPeriod;

        public string Period
        {
            get { return FPeriod; }
            set { FPeriod = value; }
        }

        /// <summary>
        /// 对应的凭证字
        /// </summary>                    

        private string FGroupID;

        public string GroupID
        {
            get { return FGroupID; }
            set { FGroupID = value; }
        }

        /// <summary>
        /// 对应的凭证号
        /// </summary>
        private string FNumber;

        public string Number
        {
            get { return FNumber; }
            set { FNumber = value; }
        }

        

        /// <summary>
        /// 对应的借方
        /// </summary>
        private string FDebitTotal;

        public string DebitTotal
        {
            get { return FDebitTotal; }
            set { FDebitTotal = value; }
        }

        /// <summary>
        /// 对应的贷方
        /// </summary>
        private string FCreditTotal;

        public string CreditTotal
        {
            get { return FCreditTotal; }
            set { FCreditTotal = value; }
        }

        /// <summary>
        /// 制单人
        /// </summary>
        private string FPreparerID;

        public string PreparerID
        {
            get { return FPreparerID; }
            set { FPreparerID = value; }
        }

        /// <summary>
        /// 对应的参考信息
        /// </summary>
        private string FReference;

        public string Reference
        {
            get { return FReference; }
            set { FReference = value; }
        }

        /// <summary>
        /// 对应的往来业务日期
        /// </summary>
        private string FTransDate;

        public string TransDate
        {
            get { return FTransDate; }
            set { FTransDate = value; }
        }


        /// <summary>
        /// 对应的序号
        /// </summary>
        private string FSerialNum;

        public string SerialNum
        {
            get { return FSerialNum; }
            set { FSerialNum = value; }
        }


        /// <summary>
        /// 对应的借方科目
        /// </summary>
        private string FCreditAcct;

        public string CreditAcct
        {
            get { return FCreditAcct; }
            set { FCreditAcct = value; }
        }


        /// <summary>
        /// 对应的凭证摘要
        /// </summary>
        private string FExplanation;

        public string Explanation
        {
            get { return FExplanation; }
            set { FExplanation = value; }
        }

        /// <summary>
        /// 对应的凭证摘要
        /// </summary>
        private string FHandler;

        public string Handler
        {
            get { return FHandler; }
            set { FHandler = value; }
        }

        /// <summary>
        /// 对应的往来业务
        /// </summary>
        private string FParameter;
        public string Parameter
        {
            get { return FParameter; }
            set { FParameter = value; }
        }

        ///// <summary>
        ///// 对应的
        ///// </summary>
        //private string FBianhao;

        //public string Parameter
        //{
        //    get { return FParameter; }
        //    set { FParameter = value; }
        //}

        ///// <summary>
        ///// 对应的往来业务
        ///// </summary>
        //private string FParameter;

        //public string Parameter
        //{
        //    get { return FParameter; }
        //    set { FParameter = value; }
        //}
      

        /// <summary>
        /// 对应的批注
        ///用于预算系统暂存数据:
        /// 合同编号
        /// 合同名称
        /// 请示编码
        /// 请示名称
        /// 项目编码
        /// 项目名称
        /// 预算编码
        /// 预算名称
        /// </summary>
        private string FFootNote;

        public string FootNote
        {
            get { return FFootNote; }
            set { FFootNote = value; }
        }

        /// <summary>
        /// 对应的分录开头
        /// </summary>
        private List<voucherEnModel> enModelist = new List<voucherEnModel>();
        public  List<voucherEnModel> Entrys // = new List<voucherEnModel>();
        {
            get { return enModelist; }
            set { enModelist = value; }
        }

    }
}