using Moq;
using ObiletCase.Constants;
using ObiletCase.Interface;
using ObiletCase.Models;
using ObiletCase.Models.Request;
using ObiletCase.Services;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ObiletCase.Test
{
    public class CallApiServiceTest
    {
        [Fact]
        public async Task CallApi_Should_Return_Response_Successfully()
        {
            // Arrange
            var mockService = new Mock<ICallApiService>();
            var sampleRequest = new { test = "data" };

            var expectedResponse = new ResponseModel<SessionResponse> { Status = "Success"};

            mockService.Setup(x => x.CallApi<object, ResponseModel<SessionResponse>>("sample/path", It.IsAny<object>()))
                       .ReturnsAsync(expectedResponse);

            // Act
            var result = await mockService.Object.CallApi<object, ResponseModel<SessionResponse>>("sample/path", sampleRequest);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Success", result.Status);
        }

        [Fact]
        public async Task Real_CallApi_Should_Return_Valid_Response()
        {
            var service = new CallApiService(); // Gerçek servisi çağırır
            
            var bodyObject = new SessionRequestModel
            {
                Type = 1,
                Connection = new Connection
                {
                    IpAddress = ConnectionValues.IP_ADDRESS,
                    Port = ConnectionValues.PORT
                },
                Browser = new Browser
                {
                    Name = ConnectionValues.BROWSER_NAME,
                    Version = ConnectionValues.BROWSER_VERSION
                }
            };
            var result = await service.CallApi<SessionRequestModel, SessionResponse>(UrlPaths.GET_SESSION, bodyObject);
            
            Assert.NotNull(result);
        }
    }
}
