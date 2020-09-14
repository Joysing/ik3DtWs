using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ik3DtWs
{
    public class VSModel
    {
        public VSModel() 
        {
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
        /// 对应的凭证部门编号
        /// </summary>
        private string FDeptNum;
        
        public string DeptNum
        {
            get { return FDeptNum; }
            set { FDeptNum = value; }
        }

        /// <summary>
        /// 对应的凭证部门名称
        /// </summary>
        private string FDeptName;

        public string DeptName
        {
            get { return FDeptName; }
            set { FDeptName = value; }
        }

        /// <summary>
        /// 对应的凭证编号
        /// </summary>
        private string MaxFNumber;

        public string MaxNumber
        {
            get { return MaxFNumber; }
            set { MaxFNumber = value; }
        }

        /// <summary>
        /// 对应的凭证编号
        /// </summary>
        private string MinFNumber;

        public string MinNumber
        {
            get { return MinFNumber; }
            set { MinFNumber = value; }
        }
    }
}