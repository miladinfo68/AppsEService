using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IAUEC_Apps.DAO.CommonDAO;
using IAUEC_Apps.DTO.AdobeClasses;
using System.Data;
using System.Configuration;

namespace IAUEC_Apps.Business.Adobe
{
    public class RecordsBusiness
    {
        RecordsDAO recDAO = new RecordsDAO();
        SettingBusiness setb = new SettingBusiness();

        public List<RecordsDTO> LinkOfClassWithLessonCodeByCodeClassAndTime(string code, string date, string term)
        {
            List<RecordsDTO> assetList = new List<RecordsDTO>();
            SettingDTO setdto = setb.GetSettingByTermC(term);
            DAO.Adobe.SettingDAO sdao = new DAO.Adobe.SettingDAO();

            if (term == "93-94-1")
            {

                DataTable dtvc = recDAO.LinkOfClassWithLessonCodeByCodeClassAndTime(code, date, term);

                for (int i = 0; i < dtvc.Rows.Count; i++)
                {
                    RecordsDTO asset = new RecordsDTO();
                    AdobeBusiness adobeBusiness = new AdobeBusiness();
                    asset.ClassCode = dtvc.Rows[i]["code"].ToString();
                    asset.LessonCode = dtvc.Rows[i]["lesson_code"].ToString();
                    asset.Size = dtvc.Rows[i]["storage"].ToString();
                    asset.Duration = dtvc.Rows[i]["Minute"].ToString();
                    asset.FileType = dtvc.Rows[i]["fileType"].ToString();
                    asset.FileName = dtvc.Rows[i]["name"].ToString();
                    asset.Date = dtvc.Rows[i]["Shamsi Date"].ToString();
                    asset.Session = dtvc.Rows[i]["session"].ToString();

                    // asset.Link = dt.Rows[i]["URL"].ToString();
                    asset.Link = adobeBusiness.OpenMeetingAsView("http://192.168.30.140/", setdto.vName, setdto.vpass, dtvc.Rows[i]["SCO_ID"].ToString(), dtvc.Rows[i]["_val"].ToString(), "view", sdao.Decrypt(setdto.aPass, true));


                    assetList.Add(asset);
                }

                DataTable dtAdobe = recDAO.LinkOfClassWithLessonCodeByCodeClassAndTime(code, date, term);
                for (int i = 0; i < dtAdobe.Rows.Count; i++)
                {
                    RecordsDTO asset = new RecordsDTO();
                    AdobeBusiness adobeBusiness = new AdobeBusiness();
                    asset.ClassCode = dtAdobe.Rows[i]["code"].ToString();
                    asset.LessonCode = dtAdobe.Rows[i]["lesson_code"].ToString();
                    asset.Size = dtAdobe.Rows[i]["storage"].ToString();
                    asset.Duration = dtAdobe.Rows[i]["Minute"].ToString();
                    asset.FileType = dtAdobe.Rows[i]["fileType"].ToString();
                    asset.FileName = dtAdobe.Rows[i]["name"].ToString();
                    asset.Date = dtAdobe.Rows[i]["Shamsi Date"].ToString();
                    asset.Session = dtAdobe.Rows[i]["session"].ToString();
                    // asset.Link = dt.Rows[i]["URL"].ToString();
                    asset.Link = adobeBusiness.OpenMeetingAsView("http://adobe.iauec.ac.ir/", setdto.vName, setdto.vpass, dtAdobe.Rows[i]["SCO_ID"].ToString(), dtAdobe.Rows[i]["_val"].ToString(), "view", sdao.Decrypt(setdto.aPass, true));


                    assetList.Add(asset);
                }
            }

            else
            {
                DataTable dtAdobe = recDAO.LinkOfClassWithLessonCodeByCodeClassAndTime(code, date, term);
                for (int i = 0; i < dtAdobe.Rows.Count; i++)
                {
                    RecordsDTO asset = new RecordsDTO();
                    AdobeBusiness adobeBusiness = new AdobeBusiness();
                    asset.ClassCode = dtAdobe.Rows[i]["code"].ToString();
                    asset.LessonCode = dtAdobe.Rows[i]["lesson_code"].ToString();
                    asset.Size = dtAdobe.Rows[i]["storage"].ToString();
                    asset.Duration = dtAdobe.Rows[i]["Minute"].ToString();
                    asset.FileType = dtAdobe.Rows[i]["fileType"].ToString();
                    asset.FileName = dtAdobe.Rows[i]["name"].ToString();
                    asset.Date = dtAdobe.Rows[i]["Shamsi Date"].ToString();
                    asset.Session = dtAdobe.Rows[i]["session"].ToString();
                    // asset.Link = dt.Rows[i]["URL"].ToString();
                    if (setdto.version == "9.5")
                    {
                        asset.Link = adobeBusiness.OpenMeetingAsView(setdto.DomainName, setdto.vName, setdto.vpass, dtAdobe.Rows[i]["SCO_ID"].ToString(), dtAdobe.Rows[i]["_val"].ToString(), "", sdao.Decrypt(setdto.aPass, true));
                    }
                    else
                    {
                        asset.Link = adobeBusiness.OpenMeetingAsView(setdto.DomainName, setdto.vName, setdto.vpass, dtAdobe.Rows[i]["SCO_ID"].ToString(), dtAdobe.Rows[i]["_val"].ToString(), "view", sdao.Decrypt(setdto.aPass, true));

                    }


                    assetList.Add(asset);
                }
            }
            return assetList;

        }

