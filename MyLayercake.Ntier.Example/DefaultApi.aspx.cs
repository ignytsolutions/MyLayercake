using MyLayercake.NTier.Example.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MyLayercake.Ntier.Example {
    public partial class DefaultApi : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            if (!this.IsPostBack) {
                this.PopulateGridView();
            }
        }

        private void PopulateGridView() {
            string apiUrl = "http://localhost:44339/api/ContactPerson";

            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;
            string json = client.UploadString(apiUrl + "/Get", string.Empty);

            gvContactPersons.DataSource = (new JavaScriptSerializer()).Deserialize<ContactPersonList>(json);
            gvContactPersons.DataBind();
        }
    }
}