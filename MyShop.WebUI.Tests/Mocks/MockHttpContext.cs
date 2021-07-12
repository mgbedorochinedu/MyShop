using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MyShop.WebUI.Tests.Mocks
{
    public class MockHttpContext : HttpContextBase
    {
        private MockRequest request;
        private MockResponse  response;
        private HttpCookieCollection  cookie;
        private IPrincipal FakeUser; 

        public MockHttpContext()
        {
            cookie = new HttpCookieCollection();
            this.request = new MockRequest(cookie);
            this.response = new MockResponse(cookie);
        }

        //Created override that return our 'FakeUser'
        public override IPrincipal User 
        {
            get
            {
                return this.FakeUser; 
            }
            set
            {
                this.FakeUser = value; 
            }
        }

        public override HttpRequestBase Request
        {
            get
            {
                return request;
            }
        }
        public override HttpResponseBase Response
        {
            get
            {
                return response;
            }
        }
    }

        public class MockResponse : HttpResponseBase
        {
            private readonly HttpCookieCollection cookies;

            public MockResponse(HttpCookieCollection cookies)
            {
                this.cookies = cookies;
            }
            public override HttpCookieCollection Cookies
            {
                get
                {
                    return cookies;
                }
            }
        }

        public class MockRequest : HttpRequestBase
        {
            private readonly HttpCookieCollection cookies;

            public MockRequest(HttpCookieCollection cookies)
            {
                this.cookies = cookies;
            }
            public override HttpCookieCollection Cookies
            {
                get
                {
                    return cookies;
                }
            }
        }

}
