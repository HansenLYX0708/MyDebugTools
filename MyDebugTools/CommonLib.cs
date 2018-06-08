using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib
{
    public class FilePath
    {
        static public string GetFileName(string fullPath)
        {
            string[] pathInfo = fullPath.Split('\\');
            if (pathInfo.Length > 1)
            {
                return pathInfo[pathInfo.Length - 1];
            }
            return "";
        }

        static public string GetFilePath(string fullPath)
        {
            string[] pathInfo = fullPath.Split('\\');
            if (pathInfo.Length > 1)
            {
                int idx = fullPath.LastIndexOf('\\');
                string resultStr = fullPath.Substring(0, idx - 0);
                return resultStr;
            }
            return "";
        }

        static public string GetFileNameNoExtend(string fullPath)
        {
            string fileName = GetFileName(fullPath);
            if (fileName.Length > 0)
            {
                string[] extInfo = fileName.Split('.');
                if (extInfo.Length > 1)
                {
                    int idx = fileName.LastIndexOf('.');
                    string finalStr = fileName.Substring(0, idx - 0);
                    return finalStr;
                }
            }
            return "";
        }

        static public string GetFileExtend(string fullPath)
        {
            string fileName = GetFileName(fullPath);
            if (fileName.Length > 0)
            {
                string[] extInfo = fileName.Split('.');
                if (extInfo.Length > 1)
                {
                    int idx = fileName.LastIndexOf('.');
                    string finalStr = fileName.Substring(idx + 1, fileName.Length - idx - 1);
                    return finalStr;
                }
            }
            return "";
        }
    }
}
