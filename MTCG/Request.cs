using System;
using System.Collections;
using System.IO;


namespace MTCG
{
    public class Request
    {
        public enum METHODE { GET,POST,PUT,DELETE}
        
        public String path;
        public Hashtable httpHeaders = new Hashtable();
        private String httpVersion;
        private String payload;
        public METHODE method;

        
        public METHODE getMethode()
        {
            return this.method;
        }
        public String getContentType()
        {
            String cT = (String)this.httpHeaders["Content-Type"];
            return cT;
        }
        public String getPayload()
        {
            return this.payload;
        }
        public Hashtable getHeaders()
        {
            return this.httpHeaders;
        }
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
                
                this.method = (METHODE)method;
                this.path = tokens[1];
                this.httpVersion = tokens[2];

                // print request in Console
                Console.WriteLine("  method: " + this.method);
                Console.WriteLine("  path: " + this.path);
                //Console.WriteLine("httpVersion: " + this.httpVersion);

                //read header
                while (!streamreader.EndOfStream && (line = streamreader.ReadLine()!) != "")
                {
                    String[] split = line.Split(": ", 2);
                    if (split.Length == 2)
                    {
                        this.httpHeaders.Add(split[0], split[1]);
                    }
                }

                if (this.httpHeaders.ContainsKey("Content-Type") && this.httpHeaders.ContainsKey("Content-Length"))
                {
                    int content_len = Convert.ToInt32(this.httpHeaders["Content-Length"]);
                    char[] buf = new char[content_len];
                    int hasRead = inputStream.Read(buf, 0,content_len);
                    if (hasRead != content_len)
                        throw new Exception("Payload was not the expected lenght " + hasRead);
                    this.payload = new String(buf);
                    //ToDo
                    //Console.WriteLine("payload:" + this.payload);
                }
                
            }
            catch (Exception exc)
            {
                Console.WriteLine("error occurred: " + exc.Message);
                throw;
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
