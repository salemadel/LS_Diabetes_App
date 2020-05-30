using LS_Diabetes_App.Models;
using LS_Diabetes_App.Servies;
using Newtonsoft.Json;
using Plugin.Connectivity;
using Plugin.SecureStorage;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace LS_Diabetes_App.Api
{
    public class RestApi : ViewModelBase
    {
        private readonly string ipAdress = "192.168.100.5:5000";
        public async Task<(bool, string)> EmailCheck(string email)
        {
            string error_msg = null;
            bool Succes = false;
            if (true)
            {
                var keyvalues = new List<KeyValuePair<string, string>>{
                new KeyValuePair<string, string>("email" , email),
              
            };
                try
                {
                    var request = new HttpRequestMessage(HttpMethod.Post, "http://" + ipAdress + "/api/auth/check")
                    {
                        Content = new FormUrlEncodedContent(keyvalues)
                    };
                    var client = new HttpClient();
                    client.Timeout = TimeSpan.FromSeconds(20);
                    var responce = await client.SendAsync(request);
                    var json = await responce.Content.ReadAsStringAsync();

                    dynamic obj = JsonConvert.DeserializeObject(json);
                    
                    if (obj.exists == false)
                    {
                        Succes = true;
                    }
                    else
                    {
                      
                        error_msg = "Email already existe";
                    }
                }
                catch (Exception ex)
                {
                    error_msg = ex.Message;
                }
            }
            else
            {
                error_msg = Resources["InternetMessage"];
            }

            return (Succes, error_msg);
        }
        public async Task<(bool,bool, string)> SocialChack(string network , string id)
        {
            string error_msg = null;
            bool Succes = false;
            bool existe = false;
            if (true)
            {
                var keyvalues = new List<KeyValuePair<string, string>>{
                new KeyValuePair<string, string>("network" , network),
                new KeyValuePair<string, string>("id" , id),
            };
                try
                {
                    var request = new HttpRequestMessage(HttpMethod.Post, "http://" + ipAdress + "/api/auth/social")
                    {
                        Content = new FormUrlEncodedContent(keyvalues)
                    };
                    var client = new HttpClient();
                    client.Timeout = TimeSpan.FromSeconds(20);
                    var responce = await client.SendAsync(request);
                    var json = await responce.Content.ReadAsStringAsync();

                    dynamic obj = JsonConvert.DeserializeObject(json);

                    if (obj.exists == false)
                    {
                        Succes = true;
                        existe = false;
                        
                    }
                    else
                    {

                        Succes = true;
                        existe = true;
                        string token = obj["token"];
                        token = token.Substring(7, token.Length - 7);
                        CrossSecureStorage.Current.SetValue("LogguedIn", "True");
                        CrossSecureStorage.Current.SetValue("acces_token", token);
                    }
                }
                catch (Exception ex)
                {
                    error_msg = ex.Message;
                }
            }
            else
            {
                error_msg = Resources["InternetMessage"];
            }

            return (Succes,existe , error_msg);
        }

        public async Task<(bool, string)> Resigter(Profil_Model user, string password)
        {
            string error_msg = null;
            bool Succes = false;
            if (true)
            {
                var keyvalues = new List<KeyValuePair<string, string>>{
                new KeyValuePair<string, string>("email" , user.Email),
                new KeyValuePair<string, string>("facebook_id" , user.Email),
                new KeyValuePair<string, string>("google_id" , user.Email),
                new KeyValuePair<string, string>("avatar" , user.Avatar),
                new KeyValuePair<string, string>("firstname" , user.FirstName),
                new KeyValuePair<string, string>("lastname" , user.LastName),
                new KeyValuePair<string, string>("birthday" , user.Birth_Date.ToString("yyyy-MM-dd")),
                new KeyValuePair<string, string>("password" , password),
                new KeyValuePair<string, string>("diabetestype" , user.DiabetesType),
                new KeyValuePair<string, string>("glucometer" , user.Glucometer),
                new KeyValuePair<string, string>("diagnisis_date" , user.Diagnostic_Year.ToString()),
                new KeyValuePair<string, string>("height" , user.Height.ToString()),
                new KeyValuePair<string, string>("sex" , user.Sexe),
                };


                try
                {
                    var request = new HttpRequestMessage(HttpMethod.Post, "http://" + ipAdress + "/api/auth/register")
                    {
                        Content = new FormUrlEncodedContent(keyvalues)
                    };
                    var client = new HttpClient();
                    client.Timeout = TimeSpan.FromSeconds(20);
                    var responce = await client.SendAsync(request);
                    var json = await responce.Content.ReadAsStringAsync();

                    dynamic obj = JsonConvert.DeserializeObject(json);

                    if (obj.registred == true)
                    {
                        Succes = true;

                        string token = obj["token"];
                        token = token.Substring(7, token.Length - 7);
                        CrossSecureStorage.Current.SetValue("LogguedIn", "True");
                        CrossSecureStorage.Current.SetValue("acces_token", token);
                    }
                    else
                    {

                        error_msg = obj.error;
                    }
                }
                catch (Exception ex)
                {
                    error_msg = ex.Message;
                }
            }
            else
            {
                error_msg = Resources["InternetMessage"];
            }

            return (Succes, error_msg);
        }
        public async Task<(bool, string)> Login(string username, string password)
        {
            string error_msg = null;
            bool Succes = false;
            if (true)
            {
                var keyvalues = new List<KeyValuePair<string, string>>{
                new KeyValuePair<string, string>("email" , username),
                new KeyValuePair<string, string>("password" , password),
               
            };
                try
                {
                    var request = new HttpRequestMessage(HttpMethod.Post, "http://" + ipAdress + "/api/auth/login")
                    {
                        Content = new FormUrlEncodedContent(keyvalues)
                    };
                    var client = new HttpClient();
                    client.Timeout = TimeSpan.FromSeconds(20);
                    var responce = await client.SendAsync(request);
                    var json = await responce.Content.ReadAsStringAsync();

                    dynamic obj = JsonConvert.DeserializeObject(json);
                    if (!json.Contains("name") & !json.Contains("password"))
                    {
                        Succes = true;
                        string token = obj["token"];
                        System.Diagnostics.Debug.WriteLine(token);
                        token = token.Substring(7, token.Length - 7);
                        System.Diagnostics.Debug.WriteLine(token);
                        CrossSecureStorage.Current.SetValue("UserName", username);
                        CrossSecureStorage.Current.SetValue("PassWord", password);
                        CrossSecureStorage.Current.SetValue("LogguedIn", "True");
                        CrossSecureStorage.Current.SetValue("acces_token", token);
                    }
                    else
                    {
                        string error_name = obj["name"];
                        string error_password = obj["password"];
                        error_msg = error_name ?? error_password;
                    }
                }
                catch (Exception ex)
                {
                    error_msg = ex.Message;
                }
            }
            else
            {
                error_msg = Resources["InternetMessage"];
            }

            return (Succes, error_msg);
        }
        public async Task<(bool, string)> GetProfile()
        {
            string error_msg = null;
            bool Succes = false;
            if (true)
            {
                try
                {
                    var _token = CrossSecureStorage.Current.HasKey("acces_token");
                    if (_token)
                    {
                        string token = CrossSecureStorage.Current.GetValue("acces_token");
                        HttpClient client = new HttpClient();
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                        client.Timeout = TimeSpan.FromSeconds(120);
                        var json = await client.GetStringAsync("http://" + ipAdress + "/api/patient/profile");
                        if (!json.Contains("msg") & !json.Contains("error"))
                        {
                            Succes = true;
                            error_msg = json;
                        }
                        else
                        {
                            dynamic obj = JsonConvert.DeserializeObject(json);
                            string msg = null;
                            string error = null;
                            try
                            {
                                msg = obj["msg"];
                                error = obj["error"];
                            }
                            catch
                            {
                            }
                            error_msg = msg ?? error;
                        }
                    }
                }
                catch (Exception ex)
                {
                    error_msg = "Un Probléme est survenu Lors du téléchargemt profil ";
                }
            }
            else
            {
                error_msg = "Veuillez verifier votre connexion internet";
            }
            return (Succes, error_msg);
        }
        private async Task<bool> Connectivity_check()
        {

            var connectivity = CrossConnectivity.Current;
            if (!connectivity.IsConnected)
                return false;

            var reachable = await connectivity.IsRemoteReachable(ipAdress);

            return reachable;
        }
    }
}
