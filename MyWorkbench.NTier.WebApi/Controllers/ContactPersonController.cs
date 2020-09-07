using MyLayercake.NTier.Example.BusinessLayer;
using MyLayercake.NTier.Example.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
namespace MyWorkbench.NTier.WebApi.Controllers
{
    public class ContactPersonController : ApiController
    {
        [Route("api/ContactPerson/Get")]
        [HttpPost]
        public JsonResult<ContactPersonList> Get() {
            ContactPersonList personlist = ContactPersonManager.GetList();

            return Json<ContactPersonList>(personlist);
        }

        [HttpGet]
        public JsonResult<ContactPerson> Get(int id) {
            ContactPerson person = ContactPersonManager.GetItem(id);

            return Json<ContactPerson>(person);
        }

        [HttpPost]
        public bool Insert(ContactPerson ContactPerson) {
            ContactPersonManager.Save(ContactPerson);

            return true;
        }

        [HttpPost]
        public bool Update(ContactPerson ContactPerson) {
            ContactPersonManager.Save(ContactPerson);

            return true;
        }
    }
}
