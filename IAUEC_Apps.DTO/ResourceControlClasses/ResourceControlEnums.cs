using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ResourceControl.PL.Forms
{
    public static class ResourceControlEnums
    {
        public enum RequestStaus
        {
            submitted = 0,
            sent = 1,
            approved = 2,
            denied = 3,
            informed = 4,
            lost = 5
        }
        public enum RequestDefenceStaus
        {  /// <summary>
           /// 0
           /// </summary>
            submitted = 0,
            /// <summary>
            /// 1
            /// </summary>
            educationApprove = 9,

            /// <summary>
            /// 2
            /// </summary>
            approved = 2,
            /// <summary>
            /// 3
            /// </summary>
            denied = 3,
            /// <summary>
            /// 4
            /// </summary>
            informed = 4,
            /// <summary>
            /// 5
            /// </summary>
            lost = 5,
            /// <summary>
            /// 6
            /// </summary>
            technicalApprove = 6,
            /// <summary>
            /// 7
            /// </summary>
            deleted = 7,
            /// <summary>
            /// 8
            /// </summary>
            all = 8,


            /// <summary>
            /// 9
            /// </summary>
           FinancialApprove =1,


        }
    }
}
