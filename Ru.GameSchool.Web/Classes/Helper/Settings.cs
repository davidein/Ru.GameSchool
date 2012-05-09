using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Ru.GameSchool.Web.Classes.Helper
{
    public class Settings
    {
        public static string ProjectUploadFolder
        {
            get
            {
                var path = ConfigurationManager.AppSettings["Ru.GameSchool.ProjectUpload"];
                if (string.IsNullOrEmpty(path))
                    throw new Exception("Ru.GameSchool.ProjectUpload is missing from web.config.");
                return path;
            }
        }

        public static string ProjectMaterialVirtualFolder
        {
            get
            {
                var path = ConfigurationManager.AppSettings["Ru.GameSchool.ProjectMaterialVirtualFolder"];
                if (string.IsNullOrEmpty(path))
                    throw new Exception("Ru.GameSchool.ProjectMaterialVirtualFolder is missing from web.config.");
                return path;
            }
        }

    }
}