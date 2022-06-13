using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SalesWeb.Universal.DAL;
using SalesWeb.Areas.Security.Models.BEL;
using SalesWeb.Areas.SpaMisReport.Models.BEL;
using SalesWeb.Universal.Gateway;

namespace SalesWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly DBHelper _dbHelper = new DBHelper();
        private readonly ErrorLogger _errorLogger = new ErrorLogger();
        private bool _isChildHasUrl;
        
        public ActionResult Index()
        {
            if (Session["USER_ID"] == null)
            {
                return RedirectToAction("Login", "Home", new { param = "SessionOut", area = "" });
            }
            return View();
        }

        public ActionResult Login(string param)
        {
            if (Session["USER_ID"] != null)
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            if (!string.IsNullOrEmpty(param))
            {
                ViewBag.myData = "Session Timeout occured";
            }

            return View();
        }
        public string TryLogin(UserLogin model)
        {
            try
            {
                LoginRegistrationDAO loginRegistrationDAO = new LoginRegistrationDAO();
                if (model.Username.Length <= 0 || model.Password.Length <= 0) return "false";
                var v = loginRegistrationDAO.CheckUserCredential();
                var verifiedUserCredential = loginRegistrationDAO.CheckUserCredential().FirstOrDefault(m => m.USER_NAME.Equals(model.Username) && m.PASSWORD.Equals(model.Password));

                if (verifiedUserCredential == null) return "false";
                var userQry =
                   " SELECT" +
                   " A.USER_ID," +
                   " A.USER_NAME," +
                   " A.ACCESS_LOCATION," +
                   " C.EMPLOYEE_ID," +
                   " C.EMPLOYEE_CODE," +
                   " B.ROLE_ID," +
                   " C.EMPLOYEE_NAME," +
                   " D.COMP_NAME," +
                   " D.COMP_ADDR1," +
                   " D.COMP_LOGO_URL," +
                   " (SELECT COUNT(*) FROM USER_PRODUCT_DTL WHERE USER_ID=A.USER_ID) USER_BASE_REPORT_FILTER," +
                   " A.DOWNLOAD_STATUS" +
                   " FROM SC_USER_LOGIN A" +
                   " INNER JOIN SC_ROLE_USER_CONF B ON A.USER_ID=B.USER_ID" +
                   " INNER JOIN SC_EMPLOYEE_INFO C ON A.EMPLOYEE_ID=C.EMPLOYEE_ID" +
                   " INNER JOIN SC_COMPANY D " +
                   " ON C.COMPANY_ID=D.ID" +
                   " WHERE UPPER(A.USER_NAME)='" + verifiedUserCredential.USER_NAME.ToUpper() + "' " +
                   " AND A.PASSWORD='" + verifiedUserCredential.PASSWORD + "' " +
                   " AND A.STATUS='Active' ";
                var dt = _dbHelper.GetDataTable(userQry);

                var item = (from DataRow row in dt.Rows
                            select new UserLogin
                            {
                                UserId = Convert.ToInt32(row["USER_ID"]),
                                Username = (row["USER_NAME"]).ToString(),
                                EmployeeCode = row["EMPLOYEE_CODE"].ToString()
                            }).ToList();
                if (item.Count > 0)
                {
                    if (!string.IsNullOrEmpty(item[0].Username) && !string.IsNullOrEmpty(item[0].EmployeeCode))
                    {
                        var userLogins = (from DataRow row in dt.Rows
                                          select new UserLogin
                                          {
                                              UserId = Convert.ToInt32(row["USER_ID"]),
                                              RoleId = Convert.ToInt32(row["ROLE_ID"]),
                                              Username = (row["USER_NAME"]).ToString(),
                                              EmployeeID = Convert.ToInt32(row["EMPLOYEE_ID"]),
                                              EmployeeCode = row["EMPLOYEE_CODE"].ToString(),
                                              EmployeeName = row["EMPLOYEE_NAME"].ToString(),
                                              //Code = row["CODE"].ToString(),
                                              AccessLevel = row["ACCESS_LOCATION"].ToString(),
                                              CompanyName = row["COMP_NAME"].ToString(),
                                              CompanyAddress = row["COMP_ADDR1"].ToString(),
                                              CompanyLogoUrl = row["COMP_LOGO_URL"].ToString(),
                                              UserBaseReportFilter = Convert.ToInt32(row["USER_BASE_REPORT_FILTER"]),
                                              ReportDownLoadStatus = row["DOWNLOAD_STATUS"].ToString()
                                          }).ToList();

                        if (userLogins.Count <= 0) return "false";

                        Session["USER_ID"] = userLogins[0].UserId.ToString();
                        Session["ROLE_ID"] = userLogins[0].RoleId.ToString();
                        Session["USER_NAME"] = userLogins[0].Username;
                        Session["EMPLOYEE_ID"] = userLogins[0].EmployeeID;
                        Session["EMPLOYEE_CODE"] = userLogins[0].EmployeeCode;
                        Session["EMPLOYEE_NAME"] = userLogins[0].EmployeeName ?? "N/A";
                        //Session["CODE"] = userLogins[0].Code ?? "N/A";
                        Session["ACCESS_LOCATION"] = userLogins[0].AccessLevel ?? "N/A";
                        Session["COMP_NAME"] = userLogins[0].CompanyName ?? "N/A";
                        Session["COMP_ADDR"] = userLogins[0].CompanyAddress ?? "N/A";
                        Session["COMP_LOGO_URL"] = userLogins[0].CompanyLogoUrl ?? "N/A";

                        Session["USER_BASE_REPORT_FILTER"] = userLogins[0].UserBaseReportFilter.ToString();

                        Session["DOWNLOAD_STATUS"] = userLogins[0].ReportDownLoadStatus.ToString();

                        Session["UserMenu"] = "";


                        _dbHelper.InsertAudit("L", "LogIn", "SC_USER_LOGIN", userLogins[0].UserId);



                        return "true";
                    }
                }
                return "false";
            }
            catch (Exception e)
            {
                var lineNum = e.StackTrace.Substring(e.StackTrace.LastIndexOf(' '));
                _errorLogger.GetErrorMessage(e.Message, "Home", lineNum);
                return "false";
            }
        }
        [HttpGet]
        public string CreateMenu()
        {
            try
            {
                Session["UserMenu"] = "";
                if (Session["UserMenu"].ToString().Length != 0) return Session["UserMenu"].ToString();
                var rlId = Convert.ToInt32(Session["ROLE_ID"].ToString());
                var htmlMenu = "";
                var mhQry = "  SELECT DISTINCT " +
                            "  B.MENU_NAME," +
                            "  B.MENU_DISPLAY_NAME, " +
                            "  B.MENU_ID, " +
                            "  A.PARENT_SEQ " +
                            "  FROM SC_MENU_CONF A " +
                            "  INNER JOIN SC_MENU_INFO B ON A.PARENT_ID = B.MENU_ID" +
                            "  INNER JOIN  SC_ROLE_MENU_CONF C ON A.ID = C.MC_ID " +
                             " INNER JOIN SC_ROLE_INFO D ON C.ROLE_ID=D.ROLE_ID" +
                            "  WHERE B.MENU_ID NOT IN(SELECT CHILD_ID FROM SC_MENU_CONF) " +
                            "  AND C.ROLE_ID =" + rlId + "" +
                            "  AND B.MENU_STATUS='Active'"+
                            "  AND D.STATUS='Active'" +
                            " ORDER BY A.PARENT_SEQ";
                var mHdt = _dbHelper.GetDataTable(mhQry);
                var mhList = (from DataRow row in mHdt.Rows
                              select new MenuInfo
                              {
                                  MId = Convert.ToInt32(row["MENU_ID"].ToString()),
                                  MName = row["MENU_DISPLAY_NAME"].ToString()
                              }).ToList();
                htmlMenu += "<li class='header'>MAIN NAVIGATION</li>";
                foreach (var u in mhList)
                {
                    _isChildHasUrl = true;
                    var htmlChildMenu = IsThisIdHasAnyChild(u.MId, rlId, 20);
                    if (!_isChildHasUrl) continue;
                    htmlMenu += " <li class='treeview'>";
                    htmlMenu += " <a href = '#' >";
                    htmlMenu += " <i class='fa fa-th'></i> <span>" + u.MName + "</span>";
                    htmlMenu += " <span class='pull-right-container' >";
                    htmlMenu += " <i class='fa fa-angle-left pull-right'></i>";
                    htmlMenu += " </span>";
                    htmlMenu += " </a>";
                    htmlMenu += htmlChildMenu;
                    htmlMenu += "</li>";
                }
                htmlMenu += "</li>";
                Session["UserMenu"] = htmlMenu;

                return Session["UserMenu"].ToString();
            }
            catch (Exception e)
            {
                var lineNum = e.StackTrace.Substring(e.StackTrace.LastIndexOf(' '));
                _errorLogger.GetErrorMessage(e.Message, "Home", lineNum);
                throw;
            }
        }
        public string IsThisIdHasAnyChild(int id, int rlId, int marginSize)
        {
            try
            {
                var htmlMenu = "";
                var smQry = " SELECT DISTINCT " +
                            " B.MENU_NAME," +
                            " B.MENU_DISPLAY_NAME," +
                            " B.MENU_ID," +
                            " A.URL," +
                            " A.CHILD_SEQ," +
                            " A.BG_COLOR " +
                            " FROM SC_MENU_CONF A " +
                            " INNER JOIN SC_MENU_INFO B ON A.CHILD_ID = B.MENU_ID" +
                            " INNER JOIN SC_ROLE_MENU_CONF C ON A.ID = C.MC_ID " +
                            " INNER JOIN SC_ROLE_INFO D ON C.ROLE_ID=D.ROLE_ID" +
                            " WHERE A.PARENT_ID = " + id + " " +
                            " AND C.ROLE_ID = " + rlId +
                            " AND B.MENU_STATUS='Active'" +
                            " AND D.STATUS='Active'" +
                            " ORDER BY A.CHILD_SEQ";
                var sMdt = _dbHelper.GetDataTable(smQry);
                var smList = (from DataRow row in sMdt.Rows
                              select new MenuInfo
                              {
                                  MId = Convert.ToInt32(row["MENU_ID"].ToString()),
                                  MName = row["MENU_DISPLAY_NAME"].ToString(),
                                  BgColor = row["BG_COLOR"].ToString(),
                                  Url = row["URL"].ToString()

                              }).ToList();

                if (smList.Any())
                {
                    //_marginSize = +3;
                    //htmlMenu = htmlMenu + " <ul class='menu-content'>";
                    htmlMenu += " <ul class='treeview-menu' >";
                    foreach (var v in smList)
                    {
                        if (string.IsNullOrEmpty(v.Url))
                        {
                            //htmlMenu = htmlMenu + "<li class='has-sub'>";
                            //htmlMenu = htmlMenu + "<a href = '#'  class='menu-item'><i class='icon-android-funnel'></i>" + v.MName + "</a>";
                            htmlMenu = htmlMenu + " <li class='treeview'><a style='padding-left: " + marginSize + "px;' href = '#' ><i class='fa fa-share '></i>  " + v.MName + " ";
                            htmlMenu += " <span class='pull-right-container' >";
                            htmlMenu += " <i class='fa fa-angle-left pull-right'></i>";
                            htmlMenu += " </span>";
                            htmlMenu += " </a>";
                            htmlMenu += IsThisIdHasAnyChild(v.MId, rlId, 30);
                            _isChildHasUrl = true;
                            htmlMenu += "</li>";
                        }
                        else
                        {

                            //htmlMenu = htmlMenu + " <li class=''><a style='padding-left: " + 35 + "px;background: #05475f;' class='treeview-menu-child' href = '" + v.Url + "' ><i class='fa fa-circle-o '></i>  " + v.MName + "</a></li>";
                            htmlMenu = htmlMenu + " <li class=''><a style='padding-left: " + 35 + "px;background: " + v.BgColor + ";' class='treeview-menu-child' href = '" + v.Url + "' ><i class='fa fa-circle-o '></i>  " + v.MName + "</a></li>";
                        }

                        //htmlMenu = htmlMenu + "</li>";
                    }
                    htmlMenu += " </ul>";
                }
                else
                {
                    _isChildHasUrl = false;
                }
                return htmlMenu;
            }
            catch (Exception e)
            {
                var lineNum = e.StackTrace.Substring(e.StackTrace.LastIndexOf(' '));
                _errorLogger.GetErrorMessage(e.Message, "Home", lineNum);
                throw;
            }
        }

        [HttpPost]
        public JsonResult EventPermission(string url)
        {
            //var empId = Convert.ToInt32(Session["USER_ID"].ToString());
            // var roleName = Session["ROLE_NAME"].ToString();
            try
            {
                var rlId = Convert.ToInt32(Session["ROLE_ID"].ToString());
                var eventQry = " SELECT " +
                               " C.MENU_DISPLAY_NAME," +
                               " C.MENU_NAME," +
                               " B.SV," +
                               " B.VW," +
                               " B.DL " +
                               " FROM SC_MENU_CONF A " +
                               " INNER JOIN SC_ROLE_MENU_CONF B ON A.ID=B.MC_ID " +
                               " INNER JOIN SC_MENU_INFO C ON C.MENU_ID=A.CHILD_ID " +
                               " WHERE B.ROLE_ID=" + rlId + 
                               " AND A.URL='" + url + "'"+
                               " AND C.MENU_STATUS='Active'";

                var eventDt = _dbHelper.GetDataTable(eventQry);
                var mhList = (from DataRow row in eventDt.Rows
                              select new EventPermission
                              {

                                  DisplayName = row["MENU_DISPLAY_NAME"].ToString(),
                                  MenuName = row["MENU_NAME"].ToString(),
                                  Sv = row["SV"].ToString(),
                                  Dl = row["DL"].ToString(),
                                  Vw = row["VW"].ToString()

                              }).ToList();

                return Json(new { Data = mhList }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                var lineNum = e.StackTrace.Substring(e.StackTrace.LastIndexOf(' '));
                _errorLogger.GetErrorMessage(e.Message, "Home", lineNum);
                throw;
            }
        }
        [HttpPost]
        public JsonResult GetReportName(string url)
        {
            try
            {
                var rlId = Convert.ToInt32(Session["ROLE_ID"].ToString());
                var eventQry = "SELECT * FROM SC_ROLE_REPORT_CONF A INNER JOIN SC_REPORT_INFO B ON A.REPORT_ID=B.REPORT_ID " +
                               " WHERE A.ROLE_ID=" + rlId + " AND B.FORM_URL='" + url + "' ORDER BY REPORT_NAME";

                var eventDt = _dbHelper.GetDataTable(eventQry);
                var rList = (from DataRow row in eventDt.Rows
                             select new ReportInfoBEL
                             {
                                 ReportName = row["REPORT_NAME"].ToString(),
                                 DisplayName = row["DISPLAY_NAME"].ToString()
                             }).ToList();
                //reportMenu += "<div class='checkbox-group' ng-init=ReportName="+rList[0].ReportName+"'>";
                //var a = "true";
                //foreach (var u in rList)
                //{
                //    reportMenu += "<div class='form-group'><div class='col-md-12'><label class='control-label'>";
                //    reportMenu += "<input  type='radio'  name='ReportName' ng-model='ReportName' value='" + u.ReportName + "'/> " + u.DisplayName + "";
                //    reportMenu += "</label></div></div>";

                //}

                //reportMenu += "<script>$('#" + rList[0].ReportName + "').prop('checked', true);</script >";


                //return reportMenu;
                return Json(new { ReportRadioList = rList, Status = "" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                var lineNum = e.StackTrace.Substring(e.StackTrace.LastIndexOf(' '));
                _errorLogger.GetErrorMessage(e.Message, "Home", lineNum);
                throw;
                //return Json(new { Status = e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public JsonResult GetVarField()
        {
            try
            {
                const string varFieldQry = "SELECT DEPOT_VAR, REGION_VAR, ZONE_VAR, AREA_VAR, TERRITORY_VAR, MARKET_VAR FROM SC_VAR_FIELD_NAME ";
                var varFieldDt = _dbHelper.GetDataTable(varFieldQry);
                var varFieldList = (from DataRow row in varFieldDt.Rows
                                    select new VarFieldName
                                    {
                                        DepotVar = row["DEPOT_VAR"].ToString(),
                                        ZoneVar = row["ZONE_VAR"].ToString(),
                                        RegionVar = row["REGION_VAR"].ToString(),
                                        AreaVar = row["AREA_VAR"].ToString(),
                                        TerritoryVar = row["TERRITORY_VAR"].ToString(),
                                        MarketVar = row["MARKET_VAR"].ToString()
                                    }).ToList();
                return Json(new { Data = varFieldList }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                var lineNum = e.StackTrace.Substring(e.StackTrace.LastIndexOf(' '));
                _errorLogger.GetErrorMessage(e.Message, "Home", lineNum);
                throw;
            }
        }

        [HttpGet]
        public JsonResult GetQuarterInfo()
        {
            try
            {
                const string varQuarterQry = " SELECT QUARTER_ID, QUARTER_NAME, QUARTER_SHORT_NAME, FROM_MM, TO_MM, QUARTER_MONTH_NAME " +
                                             " FROM PHARMA_ERP_SQA.QUARTER_INFO";
                var varQuarterDt = _dbHelper.GetDataTable(varQuarterQry);
                var varQuarterList = (from DataRow row in varQuarterDt.Rows
                                    select new VarQuarterInfo
                                    {
                                        //QuarterValue = DateTime.Now.Year.ToString()+row["FROM_MM"].ToString()+"-"+ DateTime.Now.Year.ToString()+row["TO_MM"].ToString(),
                                        QuarterValue = row["FROM_MM"].ToString()+"-"+ row["TO_MM"].ToString(),
                                        QuarterName = row["QUARTER_SHORT_NAME"].ToString()
                                    }).ToList();
                return Json(new { Data = varQuarterList}, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                var lineNum = e.StackTrace.Substring(e.StackTrace.LastIndexOf(' '));
                _errorLogger.GetErrorMessage(e.Message, "Home", lineNum);
                throw;
            }
        }
        [HttpGet]
        public JsonResult GetVarDesig()
        {
            try
            {
                const string varFieldQry = "SELECT VAR_ID, DEPOT_DESIG_VAR, ZONE_DESIG_VAR, REGION_DESIG_VAR, AREA_DESIG_VAR, TERRITORY_DESIG_VAR," +
                    " MARKET_DESIG_VAR FROM SC_VAR_DESIG_NAME ";
                var varFieldDt = _dbHelper.GetDataTable(varFieldQry);
                var varFieldList = (from DataRow row in varFieldDt.Rows
                                    select new VarDesigName
                                    {
                                        DepotDesigVar = row["DEPOT_DESIG_VAR"].ToString(),
                                        ZoneDesigVar = row["ZONE_DESIG_VAR"].ToString(),
                                        RegionDesigVar = row["REGION_DESIG_VAR"].ToString(),
                                        AreaDesigVar = row["AREA_DESIG_VAR"].ToString(),
                                        TerritoryDesigVar = row["TERRITORY_DESIG_VAR"].ToString(),
                                        MarketDesigVar = row["MARKET_DESIG_VAR"].ToString()
                                    }).ToList();
                return Json(new { Data = varFieldList }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                var lineNum = e.StackTrace.Substring(e.StackTrace.LastIndexOf(' '));
                _errorLogger.GetErrorMessage(e.Message, "Home", lineNum);
                throw;
            }
        }
       
        public ActionResult LogOut()
        {
            Session.RemoveAll();
            Session.Abandon();
            return RedirectToAction("Login", "Home", "");
        }
        [HttpPost]
        public ActionResult UploadFile(string folderName)
        {
            try
            {
                HttpPostedFileBase files = Request.Files[0];
                if (files == null)
                {
                    return Json(new { Status = "Not Ok" });
                }
                //var fileName = Path.GetFileName(files.FileName);
                string empId = Session["USER_ID"].ToString();
                var fileName = empId + "_" + folderName + Path.GetExtension(files.FileName);
                var physicalPath = Path.Combine(Server.MapPath("~/UploadDoc/" + folderName), fileName ?? throw new InvalidOperationException());
                if (!Directory.Exists(Path.Combine(Server.MapPath("~/UploadDoc/" + folderName))))
                {
                    Directory.CreateDirectory(Path.Combine(Server.MapPath("~/UploadDoc/" + folderName)));
                }
                if (System.IO.File.Exists(physicalPath))
                {
                    System.IO.File.Delete(physicalPath);
                }
                files.SaveAs(physicalPath);
                return Json(new { fileName = fileName, physicalPath = physicalPath, Status = "Ok" });
            }
            catch (Exception e)
            {
                var lineNum = e.StackTrace.Substring(e.StackTrace.LastIndexOf(' '));
                _errorLogger.GetErrorMessage(e.Message, "FileUtility", lineNum);
                return Json(new { Status = e.Message });
            }

        }











    }

}


