using System;
using System.Collections.Generic;
using System.Text;

namespace CyProject.Model.JsonModel
{
   public class GetExcuteTaskRes:BaseJsonRes
    {

        /// <summary>
        /// 状态  0为已经完成 1为未完成  2为错误
        /// </summary>
        public int Status { get; set; }  



    }
}
