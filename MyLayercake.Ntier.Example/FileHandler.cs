using System;
using System.Text;
using System.Web;

namespace MyLayercake.Ntier.Example {
    public class FileHandler : IHttpHandler {
        /// <summary>
        /// You will need to configure this handler in the Web.config file of your 
        /// web and register it with IIS before being able to use it. For more information
        /// see the following link: https://go.microsoft.com/?linkid=8101007
        /// </summary>
        #region IHttpHandler Members

        public bool IsReusable {
            // Return false in case your Managed Handler cannot be reused for another request.
            // Usually this would be false in case you have some state information preserved per request.
            get { return true; }
        }

        public void ProcessRequest(HttpContext context) {
            context.Response.ClearContent();
            context.Response.Clear();
            context.Response.ContentType = "text/plain";
            context.Response.AddHeader("Content-Disposition", "attachment; filename=DownloadedData.csv;");
            StringBuilder sb = new StringBuilder();
            context.Response.Write(sb.ToString());
            context.Response.Flush();
            context.Response.End();
        }

        #endregion
    }
}
