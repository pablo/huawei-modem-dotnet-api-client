using huaweisms.http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Mime;
using System.Xml;

namespace huaweisms.data {

    public class ApiConfig
    {

        public ApiConfig()
        {
            this.BaseURL = "http://192.168.8.1/api";
            this.HttpTimeout = 10000;
        }

        public string BaseURL
        {
            get; set;
        }

        public int HttpTimeout
        {
            get; set;
        }

    }

    public class ApiResponse
    {

        public enum ApiResponseType
        {
            XML,
            TEXT,
            JSON,
            HTML,
            UNDEFINED
        }


        private ApiResponse()
        {
            Response = new Dictionary<string, object>();
        }

        public Dictionary<string, object> Response
        {
            get; private set;
        }

        public ApiResponseType Type
        {
            get; private set;
        }

        public ContentType ContentType
        {
            get; private set;
        }

        private void ParseXmlResponse(string xml)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(xml);

            foreach(XmlNode node in xmlDocument.DocumentElement.ChildNodes)
            {
                if (node.NodeType == XmlNodeType.Element)
                {
                    Response[node.Name] = node.InnerText;
                }
            }
        }

        private void ParseTextResponse(string text)
        {

            Response["data"] = text;
        }

        public static ApiResponse ReadAndCreateApiResponse(HttpResponseMessage response)
        {
            ApiResponse ret = new ApiResponse();

            IEnumerable<string> contentTypes;
            if (response.Headers.TryGetValues("Content-Type", out contentTypes))
            {
                var contentType = new ContentType(contentTypes.First());
                ret.ContentType = contentType;
                if (contentType.MediaType.Contains("text/xml"))
                {
                    ret.Type = ApiResponseType.XML;
                }
                else if (contentType.MediaType.Contains("text/plain"))
                {
                    ret.Type = ApiResponseType.TEXT;
                }
                else if (contentType.MediaType.Contains("text/html"))
                {
                    ret.Type = ApiResponseType.HTML;
                }
                else if (contentType.MediaType.Contains("application/json"))
                {
                    ret.Type = ApiResponseType.JSON;
                }
            } else
            {
                // if Content-Type not set, assume XML
                ret.ContentType = new ContentType("text/xml; charset=utf-8");
                ret.Type = ApiResponseType.XML;
            }

            switch (ret.Type)
            {
                case ApiResponseType.XML:
                    ret.ParseXmlResponse(response.Content.ReadAsStringAsync().Result);
                    break;
                case ApiResponseType.TEXT:
                    ret.ParseTextResponse(response.Content.ReadAsStringAsync().Result);
                    break;
                default:
                    throw new Exception("Response Type: " + ret.Type + " currently not supported");
            }

            return ret;
        }

        public ApiResponse(HttpContent httpContent)
        {

        }


    }

    public class ApiCtx
    {

        public ApiCtx(ApiConfig config)
        {
            this.Config = config;
            this.Session = new HttpSession(config);
            this.LoggedIn = false;
        }

        public ApiConfig Config
        {
            get; private set;
        }

        public HttpSession Session
        {
            get; private set;
        }

        public string SessionId
        {
            get; set;
        }

        public string TokInfo
        {
            get; set;
        }

        public bool LoggedIn
        {
            get; set;
        }

    }

}