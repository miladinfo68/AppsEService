using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ResourceControl.Entity
{
    [Serializable]
    public class RequestFR
    {
        int id;
        string subject;
        string note;
        string answernote;
        string answer_time;
        string issue_time;
        int status;
        int issuerID;
        string send_time;
        int catID;
        int senderID;
        int replierID;
        bool isDeleted;
        string issuerName;
        string location;
        int capacity;
        string coursename;
        int courseDID;
        int daneshID;
        string namedanesh;
        string nameresh;
        string resource;
        int resourceID;
        public bool? IsRejectFinancial { get; set; }

        private List<RequestDateTime> _dateTimeRange;

        public List<RequestDateTime> DateTimeRange
        {
            get { return _dateTimeRange; }
            set { _dateTimeRange = value; }
        }

        public int ID
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }
        public string Subject
        {
            get
            {
                return subject;
            }
            set
            {
                subject = value;
            }
        }
        public string Note
        {
            get
            {
                return note;
            }
            set
            {
                note = value;
            }
        }
        public string Answernote
        {
            get
            {
                return answernote;
            }
            set
            {
                answernote = value;
            }
        }
        public string Answer_time
        {
            get
            {
                return answer_time;
            }
            set
            {
                answer_time = value;
            }
        }
        public string Issue_time
        {
            get
            {
                return issue_time;
            }
            set
            {
                issue_time = value;
            }
        }
        public int Status
        {
            get
            {
                return status;
            }
            set
            {
                status = value;
            }
        }
        public int IssuerID
        {
            get
            {
                return issuerID;
            }
            set
            {
                issuerID = value;
            }
        }
        public string Send_time
        {
            get
            {
                return send_time;
            }
            set
            {
                send_time = value;
            }
        }
       
        public int CatID
        {
            get
            {
                return catID;
            }
            set
            {
                catID = value;
            }
        }
        public int ReplierID
        {
            get
            {
                return replierID;
            }
            set
            {
                replierID = value;
            }
        }
       
        public int SenderID 
        { 
            get 
            { 
                return senderID; 
            }
            set 
            { 
                senderID = value; 
            } 
        }
        public bool IsDeleted
        {
            get
            {
                return isDeleted;
            }
            set
            {
                isDeleted = value;
            }
        }
        public string IssuerName
        {
            get 
            { 
                return issuerName; 
            }
            set 
            {
                issuerName = value; 
            }
        }
        public string Location
        {
            get
            {
                return location;
            }
            set
            {
                location = value;
            }
        }
        public int Capacity
        {
            get
            {
                return capacity;
            }
            set
            {
                capacity = value;
            }
        }

        public string CourseName
        {
            get
            {
                return coursename;
            }

            set
            {
                coursename = value;
            }
        }

        public int CourseDID
        {
            get
            {
                return courseDID;
            }

            set
            {
                courseDID = value;
            }
        }

        public int DaneshID
        {
            get
            {
                return daneshID;
            }

            set
            {
                daneshID = value;
            }
        }
        public string Nameresh
        {
            get
            {
                return nameresh;
            }

            set
            {
                nameresh = value;
            }
        }


       public string Namedanesh
        {
            get
            {
                return namedanesh;
            }

            set
            {
                namedanesh = value;
            }
        }
        public string ResourceName
        {
            get
            {
                return resource;
            }

            set
            {
                resource = value;
            }
        }
        public int ResourceId
        {
            get
            {
                return resourceID;
            }

            set
            {
                resourceID = value;
            }
        }
    }
}