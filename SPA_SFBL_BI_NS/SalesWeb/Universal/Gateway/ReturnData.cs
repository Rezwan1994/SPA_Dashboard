using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Web;

namespace SalesWeb.Universal.Gateway
{
    public class ReturnData
    {
        public string ProcessSlno { get; set; }
        public long MstID { get; set; }
        public long DtlID { get; set; }

        public long ProductTypeID { get; set; }
        public long MaxID { get; set; }
        public string MaxCode { get; set; }
        public string IuMode { get; set; }
        public string MSG { get; set; }
        public string ExceptionReturn { get; set; }
        public object ListReturn { get; set; }
        public string ProcessRunMessage = "Process Successfully Run";
        public string InsertMessage = "Saved Successfully";
        public string UpdateMessage = "Updated Successfully";
        public string DeleteMessage = "Deleted Successfully";
        public string RefreshMessage = "Materialized View Refresh Complete";
        public string StatusChangeMessage = "Status Successfully Changed";
        public static string GetHostName
        {
            get
            {
                string strComputerName = Environment.MachineName.ToString();
                return strComputerName;
            }
        }

        public static string GetIP
        {
            get
            {
                System.Web.HttpContext context = System.Web.HttpContext.Current;
                string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

                if (!string.IsNullOrEmpty(ipAddress))
                {
                    string[] addresses = ipAddress.Split(',');
                    if (addresses.Length != 0)
                    {
                        return addresses[0];
                    }
                }

                return context.Request.ServerVariables["REMOTE_ADDR"];
            }
        }


        public String GetIpAddress()
        {
            //String ip = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            //if (string.IsNullOrEmpty(ip))
            //{
            //    ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            //}
            //if (ip == "::1")
            //{
            //    ip = "127.0.0.1";
            //}
            //return ip;


            string localIp = "";
            if (!System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
            {
                return localIp;
            }

            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    localIp = ip.ToString();
                    break;
                }
            }
            return localIp;

        }
    }
}