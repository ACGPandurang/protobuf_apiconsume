using ProtoBuf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace TestApp
{
    class WebHelper
    {
        public async Task<ProtobufModelDto> GetProtoBufData()
        {
            ProtobufModelDto protobufModelDto = new ProtobufModelDto();
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-protobuf"));

                string sURL = "http://192.168.0.102:45455/api/values/4";
                HttpResponseMessage httpResponse = await client.GetAsync(sURL);

                if (httpResponse.StatusCode == HttpStatusCode.OK)
                {

                    var streamData = httpResponse.Content.ReadAsStreamAsync().Result;

                    protobufModelDto = Serializer.Deserialize<ProtobufModelDto>(streamData);


                    // Parse the response body. Blocking!
                    //var p = httpResponse.Content.ReadAsAsync<ProtobufModelDto>(new[] { new ProtoBufFormatter() }).Result;
                    //Console.WriteLine("{0}\t{1};\t{2}", p.Name, p.StringValue, p.Id);
                }
                else
                {
                    Console.WriteLine("{0} ({1})", (int)httpResponse.StatusCode, httpResponse.ReasonPhrase);
                }

                return protobufModelDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<ProtobufModelDto> PostProtoBufData()
        {
            string sURL = "http://192.168.0.102:45455/api/values";
            try
            {
                var client = new HttpClient();

                var request = new HttpRequestMessage(HttpMethod.Post, sURL);
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-protobuf"));

                var protobufModelDto = new ProtobufModelDto
                {
                    Id = 1234,
                    Name = "ganesh gaikwad",
                    StringValue = "reader3@abc.com"
                };

                using (MemoryStream stream = new MemoryStream())
                {
                    ProtoBuf.Serializer.Serialize(stream, protobufModelDto);
                    request.Content = new ByteArrayContent(stream.ToArray());

                    var response = client.SendAsync(request).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var reader = ProtoBuf.Serializer.Deserialize<ProtobufModelDto>(await response.Content.ReadAsStreamAsync());

                        return reader;
                    }
                }

                return protobufModelDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
