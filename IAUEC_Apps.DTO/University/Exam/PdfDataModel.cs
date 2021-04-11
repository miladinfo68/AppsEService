using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAUEC_Apps.DTO.University.Exam
{
    public class PdfDataModel
    {
        public string StCode { get; set; }
        public string StName { get; set; }
        public string StFamliy { get; set; }
        public string StLevel { get; set; }
        public string StField { get; set; }

        public System.Drawing.PointF PointStCode { get; set; }
        public System.Drawing.PointF PointStName { get; set; }
        public System.Drawing.PointF PointStFamliy { get; set; }
        public System.Drawing.PointF PointStLevel { get; set; }
        public System.Drawing.PointF PointStField { get; set; }
    }
}
