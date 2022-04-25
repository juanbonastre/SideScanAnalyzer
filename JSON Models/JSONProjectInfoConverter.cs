using Newtonsoft.Json.Linq;
using SideScanAnalyzer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;

namespace SideScanAnalyzer.JSON_Models
{
    public static class JSONProjectInfoConverter 
    { 

        public static string ToJSONString(ProjectInfo projectInfo)
        {
            JObject json = new JObject();

            json["ProjectParentDirectoryPath"] = projectInfo.ProjectParentDirectoryPath;
            json["ProjectName"] = projectInfo.ProjectName;
            json["CreationTime"] = projectInfo.CreationTime;
            json["LastModificationTime"] = projectInfo.LastModificationTime;
            json["ImageSize"] = projectInfo.ImageSize;
            json["SelectedImagePath"] = projectInfo.SelectedImagePath;

            return json.ToString();
        }
        public static ProjectInfo ToProjectInfo(string jsonString)
        {
            JObject json = JObject.Parse(jsonString);

            ProjectInfo projectInfo = new ProjectInfo();

            try { projectInfo.ProjectParentDirectoryPath = (string)json["ProjectParentDirectoryPath"];  }
            catch (Exception) { projectInfo.ProjectParentDirectoryPath = ""; }

            try { projectInfo.ProjectName = (string)json["ProjectName"]; }
            catch (Exception) { projectInfo.ProjectName = ""; }

            try { projectInfo.CreationTime = DateTimeOffset.Parse(json["CreationTime"].ToString()); }
            catch (Exception) { projectInfo.CreationTime = DateTime.Now; }

            try { projectInfo.LastModificationTime = DateTimeOffset.Parse(json["LastModificationTime"].ToString()); }
            catch (Exception) { projectInfo.LastModificationTime = DateTime.Now; }

            try { projectInfo.ImageSize = int.Parse(json["ImageSize"].ToString()); }
            catch (Exception) { projectInfo.ImageSize = 96; }

            try { projectInfo.SelectedImagePath = json["SelectedImagePath"].ToString(); }
            catch (Exception) { projectInfo.SelectedImagePath = null; }

            return projectInfo;
        }
    }
}
