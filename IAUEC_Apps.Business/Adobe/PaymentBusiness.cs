using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IAUEC_Apps.DTO.AdobeClasses;
using IAUEC_Apps.DAO.CommonDAO;
using IAUEC_Apps.DTO.CommonClasses;
using System.Data;

namespace IAUEC_Apps.Business.Adobe
{
    public class PaymentBusiness
    {
        PaymentDAO pDAO = new PaymentDAO();
        public List<PaymentDTO> Get_Student_Payment(string stcode)
        {
            List<PaymentDTO> lstPayment = new List<PaymentDTO>();
           
            DataTable dt = new DataTable();
            dt = pDAO.Get_Student_Payment(stcode);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                PaymentDTO payDTO = new PaymentDTO();
                payDTO.OrderId = int.Parse(dt.Rows[i]["OrderID"].ToString());
                payDTO.Amount = long.Parse(dt.Rows[i]["AmountTrans"].ToString());
                payDTO.stcode = dt.Rows[i]["StudentCode"].ToString();
                payDTO.tterm = dt.Rows[i]["tterm"].ToString();
                payDTO.PersianDate = dt.Rows[i]["PersianDate"].ToString();
                if (dt.Rows[i]["TraceNumber"].ToString() != "")
                    payDTO.TraceNumber = long.Parse(dt.Rows[i]["TraceNumber"].ToString());
                else
                    payDTO.TraceNumber = 0;
                payDTO.AppStatus = dt.Rows[i]["paystatus"].ToString();
                lstPayment.Add(payDTO);
            }
            return lstPayment;
        }
        public List<PaymentDTO> GetAllPayment(string FrmPersianDate, string toPersianDate)
        {
            List<PaymentDTO> lstPayment = new List<PaymentDTO>();

            DataTable dt = new DataTable();
            dt = pDAO.GetAllPayment(FrmPersianDate, toPersianDate);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                PaymentDTO payDTO = new PaymentDTO();
                payDTO.OrderId = int.Parse(dt.Rows[i]["OrderID"].ToString());
                payDTO.Amount = long.Parse(dt.Rows[i]["AmountTrans"].ToString());
                payDTO.stcode = dt.Rows[i]["StudentCode"].ToString();
                payDTO.tterm = dt.Rows[i]["tterm"].ToString();
                payDTO.PersianDate = dt.Rows[i]["PersianDate"].ToString();
                if (dt.Rows[i]["TraceNumber"].ToString() != "")
                    payDTO.TraceNumber = long.Parse(dt.Rows[i]["TraceNumber"].ToString());
                else
                    payDTO.TraceNumber = 0;
                payDTO.AppStatus = dt.Rows[i]["paystatus"].ToString();
                
                lstPayment.Add(payDTO);
            }
            return lstPayment;
        }
        public int GetSumPayment()
        {
           return pDAO.GetSumPayment();
        }
        public void UpdatePaymentStatus(PaymentDTO PaymentDTO)
        {
            PaymentDAO payDAO = new PaymentDAO();
            payDAO.UpdateMellatPaymentStatus(PaymentDTO);
        
        }
    }
}
