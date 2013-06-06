using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    class WikipediaTestHelper
    {
        public static String GetRandomTestString()
        {
            var webRequest = (HttpWebRequest) WebRequest.Create(" http://en.wikipedia.org/wiki/Special:Random");
            webRequest.AllowAutoRedirect = true;
            using (var response = (HttpWebResponse)webRequest.GetResponse())
            {
                
                var responseStream = response.GetResponseStream();
                if (responseStream != null)
                {
                    using (var reader = new StreamReader(responseStream))
                    {
                        String responseText = reader.ReadToEnd();
                        return responseText;
                    }
                }
            }
            return null;

        }
    }
}
