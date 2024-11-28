using ServerApi;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace ServerApi
{
    public class RevitServer
    {
        public static readonly Logger _logger = Logger.GetLoggerFor<RevitServer>();

        public string _baseUri;

        public JavaScriptSerializer _ser;

        public string BaseUri
        {
            get
            {
                if (_baseUri == null)
                {
                    if (Ver == 2012)
                    {
                        _baseUri = "http://" + Host + "/RevitServerAdminRESTService/AdminRESTService.svc";
                    }
                    else
                    {
                        _baseUri = "http://" + Host + "/RevitServerAdminRESTService" + Ver + "/AdminRESTService.svc";
                    }
                }

                return _baseUri;
            }
        }

        public string Host { get; private set; }

        public int Ver { get; private set; }

        private JavaScriptSerializer Serializer
        {
            get
            {
                //IL_0010: Unknown result type (might be due to invalid IL or missing references)
                //IL_001a: Expected O, but got Unknown
                if (_ser == null)
                {
                    _ser = new JavaScriptSerializer();
                    _ser.MaxJsonLength = int.MaxValue;
                }

                return _ser;
            }
        }

        public RevitServer(string host, int version)
        {
            if (string.IsNullOrEmpty(host))
            {
                throw new ArgumentNullException("host");
            }

            _logger.Debug("RevitServer() construtor called. host = " + host + ", version = " + version);
            Host = host;
            if (version < 2012)
            {
                _logger.Debug("version< 2012, throwing ArgumentException");
                throw new ArgumentException("Version less the 2012 is invalid.");
            }

            Ver = version;
        }

        public void CancelInProgressLock(string path)
        {
            try
            {
                _logger.Debug("CancelInProgressLock() called...");
                if (string.IsNullOrEmpty(path))
                {
                    throw new ArgumentNullException("path");
                }

                byte[] array = new byte[0];
                using (GetHttpWebResponse("/" + EscapeDataString(path) + "/inProgressLock", "DELETE"))
                {
                }
            }
            catch (Exception exception)
            {
                _logger.Error("Exception thrown in CancelInProgressLock()", exception);
                throw;
            }
        }

        public void Copy(string srcPath, string destPath, bool replaceExisting)
        {
            try
            {
                _logger.Debug("Copy() called...");
                if (string.IsNullOrEmpty(srcPath))
                {
                    throw new ArgumentNullException("srcPath");
                }

                if (string.IsNullOrEmpty(destPath))
                {
                    throw new ArgumentNullException("destPath");
                }

                CopyMove(srcPath, destPath, replaceExisting, "Copy");
            }
            catch (Exception exception)
            {
                _logger.Error("Exception thrown in Copy()", exception);
                throw;
            }
        }

        public void CreateFolder(string folderPath)
        {
            try
            {
                _logger.Debug("CreateFolder() called...");
                byte[] data = new byte[0];
                using (GetHttpWebResponse("/" + EscapeDataString(folderPath), "PUT", data))
                {
                }
            }
            catch (Exception exception)
            {
                _logger.Error("Exception thrown in CreateFolder()", exception);
                throw;
            }
        }

        public void Delete(string path)
        {
            try
            {
                _logger.Debug("Delete() called...");
                if (string.IsNullOrEmpty(path))
                {
                    throw new ArgumentNullException("path");
                }

                using (GetHttpWebResponse("/" + EscapeDataString(path) + "?newObjectName=", "DELETE"))
                {
                }
            }
            catch (Exception exception)
            {
                _logger.Error("Exception thrown in Delete()", exception);
                throw;
            }
        }

        public ServerApi.DirectoryInfo GetDirectoryInfo(string folderPath)
        {
            try
            {
                _logger.Debug("GetDirectoryInfo() called...");
                return GetResponseObject<ServerApi.DirectoryInfo>("/" + EscapeDataString(folderPath) + "/DirectoryInfo", "GET");
            }
            catch (Exception exception)
            {
                _logger.Error("Exception thrown in GetDirectoryInfo()", exception);
                throw;
            }
        }

        public FolderContents GetFolderContents(string rootFolderPath, int depth)
        {
            _logger.Debug("GetFolderContents(string folderPath, int depth) called...");
            if (rootFolderPath == null)
            {
                throw new ArgumentNullException("rootFolderPath");
            }

            _logger.Debug("rootFolderPath= {0}", rootFolderPath);
            _logger.Debug("depth =  {0}", depth);
            if (depth < 0)
            {
                throw new ArgumentOutOfRangeException("depth must be >= 0", "depth");
            }

            _logger.Debug("Getting root folder contents...");
            FolderContents folderContents = GetFolderContents(rootFolderPath);
            List<string> list = new List<string>(folderContents.Folders.Select((Folder f) => f.Path));
            List<string> list2 = new List<string>();
            int num = 2;
            while (list.Count > 0 && (num <= depth || depth == 0))
            {
                _logger.Debug("Reading folder contents. currentDepth = " + num + ", depth = " + depth);
                foreach (string item in list)
                {
                    _logger.Debug("Getting folder contents of: " + item);
                    FolderContents folderContents2 = GetFolderContents(item);
                    if (folderContents2.Folders != null && folderContents2.Folders.Count > 0)
                    {
                        if (folderContents.Folders == null)
                        {
                            folderContents.Folders = new List<Folder>();
                        }

                        foreach (Folder folder in folderContents2.Folders)
                        {
                            _logger.Debug("Adding folder:" + folder.Path);
                            folderContents.Folders.Add(folder);
                        }
                    }

                    if (folderContents2.ModelLocksInProgress != null && folderContents2.ModelLocksInProgress.Count > 0)
                    {
                        if (folderContents.ModelLocksInProgress == null)
                        {
                            folderContents.ModelLocksInProgress = new List<ModelLock>();
                        }

                        foreach (ModelLock item2 in folderContents2.ModelLocksInProgress)
                        {
                            _logger.Debug("Adding model lock:" + item2.ModelPath);
                            folderContents.ModelLocksInProgress.Add(item2);
                        }
                    }

                    if (folderContents2.Models != null && folderContents2.Models.Count > 0)
                    {
                        if (folderContents.Models == null)
                        {
                            folderContents.Models = new List<Model>();
                        }

                        foreach (Model model in folderContents2.Models)
                        {
                            _logger.Debug("Adding model:" + model.Path);
                            folderContents.Models.Add(model);
                        }
                    }

                    foreach (Folder folder2 in folderContents2.Folders)
                    {
                        _logger.Debug("Queuing sub folder path: " + folder2.Path);
                        list2.Add(folder2.Path);
                    }
                }

                list = list2;
                list2 = new List<string>();
                num++;
            }

            return folderContents;
        }

        public FolderContents GetFolderContents(string folderPath)
        {
            try
            {
                _logger.Debug("GetFolderContents(string folderPath) called...");
                if (folderPath == null)
                {
                    throw new ArgumentNullException("folderPath");
                }

                _logger.Debug("folderPath = " + folderPath);
                FolderContents responseObject = GetResponseObject<FolderContents>("/" + EscapeDataString(folderPath) + "/contents", "GET");
                if (responseObject.Models != null)
                {
                    foreach (Model model in responseObject.Models)
                    {
                        model.Path = ((!string.IsNullOrEmpty(folderPath)) ? (folderPath + "\\" + model.Name) : model.Name);
                    }
                }

                if (responseObject.Folders != null)
                {
                    foreach (Folder folder in responseObject.Folders)
                    {
                        folder.Path = ((!string.IsNullOrEmpty(folderPath)) ? (folderPath + "\\" + folder.Name) : folder.Name);
                    }
                }

                return responseObject;
            }
            catch (Exception exception)
            {
                _logger.Error("Exception thrown in GetFolderContents()", exception);
                throw;
            }
        }

        public LockedDescendents GetLockedDescendents(string folderPath)
        {
            try
            {
                _logger.Debug("GetLockedDescendents() called...");
                return GetResponseObject<LockedDescendents>("/" + EscapeDataString(folderPath) + "/descendent/locks", "GET");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ModelHistory GetModelHistory(string modelPath)
        {
            try
            {
                _logger.Debug("GetModelHistory() called...");
                if (string.IsNullOrEmpty(modelPath))
                {
                    throw new ArgumentNullException("modelPath");
                }

                return GetResponseObject<ModelHistory>("/" + EscapeDataString(modelPath) + "/history", "GET");
            }
            catch (Exception exception)
            {
                _logger.Error("Exception thrown in GetModelHistory()", exception);
                throw;
            }
        }

        public ModelInfo GetModelInfo(string modelPath)
        {
            try
            {
                _logger.Debug("GetModelInfo() called...");
                if (modelPath == null)
                {
                    throw new ArgumentNullException("modelPath");
                }

                return GetResponseObject<ModelInfo>("/" + EscapeDataString(modelPath) + "/modelInfo", "GET");
            }
            catch (Exception exception)
            {
                _logger.Error("Exception thrown in GetModelInfo()", exception);
                throw;
            }
        }

        public IList<Parameter> GetProjectInfo(string modelPath)
        {
            try
            {
                _logger.Debug("GetProjectInfo() called...");
                if (string.IsNullOrEmpty(modelPath))
                {
                    throw new ArgumentNullException("modelPath");
                }

                using HttpWebResponse httpWebResponse = GetHttpWebResponse("/" + EscapeDataString(modelPath) + "/projectInfo", "GET");
                using Stream stream = httpWebResponse.GetResponseStream();
                using StreamReader streamReader = new StreamReader(stream, Encoding.UTF8);
                string text = streamReader.ReadToEnd();
                _logger.Debug("jsonStr = " + text);
                object[] array = (object[])Serializer.DeserializeObject(text);
                List<Parameter> list = new List<Parameter>();
                object[] array2 = array;
                for (int i = 0; i < array2.Length; i++)
                {
                    Dictionary<string, object> dictionary = (Dictionary<string, object>)array2[i];
                    string group = (string)dictionary["A:title"];
                    foreach (KeyValuePair<string, object> item in dictionary)
                    {
                        if (item.Key == "A:title")
                        {
                            continue;
                        }

                        Dictionary<string, object> dictionary2 = (Dictionary<string, object>)item.Value;
                        Parameter parameter = new Parameter();
                        parameter.Group = group;
                        parameter.Name = item.Key;
                        if (dictionary2.ContainsKey("@displayName"))
                        {
                            parameter.DisplayName = (string)dictionary2["@displayName"];
                        }

                        if (dictionary2.ContainsKey("#text"))
                        {
                            parameter.Text = (string)dictionary2["#text"];
                        }

                        string text2 = (string)dictionary2["@type"];
                        switch (text2)
                        {
                            case "system":
                                parameter.Type = ParamType.System;
                                break;
                            case "shared":
                                parameter.Type = ParamType.Shared;
                                break;
                            case "custom":
                                parameter.Type = ParamType.Custom;
                                break;
                            default:
                                throw new InvalidDataException("Unexpected parameter type value returned from Revit Server API: " + text2);
                        }

                        if (dictionary2.ContainsKey("@typeOfParameter"))
                        {
                            switch ((string)dictionary2["@typeOfParameter"])
                            {
                                case "Length":
                                    parameter.ValueType = ParamValueType.Length;
                                    break;
                                case "Material":
                                    parameter.ValueType = ParamValueType.Material;
                                    break;
                                case "Multiline Text":
                                    parameter.ValueType = ParamValueType.MultilineText;
                                    break;
                                case "Number":
                                    parameter.ValueType = ParamValueType.Number;
                                    break;
                                case "Text":
                                    parameter.ValueType = ParamValueType.Text;
                                    break;
                                case "YesNo":
                                    parameter.ValueType = ParamValueType.YesNo;
                                    break;
                                default:
                                    parameter.ValueType = ParamValueType.NotImplemented;
                                    break;
                            }
                        }

                        list.Add(parameter);
                    }
                }

                return list;
            }
            catch (Exception exception)
            {
                _logger.Error("Exception thrown in GetProjectInfo()", exception);
                throw;
            }
        }

        public ServerProperties GetServerProperties()
        {
            try
            {
                _logger.Debug("GetServerProperties() called...");
                return GetResponseObject<ServerProperties>("/serverProperties", "GET");
            }
            catch (Exception exception)
            {
                _logger.Error("Exception thrown in GetServerProperties()", exception);
                throw;
            }
        }

        public Image GetThumbnail(string modelPath, int width, int height)
        {
            try
            {
                _logger.Debug("GetThumbnail() called...");
                if (string.IsNullOrEmpty(modelPath))
                {
                    throw new ArgumentNullException("modelPath");
                }

                using HttpWebResponse httpWebResponse = GetHttpWebResponse("/" + EscapeDataString(modelPath) + "/thumbnail?width=" + width + "&height=" + height, "GET");
                using Stream stream = httpWebResponse.GetResponseStream();
                return Image.FromStream(stream);
            }
            catch (Exception exception)
            {
                _logger.Error("Exception thrown in GetThumbnail()", exception);
                throw;
            }
        }

        public void Lock(string path)
        {
            try
            {
                _logger.Debug("Lock() called...");
                if (string.IsNullOrEmpty(path))
                {
                    throw new ArgumentNullException("path");
                }

                byte[] data = new byte[0];
                using (GetHttpWebResponse("/" + EscapeDataString(path) + "/lock", "PUT", data))
                {
                }
            }
            catch (Exception exception)
            {
                _logger.Error("Exception thrown in Lock()", exception);
                throw;
            }
        }

        public void Move(string srcPath, string destPath, bool replaceExisting)
        {
            try
            {
                _logger.Debug("Move() called...");
                if (string.IsNullOrEmpty(srcPath))
                {
                    throw new ArgumentNullException("srcPath");
                }

                if (string.IsNullOrEmpty(destPath))
                {
                    throw new ArgumentNullException("destPath");
                }

                CopyMove(srcPath, destPath, replaceExisting, "Move");
            }
            catch (Exception exception)
            {
                _logger.Error("Exception thrown in Move()", exception);
                throw;
            }
        }

        public void Rename(string path, string newName)
        {
            try
            {
                _logger.Debug("Rename() called...");
                if (string.IsNullOrEmpty(path))
                {
                    throw new ArgumentNullException("path");
                }

                if (string.IsNullOrEmpty(newName))
                {
                    throw new ArgumentNullException("newName");
                }

                using (GetHttpWebResponse("/" + EscapeDataString(path) + "?newObjectName=" + EscapeDataString(newName), "DELETE"))
                {
                }
            }
            catch (Exception exception)
            {
                _logger.Error("Exception thrown in Rename()", exception);
                throw;
            }
        }

        public void Unlock(string path, bool objectMustExist)
        {
            try
            {
                _logger.Debug("Unlock() called...");
                if (string.IsNullOrEmpty(path))
                {
                    throw new ArgumentNullException("path");
                }

                byte[] array = new byte[0];
                using (GetHttpWebResponse("/" + EscapeDataString(path) + "/lock?objectMustExist=" + objectMustExist, "DELETE"))
                {
                }
            }
            catch (Exception exception)
            {
                _logger.Error("Exception thrown in Unlock()", exception);
                throw;
            }
        }

        public UnlockDescendentsResult UnlockDescendents(string folderPath)
        {
            try
            {
                _logger.Debug("UnlockDescendents() called...");
                byte[] array = new byte[0];
                return GetResponseObject<UnlockDescendentsResult>("/" + EscapeDataString(folderPath) + "/descendent/locks", "DELETE");
            }
            catch (Exception exception)
            {
                _logger.Error("Exception thrown in UnlockDescendents()", exception);
                throw;
            }
        }

        private static string EscapeDataString(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                return "|";
            }

            string text = path.Replace("%", Uri.EscapeDataString("%")).Replace("#", Uri.EscapeDataString("#")).Replace("&", Uri.EscapeDataString("&"))
                .Replace("@", Uri.EscapeDataString("@"))
                .Replace("+", Uri.EscapeDataString("+"))
                .Replace("!", "%21")
                .Replace('/', '|')
                .Replace('\\', '|');
            text.TrimStart(default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char));
            text.TrimEnd(default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char), default(char));
            return text;
        }

        private void CopyMove(string srcPath, string destPath, bool replaceExisting, string pasteAction)
        {
            _logger.Debug("CopyMove() called...");
            byte[] data = new byte[0];
            using (GetHttpWebResponse("/" + EscapeDataString(srcPath) + "?destinationObjectPath=" + EscapeDataString(destPath) + "&pasteAction=" + pasteAction + "&replaceExisting=" + replaceExisting, "POST", data))
            {
            }
        }

        private HttpWebResponse GetHttpWebResponse(string relativeUri, string method, byte[] data = null)
        {
            _logger.Debug("GetHttpWebResponse() called. relativeUri = " + relativeUri + ", method" + method);
            WebRequest revitServerApiRequest = GetRevitServerApiRequest(relativeUri, method, data);
            Stopwatch stopwatch = Stopwatch.StartNew();
            _logger.Debug("Making HTTP " + method + " request to " + relativeUri + "...");
            HttpWebResponse result = (HttpWebResponse)revitServerApiRequest.GetResponse();
            stopwatch.Stop();
            _logger.Debug("HTTP Request completed in " + stopwatch.ElapsedMilliseconds + " ms");
            return result;
        }

        private T GetResponseObject<T>(string relativeUri, string method)
        {
            _logger.Debug("GetResponseObject<T>() called. relativeUri = " + relativeUri + ", method" + method);
            using HttpWebResponse httpWebResponse = GetHttpWebResponse(relativeUri, method);
            using Stream stream = httpWebResponse.GetResponseStream();
            using StreamReader streamReader = new StreamReader(stream, Encoding.UTF8);
            string text = streamReader.ReadToEnd();
            _logger.Debug("jsonStr = " + text);
            return Serializer.Deserialize<T>(text);
        }

        private WebRequest GetRevitServerApiRequest(string relativeUri, string method, byte[] data = null)
        {
            if (string.IsNullOrEmpty(relativeUri))
            {
                throw new ArgumentNullException("relativeUri");
            }

            if (string.IsNullOrEmpty(method))
            {
                throw new ArgumentNullException("method");
            }

            _logger.Debug("GetRevitServerApiRequest() construtor called. relativeUri = " + relativeUri + ", method = " + method);
            Uri uri = new Uri(BaseUri + relativeUri);
            _logger.Debug("Uri = " + uri.ToString());
            WebRequest webRequest = WebRequest.Create(uri);
            webRequest.Method = method;
            webRequest.Headers.Add("User-Name", Environment.UserName);
            webRequest.Headers.Add("User-Machine-Name", Environment.MachineName);
            webRequest.Headers.Add("Operation-GUID", Guid.NewGuid().ToString());
            if (data != null)
            {
                webRequest.GetRequestStream().Write(data, 0, data.Length);
            }

            return webRequest;
        }
    }
}

