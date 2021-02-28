﻿using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using RMDataManager.Library.DataAccess;
using RMDataManager.Library.Models;
using RMDataManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace RMDataManager.Controllers
{
    [Authorize]
    [RoutePrefix("api/User")]
    public class UserController : ApiController
    {
        [HttpGet]
        // GET: User/GetbyId/5
        public UserModel GetById()
        {
            string userId = RequestContext.Principal.Identity.GetUserId();
            UserData data = new UserData();
           return data.GetUserById(userId).FirstOrDefault();
        }
        [Authorize(Roles ="Admin")]
        [HttpGet]
        [Route("Admin/GetAllUsers")]
        public List<ApplicationUserModel> GetAllUsers()
        {

            List<ApplicationUserModel> output = new List<ApplicationUserModel>();

            using (var context = new ApplicationDbContext())
            {
                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);
                var users = userManager.Users.ToList();
                var roles = context.Roles.ToList();
                foreach (var user in users)
                {
                    ApplicationUserModel u = new ApplicationUserModel()
                    {
                        Id = user.Id,
                        Email = user.Email
                    };
                    foreach (var userRole in user.Roles)
                    {
                        u.Roles.Add(userRole.RoleId, roles.Where(x => x.Id == userRole.RoleId).First().Name);
                    }
                    output.Add(u);
                }
            }
            return output;
        }
        
    }
}
