using System.Collections.Generic;
using ResourceControl.Entity;

namespace IAUEC_Apps.Business.ResourceControl
{
    public class ComprareForId : IEqualityComparer<RequestFR>
    {
        public bool Equals(RequestFR x, RequestFR y)
        {
            var flag = (x.ID == y.ID);
            return flag;
        }

        public int GetHashCode(RequestFR obj)
        {
            return obj.ID.GetHashCode();
        }
    }
}