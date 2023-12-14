using BusinessLayer.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.DAL
{
    public class DAL_STANDARD_COST_PROD
    {
        public List<AggCost_Production_Dtl> GET_Standard_CostProd_DTLS(string Comp_Code, string Branch_Code, string EFFECTIVE_DATE)
        {
            SqlParameter[] param = {
                                       new SqlParameter("@COMP_CODE", Comp_Code),
                                       new SqlParameter("@BRANCH_CODE", Branch_Code),  
                                        new SqlParameter("@Effective_Date", EFFECTIVE_DATE),  
                                           //new SqlParameter("@LocationCode", LocationCode), 
                                           // new SqlParameter("@productCode", ProductCode),  
                                           //  new SqlParameter("@ProductMine_Code", ProductMine_code),  
                                    
                                   };
            DataTable dt = new DataAccess(sqlConnection.GetConnectionString_SGX()).GetDataTable("[AGG].[USP_SELECT_STANDARD_COST_PROD_DTLS]", CommandType.StoredProcedure, param);
            List<AggCost_Production_Dtl> list = new List<AggCost_Production_Dtl>();
            AggCost_Production_Dtl dtl = null;


            foreach (DataRow row in dt.Rows)
            {
                dtl = new AggCost_Production_Dtl();
                dtl.SCC_ID = Convert.ToDecimal(row["SCC_ID"] == DBNull.Value ? 0 : row["SCC_ID"]);
                dtl.SC_ID = Convert.ToDecimal(row["SC_ID"] == DBNull.Value ? 0 : row["SC_ID"]);
                dtl.PCG_ID = Convert.ToInt32(row["PCG_ID"] == DBNull.Value ? 0 : row["PCG_ID"]);
                dtl.PCG_NAME = Convert.ToString(row["PCG_NAME"]);
                dtl.PCSG_ID = Convert.ToInt32(row["PCSG_ID"] == DBNull.Value ? 0 : row["PCSG_ID"]);
                dtl.PCSG_NAME = Convert.ToString(row["PCSG_NAME"]);
                dtl.StandardCost = Convert.ToInt32(row["StandardCost"] == DBNull.Value ? 0 : row["StandardCost"]);
                list.Add(dtl);
            }
            return list;
        }
        public string INSERT_Standard_CostProd(string Comp_Code, string Branch_Code, string Emp_Code, AggCost_Production Aggcost)
        {
            string errorMsg = "";

            using (var connection = new SqlConnection(sqlConnection.GetConnectionString_SGX()))
            {
                connection.Open();
                SqlCommand command;
                SqlTransaction transactionScope = null;
                transactionScope = connection.BeginTransaction(IsolationLevel.ReadCommitted);
                try
                {
                    SqlParameter[] param =
                    { 
                      new SqlParameter("@ERRORSTR", SqlDbType.VarChar, 200)
                     ,new SqlParameter("@SC_ID", SqlDbType.Decimal)  
                     ,new SqlParameter("@COMP_CODE", Comp_Code)
                     ,new SqlParameter("@BRANCH_CODE", Branch_Code)
                     //,new SqlParameter("@LocationCode", Aggcost.LocationCode)   
                     //,new SqlParameter("@Effective_Date", Aggcost.EFFECTIVE_DATE)  
                     //,new SqlParameter("@productCode", Aggcost.ProductCode) 
                             ,new SqlParameter("@LocationCode",( Aggcost.LocationCode == null)?(object)DBNull.Value : Aggcost.LocationCode)
                     ,new SqlParameter("@Effective_Date", (Aggcost.EFFECTIVE_DATE == null)?(object)DBNull.Value : Aggcost.EFFECTIVE_DATE)
                     ,new SqlParameter("@productCode", (Aggcost.ProductCode == null)?(object)DBNull.Value : Aggcost.ProductCode)
                     ,new SqlParameter("@ProductMine_Code", (Aggcost.ProductMine_code == null)?(object)DBNull.Value : Aggcost.ProductMine_code) 
                     ,new SqlParameter("@ADDED_BY", Emp_Code)  
                    };
  
                    param[0].Direction = ParameterDirection.Output; 
                    param[1].Direction = ParameterDirection.Output;

                    new DataAccess().InsertWithTransaction("[AGG].[USP_INSERT_STANDARD_COST_PROD_HDR]", CommandType.StoredProcedure, out command, connection, transactionScope, param);
                    decimal SC_ID = (decimal)command.Parameters["@SC_ID"].Value;
                    string error_1 = (string)command.Parameters["@ERRORSTR"].Value;
                    if (SC_ID == -1) { errorMsg = error_1; }

                    if (SC_ID > 0)
                    {
                        if (Aggcost.AggCost_Production_Dtl_List != null)
                        {
                            foreach (AggCost_Production_Dtl item in Aggcost.AggCost_Production_Dtl_List)
                            {
                                SqlParameter[] param2 =
                                    {
                                       new SqlParameter("@ERRORSTR", SqlDbType.VarChar, 200)
                                      ,new SqlParameter("@SCC_ID", SqlDbType.Decimal)  
                                      ,new SqlParameter("@SC_ID", SC_ID)  
                                      ,new SqlParameter("@PCG_ID", (item.PCG_ID == null) ? (object)DBNull.Value  : item.PCG_ID)
                                      ,new SqlParameter("@PCSG_ID", (item.PCSG_ID == null) ? (object)DBNull.Value  : item.PCSG_ID)
                                      ,new SqlParameter("@StandardCost", (item.StandardCost == null) ? 0   : item.StandardCost)
                               
                                    };
                                param2[0].Direction = ParameterDirection.Output;
                                param2[1].Direction = ParameterDirection.Output;
                                new DataAccess().InsertWithTransaction("[AGG].[USP_INSERT_STANDARD_COST_PROD_dtl]", CommandType.StoredProcedure, out command, connection, transactionScope, param2);
                                decimal SCC_ID = (decimal)command.Parameters["@SCC_ID"].Value;
                                string error_2 = (string)command.Parameters["@ERRORSTR"].Value;
                                if (SCC_ID == -1) { errorMsg = error_2; break; }

                            }
                        }
                    }

                    if (errorMsg == "")
                    {
                        transactionScope.Commit();
                    }
                    else
                    {
                        transactionScope.Rollback();
                    }
                }
                catch (Exception ex)
                {
                    try
                    {
                        transactionScope.Rollback();
                    }
                    catch (Exception ex1)
                    {
                        errorMsg = "Error: Exception occured. " + ex1.StackTrace.ToString();
                    }
                }
                finally
                {
                    connection.Close();
                }
            }
            return errorMsg;
        }

    }
}
