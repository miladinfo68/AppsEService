using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IAUEC_Apps.DAO.CommonDAO;
using IAUEC_Apps.DTO.AdobeClasses;
using System.Data;


namespace IAUEC_Apps.Business.Adobe
{
    public class ClassBusiness
    {
        ClassDAO clsDAO = new ClassDAO();
        public List<ClassListDTO> Show_Class_List(string stcode, string term)
        {
            List<ClassListDTO> list_All_Class = clsDAO.Show_Class_List(stcode, term);
            List<ClassListDTO> list_merge_Class = clsDAO.Show_Merge_Class_List(stcode, term);
            foreach (var item in list_merge_Class)
            {
                list_All_Class.Add(item);
            }

            return list_All_Class;
        }

        public List<ClassListDTO> Show_similarClass_List(string did, string stcode)
        {
            List<ClassListDTO> list_All_SimilarClass = clsDAO.Show_SimilarClass_List(did, stcode);
            List<string> listOfDid = new List<string>();
            foreach (var item in list_All_SimilarClass)
            {
                listOfDid.Add(item.ClassCode.ToString());
            }

            List<ClassListDTO> list_merge_Class = clsDAO.Show_similar_Merge_Class_List(listOfDid);
            foreach (var item in list_merge_Class)
            {
                list_All_SimilarClass.Add(item);
            }

            return list_All_SimilarClass;
            if (list_All_SimilarClass.Count==0)
            {
                return null;

            }
        }



        public void OpenClass(string stcode)
        {
            clsDAO.OpenClass(stcode);
        }

        public DataTable Show_Class_List_ByTerm(string term, int code, string name)
        {
            return clsDAO.Show_Class_List_byTerm(term, code, name);
        }

        public void MergeClass(ClassListDTO cls)
        {
            clsDAO.MergeClass(cls);
        }

        public void EditMergeClass(ClassListDTO cls)
        {
            clsDAO.EditMergeClass(cls);
        }

        public DataTable getMergeClass(string term)
        {
            return clsDAO.getMergeClass(term);
        }
        public DataTable CheckMergeCode(string mergeCode)
        {
            return clsDAO.CheckMergeCode(mergeCode);
        }
        public DataTable getMergeCodeClasses(string term, int mergeCode)
        {
            return clsDAO.getMergeCodeClasses(term, mergeCode);
        }

        public DataTable CheckMergeCode(ClassListDTO mrgClass)
        {
            return clsDAO.CheckMergeCode(mrgClass);
        }
        
        public int DeleteFromMergeClass(string term, int codedars)
        {
           return clsDAO.DeleteFromMergeClass(term, codedars);            
        }

        public void DeleteMergeClasses(string term, int mergeCode)
        {
            clsDAO.DeleteMergeClasses(term, mergeCode);
        }

        public DataTable getProfName()
        {
            return clsDAO.getProfName();
        }
 public DataTable getProfMergeName()
        {
            return clsDAO.getProfMergeName();
        }
        public DataTable getActiveProfName()
        {
            return clsDAO.getActiveProfName();
        }
    }
}
