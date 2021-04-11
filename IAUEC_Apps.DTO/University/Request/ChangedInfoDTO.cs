using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAUEC_Apps.DTO.University.Request
{
    [Serializable]
    public class ChangedInfoDTO
    {
        public ChangedInfoDTO()
        {

        }
        public ChangedInfoDTO(int code_ostad)
        {
            State = FieldChangeState.beforSubmit;
            Code_Ostad = code_ostad;
        }
        public int Code_Ostad { get; set; }

        public int ControlToFieldId { get; set; }

        public string ControlId { get; set; }

        public string OldValue { get; set; }

        private string _newValue;

        public string NewValue
        {
            get
            {
                return _newValue;
            }
            set
            {
                _newValue = value.Trim();
            }
        }

        public FieldChangeState State { get; set; }

        public int ProfessorRequestId { get; set; }

    }

    public enum FieldChangeState
    {
        beforSubmit = 0,
        Submitted = 1,
        Approved = 2,
        Denied = 3
    }
}
