using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ik3DtWs
{
    public class hsxmModel
    {
        public hsxmModel()
        {
        }
        /// <summary>
        /// 对应的费用核算项目编号
        /// </summary>                    

        private string FNumber;

        public string Number
        {
            get { return FNumber; }
            set { FNumber = value; }
        }

        /// <summary>
        /// 对应的费用核算项目名称
        /// </summary> 
        private string FName;

        public string Name
        {
            get { return FName; }
            set { FName = value; }
        }

        /// <summary>
        /// 对应的费用核算项目父级编码
        /// </summary> 
        private string FParentNumber;

        public string ParentNumber
        {
            get { return FParentNumber; }
            set { FParentNumber = value; }
        }

        /// <summary>
        /// 对应的费用核算项目父级名称
        /// </summary> 
        private string FParentName;

        public string ParentName
        {
            get { return FParentName; }
            set { FParentName = value; }
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