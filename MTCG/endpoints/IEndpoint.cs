using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCG
{
    public interface IEndpoint
    {
        public bool canProcrss(Request request);
        public Response handleRequest(Request request);
    }
}
