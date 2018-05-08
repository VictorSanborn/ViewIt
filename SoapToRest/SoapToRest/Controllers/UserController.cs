﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SoapToRest.Controllers
{
    public class UserController : ApiController
    {
        UserService.Service1Client client = new UserService.Service1Client();

        public HttpStatusCode LoginUser(string username, string password)
        {

            bool result = client.LoginUser(username, password);

            if (result)
            {
                return HttpStatusCode.OK;
            }
            else
            {
                return HttpStatusCode.Unauthorized;
            }
        }
        [HttpPost]
        public HttpStatusCode CreateUser(string username, string password, string firstname, string lastname, string email)
        {
            try
            {
                client.CreateUser(username, password, firstname, lastname, email);
            }
            catch
            {
                return HttpStatusCode.Forbidden;
            }

            return HttpStatusCode.OK;
        }

        public int GetUserID(string username, string password)
        {
            int result = 0;
            try
            {
                result = client.GetUserID(username, password);
            }
            catch
            {
               
            }

            return result;

        }

        public UserService.UserInfo[] GetUserInfo()
        {

            UserService.UserInfo[] users = new UserService.UserInfo[] { };

            try
            {
                users = client.GetUserInfo();
            }
            catch
            {

            }

            return users;

        }
        public UserService.UserInfo GetSpecificUserInfo(int userID)
        {
            UserService.UserInfo user = new UserService.UserInfo();
            UserService.UserInfo[] users = new UserService.UserInfo[] { };

            try
            {
                users = client.GetUserInfo();
                foreach (UserService.UserInfo u in users)
                {
                    if (u.ID == userID)
                    {
                        user = u;
                    }
                }
            }
            catch
            {

            }

            return user;

        }

        public HttpStatusCode UpdateEstablishment(int establishmentID, string name, string description, int rating, int userID)
        {
            UserService.UserInfo user = new UserService.UserInfo();

            try 
            {
                client.UpdateEstablishment(establishmentID, name, description, rating, userID);
            }
            catch (Exception e)
            {
                return HttpStatusCode.NotFound;
            }

            return HttpStatusCode.OK;

        }
    }
}