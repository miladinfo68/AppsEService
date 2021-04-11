using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace IAUEC_Apps.DTO.ResourceControlClasses
{
  public  class InformationOstadForDefenceStudent
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string AddressSignature { get; set; }
        public string TypeOS { get; set; }
        public int IdTypeOs { get; set; }
        public string Mobile { get; set; }
        public Image ImageSign { set; get; }
        public byte[] byteSign { get; set; }
        public string singAddress { get; set; }

    }
}
