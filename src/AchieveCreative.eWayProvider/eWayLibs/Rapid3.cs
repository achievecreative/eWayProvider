using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace AchieveCreative.eWayProvider.eWayLibs
{
    public class Rapid3
    {
        private static readonly Dictionary<string, string> _ewayErrorMessagesByCodes;

        static Rapid3()
        {
            string ewayErrorsFile = HttpContext.Current.Server.MapPath("~/App_Data/eway_err_msgs.cfg");

            _ewayErrorMessagesByCodes = new Dictionary<string, string>();
            if (!File.Exists(ewayErrorsFile)) return;

            foreach (string line in File.ReadAllLines(ewayErrorsFile))
            {
                var kvPair = line.Split(new[] { '=' });
                if (kvPair.Length != 2) continue;
                _ewayErrorMessagesByCodes[kvPair[0].Trim()] = kvPair[1].Trim();
            }
        }

        public static string GetEwayErrorMessage(string ewayErrorCode)
        {
            string errMsg;
            if (!_ewayErrorMessagesByCodes.TryGetValue(ewayErrorCode, out errMsg))
            {
                return ewayErrorCode;
            }
            return errMsg;
        }

        public static CreateAccessCodeResponse CreateAccessCode(CreateAccessCodeRequest request, IDictionary<string, string> settings)
        {

            // the REST style URL we are going to POST to to create an access code
            string url = string.Format("{0}/AccessCodes", settings["RapidAPI.URL"]);

            // convert our request into JSON
            string json = JsonConvert.SerializeObject(request, new StringEnumConverter());

            // create a web request to POST data to
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);

            // read username & password from web.config
            string username = settings["RapidAPI.APIKey"];
            string password = settings["RapidAPI.Password"];

            // add Basic HTTP Authentication
            webRequest.Headers.Add("Authorization", "Basic " + Convert.ToBase64String(Encoding.ASCII.GetBytes(username + ":" + password)));

            // configure request
            webRequest.Method = "POST";
            webRequest.ContentType = "application/json";
            webRequest.ContentLength = json.Length;

            // write our request to the output stream
            using (StreamWriter streamOut = new StreamWriter(webRequest.GetRequestStream(), System.Text.Encoding.ASCII))
                streamOut.Write(json);

            try
            {
                // read in the response from the server, deserializing into our type
                using (StreamReader streamIn = new StreamReader(webRequest.GetResponse().GetResponseStream()))
                    return JsonConvert.DeserializeObject<CreateAccessCodeResponse>(streamIn.ReadToEnd());
            }
            catch (WebException webException)
            {
                //string error = "Unable to create access code";
                string error = "Unable to create access code" + webException.Message;

                using (StreamReader stream = new StreamReader(webException.Response.GetResponseStream()))
                    error += stream.ReadToEnd();

                return new CreateAccessCodeResponse(error);
            }
        }

        internal static GetAccessCodeResultResponse GetAccessCodeResult(GetAccessCodeResultRequest request, IDictionary<string, string> settings)
        {
            // contact the server to get the response
            string url = string.Format("{0}/AccessCode/{1}", settings["RapidAPI.URL"], request.AccessCode);
            string error = "Unable to retreive access code result";
            string json = "{}";


            // create a webrequest
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);

            // configure basic authentication
            string username = settings["RapidAPI.APIKey"];
            string password = settings["RapidAPI.Password"];

            // add authentication to webRequest
            webRequest.Headers.Add("Authorization", "Basic " + Convert.ToBase64String(Encoding.ASCII.GetBytes(username + ":" + password)));

            // configure webRequest
            webRequest.Method = "GET";
            webRequest.ContentType = "application/json";

            try
            {
                // send & read in response
                using (StreamReader streamIn = new StreamReader(webRequest.GetResponse().GetResponseStream()))
                    json = streamIn.ReadToEnd();
            }
            catch (WebException webException)
            {
                // attempt to get any additional information from the server
                using (StreamReader stream = new StreamReader(webException.Response.GetResponseStream()))
                    error = stream.ReadToEnd();
            }

            // if we got a response..
            if (!string.IsNullOrEmpty(json))
            {
                // convert the text json to an object
                GetAccessCodeResultResponse result = JsonConvert.DeserializeObject<GetAccessCodeResultResponse>(json);
                return result;
            }
            else
            {
                GetAccessCodeResultResponse errorResult = new GetAccessCodeResultResponse();
                errorResult.ResponseCode = "error";
                errorResult.ResponseMessage = error;
                return errorResult;
            }
        }
    }
}