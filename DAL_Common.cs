using BusinessLayer.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BusinessLayer.DAL
{
    public class DAL_Common
    {

        //public List<Shift_Mst> SearchEmployee_Autoextender(string EmployeeName, string Employee_Code, string Comp_Code, string Branch_Code, string Department_Code)
        //{
        //    SqlParameter[] param = { 
        //                               new SqlParameter("@EmployeeName", EmployeeName), 
        //                               new SqlParameter("@Employee_Code", Employee_Code), 
        //                               new SqlParameter("@Comp_Code", Comp_Code), 
        //                               new SqlParameter("@Branch_Code", Branch_Code),
        //                               new SqlParameter("@Department_Code", Department_Code), 
        //                               new SqlParameter("@sessionEmpCode", HttpContext.Current.Session["LogIn_Code"].ToString());
        //                           };


        //    DataTable dt = new DataAccess(sqlConnection.GetConnectionString()).GetDataTable("usp_SearchEmployee_Autoextender", CommandType.StoredProcedure, param);
        //    List<Shift_Mst> list = new List<Shift_Mst>();

        //    if (dt.Rows.Count > 0)
        //    {
        //        foreach (DataRow row in dt.Rows)
        //        {
        //            list.Add(new Shift_Mst
        //            {
        //                Shift_Code = Convert.ToString(row["Shift_Code"] == DBNull.Value ? 0 : row["Shift_Code"]),
        //                Branch_Code = Convert.ToString(row["Branch_Code"] == DBNull.Value ? "" : row["Branch_Code"]),
        //                Branch_Name = Convert.ToString(row["Branch_Name"] == DBNull.Value ? "" : row["Branch_Name"]),
        //                GraceInTime = Convert.ToString(row["GraceInTime"] == DBNull.Value ? "" : row["GraceInTime"]),
        //                Shift = Convert.ToString(row["Shift"] == DBNull.Value ? "" : row["Shift"]),
        //                InTime = Convert.ToString(row["InTime"] == DBNull.Value ? "" : row["InTime"]),
        //                OutTime = Convert.ToString(row["OutTime"] == DBNull.Value ? "" : row["OutTime"]),
        //                HalfDayIn = Convert.ToString(row["HalfDayIn"] == DBNull.Value ? "" : row["HalfDayIn"]),
        //                HalfDayOut = Convert.ToString(row["HalfDayOut"] == DBNull.Value ? 0 : row["HalfDayOut"]),
        //                ShiftDetails = Convert.ToString(row["ShiftDetails"] == DBNull.Value ? "" : row["ShiftDetails"]),
        //                WeeklyOffDay = Convert.ToString(row["WeeklyOffDay"] == DBNull.Value ? "" : row["WeeklyOffDay"]),

        //            });
        //        }
        //    }
        //    return list;
        //}

        public List<PLANT_RUN_REASON_MST> GetPlantRunReasonList()
        {
            DataTable dt = new DataAccess(sqlConnection.GetConnectionString_SGX()).GetDataTable("[AGG].[USP_SELECT_PLANT_RUN_REASON_MST]", CommandType.StoredProcedure);
            List<PLANT_RUN_REASON_MST> list = new List<PLANT_RUN_REASON_MST>();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    list.Add(new PLANT_RUN_REASON_MST
                    {
                        ID = Convert.ToInt32(row["ID"] == DBNull.Value ? 0 : row["ID"]),
                        REASON_NAME = Convert.ToString(row["REASON_NAME"] == DBNull.Value ? "" : row["REASON_NAME"]),
                        REASON_CODE = Convert.ToString(row["REASON_CODE"] == DBNull.Value ? "" : row["REASON_CODE"]),
                    });
                }
            }
            return list;
        }


        public List<PRODUCT_SIZE_MST> GetProductSizeList(string productCode, int ProductType_Code)
        {

            SqlParameter[] param = { new SqlParameter("@productCode", productCode), new SqlParameter("@ProductType_Code", ProductType_Code) };
            DataTable dt = new DataAccess(sqlConnection.GetConnectionString_SGX()).GetDataTable("[AGG].[usp_SelectProductSize]", CommandType.StoredProcedure, param);
            List<PRODUCT_SIZE_MST> list = new List<PRODUCT_SIZE_MST>();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    list.Add(new PRODUCT_SIZE_MST
                    {
                        ProductSize_Code = Convert.ToInt32(row["ProductSize_Code"] == DBNull.Value ? 0 : row["ProductSize_Code"]),
                        productCode = Convert.ToString(row["ProductCode"] == DBNull.Value ? "" : row["ProductCode"]),
                        ProductType_Code = Convert.ToInt32(row["ProductType_Code"] == DBNull.Value ? 0 : row["ProductType_Code"]),

                        Size = Convert.ToString(row["Size"] == DBNull.Value ? "" : row["Size"]),
                        HSNCode = Convert.ToString(row["HSNCode"] == DBNull.Value ? "" : row["HSNCode"]),
                        UOM = Convert.ToString(row["UOM"] == DBNull.Value ? "" : row["UOM"]),
                    });
                }


            }
            return list;
        }

        public SelectDate GetSelect_Date(string Comp_Code, string Branch_Code, string ForCode)
        {

            SqlParameter[] param = {
                                       new SqlParameter("@COMP_CODE", Comp_Code),
                                       new SqlParameter("@BRANCH_CODE", Branch_Code),
                                       new SqlParameter("@FOR_CODE", ForCode)
                                   };
            DataTable dt = new DataAccess(sqlConnection.GetConnectionString_SGX()).GetDataTable("[AGG].[USP_SELECT_DATE]", CommandType.StoredProcedure, param);
            SelectDate _selectDate = new SelectDate();
            if (dt.Rows.Count > 0)
            {
                _selectDate.SELECT_DATE = Convert.ToString(dt.Rows[0]["SELECT_DATE"]);
                _selectDate.ID = Convert.ToDecimal(dt.Rows[0]["ID"] == DBNull.Value ? "0" : dt.Rows[0]["ID"]);
                _selectDate.SHIFT_TIME = Convert.ToString(dt.Rows[0]["SHIFT_TIME"] == DBNull.Value ? "" : dt.Rows[0]["SHIFT_TIME"]);
                _selectDate.IS_LOCKED = Convert.ToInt32(dt.Rows[0]["IS_LOCKED"] == DBNull.Value ? "0" : dt.Rows[0]["IS_LOCKED"]);
                _selectDate.LAST_DATE = Convert.ToString(dt.Rows[0]["LAST_DATE"]);
            }
            return _selectDate;
        }


        public AUTOMAIL_RECEIPENT GetAutoMail_Dtls(string Automail_Code)
        {

            SqlParameter[] param = {
                                       new SqlParameter("@Automail_Code", Automail_Code) 
                                   };
            DataTable dt = new DataAccess(sqlConnection.GetConnectionString_SGX()).GetDataTable("usp_Tbl_AutoMail", CommandType.StoredProcedure, param);
            AUTOMAIL_RECEIPENT _autoMail = new AUTOMAIL_RECEIPENT();
            if (dt.Rows.Count > 0)
            {
                _autoMail.Mail_Id = Convert.ToInt32(dt.Rows[0]["Mail_Id"]);
                _autoMail.Branch_Code = Convert.ToString(dt.Rows[0]["Branch_Code"] == DBNull.Value ? "" : dt.Rows[0]["Branch_Code"]);
                _autoMail.Mail_Code = Convert.ToString(dt.Rows[0]["Mail_Code"] == DBNull.Value ? "" : dt.Rows[0]["Mail_Code"]);
                _autoMail.Mail_To = Convert.ToString(dt.Rows[0]["Mail_To"] == DBNull.Value ? "" : dt.Rows[0]["Mail_To"]);
                _autoMail.Mail_CC = Convert.ToString(dt.Rows[0]["Mail_CC"] == DBNull.Value ? "" : dt.Rows[0]["Mail_CC"]);
                _autoMail.Mail_Bcc = Convert.ToString(dt.Rows[0]["Mail_Bcc"] == DBNull.Value ? "" : dt.Rows[0]["Mail_Bcc"]);
            }
            return _autoMail;
        }


        public List<EMPLOYEE_BRACH_ACCESS> GetEmployeeBrachAccess_List(string Employee_Code, string Comp_Code)
        {

            SqlParameter[] param = { new SqlParameter("@Employee_Code", Employee_Code), new SqlParameter("@Comp_Code", Comp_Code) };
            DataTable dt = new DataAccess(sqlConnection.GetConnectionString()).GetDataTable("usp_SelectEmployeeBrachAccessByCompany", CommandType.StoredProcedure, param);
            List<EMPLOYEE_BRACH_ACCESS> list = new List<EMPLOYEE_BRACH_ACCESS>();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    list.Add(new EMPLOYEE_BRACH_ACCESS
                    {
                        Employee_Code = Convert.ToString(row["Employee_Code"] == DBNull.Value ? "" : row["Employee_Code"]),
                        Branch_Code = Convert.ToString(row["Branch_Code"] == DBNull.Value ? "" : row["Branch_Code"]),
                        Branch_Name = Convert.ToString(row["Branch_Name"] == DBNull.Value ? "" : row["Branch_Name"]),
                        State_Code = Convert.ToString(row["State_Code"] == DBNull.Value ? "" : row["State_Code"]),
                        Region_Code = Convert.ToString(row["Region_Code"] == DBNull.Value ? "" : row["Region_Code"]),

                    });
                }

            }
            return list;
        }


        public List<EMPLOYEE_DETAILS> GetEmployee_List(string EmployeeName, string Employee_Code, string Comp_Code, string Branch_Code, string Department_Code, string LogIn_Code, string TransferType)
        {

            SqlParameter[] param = { 
                                       new SqlParameter("@EmployeeName", EmployeeName), 
                                       new SqlParameter("@Employee_Code", Employee_Code),
                                       new SqlParameter("@Comp_Code", Comp_Code) ,
                                       new SqlParameter("@Branch_Code", Branch_Code),
                                       new SqlParameter("@Department_Code", Department_Code),
                                       new SqlParameter("@sessionEmpCode", LogIn_Code),
                                       new SqlParameter("@TransferType", TransferType)
                                   };
            DataTable dt = new DataAccess(sqlConnection.GetConnectionString()).GetDataTable("usp_SearchEmployee_Autoextender", CommandType.StoredProcedure, param);
            List<EMPLOYEE_DETAILS> list = new List<EMPLOYEE_DETAILS>();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    list.Add(new EMPLOYEE_DETAILS
                    {
                        EmployeeName = Convert.ToString(row["EmployeeName"] == DBNull.Value ? "" : row["EmployeeName"]),
                        Employee_Code = Convert.ToString(row["Employee_Code"] == DBNull.Value ? "" : row["Employee_Code"]),
                        Comp_Code = Convert.ToString(row["Comp_Code"] == DBNull.Value ? "" : row["Comp_Code"]),
                        Comp_Name = Convert.ToString(row["Comp_Name"] == DBNull.Value ? "" : row["Comp_Name"]),
                        Branch_Code = Convert.ToString(row["Branch_Code"] == DBNull.Value ? "" : row["Branch_Code"]),
                        Branch_Name = Convert.ToString(row["Branch_Name"] == DBNull.Value ? "" : row["Branch_Name"]),
                        Department_Code = Convert.ToString(row["Department_Code"] == DBNull.Value ? "" : row["Department_Code"]),
                        Long_Title = Convert.ToString(row["Long_Title"] == DBNull.Value ? "" : row["Long_Title"]),
                        Designation_Code = Convert.ToString(row["Designation_Code"] == DBNull.Value ? "" : row["Designation_Code"]),
                        FunRpt_Employee_Code = Convert.ToString(row["FunRpt_Employee_Code"] == DBNull.Value ? "" : row["FunRpt_Employee_Code"]),
                        AdmRpt_Employee_Code = Convert.ToString(row["AdmRpt_Employee_Code"] == DBNull.Value ? "" : row["AdmRpt_Employee_Code"]),
                        activeFlag = Convert.ToString(row["activeFlag"] == DBNull.Value ? "" : row["activeFlag"])
                    });
                }

            }
            return list;
        }

        public List<CustomerMaster> GetCustomerMaster_List(string custCode, string custName)
        {

            SqlParameter[] param = { new SqlParameter("@custCode", custCode), new SqlParameter("@custName", custName) };
            DataTable dt = new DataAccess(sqlConnection.GetConnectionString_SGX()).GetDataTable("usp_selectSearchCustomerMaster", CommandType.StoredProcedure, param);
            List<CustomerMaster> list = new List<CustomerMaster>();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    list.Add(new CustomerMaster
                    {
                        custCode = Convert.ToString(row["custCode"] == DBNull.Value ? "" : row["custCode"]),
                        custName = Convert.ToString(row["custName"] == DBNull.Value ? "" : row["custName"])

                    });
                }

            }
            return list;
        }


        public List<GradeMaster> GetGradeList()
        {
            DataTable dt = new DataAccess(sqlConnection.GetConnectionString_SGX()).GetDataTable("[AGG].[USP_SELECT_GRADE_MST]", CommandType.StoredProcedure);
            List<GradeMaster> list = new List<GradeMaster>();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    list.Add(new GradeMaster
                    {
                        Grade_Id = Convert.ToInt32(row["Grade_Id"] == DBNull.Value ? 0 : row["Grade_Id"]),
                        Grade_Name = Convert.ToString(row["Grade_Name"] == DBNull.Value ? "" : row["Grade_Name"])
                    });
                }
            }
            return list;
        }


        public List<MaterialMaster> GetMaterialList()
        {
            DataTable dt = new DataAccess(sqlConnection.GetConnectionString_SGX()).GetDataTable("[AGG].[USP_SELECT_MATERIAL_MST]", CommandType.StoredProcedure);
            List<MaterialMaster> list = new List<MaterialMaster>();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    list.Add(new MaterialMaster
                    {
                        Material_Id = Convert.ToInt32(row["Material_Id"] == DBNull.Value ? 0 : row["Material_Id"]),
                        Material_Name = Convert.ToString(row["Material_Name"] == DBNull.Value ? "" : row["Material_Name"])
                    });
                }
            }
            return list;
        }



        public List<Product_Master> GetProductnameList()
        {
            DataTable dt = new DataAccess(sqlConnection.GetConnectionString_SGX()).GetDataTable("[dbo].[Usp_Get_Product_Master]", CommandType.StoredProcedure);
            List<Product_Master> list = new List<Product_Master>();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    list.Add(new Product_Master
                    {


                        productName = Convert.ToString(row["productName"] == DBNull.Value ? "" : row["productName"]),
                        productDesc = Convert.ToString(row["productDesc"] == DBNull.Value ? "" : row["productDesc"]),
                        productCode = Convert.ToString(row["productCode"] == DBNull.Value ? "" : row["productCode"])
                    });
                }


            }
            return list;
        }


        public List<PRODUCT_SIZE_MST> GetProductSize(int ProductType_Code)
        {

            SqlParameter[] param = { new SqlParameter("@ProductType_Code", ProductType_Code) };
            DataTable dt = new DataAccess(sqlConnection.GetConnectionString_SGX()).GetDataTable("[usp_Product_SizebyType]", CommandType.StoredProcedure, param);
            List<PRODUCT_SIZE_MST> list = new List<PRODUCT_SIZE_MST>();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    list.Add(new PRODUCT_SIZE_MST
                    {
                        ProductSize_Code = Convert.ToInt32(row["ProductSize_Code"] == DBNull.Value ? 0 : row["ProductSize_Code"]),
                        Size = Convert.ToString(row["Size"] == DBNull.Value ? "" : row["Size"]),

                    });
                }


            }
            return list;
        }

        public List<ProductGrade_Master> GetGrade(string productCode)
        {
            SqlParameter[] param = { new SqlParameter("@productCode", productCode) };
            DataTable dt = new DataAccess(sqlConnection.GetConnectionString_SGX()).GetDataTable("[dbo].[usp_SelectProductGrade]", CommandType.StoredProcedure, param);
            List<ProductGrade_Master> list = new List<ProductGrade_Master>();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    list.Add(new ProductGrade_Master
                    {
                        ProductGrade_Code = Convert.ToInt32(row["ProductGrade_Code"] == DBNull.Value ? 0 : row["ProductGrade_Code"]),
                        Grade_Name = Convert.ToString(row["Grade_Name"] == DBNull.Value ? "" : row["Grade_Name"])
                    });
                }
            }
            return list;
        }

        public List<ProductType_Master> GetType(string productCode)
        {
            SqlParameter[] param = { new SqlParameter("@productCode", productCode) };
            DataTable dt = new DataAccess(sqlConnection.GetConnectionString_SGX()).GetDataTable("[dbo].[usp_SelectProductType]", CommandType.StoredProcedure, param);
            List<ProductType_Master> list = new List<ProductType_Master>();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    list.Add(new ProductType_Master
                    {
                        ProductType_Code = Convert.ToInt32(row["ProductType_Code"] == DBNull.Value ? 0 : row["ProductType_Code"]),
                        Short_Name = Convert.ToString(row["Short_Name"] == DBNull.Value ? "" : row["Short_Name"]),
                        Long_Name = Convert.ToString(row["Long_Name"] == DBNull.Value ? "" : row["Long_Name"])

                    });
                }
            }
            return list;
        }


      
        public List<Packaging_Master> GetPackageList()
        {
            DataTable dt = new DataAccess(sqlConnection.GetConnectionString_SGX()).GetDataTable("[dbo].[USP_EX_EXPORTLEAD_PACKAGING]", CommandType.StoredProcedure);
            List<Packaging_Master> list = new List<Packaging_Master>();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    list.Add(new Packaging_Master
                    {


                        package_id = Convert.ToInt32(row["PACKAGE_ID"] == DBNull.Value ? 0 : row["PACKAGE_ID"]),
                        package_type_name = Convert.ToString(row["PACKAGE_TYPE_NAME"] == DBNull.Value ? "" : row["PACKAGE_TYPE_NAME"]),
                       
                    });
                }


            }
            return list;
        }

        public List<VM_Term_Condition> GetTermCondition()
        {
           
            DataTable dt = new DataAccess(sqlConnection.GetConnectionString_SGX()).GetDataTable("[AGG].[USP_SELECT_QUOTATION_TERMS]", CommandType.StoredProcedure);
            List<VM_Term_Condition> list = new List<VM_Term_Condition>();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                    list.Add(new VM_Term_Condition
                        {

                            TC_ID = Convert.ToInt32(row["TC_ID"] == DBNull.Value ? 0 : row["TC_ID"]),
                            TERM_CONDITION = Convert.ToString(row["TERM_CONDITION"] == DBNull.Value ? "" : row["TERM_CONDITION"]),

                        });
            }
            return list;
        }



        public List<BrandName_Master> GeBrandNameMaster()
        {
            DataTable dt = new DataAccess(sqlConnection.GetConnectionString_SGX()).GetDataTable("[AGG].[USP_SELECT_BRAND_MST]", CommandType.StoredProcedure);
            List<BrandName_Master> list = new List<BrandName_Master>();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    list.Add(new BrandName_Master
                    {
                        BRAND_ID = Convert.ToInt32(row["BRAND_ID"] == DBNull.Value ? 0 :  row["BRAND_ID"]),
                        BRAND_NAME = Convert.ToString(row["BRAND_NAME"] == DBNull.Value ? "" : row["BRAND_NAME"])
                    });
                }
            }
            return list;
        }


        public List<ADDRESS_TYPE_MST> GetAddressTypeList()
        {
            DataTable dt = new DataAccess(sqlConnection.GetConnectionString_SGX()).GetDataTable("usp_Select_AddressType_Mst", CommandType.StoredProcedure);
            List<ADDRESS_TYPE_MST> list = new List<ADDRESS_TYPE_MST>();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    list.Add(new ADDRESS_TYPE_MST
                    {
                        AddressType_Code = Convert.ToString(row["AddressType_Code"] == DBNull.Value ? "" : row["AddressType_Code"]),
                        AddressType_Name = Convert.ToString(row["Short_Title"] == DBNull.Value ? "" : row["Short_Title"])
                    });
                }
            }
            return list;
        }


        public List<VM_DROPDOWN_LIST> GetStateList(string city_code)
        {
            SqlParameter[] param = { new SqlParameter("@city_code", city_code) };
            DataTable dt = new DataAccess(sqlConnection.GetConnectionString_SGX()).GetDataTable("usp_selectState", CommandType.StoredProcedure, param);

            
            List<VM_DROPDOWN_LIST> list = new List<VM_DROPDOWN_LIST>();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    list.Add(new VM_DROPDOWN_LIST
                    {
                        ValueField = Convert.ToString(row["State_ID"] == DBNull.Value ? "" : row["State_ID"]),
                        TextField = Convert.ToString(row["State_Name"] == DBNull.Value ? "" : row["State_Name"])
                    });
                }
            }
            return list;
        }

        public List<VM_DROPDOWN_LIST> GetCityList(string STATE_ID)
        {
            SqlParameter[] param = { new SqlParameter("@STATE_ID", STATE_ID) };
            DataTable dt = new DataAccess(sqlConnection.GetConnectionString_SGX()).GetDataTable("usp_Select_City_Mst", CommandType.StoredProcedure, param);


            List<VM_DROPDOWN_LIST> list = new List<VM_DROPDOWN_LIST>();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    list.Add(new VM_DROPDOWN_LIST
                    {
                        ValueField = Convert.ToString(row["city_code"] == DBNull.Value ? "" : row["city_code"]),
                        TextField = Convert.ToString(row["city_Name"] == DBNull.Value ? "" : row["city_Name"])
                    });
                }
            }
            return list;
        }


        public List<Component_Master> GetComponentList()
        { 
            
            DataTable dt = new DataAccess(sqlConnection.GetConnectionString_SGX()).GetDataTable("[AGG].[USP_SELECT_COMPONENT_MST]", CommandType.StoredProcedure);
            List<Component_Master> list = new List<Component_Master>();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    list.Add(new Component_Master
                    {
                        Com_ID = Convert.ToInt32(row["Com_ID"] == DBNull.Value ? 0 : row["Com_ID"]),
                        Com_Name = Convert.ToString(row["Com_Name"] == DBNull.Value ? "" : row["Com_Name"]),
                       
                    });
                }


            }
            return list;
        }


        public List<CostLocation_Mater> GetLocationList(string @empCode)
        {
            SqlParameter[] param = { new SqlParameter("@empCode", empCode) };
            DataTable dt = new DataAccess(sqlConnection.GetConnectionString_SGX()).GetDataTable("usp_Fill_Location", CommandType.StoredProcedure, param);


            List<CostLocation_Mater> list = new List<CostLocation_Mater>();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    list.Add(new CostLocation_Mater
                    {
                        locationCode = Convert.ToString(row["locationCode"] == DBNull.Value ? "" : row["locationCode"]),
                        locationName = Convert.ToString(row["locationName"] == DBNull.Value ? "" : row["locationName"])
                    });
                }
            }
            return list;
        }

        public List<CostProduct_Master> GetProductList(string LocationCode)
        {
            SqlParameter[] param = { new SqlParameter("@LocationCode", LocationCode) };
            DataTable dt = new DataAccess(sqlConnection.GetConnectionString_SGX()).GetDataTable("usp_SelectProductByLocation", CommandType.StoredProcedure, param);


            List<CostProduct_Master> list = new List<CostProduct_Master>();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    list.Add(new CostProduct_Master
                    {
                        ProductCode = Convert.ToString(row["ProductCode"] == DBNull.Value ? "" : row["ProductCode"]),
                        productName = Convert.ToString(row["productName"] == DBNull.Value ? "" : row["productName"]),
                        ProductDesc = Convert.ToString(row["ProductDesc"] == DBNull.Value ? "" : row["ProductDesc"])
                    });
                }
            }
            return list;
        }
        public List<CostPlantMine_Master> GetPlantMineList(string loc)
        {
            SqlParameter[] param = { new SqlParameter("@loc", loc) };
            DataTable dt = new DataAccess(sqlConnection.GetConnectionString_SGX()).GetDataTable("usp_fill_PlantMine", CommandType.StoredProcedure, param);


            List<CostPlantMine_Master> list = new List<CostPlantMine_Master>();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    list.Add(new CostPlantMine_Master
                    {
                        ProductMine_code = Convert.ToInt32(row["ProductMine_code"] == DBNull.Value ? "" : row["ProductMine_code"]),
                        Long_Name = Convert.ToString(row["Long_Name"] == DBNull.Value ? "" : row["Long_Name"]),
                       
                    });
                }
            }
            return list;
        }
    }

}
  