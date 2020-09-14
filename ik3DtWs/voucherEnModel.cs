using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ik3DtWs
{
    public class voucherEnModel
    {
        public voucherEnModel()
        {
        }

        /// <summary>
        /// 对应的科目代码
        /// </summary>                    
        private string FAccountNum;

        public string AccountNum
        {
            get { return FAccountNum; }
            set { FAccountNum = value; }
        }

        /// <summary>
        /// 对应的科目名称
        /// </summary>
        private string FAccountName;

        public string AccountName
        {
            get { return FAccountName; }
            set { FAccountName = value; }
        }

     

        /// <summary>
        /// 对应的原币金额
        /// </summary>
        private string FAmountFor;

        public string AmountFor
        {
            get { return FAmountFor; }
            set { FAmountFor = value; }
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
        /// 分录对应部门编号
        /// </summary>
        private string FDeptNum;

        public string DeptNum
        {
            get { return FDeptNum; }
            set { FDeptNum = value; }
        }


        /// <summary>
        /// 分录对应部门名称
        /// </summary>
        private string FDeptName;

        public string DeptName
        {
            get { return FDeptName; }
            set { FDeptName = value; }
        }


        /// <summary>
        /// 分录对应项目编号
        /// </summary>
        private string FProjNum;

        public string ProjNum
        {
            get { return FProjNum; }
            set { FProjNum = value; }
        }

        /// <summary>
        /// 分录对应项目名称
        /// </summary>
        private string FProjName;

        public string ProjName
        {
            get { return FProjName; }
            set { FProjName = value; }
        }

        /// <summary>
        /// 对应的分录序号
        /// </summary>
        private string FEntryID;

        public string EntryID
        {
            get { return FEntryID; }
            set { FEntryID = value; }
        }

        /// <summary>
        /// 对应的借贷方向
        /// </summary>
        private string FDC;

        public string DC
        {
            get { return FDC; }
            set { FDC = value; }
        }


        /// <summary>
        /// 对应的往来业务
        /// </summary>
        private string FTransNo;

        public string TransNo
        {
            get { return FTransNo; }
            set { FTransNo = value; }
        }
        /// <summary>
        /// 对应的往来业务
        /// </summary>
        private string FSettleNo;

        public string SettleNo
        {
            get { return FSettleNo; }
            set { FSettleNo = value; }
        }
    }
}