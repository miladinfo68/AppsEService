using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAUEC_Apps.DTO.University.Faculty
{
    public class EditInfo
    {
    }

    public struct editInfoStruct
    {
        private int _hrId;
        private Int64 _codeOstad;
        private string _idd;
        private string _idd_Melli;
        private string _name;
        private string _family;
        private string _fatherName;
        private string _salTavalod;
        private int _nezam;
        private bool _taahol;
        private int _maghta;
        private int _reshte;
        private string _siba;
        private string _salMadrak;
        private string _sanavat;
        private int _Keshvar;
        private int _typeUniMadrak;
        private int _nameUniMadrak;
        private bool _bazneshaste;
        private bool _bime;
        private int _bimeType;
        private string _bimeNum;
        private string _telHome;
        private string _telKar;
        private string _telMobile;
        private string _email;
        private int _ostanHome;
        private int _shahrHome;
        private string _codePosti;
        private string _addressHome;
        private int _ostanKar;
        private int _shahrKar;
        private string _addressKar;
        private bool _sex;
        private string _description;

        public int hrId { get { return _hrId; } set { try { _hrId = value; } catch { _hrId = 0; } } }
        public Int64 codeOstad { get { return _codeOstad; } set { try { _codeOstad = value; } catch { _codeOstad = 0; } } }
        public int nezam { get { return _nezam; } set { try { _nezam = value; } catch { _nezam = 0; } } }
        public int maghta { get { return _maghta; } set { try { _maghta = value; } catch { _maghta = 0; } } }
        public int reshte { get { return _reshte; } set { try { _reshte = value; } catch { _reshte = 0; } } }
        public int keshvar { get { return _Keshvar; } set { try { _Keshvar = value; } catch { _Keshvar = 0; } } }
        public int typeUniMadrak { get { return _typeUniMadrak; } set { try { _typeUniMadrak = value; } catch { _typeUniMadrak = 0; } } }
        public int nameUniMadrak { get { return _nameUniMadrak; } set { try { _nameUniMadrak = value; } catch { _nameUniMadrak = 0; } } }
        public int bimeType { get { return _bimeType; } set { try { _bimeType = value; } catch { _bimeType = 0; } } }
        public int ostanHome { get { return _ostanHome; } set { try { _ostanHome = value; } catch { _ostanHome = 0; } } }
        public int shahrHome { get { return _shahrHome; } set { try { _shahrHome = value; } catch { _shahrHome = 0; } } }
        public int ostanKar { get { return _ostanKar; } set { try { _ostanKar = value; } catch { _ostanKar = 0; } } }
        public int shahrKar { get { return _shahrKar; } set { try { _shahrKar = value; } catch { _shahrKar = 0; } } }
        public bool taahol { get { return _taahol; } set { try { _taahol = value; } catch { _taahol = false; } } }
        public bool bazneshaste { get { return _bazneshaste; } set { try { _bazneshaste = value; } catch { _bazneshaste = false; } } }
        public bool bime { get { return _bime; } set {try {  _bime = value; } catch { _bime = false; } } }
        public string idd { get { if (_idd != null) return _idd.Trim(); else return ""; } set { try { _idd = value; } catch { _idd = ""; } } }
        public string idd_Melli { get { if (_idd_Melli != null) return _idd_Melli.Trim(); else return ""; } set { try { _idd_Melli = value; } catch { _idd_Melli = ""; } } }
        public string salTavalod { get { if (_salTavalod != null) return _salTavalod.Trim(); else return ""; } set { try { _salTavalod = value; } catch { _salTavalod = ""; } } }
        public string salMadrak { get { if (_salMadrak != null) return _salMadrak.Trim(); else return ""; } set { try { _salMadrak = value; } catch { _salMadrak = ""; } } }
        public string sanavat { get { if (_sanavat != null) return _sanavat.Trim(); else return ""; } set { try { _sanavat = value; } catch { _sanavat = ""; } } }
        public string siba { get { if (_siba != null) return _siba.Trim(); else return ""; } set { if (value != null) try { _siba = value; } catch { _siba = null; } } }
        public string bimeNum { get { if (_bimeNum != null) return _bimeNum.Trim(); else return ""; } set { try { _bimeNum = value; } catch { _bimeNum = null; } } }
        public string telHome { get { if (_telHome != null) return _telHome.Trim(); else return ""; } set { try { _telHome = value; } catch { _telHome = null; } } }
        public string telKar { get { if (_telKar != null) return _telKar.Trim(); else return ""; } set { try { _telKar = value; } catch { _telKar = null; } } }
        public string telMobile { get { if (_telMobile!= null) return _telMobile.Trim(); else return ""; } set { try { _telMobile = value; } catch { _telMobile = null; } } }
        public string codePosti { get { if (_codePosti != null) return _codePosti.Trim(); else return ""; } set { try { _codePosti = value; } catch { _codePosti = null; } } }
        public string name { get { if (_name != null) return _name.Trim(); else return ""; } set { try { _name = value; } catch { _name = null; } } }
        public string family { get { if (_family != null) return _family.Trim(); else return ""; } set { try { _family = value; } catch { _family =null; } } }
        public string fatherName { get { if (_fatherName != null) return _fatherName.Trim(); else return ""; } set { try { _fatherName = value; } catch { _fatherName = null; } } }
        public string addressHome { get { if (_addressHome!= null) return _addressHome.Trim(); else return ""; } set { try { _addressHome = value; } catch { _addressHome = null; } } }
        public string addressKar { get { if (_addressKar != null) return _addressKar.Trim(); else return ""; } set { try { _addressKar = value; } catch { _addressKar = null; } } }
        public string email { get { if (_email != null) return _email.Trim(); else return ""; } set { try { _email = value; } catch { _email = ""; } } }
        public bool sexIsMan { get { return _sex; } set { try { _sex = value; } catch { _sex = false; } } }
        public string description { get { if (_description != null) return _description.Trim(); else return ""; } set { try { _description = value; } catch { _description = null; } } }
    }
}