        public List<RecordsDTO> MakeOffLineClassWithLessonCodeByCodeClassAndTime(string code, string date, string term)
        {

            SettingDTO setdto = setb.GetSettingByTermC(term);
            DAO.Adobe.SettingDAO sdao = new DAO.Adobe.SettingDAO();
            List<RecordsDTO> assetList = new List<RecordsDTO>();
            if (term == "93-94-1")
            {
                DataTable dt_VC = recDAO.LinkOfClassWithLessonCodeByCodeClassAndTime(code, date, term);


                for (int i = 0; i < dt_VC.Rows.Count; i++)
                {
                    RecordsDTO asset = new RecordsDTO();
                    AdobeBusiness adobe = new AdobeBusiness();
                    asset.ClassCode = dt_VC.Rows[i]["code"].ToString();
                    asset.LessonCode = dt_VC.Rows[i]["lesson_code"].ToString();
                    asset.Size = dt_VC.Rows[i]["storage"].ToString();
                    asset.Duration = dt_VC.Rows[i]["Minute"].ToString();
                    asset.FileType = dt_VC.Rows[i]["fileType"].ToString();
                    asset.FileName = dt_VC.Rows[i]["name"].ToString();
                    asset.Date = dt_VC.Rows[i]["Shamsi Date"].ToString();
                    // asset.Session = dt_VC.Rows[i]["session"].ToString();
                    // asset.Link = dt.Rows[i]["URL"].ToString();
                    asset.Link = adobe.OpenMeetingAsHost("http://192.168.30.140//", setdto.hName, setdto.hpass, dt_VC.Rows[i]["SCO_ID"].ToString(), dt_VC.Rows[i]["_val"].ToString(), "offline", sdao.Decrypt(setdto.aPass, true));


                    assetList.Add(asset);
                }


                //------------------------Adobe
                DataTable dt_Adobe = recDAO.LinkOfClassWithLessonCodeByCodeClassAndTime(code, date, term);

                for (int i = 0; i < dt_Adobe.Rows.Count; i++)
                {
                    RecordsDTO asset = new RecordsDTO();
                    AdobeBusiness adobe = new AdobeBusiness();
                    asset.ClassCode = dt_Adobe.Rows[i]["code"].ToString();
                    asset.LessonCode = dt_Adobe.Rows[i]["lesson_code"].ToString();
                    asset.Size = dt_Adobe.Rows[i]["storage"].ToString();
                    asset.Duration = dt_Adobe.Rows[i]["Minute"].ToString();
                    asset.FileType = dt_Adobe.Rows[i]["fileType"].ToString();
                    asset.FileName = dt_Adobe.Rows[i]["name"].ToString();
                    asset.Date = dt_Adobe.Rows[i]["Shamsi Date"].ToString();
                    //  asset.Session = dt_Adobe.Rows[i]["session"].ToString();
                    // asset.Link = dt.Rows[i]["URL"].ToString();
                    asset.Link = adobe.OpenMeetingAsHost("http://adobe.iauec.ac.ir/", setdto.hName, setdto.hpass, dt_Adobe.Rows[i]["SCO_ID"].ToString(), dt_Adobe.Rows[i]["_val"].ToString(), "offline", sdao.Decrypt(setdto.aPass, true));


                    assetList.Add(asset);
                }
            }

            else
            {



                DataTable dt_live = recDAO.LinkOfClassWithLessonCodeByCodeClassAndTime(code, date, term);

                for (int i = 0; i < dt_live.Rows.Count; i++)
                {
                    RecordsDTO asset = new RecordsDTO();
                    AdobeBusiness adobe = new AdobeBusiness();
                    asset.ClassCode = dt_live.Rows[i]["code"].ToString();
                    asset.LessonCode = dt_live.Rows[i]["lesson_code"].ToString();
                    asset.Size = dt_live.Rows[i]["storage"].ToString();
                    asset.Duration = dt_live.Rows[i]["Minute"].ToString();
                    asset.FileType = dt_live.Rows[i]["fileType"].ToString();
                    asset.FileName = dt_live.Rows[i]["name"].ToString();
                    asset.Date = dt_live.Rows[i]["Shamsi Date"].ToString();
                    //asset.Session = dt_live.Rows[i]["session"].ToString();
                    // asset.Link = dt.Rows[i]["URL"].ToString();
                    asset.Link = adobe.OpenMeetingAsHost(setdto.DomainName, setdto.hName, setdto.hpass, dt_live.Rows[i]["SCO_ID"].ToString(), dt_live.Rows[i]["_val"].ToString(), "offline", sdao.Decrypt(setdto.aPass, true));


                    assetList.Add(asset);
                }
            }
            return assetList;

        }


