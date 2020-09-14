using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ik3DtWs
{
    public class oaBm
    {
        public oaBm() { }

        /// <summary>
        /// 对应的部门编号
        /// </summary>
        private string fDeptBH;

        public string DeptBH
        {
            get { return fDeptBH; }
            set { fDeptBH = value; }
        }

        /// <summary>
        /// 对应的部门名称
        /// </summary> 
        private string fDeptName;

        public string DeptName
        {
            get { return fDeptName; }
            set { fDeptName = value; }
        }

        /// <summary>
        /// 对应的部门简称
        /// </summary> 
        private string fBMJC;

        public string BMJC
        {
            get { return fBMJC; }
            set { fBMJC = value; }
        }

        /// <summary>
        /// 对应的组织
        /// </summary> 
        private string fZZ;

        public string ZZ
        {
            get { return fZZ; }
            set { fZZ = value; }
        }


        /// <summary>
        /// 对应的组织编号
        /// </summary> 
        private string fZZBH;
        public string ZZBH
        {
            get { return fZZBH; }
            set { fZZBH = value; }
        }


        /// <summary>
        /// 对应的部门名称
        /// </summary> 
        private string fLevel;

        public string Level
        {
            get { return fLevel; }
            set { fLevel = value; }
        }


        /// <summary>
        /// 对应的状态
        /// </summary> 
        private string fEnable;

        public string Enable
        {
            get { return fEnable; }
            set { fEnable = value; }
        }

        /// <summary>
        /// 对应部门父级id
        /// </summary> 
        private string fParentDeptID;

        public string ParentDeptID
        {
            get { return fParentDeptID; }
            set { fParentDeptID = value; }
        }


        /// <summary>
        /// 对应部门父级名称
        /// </summary> 
        private string fParentDeptName;

        public string ParentDeptName
        {
            get { return fParentDeptName; }
            set { fParentDeptName = value; }
        }
    }
}