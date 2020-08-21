using RMDataManager.Library.DataAccess;
using RMDataManager.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace RMDataManager.Controllers
{
    [Authorize]
    [RoutePrefix("api/User")]
    public class UserController : ApiController
    {

        // GET: User/GetbyId/5
        public List<UserModel> GetbyId(string id)
        {
            UserData data = new UserData();
            data.GetUserById(id);
            return View();
        }
        
    }
}
