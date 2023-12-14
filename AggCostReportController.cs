using BusinessLayer;
using BusinessLayer.DAL;
using BusinessLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eSGBIZ.Controllers
{
    public class AggCostReportController : BaseController
    {
        //
        // GET: /AggCostReport/
  [Authorize]
        public ActionResult AggCostProduction()
        {
            return View();
        }

  public ActionResult AggCostProduction_Entry(string EFFECTIVE_DATE)
  {
      ViewBag.Title = "Standard Cost Of Production";
      AggCost_Production Aggcost = new AggCost_Production();

      SelectDate _selectDate = new DAL_Common().GetSelect_Date(emp.Comp_Code, emp.BranchCode, "AGGPROD_DATE");
      Aggcost.EFFECTIVE_DATE = _selectDate.SELECT_DATE;
      Aggcost.hdnAGGCOSTPROD_DATE = _selectDate.SELECT_DATE;

      DateTime dt;

      if (string.IsNullOrWhiteSpace(EFFECTIVE_DATE))
      {
          dt = DateTime.Now;
      }
      else
      {

          if (DateTime.TryParse(EFFECTIVE_DATE, out dt) == false)
          {
              dt = DateTime.Now;
          }
      }

      Aggcost.EFFECTIVE_DATE = dt.ToString("dd/MM/yyyy");



      List<CostLocation_Mater> LocList = new DAL_Common().GetLocationList(emp.Employee_Code);
      Aggcost.Location_List = new SelectList(LocList, "locationCode", "locationName");

      List<CostProduct_Master> ProdList = new DAL_Common().GetProductList("L0009");
      Aggcost.PRODUCT_LIST = new SelectList(ProdList, "ProductCode", "productName", "ProductDesc");

      List<CostPlantMine_Master> PlantMineList = new DAL_Common().GetPlantMineList("L0011");
      Aggcost.PLANT_LIST = new SelectList(PlantMineList, "ProductMine_code", "Long_Name");

      List<AggCost_Production_Dtl> dtl = new DAL_STANDARD_COST_PROD().GET_Standard_CostProd_DTLS(emp.Comp_Code, emp.BranchCode,Aggcost.EFFECTIVE_DATE);
      Aggcost.AggCost_Production_Dtl_List = dtl;

      return View(Aggcost);
  }

          [HttpPost]
          [SubmitButtonSelector(Name = "Save")]
          [ActionName("AggCostProduction_Entry")]
          public ActionResult STANDARD_COSTPROD_SAVE(AggCost_Production Aggcost)
          {
              ViewBag.Header = "Standard Cost Of Production";

              var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

              if (ModelState.IsValid)
              {
                  try
                  {

                      string result = new DAL_STANDARD_COST_PROD().INSERT_Standard_CostProd(emp.Comp_Code, emp.BranchCode, emp.Employee_Code, Aggcost);

                      if (result == "")
                      {
                          Success(string.Format("<b>Standard Cost Of Production Inserted Successfully</b>"), true);
                          return RedirectToAction("AggCostProduction", "AggCostReport");
                      }
                      else
                      {
                          Danger(string.Format("<b>Error:</b>" + result), true);
                      }

                  }
                  catch (Exception ex)
                  {
                      Danger(string.Format("<b>Error:</b>" + ex.Message), true);
                  }
              }
              else
              {
                  Danger(string.Format("<b>Error:102 :</b>" + string.Join("; ", ModelState.Values.SelectMany(z => z.Errors).Select(z => z.ErrorMessage))), true);
              }

              return View(Aggcost);
          }
    }
}
