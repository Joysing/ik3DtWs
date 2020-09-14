using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ik3DtWs
 {
    public class deptModel
    {
        public deptModel()
        { }

        /// <summary>
        /// 对应的部门编号
        /// </summary>
        
        private string FNumber;
        
        public string Number
        {
            get { return FNumber; }
            set { FNumber = value; }
        }

        /// <summary>
        /// 对应的部门名称
        /// </summary> 
       private string FName;
        
        public string Name
        {
            get { return FName; }
            set { FName = value; }
        }

        /// <summary>
        /// 对应的上级部门编码
        /// </summary> 
        private string FParentNumber;

        public string ParentNumber
        {
            get { return FParentNumber; }
            set { FParentNumber = value; }
        }

        /// <summary>
        /// 对应的上级部门名称
        /// </summary> 
        private string FParentName;

        public string ParentName
        {
            get { return FParentName; }
            set { FParentName = value; }
        }

        /// <summary>
        /// 对应的部门属性
        /// </summary> 
        private string FDProperty;

        public string Property
        {
            get { return FDProperty; }
            set { FDProperty = value; }
        }


        /// <summary>
        /// 对应的成本核算类型
        /// </summary> 
        private string FCostAccountType;

        public string CostAccountType
        {
            get { return FCostAccountType; }
            set { FCostAccountType = value; }
        }


        /// <summary>
        /// 对应的成本核算类型
        /// </summary> 
        private string FNote;

        public string Note
        {
            get { return FNote; }
            set { FNote = value; }
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