        public List<RecordsDTO> EditLinkOfClassByCodeClassAndDay(string code, string Date, int time, string term)
        {
            SettingDTO setdto = setb.GetSettingByTermC(term);
            List<RecordsDTO> assetList = new List<RecordsDTO>();
            DAO.Adobe.SettingDAO sdao = new DAO.Adobe.SettingDAO();
            if (term == "93-94-1")
            {
                DataTable dt_VC = recDAO.EditLinkOfClassByClassCodeAndDateAndTime(code, Date, time, term);


                for (int i = 0; i < dt_VC.Rows.Count; i++)
                {
                    RecordsDTO asset = new RecordsDTO();
                    AdobeBusiness adobe = new AdobeBusiness();
                    asset.ClassCode = dt_VC.Rows[i]["code"].ToString();
                    asset.LessonCode = dt_VC.Rows[i]["lesson_code"].ToString();
                    asset.Size = dt_VC.Rows[i]["storage"].ToString();
                    asset.Duration = dt_VC.Rows[i]["Minute"].ToString();
                    asset.FileType = dt_VC.Rows[i]["fileType"].ToString();
                    asset.FileName = dt_VC.Rows[i]["name"].ToString();
                    asset.Date = dt_VC.Rows[i]["Shamsi Date"].ToString();
                    // asset.Session = dt_VC.Rows[i]["session"].ToString();
                    // asset.Link = dt.Rows[i]["URL"].ToString();
                    asset.Link = adobe.OpenMeetingAsHost("http://192.168.30.140/", setdto.hName, setdto.hpass, dt_VC.Rows[i]["SCO_ID"].ToString(), dt_VC.Rows[i]["_val"].ToString(), "edit", sdao.Decrypt(setdto.aPass, true));


                    assetList.Add(asset);
                }


                //------------------------Adobe
                DataTable dt_Adobe = recDAO.EditLinkOfClassByClassCodeAndDateAndTime(code, Date, time, term);

                for (int i = 0; i < dt_Adobe.Rows.Count; i++)
                {
                    RecordsDTO asset = new RecordsDTO();
                    AdobeBusiness adobe = new AdobeBusiness();
                    asset.ClassCode = dt_Adobe.Rows[i]["code"].ToString();
                    asset.LessonCode = dt_Adobe.Rows[i]["lesson_code"].ToString();
                    asset.Size = dt_Adobe.Rows[i]["storage"].ToString();
                    asset.Duration = dt_Adobe.Rows[i]["Minute"].ToString();
                    asset.FileType = dt_Adobe.Rows[i]["fileType"].ToString();
                    asset.FileName = dt_Adobe.Rows[i]["name"].ToString();
                    asset.Date = dt_Adobe.Rows[i]["Shamsi Date"].ToString();
                    //  asset.Session = dt_Adobe.Rows[i]["session"].ToString();
                    // asset.Link = dt.Rows[i]["URL"].ToString();
                    asset.Link = adobe.OpenMeetingAsHost("http://adobe.iauec.ac.ir/", setdto.hName, setdto.hpass, dt_Adobe.Rows[i]["SCO_ID"].ToString(), dt_Adobe.Rows[i]["_val"].ToString(), "edit", sdao.Decrypt(setdto.aPass, true));


                    assetList.Add(asset);
                }
            }

            else
            {



                DataTable dt_live = recDAO.EditLinkOfClassByClassCodeAndDateAndTime(code, Date, time, term);

                for (int i = 0; i < dt_live.Rows.Count; i++)
                {
                    RecordsDTO asset = new RecordsDTO();
                    AdobeBusiness adobe = new AdobeBusiness();
                    asset.ClassCode = dt_live.Rows[i]["code"].ToString();
                    asset.LessonCode = dt_live.Rows[i]["lesson_code"].ToString();
                    asset.Size = dt_live.Rows[i]["storage"].ToString();
                    asset.Duration = dt_live.Rows[i]["Minute"].ToString();
                    asset.FileType = dt_live.Rows[i]["fileType"].ToString();
                    asset.FileName = dt_live.Rows[i]["name"].ToString();
                    asset.Date = dt_live.Rows[i]["Shamsi Date"].ToString();
                    //asset.Session = dt_live.Rows[i]["session"].ToString();
                    // asset.Link = dt.Rows[i]["URL"].ToString();
                    asset.Link = adobe.OpenMeetingAsHost(setdto.DomainName, setdto.hName, setdto.hpass, dt_live.Rows[i]["SCO_ID"].ToString(), dt_live.Rows[i]["_val"].ToString(), "edit", sdao.Decrypt(setdto.aPass, true));


                    assetList.Add(asset);
                }
            }
            return assetList;
        }

