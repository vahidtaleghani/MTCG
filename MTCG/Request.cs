using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCG
{
    public class Request
    {
        public enum METHODE { GET,POST,PUT,DELETE}
        private METHODE method;
        private String path; //ToDo: URL
        private String httpVersion;
        private String payload;
        public Hashtable httpHeaders = new Hashtable();
        private Stream inputStream;
        private static int MAX_POST_SIZE = 10 * 1024 * 1024; // 10MB
        public Request(StreamReader inputStream)
        {
            try
            {
                StreamReader streamreader = inputStream;
                if (streamreader.EndOfStream)
                    throw new Exception("request is Empty");

                String line = streamreader.ReadLine();
                if (line == "")
                    throw new Exception("line is empty");
                
                string[] tokens = line.Split(' ');
                if (tokens.Length != 3)
                    throw new Exception("invalid http request line");

                if (!Enum.TryParse(typeof(METHODE), tokens[0], out object? method))
                    throw new Exception("invalid methode");
                
                method = (METHODE)method;
                this.path = tokens[1];
                this.httpVersion = tokens[2];

                // print request in Console
                Console.WriteLine("Request:" + method);
                Console.WriteLine("method: " + method);
                Console.WriteLine("path: " + this.path);
                Console.WriteLine("httpVersion: " + this.httpVersion);

                //read header
                while (!streamreader.EndOfStream && (line = streamreader.ReadLine()!) != "")
                {
                    readHeader(line);
                }
            }
            catch (Exception exc)
            {
                Console.WriteLine("error occurred: " + exc.Message);
                throw;
            }
        }
        public void readHeader(String line)
        {
            int separator = line.IndexOf(':');

            if (separator == -1)
                throw new Exception("invalid http header line: " + line);

            String name = line.Substring(0, separator);
            int pos = separator + 1;

            while ((pos < line.Length) && (line[pos] == ' '))
                pos++; // strip any spaces

            string value = line.Substring(pos, line.Length - pos);
            Console.WriteLine("header: {0}:{1}", name, value);
            httpHeaders[name] = value;

            if (this.httpHeaders.ContainsKey("Content-Type"))
            {
                int content_len = Convert.ToInt32(this.httpHeaders["Content-Length"]);

                if (content_len > MAX_POST_SIZE || content_len <= 0)
                {
                    throw new Exception(
                        String.Format("POST Content-Length({0}) empty or too big for this simple server",
                          content_len));
                }

                byte[] buf = new byte[content_len];
                int hasRead = this.inputStream.Read(buf, 0, content_len);

                if (hasRead != content_len)
                    throw new Exception("Payload was not the expected lenght " + hasRead);

                this.payload = System.Text.Encoding.Default.GetString(buf);
                //ToDo
                Console.WriteLine("payload:" + this.payload);
            }
        }
        public bool isValid()
        {
            if (method != null && this.path != null && this.httpVersion != null)
                return true;
            else
                return false;
        }
    }
}
