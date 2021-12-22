using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TodoApps.Enties;

namespace TodoApps.DAO
{
    public class APIResponseData
    {
        protected WebClient webClient_;
        protected string address_;
        public APIResponseData()
        {
            webClient_ = new WebClient();
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.ServerCertificateValidationCallback = delegate
            { return true; };
            webClient_.BaseAddress = ConfigurationManager.AppSettings.Get("APIAddress");
            webClient_.Encoding = Encoding.UTF8;
        }

        public List<T> GetData<T>(string address)
        {
            try
            {
                var json = webClient_.DownloadString(address);
                return JsonConvert.DeserializeObject<List<T>>(json);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra khi tải dữ liệu, vui lòng kiểm tra lại đường truyền: " + Environment.NewLine + ex.Message, "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }

        public bool Create<T>(Todo todo, string address)
        {
            try
            {
                JObject jObject = new JObject(new JProperty("task", todo.task));

                var json = JsonConvert.SerializeObject(jObject);
                webClient_.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                webClient_.UploadString(address, json);
                //var uplaod = webClient_.UploadString(address, jObject.ToString());
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra khi tải dữ liệu, vui lòng kiểm tra lại đường truyền: " + Environment.NewLine + ex.Message, "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        public bool Edit<T>(Todo todo, string address)
        {
            try
            {
                JObject jObject = new JObject(new JProperty("task", todo.task));
                var json = JsonConvert.SerializeObject(jObject);
                webClient_.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                var result = webClient_.UploadString(address, json);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra khi tải dữ liệu, vui lòng kiểm tra lại đường truyền: " + Environment.NewLine + ex.Message, "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        public bool Delete<T>(string address)
        {
            try
            {
                var result = webClient_.UploadString(address, "");
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra khi tải dữ liệu, vui lòng kiểm tra lại đường truyền: " + Environment.NewLine + ex.Message, "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }
    }
}
