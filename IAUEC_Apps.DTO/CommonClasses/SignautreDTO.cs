using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAUEC_Apps.DTO.CommonClasses
{
    public class SignautreDTO
    {
        public int Id { get; set; }
        public byte[] Image { get; set; }
        public int IdentityNumber { get; set; }
        public int AppId { get; set; }

    }
}