        public List<RecordsDTO> LinkOfMeetingsAndContentsByCodeClassAndTime(string code, string date, string term)
        {
            SettingDTO setdto = setb.GetSettingByTermC(term);
            DAO.Adobe.SettingDAO sdao = new DAO.Adobe.SettingDAO();

            List<RecordsDTO> assetList = new List<RecordsDTO>();
            if (term == "93-94-1")
            {

                DataTable dtvc = recDAO.LinkOfMeetingsAndContentsByCodeClassAndTime(code, date, term);

                for (int i = 0; i < dtvc.Rows.Count; i++)
                {
                    RecordsDTO asset = new RecordsDTO();
                    AdobeBusiness adobeBusiness = new AdobeBusiness();
                    asset.ClassCode = dtvc.Rows[i]["code"].ToString();
                    asset.LessonCode = dtvc.Rows[i]["lesson_code"].ToString();
                    asset.Size = dtvc.Rows[i]["storage"].ToString();
                    asset.Duration = dtvc.Rows[i]["Minute"].ToString();
                    asset.FileType = dtvc.Rows[i]["fileType"].ToString();
                    asset.FileName = dtvc.Rows[i]["name"].ToString();
                    asset.Date = dtvc.Rows[i]["Shamsi Date"].ToString();
                    // asset.Session = dtvc.Rows[i]["session"].ToString();
                    asset.Link = adobeBusiness.OpenMeetingAsView("http://192.168.30.140/", setdto.vName, setdto.vpass, dtvc.Rows[i]["SCO_ID"].ToString(), dtvc.Rows[i]["_val"].ToString(), "view", sdao.Decrypt(setdto.aPass, true));




                    assetList.Add(asset);
                }

                DataTable dtAdobe = recDAO.LinkOfMeetingsAndContentsByCodeClassAndTime(code, date, term);
                for (int i = 0; i < dtAdobe.Rows.Count; i++)
                {
                    RecordsDTO asset = new RecordsDTO();
                    AdobeBusiness adobeBusiness = new AdobeBusiness();
                    asset.ClassCode = dtAdobe.Rows[i]["code"].ToString();
                    asset.LessonCode = dtAdobe.Rows[i]["lesson_code"].ToString();
                    asset.Size = dtAdobe.Rows[i]["storage"].ToString();
                    asset.Duration = dtAdobe.Rows[i]["Minute"].ToString();
                    asset.FileType = dtAdobe.Rows[i]["fileType"].ToString();
                    asset.FileName = dtAdobe.Rows[i]["name"].ToString();
                    asset.Date = dtAdobe.Rows[i]["Shamsi Date"].ToString();
                    // asset.Session = dtAdobe.Rows[i]["session"].ToString();
                    // asset.Link = dt.Rows[i]["URL"].ToString();

                    asset.Link = adobeBusiness.OpenMeetingAsView("http://adobe.iauec.ac.ir/", setdto.vName, setdto.vpass, dtAdobe.Rows[i]["SCO_ID"].ToString(), dtAdobe.Rows[i]["_val"].ToString(), "view", sdao.Decrypt(setdto.aPass, true));


                    assetList.Add(asset);
                }
            }

            else
            {
                DataTable dt_live = recDAO.LinkOfMeetingsAndContentsByCodeClassAndTime(code, date, term);

                for (int i = 0; i < dt_live.Rows.Count; i++)
                {
                    RecordsDTO asset = new RecordsDTO();
                    AdobeBusiness adobe = new AdobeBusiness();
                    asset.ClassCode = dt_live.Rows[i]["code"].ToString();
                    asset.LessonCode = dt_live.Rows[i]["lesson_code"].ToString();
                    asset.Size = dt_live.Rows[i]["storage"].ToString();
                    asset.Duration = dt_live.Rows[i]["Minute"].ToString();
                    asset.FileType = dt_live.Rows[i]["fileType"].ToString();
                    asset.FileName = dt_live.Rows[i]["name"].ToString();
                    asset.Date = dt_live.Rows[i]["Shamsi Date"].ToString();
                    asset.Link = adobe.OpenMeetingAsHost(setdto.DomainName, setdto.vName, setdto.vpass, dt_live.Rows[i]["SCO_ID"].ToString(), dt_live.Rows[i]["_val"].ToString(), setdto.version == "9.5" ? "" : "view", sdao.Decrypt(setdto.aPass, true));



                    assetList.Add(asset);
                }
            }
            return assetList;

        }
    }
}
