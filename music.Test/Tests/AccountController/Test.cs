using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using music.Api.Common;
using music.Api.Dtos;
using music.Domain.Entities;
using music.Test.Common;
using music.Test.Tests.AccountController.Data;
using Xunit;

namespace music.Test.Tests.AccountController
{
    public class Test : ClientProvider
    {
        [Theory]
        [ClassData(typeof(CreateAccountData))]
        //tested.. works fine
        public async Task CreateAccountTest(RegisterUserDto newUser)
        {
            var req = await Client.PostAsJsonAsync("api/account" , newUser) ; 
            var content = await req.Content.ReadAsAsync<Response<bool>>() ; 
            content.Item.Should().Be(true) ; 
        }
        [Theory]
        [InlineData("sajad564" , "123sajad")]
        //tested..works fine
        public async Task LoginTest(string Username , string Password)
        {
            var loginDto = new LoginDto {Username = Username , Password = Password};
            var req = await Client.PostAsJsonAsync("api/account/login" , loginDto) ; 
            var content = await req.Content.ReadAsAsync<Response<Token>>() ; 
            content.Item.Should().Be(true) ; 
        }
    }
}