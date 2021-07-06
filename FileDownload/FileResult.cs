using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace FileDownload
{
    public class FileResult : IHttpActionResult
    {
        public byte[] FileContents { get; }

        public string ContentType { get; set; }

        public string FileName { get; }

        public FileResult(byte[] fileContents, string contentType, string fileName)
        {
            FileContents = fileContents;
            ContentType = contentType;
            FileName = fileName;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new ByteArrayContent(FileContents);
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment") { FileName = FileName };
            response.Content.Headers.ContentType = new MediaTypeHeaderValue(ContentType);
           
            return Task.FromResult(response);
        }
    }
}