using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RecurringIntegrationApp
{
    /// <summary>
    /// Helper class for Enqueue/ Job status http requests
    /// </summary>
    class HttpClientHelper
    {
        /// <summary>
        /// Post request
        /// </summary>
        /// <param name="uri">Enqueue endpoint URI</param>
        /// <param name="authenticationHeader">Authentication header</param>
        /// <param name="bodyStream">Body stream</param>        
        /// <param name="message">ActivityMessage context</param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> SendPostRequestAsync(Uri uri, Stream bodyStream, string externalCorrelationHeaderValue = null)
        {
            using (HttpClientHandler handler = new HttpClientHandler() { UseCookies = false })
            {
                using (HttpClient httpClient = new HttpClient(handler))
                {
                    httpClient.DefaultRequestHeaders.Authorization = AuthenticationHelper.GetValidAuthenticationHeader();

                    // Add external correlation id header id specified and valid
                    if (!string.IsNullOrEmpty(externalCorrelationHeaderValue))
                    {
                        httpClient.DefaultRequestHeaders.Add(Program.ExternalCorrelationHeader, externalCorrelationHeaderValue);
                    }

                    if (bodyStream != null)
                    {
                        using (StreamContent content = new StreamContent(bodyStream))
                        {
                            return await httpClient.PostAsync(uri, content);
                        }
                    }
                }
            }

            return new HttpResponseMessage()
            {
                Content = new StringContent("Request failed at client.", Encoding.ASCII),
                StatusCode = System.Net.HttpStatusCode.PreconditionFailed
            };
        }

        /// <summary>
        /// Http Get requests for use with JobStatus API
        /// </summary>
        /// <param name="uri">Request URI</param>
        /// <returns>Task of type HttpResponseMessage</returns>
        public async Task<HttpResponseMessage> GetRequestAsync(Uri uri)
        {
            HttpResponseMessage responseMessage;

            using (HttpClientHandler handler = new HttpClientHandler() { UseCookies = false })
            {
                using (HttpClient httpClient = new HttpClient(handler))
                {
                    httpClient.DefaultRequestHeaders.Authorization = AuthenticationHelper.GetValidAuthenticationHeader();

                    responseMessage = await httpClient.GetAsync(uri).ConfigureAwait(false);
                }
            }
            return responseMessage;
        }

        /// <summary>
        /// Get the Enqueue URI
        /// </summary>
        /// <returns>Enqueue URI</returns>
        public Uri GetEnqueueUri()
        {
            //access the Connector API
            UriBuilder enqueueUri = new UriBuilder(Settings.RainierUri);
            enqueueUri.Path = Program.EnqueueRelativePath + Settings.RecurringJobId;

            // Data package
            if (Settings.IsDataPackage && !string.IsNullOrEmpty(Settings.Company))
            {
                enqueueUri.Query = "company=" + Settings.Company;
            }

            // Individual file
            else
            {
                string enqueueQuery = "entity=" + Settings.EntityName;
                // Append company if specified
                if (!string.IsNullOrEmpty(Settings.Company))
                {
                    enqueueQuery += "&company=" + Settings.Company;
                }

                enqueueUri.Query = enqueueQuery;
            }

            return enqueueUri.Uri;
        }
    }
}