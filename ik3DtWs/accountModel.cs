using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ik3DtWs
{
    public class accountModel
    {
        public accountModel()
        { }

        /// <summary>
        /// 对应的科目编号
        /// </summary>                    

        private string FNumber;

        public string Number
        {   
            get { return FNumber; }
            set { FNumber = value; }
        }

        /// <summary>
        /// 对应的科目名称
        /// </summary> 
        private string FName;

        public string Name
        {
            get { return FName; }
            set { FName = value; }
        }

        /// <summary>
        /// 对应的科目组编码
        /// </summary> 
        private string FParentNumber;

        public string ParentNumber
        {
            get { return FParentNumber; }
            set { FParentNumber = value; }
        }

        /// <summary>
        /// 对应的科目组名称
        /// </summary> 
        private string FParentName;

        public string ParentName
        {
            get { return FParentName; }
            set { FParentName = value; }
        }

        /// <summary>
        /// 对应的科目类别
        /// </summary> 
        private string FGroupID;

        public string GroupID
        {
            get { return FGroupID; }
            set { FGroupID = value; }
        }


        /// <summary>
        /// 对应的余额方向
        /// </summary> 
        private string FDC;

        public string DC
        {
            get { return FDC; }
            set { FDC = value; }
        }


        /// <summary>
        /// 对应的币别
        /// </summary> 
        private string FCurrency;

        public string Currency
        {
            get { return FCurrency; }
            set { FCurrency = value; }
        }


        /// <summary>
        /// 对应的期末调汇
        /// </summary> 
        private string FAdjustRate;

        public string AdjustRate
        {
            get { return FAdjustRate; }
            set { FAdjustRate = value; }
        }

        /// <summary>
        /// 对应的往来业务核算
        /// </summary> 
        private string FContact;

        public string Contact
        {
            get { return FContact; }
            set { FContact = value; }
        }


        /// <summary>
        /// 对应的数量金额辅助核算
        /// </summary> 
        private string FQuantities;

        public string Quantities
        {
            get { return FQuantities; }
            set { FQuantities = value; }
        }

        /// <summary>
        /// 对应的现金科目
        /// </summary> 
        private string FIsCash;

        public string IsCash
        {
            get { return FIsCash; }
            set { FIsCash = value; }
        }

        /// <summary>
        /// 对应的银行科目
        /// </summary> 
        private string FIsBank;

        public string IsBank
        {
            get { return FIsBank; }
            set { FIsBank = value; }
        }

        /// <summary>
        /// 对应的出日记账
        /// </summary> 
        private string FJournal;

        public string Journal
        {
            get { return FJournal; }
            set { FJournal = value; }
        }


        /// <summary>
        /// 对应的是否计息
        /// </summary> 
        private string FAcctint;

        public string Acctint
        {
            get { return FAcctint; }
            set { FAcctint = value; }
        }

        /// <summary>
        /// 对应的现金等价物
        /// </summary> 
        private string FIsCashFlow;

        public string IsCashFlow
        {
            get { return FIsCashFlow; }
            set { FIsCashFlow = value; }
        }

        /// <summary>
        /// 对应的预算科目
        /// </summary> 
        private string FIsBudget;

        public string IsBudget
        {
            get { return FIsBudget; }
            set { FIsBudget = value; }
        }


        /// <summary>
        /// 对应的科目受控系统
        /// </summary> 
        private string FControlSystem;

        public string ControlSystem
        {
            get { return FControlSystem; }
            set { FControlSystem = value; }
        }


        /// <summary>
        /// 对应的是核算项目
        /// </summary> 
        private string FDetailID;

        public string DetailID
        {
            get { return FDetailID; }
            set { FDetailID = value; }
        }

        /// <summary>
        /// 对应的是否禁用
        /// </summary> 
        private string FDeleted;

        public string Deleted
        {
            get { return FDeleted; }
            set { FDeleted = value; }
        }

    }
}