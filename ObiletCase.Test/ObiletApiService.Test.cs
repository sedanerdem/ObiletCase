using Moq;
using ObiletCase.Constants;
using ObiletCase.Interface;
using ObiletCase.Models;
using ObiletCase.Models.Request;
using ObiletCase.Models.Response;
using ObiletCase.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ObiletCase.Test
{
    public class ObiletApiServiceTest
    {
        private readonly Mock<ICallApiService> _mockCallApi;
        private readonly Mock<ICacheService> _mockCache;
        private readonly Mock<ILogService> _mockLog;

        private readonly ObiletApiService _service;

        private readonly SessionResponse _mockSessionResponse = new SessionResponse
        {
            Status = Status.SUCCESS,
            Message = "OK",
            Data = new DeviceSession { SessionId = "mockSession" }
        };

        public ObiletApiServiceTest()
        {
            _mockCallApi = new Mock<ICallApiService>();
            _mockCache = new Mock<ICacheService>();
            _mockLog = new Mock<ILogService>();

            _mockCallApi.Setup(x => x.CallApi<SessionRequestModel, SessionResponse>(
                    It.IsAny<string>(), It.IsAny<SessionRequestModel>()))
                .ReturnsAsync(_mockSessionResponse);

            _service = new ObiletApiService(_mockCallApi.Object, _mockCache.Object, _mockLog.Object);
        }

        [Fact]
        public async Task GetSessionWithCacheAsync_ShouldReturnSession_WhenNotCached()
        {
            _mockCache.Setup(x => x.ExistsAsync(RedisKeys.SESSION)).ReturnsAsync(false);
            _mockCallApi.Setup(x => x.CallApi<SessionRequestModel, SessionResponse>(
                It.IsAny<string>(), It.IsAny<SessionRequestModel>()))
                .ReturnsAsync(_mockSessionResponse);

            var service = new ObiletApiService(_mockCallApi.Object, _mockCache.Object, _mockLog.Object);
            var result = await service.GetSessionWithCacheAsync();

            Assert.NotNull(result);
            Assert.Equal(Status.SUCCESS, result.Status);
            _mockLog.Verify(x => x.Info(It.Is<string>(s => s.Contains("cachelendi"))), Times.AtLeastOnce);
        }

        [Fact]
        public async Task GetSessionWithCacheAsync_ShouldReturnSession_FromCache()
        {
            _mockCache.Setup(x => x.ExistsAsync(RedisKeys.SESSION)).ReturnsAsync(true);
            _mockCache.Setup(x => x.GetAsync<SessionResponse>(RedisKeys.SESSION)).ReturnsAsync(_mockSessionResponse);

            var service = new ObiletApiService(_mockCallApi.Object, _mockCache.Object, _mockLog.Object);
            var result = await service.GetSessionWithCacheAsync();

            Assert.NotNull(result);
            Assert.Equal("mockSession", result.Data.SessionId);
            _mockLog.Verify(x => x.Info(It.Is<string>(s => s.Contains("cacheden getirildi"))), Times.AtLeastOnce);
        }

        [Fact]
        public async Task GetBusLocationsAsync_ShouldReturnLocations()
        {
            var expected = new ResponseModel<List<LocationDataModel>>
            {
                Status = Status.SUCCESS,
                Data = new List<LocationDataModel> { new LocationDataModel { Id = 1, Name = "Ankara" } }
            };

            _mockCallApi.Setup(x =>
                x.CallApi<RequestModel<string>, ResponseModel<List<LocationDataModel>>>(
                    It.IsAny<string>(), It.IsAny<RequestModel<string>>()))
                .ReturnsAsync(expected);


            var result = await _service.GetBusLocationsAsync("ank");
            
            Assert.NotNull(result);
            Assert.Single(result.Data);
            Assert.Equal("Ankara", result.Data[0].Name);
        }

        [Fact]
        public async Task GetJourneysAsync_ShouldThrowException_OnFailure()
        {
            var response = new ResponseModel<List<JourneyDataModel>>
            {
                Status = Status.FAIL,
                UserMessage = "API error"
            };

            _mockCallApi.Setup(x =>
                x.CallApi<RequestModel<JourneyDataModel>, ResponseModel<List<JourneyDataModel>>>(
                    It.IsAny<string>(), It.IsAny<RequestModel<JourneyDataModel>>()))
                .ReturnsAsync(response);

            var journey = new JourneyDataModel();
            var ex = await Assert.ThrowsAsync<Exception>(() => _service.GetJourneysAsync(journey));
            Assert.Contains("API error", ex.Message);
        }

    }
}
