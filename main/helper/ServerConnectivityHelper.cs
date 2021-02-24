using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Web;
using System.Security;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;

using Newtonsoft.Json;
 
namespace safewalk.helper
{
	public class ServerConnectivityHelper : IServerConnectivityHelper
	{
		private static readonly int DEFAULT_TIMEOUT = 30000;
		private readonly int readWriteTimeout;
    
		private readonly String host;
		private readonly long port;
		private readonly bool setBypassSSLCertificate;

		public ServerConnectivityHelper(String host, long port)
		{
			this.host = host;
			this.port = port;
			this.setBypassSSLCertificate = false;
			this.readWriteTimeout = DEFAULT_TIMEOUT;
		}

		public ServerConnectivityHelper(String host, long port, bool byPassSSLCertificate)
		{
			this.host = host;
			this.port = port;
			this.setBypassSSLCertificate = byPassSSLCertificate;
			this.readWriteTimeout = DEFAULT_TIMEOUT;
		}

		public ServerConnectivityHelper(String host, long port, bool byPassSSLCertificate, int readWriteTimeout)
		{
			this.host = host;
			this.port = port;
			this.setBypassSSLCertificate = byPassSSLCertificate;
			this.readWriteTimeout = readWriteTimeout;
		}
		#region "Publics"
		public Response post(String path, Dictionary<string,string> parameters, Dictionary<string,string> headers) 
		{
			return doRequest("POST", path, parameters, headers);		
		}

		public Response put(String path, Dictionary<string,string> parameters, Dictionary<string,string> headers)
		{
			return doRequest("PUT", path, parameters, headers);
		}
    
		public Response get(String path, Dictionary<string,string> parameters, Dictionary<string,string> headers)
		{
			return doRequest("GET", path, parameters, headers);
		}
    
		public Response delete(String path, Dictionary<string,string> parameters, Dictionary<string,string> headers)
		{
			return doRequest("DELETE", path, parameters, headers);
		}
		#endregion

		#region "privates"
		private Response doRequest(String method, String path, Dictionary<string,string> parameters,  
			Dictionary<string,string> headers)
		{
			try {
				string _path = this.host + ":" + this.port + path;

				HttpWebRequest request;

				if (this.setBypassSSLCertificate)
                {
					var certificate = new X509Certificate2();
					ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(AcceptAllCertifications);
					ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

					request = (HttpWebRequest)WebRequest.Create(_path);
					request.ClientCertificates.Add(certificate);
                }
				else
                {	
					request = (HttpWebRequest)WebRequest.Create(_path);
				}
				
				switch (method) {
					case "POST":
						request.Method = WebRequestMethods.Http.Post;
						break;
					case "GET":
						request.Method = WebRequestMethods.Http.Get;
						break;
					case "PUT":
						request.Method = WebRequestMethods.Http.Put;
						break;
					case "DELETE":
						request.Method = "DELETE";
                    	break;
				}
				request.ContentType = "application/x-www-form-urlencoded";
				request.Timeout = DEFAULT_TIMEOUT;				
				request.ReadWriteTimeout = this.readWriteTimeout;
				request.Accept = "application/json";

				foreach (KeyValuePair<string, string> header in headers) {
					request.Headers.Add(header.Key, header.Value);    
				}

				byte[] buffer = Encoding.UTF8.GetBytes(this.urlEncode(parameters));
            
				if (request.Method == WebRequestMethods.Http.Put || request.Method == WebRequestMethods.Http.Post) {
					request.ContentLength = buffer.Length;
					//We open a stream for writing the postvars
					Stream PostData = request.GetRequestStream();
					PostData.Write(buffer, 0, buffer.Length);
					PostData.Close();
				}
            
				//Get the response handle, we have no true response yet!
				HttpWebResponse _response = (HttpWebResponse)request.GetResponse();

				HttpStatusCode responseCode = this.getResponseCode(_response);
				
				Stream stream = _response.GetResponseStream();
				StreamReader reader = new StreamReader(stream, Encoding.UTF8);
				String responseString = reader.ReadToEnd();
        
				return new Response(responseString, (int)responseCode);
			} catch (IOException e) {
				throw new ConnectivityException(e);
			} catch (WebException webException) {
				// .NET will throw a System.Net.WexException on StatusCodes
				// other than 200. You can catch that exception and retain 
				// the StatusCode for error handling.
				if (webException.Status ==
				            System.Net.WebExceptionStatus.ProtocolError) {
					// Protocol Error, you can read the response and handle 
					// the error based on the StatusCode.
					var exceptionResponse =
						webException.Response as System.Net.HttpWebResponse;
					if (exceptionResponse != null) {
						var exceptionResponseValue = string.Empty;
						using (var exceptionResponseStream =
							                     exceptionResponse.GetResponseStream()) {
							if (exceptionResponseStream != null) {
								using (var exceptionReader =
									                           new System.IO.StreamReader(
										                           exceptionResponseStream)) {
									exceptionResponseValue =
                                  exceptionReader.ReadToEnd();
								}
							}
						}
						// Enumerated Protocol Error.
						int statusCode = (int)exceptionResponse.StatusCode;
						string statusDescription = exceptionResponse.StatusDescription;
						string responseBody = exceptionResponseValue;
						
						return new Response(responseBody, statusCode);
					} 
				} else if (webException.Status == System.Net.WebExceptionStatus.ConnectFailure) {
					var exceptionResponse =
						webException.Response as System.Net.HttpWebResponse;
					if (exceptionResponse != null) {
						var exceptionResponseValue = string.Empty;
						using (var exceptionResponseStream =
							                     exceptionResponse.GetResponseStream()) {
							if (exceptionResponseStream != null) {
								using (var exceptionReader =
									                           new System.IO.StreamReader(
										                           exceptionResponseStream)) {
									exceptionResponseValue =
                                  exceptionReader.ReadToEnd();
								}
							}
						}
						// Enumerated Protocol Error.
						int statusCode = (int)exceptionResponse.StatusCode;
						string statusDescription = exceptionResponse.StatusDescription;
						string responseBody = exceptionResponseValue;
						return new Response(responseBody, statusCode);
					} else {
						return new Response("Unknown ConnectFailure Error", 404);
					}
				} 
				throw new ConnectivityException(webException);
			} catch (Exception e) {
				throw new ConnectivityException(e);
			}
		}


		public static bool AcceptAllCertifications(object sender, System.Security.Cryptography.X509Certificates.X509Certificate certification, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
		{
			return true;
		}

		private HttpStatusCode getResponseCode(HttpWebResponse response)
		{
			HttpStatusCode responseCode;

			try {
				responseCode = response.StatusCode;
			} catch (IOException e) {
				responseCode = HttpStatusCode.BadRequest;
			}
			return responseCode;
		}
    
		private String urlEncode(Dictionary<string,string> query)
		{
			StringBuilder builder = new StringBuilder();

        
			foreach (KeyValuePair<string, string> entry in query) {
				builder.Append(entry.Key);
				builder.Append("=");
				builder.Append(HttpUtility.UrlEncode(entry.Value, Encoding.UTF8));
				builder.Append("&");
			}

			builder.ToString();

			return builder.ToString();
		}

		#endregion

	}
}
