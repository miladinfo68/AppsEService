using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ResourceControl.Entity;

namespace IAUEC_Apps.Business.ResourceControl
{
    public class ComprareForListOfClassDistinc : IEqualityComparer<Course>
    {
        public bool Equals(Course x, Course y)
        {
            var flag = (x.DID == y.DID);
            return flag;
        }

        public int GetHashCode(Course obj)
        {
            return obj.DID.GetHashCode();
        }
    }
}
