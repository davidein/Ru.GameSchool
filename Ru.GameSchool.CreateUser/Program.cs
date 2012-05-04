using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ru.GameSchool.BusinessLayer.Services;
using Ru.GameSchool.DataLayer.Repository;

namespace Ru.GameSchool.CreateUser
{
    class Program
    {
        static void Main(string[] args)
        {
            UserInfo userInfo = new UserInfo();

            userInfo.Username = "dabbi";
            userInfo.Password = "dabbi";
            userInfo.StatusId = 1;
            userInfo.UserTypeId = 1;
            userInfo.Fullname = "Dabbi e";
            userInfo.Email = "davidein@gmail.com";
            userInfo.CreateDateTime = DateTime.Now;
            userInfo.DepartmentId = 1;

            UserService userService = new UserService();

            userService.CreateUser(userInfo);
        }
    }
}
