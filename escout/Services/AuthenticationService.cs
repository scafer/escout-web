using escout.Helpers;
using escout.Models;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace escout.Services
{
    public class AuthenticationService
    {
        private string token;

        public AuthenticationService() => this.token = string.Empty;

        public AuthenticationService(string token) => this.token = token;

        public async Task<AuthData> SignInExecuted(User user)
        {
            AuthData auth = new AuthData();
            if (!(string.IsNullOrEmpty(user.Username)) && !(string.IsNullOrEmpty(user.Password)))
            {
                try
                {
                    var response = await new RestConnector(token).PostObjectAsync(RestConnector.SIGN_IN,
                        new User(user.Username, RestUtils.GenerateSha256String(user.Password), string.Empty, false));
                    if (!string.IsNullOrEmpty(response))
                    {
                        var result = JsonConvert.DeserializeObject<AuthData>(response);

                        if (!string.IsNullOrEmpty(result.Token))
                            auth = result;
                        else
                            Console.WriteLine("Error", "Invalid username or password.", "Ok");
                    }
                    else
                        Console.WriteLine("Error:", "Something is wrong.", "Ok");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
            else
                Console.WriteLine("Error", "Invalid username or password.", "Ok");

            return auth;
        }

        public async Task<bool> SignUpExecuted(User user, bool isNotifications)
        {
            bool returnResult = false;
            if (!(string.IsNullOrEmpty(user.Username) && string.IsNullOrEmpty(user.Email) && string.IsNullOrEmpty(user.Password)))
            {
                try
                {
                    var response = await new RestConnector(token).PostObjectAsync(RestConnector.SIGN_UP,
                        new User(user.Username, RestUtils.GenerateSha256String(user.Password), user.Email, isNotifications));
                    if (!string.IsNullOrEmpty(response))
                    {
                        var result = JsonConvert.DeserializeObject<SvcResult>(response);
                        Console.WriteLine("Result", result.ErrorMessage, "OK");

                        if (result.ErrorCode == 0)
                            returnResult = true;
                    }
                    else
                    {
                        Console.WriteLine("Error:", "Something is wrong.", "OK");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
            else
                Console.WriteLine("Error:", "Something is wrong.", "OK");

            return returnResult;
        }

        public async Task<bool> ForgotPasswordExecuted(User user)
        {
            bool returnResult = false;

            if (!(string.IsNullOrEmpty(user.Username)) || !(string.IsNullOrEmpty(user.Email)))
            {
                try
                {
                    var response = await new RestConnector(token).PostObjectAsync(RestConnector.RESET_PASSWORD, user);
                    if (!string.IsNullOrEmpty(response))
                    {
                        var result = JsonConvert.DeserializeObject<SvcResult>(response);
                        Console.WriteLine("Message", result.ErrorMessage, "Ok");
                        returnResult = true;
                    }
                    else
                    {
                        Console.WriteLine("Error:", "Something is wrong.", "OK");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
            else
                Console.WriteLine("Warning", "Invalid username or email.", "Ok");

            return returnResult;
        }

        public async Task<bool> IsAuthenticated()
        {
            var response = await new RestConnector(token).GetObjectAsync(RestConnector.AUTHENTICATED);
            if (!string.IsNullOrEmpty(response))
                return false;
            else
                return true;
        }
    }
}
