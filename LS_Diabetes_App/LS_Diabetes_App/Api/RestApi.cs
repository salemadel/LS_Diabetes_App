using LS_Diabetes_App.Models;
using LS_Diabetes_App.Servies;
using Newtonsoft.Json;
using Plugin.Connectivity;
using Plugin.SecureStorage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace LS_Diabetes_App.Api
{
    public class RestApi : ViewModelBase
    {
        private readonly string ipAdress = "149.202.133.17:2000";

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

        public async Task<(bool, string)> SocialChack(string facebook_token)
        {
            string error_msg = null;
            bool Succes = false;
         
            if (true)
            {
               /* var keyvalues = new List<KeyValuePair<string, string>>{
                new KeyValuePair<string, string>("network" , network),
                new KeyValuePair<string, string>("id" , id),
            };*/
                try
                {

                    var client = new HttpClient();
                    client.Timeout = TimeSpan.FromSeconds(20);
                    var json = await client.GetStringAsync("http://" + ipAdress + "/api/auth/social?token=" + facebook_token);
                  
                    dynamic obj = JsonConvert.DeserializeObject(json);

                    if (obj.success == true)
                    {
                        Succes = true;
                   
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

            return (Succes, error_msg);
        }

        public async Task<(bool, string)> Resigter(Profil_Model user, string password, string facebook_id = null)
        {
            string error_msg = null;
            bool Succes = false;
            if (true)
            {
                var keyvalues = new List<KeyValuePair<string, string>>{
                new KeyValuePair<string, string>("email" , user.Email),
             //   new KeyValuePair<string, string>("facebook_id" , facebook_id),

                new KeyValuePair<string, string>("avatar" , user.Avatar),
                new KeyValuePair<string, string>("firstname" , user.FirstName),
                new KeyValuePair<string, string>("lastname" , user.LastName),
                new KeyValuePair<string, string>("birthday" , user.Birth_Date.ToString("yyyy-MM-dd")),
                new KeyValuePair<string, string>("password" , password),
                new KeyValuePair<string, string>("diabetestype" , user.DiabetesType),
                new KeyValuePair<string, string>("glucometer" , user.Glucometer),
                new KeyValuePair<string, string>("diagnosis_date" , user.Diagnostic_Year.ToString()),
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
                  //  System.Diagnostics.Debug.WriteLine(json);
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
                    HttpClient client = new HttpClient();

                    var request = new HttpRequestMessage(HttpMethod.Post, "http://" + ipAdress + "/api/auth/login")
                    {
                        Content = new FormUrlEncodedContent(keyvalues)
                    };

                    client.Timeout = TimeSpan.FromSeconds(20);
                    var responce = await client.SendAsync(request);
                    var json = await responce.Content.ReadAsStringAsync();
                //    System.Diagnostics.Debug.WriteLine(json);
                    dynamic obj = JsonConvert.DeserializeObject(json);
                    if (!json.Contains("name") & !json.Contains("password"))
                    {
                        Succes = true;
                        string token = obj["token"];

                        token = token.Substring(7, token.Length - 7);

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
                    //    System.Diagnostics.Debug.WriteLine(json);
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

        public async Task<(bool, string)> EmailForgotCheck(string email)
        {
            string error_msg = null;
            bool Succes = false;
            if (true)
            {
                try
                {
                    var client = new HttpClient();
                    client.Timeout = TimeSpan.FromSeconds(20);
                    var json = await client.GetStringAsync("http://" + ipAdress + "/api/auth/forgot-password?email=" + email);
                    //   var json = await responce.Content.ReadAsStringAsync();

                    dynamic obj = JsonConvert.DeserializeObject(json);

                    if (obj.sent == true)
                    {
                        Succes = true;
                    }
                    else
                    {
                        error_msg = "User dose not exisit";
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

        public async Task<(bool, string)> PostCode(string code)
        {
            string error_msg = null;
            bool Succes = false;
            if (true)
            {
                try
                {
                    var client = new HttpClient();
                    client.Timeout = TimeSpan.FromSeconds(20);
                    var json = await client.GetStringAsync("http://" + ipAdress + "/api/auth/verify?code=" + code);
                    //   var json = await responce.Content.ReadAsStringAsync();

                    dynamic obj = JsonConvert.DeserializeObject(json);

                    if (obj.success == true)
                    {
                        Succes = true;
                        string token = obj["token"];
                        token = token.Substring(7, token.Length - 7);
                        CrossSecureStorage.Current.SetValue("acces_token_password", token);
                    }
                    else
                    {
                        error_msg = "Code Expired !";
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

        public async Task<(bool, string)> EditProfil(Profil_Model user, string facebook_id = null)
        {
            string error_msg = null;
            bool Succes = false;
            if (true)
            {
                var keyvalues = new List<KeyValuePair<string, string>>{
                new KeyValuePair<string, string>("email" , user.Email),
                new KeyValuePair<string, string>("facebook_id" , facebook_id),

                new KeyValuePair<string, string>("avatar" , user.Avatar),
                new KeyValuePair<string, string>("firstname" , user.FirstName),
                new KeyValuePair<string, string>("lastname" , user.LastName),
                new KeyValuePair<string, string>("birthday" , user.Birth_Date.ToString("yyyy-MM-dd")),
          
                new KeyValuePair<string, string>("diabetestype" , user.DiabetesType),
                new KeyValuePair<string, string>("glucometer" , user.Glucometer),
                new KeyValuePair<string, string>("diagnosis_date" , user.Diagnostic_Year.ToString()),
                new KeyValuePair<string, string>("height" , user.Height.ToString()),
                new KeyValuePair<string, string>("sex" , user.Sexe),
                };

                try
                {
                    string token = CrossSecureStorage.Current.GetValue("acces_token");
                    HttpClient client = new HttpClient();
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    var request = new HttpRequestMessage(HttpMethod.Post, "http://" + ipAdress + "/api/patient/profile/edit")
                    {
                        Content = new FormUrlEncodedContent(keyvalues)
                    };

                    client.Timeout = TimeSpan.FromSeconds(20);
                    var responce = await client.SendAsync(request);
                    var json = await responce.Content.ReadAsStringAsync();

                    dynamic obj = JsonConvert.DeserializeObject(json);

                    if (obj.success == true)
                    {
                        Succes = true;
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

        public async Task<(bool, string)> ForgotPasswordEdit(string password)
        {
            string error_msg = null;
            bool Succes = false;
            if (true)
            {
                var keyvalues = new List<KeyValuePair<string, string>>{
                new KeyValuePair<string, string>("password" , password),
                };

                try
                {
                    string token = CrossSecureStorage.Current.GetValue("acces_token_password");
                    HttpClient client = new HttpClient();
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    var request = new HttpRequestMessage(HttpMethod.Post, "http://" + ipAdress + "/api/patient/profile/change-password")
                    {
                        Content = new FormUrlEncodedContent(keyvalues)
                    };

                    client.Timeout = TimeSpan.FromSeconds(20);
                    var responce = await client.SendAsync(request);
                    var json = await responce.Content.ReadAsStringAsync();

                    dynamic obj = JsonConvert.DeserializeObject(json);

                    if (obj.success == true)
                    {
                        Succes = true;
                        CrossSecureStorage.Current.DeleteKey("acces_token_password");
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
        public async Task<(bool, string)> ChangePassword(string password)
        {
            string error_msg = null;
            bool Succes = false;
            if (true)
            {
                var keyvalues = new List<KeyValuePair<string, string>>{
                new KeyValuePair<string, string>("password" , password),
                };

                try
                {
                    string token = CrossSecureStorage.Current.GetValue("acces_token");
                    HttpClient client = new HttpClient();
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    var request = new HttpRequestMessage(HttpMethod.Post, "http://" + ipAdress + "/api/patient/profile/change-password")
                    {
                        Content = new FormUrlEncodedContent(keyvalues)
                    };

                    client.Timeout = TimeSpan.FromSeconds(20);
                    var responce = await client.SendAsync(request);
                    var json = await responce.Content.ReadAsStringAsync();

                    dynamic obj = JsonConvert.DeserializeObject(json);

                    if (obj.success == true)
                    {
                        Succes = true;
                       
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

        public async Task<(bool, string)> Post_Activity(string Json_To_Upload, Stream picture = null)
        {
            string msg = null;
            bool Succes = false;
           
            if (true)
            {
                try
                {
                    var client = new HttpClient();
                    var content = new MultipartFormDataContent();

                   
                    if (picture != null)
                    {
                        StreamContent scontnent = new StreamContent(picture);
                        scontnent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                        {
                            FileName = "image.jpeg",
                            Name = "avatar"
                        };
                        scontnent.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");
                        content.Add(scontnent);
                    }


                    /* var stringcontent = new StringContent(JsonConvert.SerializeObject(values) , Encoding.UTF8, "application/json");
                     Debug.WriteLine(await stringcontent.ReadAsStringAsync());
                     content.Add(new StringContent(JsonConvert.SerializeObject(values) , Encoding.UTF8, "application/json"));*/
                    KeyValuePair<string, string> keyValue = new KeyValuePair<string, string>("activity" , Json_To_Upload);
                    content.Add(new StringContent(keyValue.Value) , keyValue.Key);

                    string token = CrossSecureStorage.Current.GetValue("acces_token");
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    client.Timeout = TimeSpan.FromSeconds(30);
                    var responce = await client.PostAsync("http://" + ipAdress + "/api/patient/activity/new", content);

                    var result = await responce.Content.ReadAsStringAsync();

                    dynamic obj = JsonConvert.DeserializeObject(result);

                    if (obj.success == true)
                    {
                        Succes = true;

                    }
                    else
                    {
                        msg = obj.error;
                    }
                }
                catch (Exception ex)
                {
                    msg = "Un Probléme est survenu ";
                }
            }
            else
            {
                msg = Resources["InternetMessage"];
            }
            return (Succes, msg);
        }

         public async Task<(bool, string)> Post_Objectifs(string type , double greater , double less)
         {
             string error_msg = null;
             bool Succes = false;
             if (true)
             {
                 var keyvalues = new List<KeyValuePair<string, string>>{
                 new KeyValuePair<string, string>("type" , type),
                 new KeyValuePair<string, string>("greater" , greater.ToString()),
                 new KeyValuePair<string, string>("less" , less.ToString()),
                 new KeyValuePair<string, string>("startDate" , DateTime.Now.ToString("yyyy-MM-dd")),
                 new KeyValuePair<string, string>("endDate" , DateTime.Now.AddYears(1).ToString("yyyy-MM-dd")),
                
                 };

                 try
                 {
                     var request = new HttpRequestMessage(HttpMethod.Post, "http://" + ipAdress + "/api/patient/objectives/new")
                     {
                         Content = new FormUrlEncodedContent(keyvalues)
                     };
                     var client = new HttpClient();
                    string token = CrossSecureStorage.Current.GetValue("acces_token");
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    client.Timeout = TimeSpan.FromSeconds(20);
                     var responce = await client.SendAsync(request);
                     var json = await responce.Content.ReadAsStringAsync();
                     dynamic obj = JsonConvert.DeserializeObject(json);

                     if (obj.success == true)
                     {
                         Succes = true;
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

        public async Task<(bool, string)> GetFollowers()
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
                        var json = await client.GetStringAsync("http://" + ipAdress + "/api/patient/followers");

                      //  System.Diagnostics.Debug.WriteLine(json);
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
        public async Task<(bool, string)> ChangeFollowerStatut(bool accepting , string doctor_id)
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
                        client.Timeout = TimeSpan.FromSeconds(30);
                        string json;
                        if(accepting)
                        {
                            json = await client.GetStringAsync("http://" + ipAdress + "/api/patient/followers/accept?id="+doctor_id);
                        }
                        else
                        {
                            json = await client.GetStringAsync("http://" + ipAdress + "/api/patient/followers/delete?id=" + doctor_id);
                        }
                        json = await client.GetStringAsync("http://" + ipAdress + "/api/patient/followers");
                       // System.Diagnostics.Debug.WriteLine(json);
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

        public async Task<(bool, string)> GetActivity()
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
                        var json = await client.GetStringAsync("http://" + ipAdress + "/api/patient/activity");

                       // System.Diagnostics.Debug.WriteLine(json);
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
        public async Task<(bool, string)> GetObjectifs()
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
                        var json = await client.GetStringAsync("http://" + ipAdress + "/api/patient/objectives");

                     //   System.Diagnostics.Debug.WriteLine(json);
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

        public async Task<(bool, string)> Post_Drugs(string Json_To_Upload , Drugs_Upload_Model Drug)
        {
            string error_msg = null;
            bool Succes = false;
            if (true)
            {
                var keyvalues = new List<KeyValuePair<string, string>>{
                new KeyValuePair<string, string>("startDate" , Drug.StartDate.ToString("yyyy-MM-dd")),
                new KeyValuePair<string, string>("drug" , Drug.Drug),

                new KeyValuePair<string, string>("dose" , Drug.Dose.ToString()),
                new KeyValuePair<string, string>("duration" , Drug.Duration.ToString()),
                new KeyValuePair<string, string>("indetermine" , Drug.Indeterminer.ToString().First().ToString().ToLower() +  Drug.Indeterminer.ToString().Substring(1)),
                new KeyValuePair<string, string>("note" , Drug.Note),

                new KeyValuePair<string, string>("period" ,Drug.Taking_Time),
                new KeyValuePair<string, string>("times" , Drug.Times),
              
                };

                try
                {
                    string token = CrossSecureStorage.Current.GetValue("acces_token");
                    HttpClient client = new HttpClient();
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                 /*   List<KeyValuePair<string, string>> keyValuePairs = new List<KeyValuePair<string, string>>();
                    KeyValuePair<string, string> keyValue = new KeyValuePair<string, string>("drug", Json_To_Upload);
                    keyValuePairs.Add(keyValue);*/
                    var request = new HttpRequestMessage(HttpMethod.Post, "http://" + ipAdress + "/api/patient/medications/new")
                    {
                        Content = new FormUrlEncodedContent(keyvalues)
                    };

                    client.Timeout = TimeSpan.FromSeconds(20);
                    var responce = await client.SendAsync(request);
                    var json = await responce.Content.ReadAsStringAsync();

                    dynamic obj = JsonConvert.DeserializeObject(json);

                    if (obj.success == true)
                    {
                        Succes = true;
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

        public async Task<(bool, string)> GetNotifications()
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
                        var json = await client.GetStringAsync("http://" + ipAdress + "/api/notifications");

                      //  System.Diagnostics.Debug.WriteLine(json);
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