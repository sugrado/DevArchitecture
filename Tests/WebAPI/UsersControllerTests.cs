﻿using System.Collections.Generic;
using System.Net;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Core.CrossCuttingConcerns.Caching;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using FluentAssertions;
using NUnit.Framework;
using Tests.Helpers;
using Tests.Helpers.Token;

namespace Tests.WebAPI
{
    [TestFixture]
    public class UsersControllerTests : BaseIntegrationTest
    {
        [Test]
        public async Task GetAll()
        {
            const string authenticationScheme = "Bearer";
            const string requestUri = "api/users/getall";

            // Arrange
            var token = MockJwtTokens.GenerateJwtToken(ClaimsData.GetClaims());
            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(authenticationScheme, token);
            var cache = new MemoryCacheProvider();

            await cache.SetAsync($"{CacheKeys.UserIdForClaim}=1", "GetUsersQuery", System.TimeSpan.FromMinutes(10));

            // Act
            var response = await HttpClient.GetAsync(requestUri);

            // Assert
            response.StatusCode.Should()?.Be(HttpStatusCode.OK);
        }
    }
}