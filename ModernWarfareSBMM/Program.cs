using Google.Cloud.Vision.V1;
using ModernWarfareSBMM.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using static Google.Cloud.Vision.V1.TextAnnotation.Types;

namespace ModernWarfareSBMM
{
    class Program
    {
        static readonly HttpClient httpClient = new HttpClient();
        static readonly string loginUrl = "https://profile.callofduty.com/cod/mapp/login";
        static readonly string registerDeviceUrl = "https://profile.callofduty.com/cod/mapp/registerDevice";

        static async Task Main(string[] args)
        {
            try
            {
                if (args.Length != 1)
                {
                    Console.WriteLine("Missing image");
                    return;
                }

                var imageFile = args[0];

                if (!File.Exists(imageFile))
                {
                    Console.WriteLine("Image file doesn't exist");
                    return;
                }

                await CodAuthenticatorAsync();

                Console.WriteLine("Logged in");

                var users = new List<UserModel>();

                ImageAnnotatorClient client = ImageAnnotatorClient.Create();

                var image = Image.FromFile(imageFile);
                TextAnnotation text = client.DetectDocumentText(image);
                foreach (var page in text.Pages)
                {
                    foreach (var block in page.Blocks)
                    {
                        foreach (var paragraph in block.Paragraphs)
                        {
                            // Realistically, if this block is actually one of the teams, you won't have less than 2 'words' here
                            // One word will always be the rank (or should be) and another would be the username.
                            // Clan tags would also show up here, but they're irrelevant and will be ignored.
                            if (paragraph.Words.Count < 2)
                            {
                                continue;
                            }

                            bool inClanTag = false;
                            DetectedBreak lastBreakingChar = default;
                            foreach (var word in paragraph.Words)
                            {
                                var fullWord = string.Join("", word.Symbols.Select(s => s.Text)).Trim();
                                var lastUser = users.LastOrDefault();
                                switch (fullWord)
                                {
                                    case "[":
                                        if (inClanTag) throw new Exception("Found clan tag opening bracket before a previous clan tag was closed");
                                        inClanTag = true;
                                        continue;
                                    case "]":
                                        if (!inClanTag) throw new Exception("Found clan tag closing bracket while not in clan tag");
                                        inClanTag = false;
                                        continue;
                                }

                                if (inClanTag)
                                {
                                    //Console.WriteLine($"Found {fullWord} in clan tag");
                                    continue;
                                }

                                if (short.TryParse(fullWord, out var level))
                                {
                                    if (level < 1 || level > 155)
                                    {
                                        Console.WriteLine($"Level {level} found but not in range.");
                                        continue;
                                    }

                                    if ((lastBreakingChar != default && lastBreakingChar.Type == DetectedBreak.Types.BreakType.EolSureSpace) || lastUser == default || lastUser.IsComplete())
                                    {
                                        users.Add(new UserModel(level, block));
                                        Console.WriteLine($"Added new possible user with level {level} to list");
                                    }
                                    else
                                    {
                                        Console.WriteLine($"Found possible level {level} but no EOL found and lastUser exists");
                                    }
                                }
                                // This regex might need to be more strict.
                                // Your Activision ID is what is displayed in-game for Modern Warfare. Display names must be between 2 and 16 characters with no special characters. Unicode characters are supported.
                                else if (new Regex(@"^(\w{2,16})$").IsMatch(fullWord))
                                {
                                    if (lastUser != default && lastUser.IsComplete() && lastBreakingChar != default && lastBreakingChar.Type == DetectedBreak.Types.BreakType.Space && block == lastUser.Block)
                                    {
                                        Console.WriteLine($"Found another part of username for {lastUser.Username} -> {fullWord}");
                                        lastUser.AppendToUsername(fullWord);
                                    }
                                    else if (lastUser == default || lastUser.IsComplete())
                                    {
                                        Console.WriteLine($"Found matching username pattern '{fullWord}' but no rank was found before");
                                        continue;
                                    }
                                    else
                                    {
                                        lastUser.SetUsername(fullWord);

                                        Console.WriteLine($"\tSet username to {fullWord}");
                                    }
                                }

                                lastBreakingChar = word.Symbols.LastOrDefault()?.Property?.DetectedBreak;
                            }
                        }

                        if (users.Count > 0)
                        {
                            var removed = users.RemoveAll(a => !a.IsComplete());
                            if (removed > 0)
                            {
                                Console.WriteLine($"Removed {removed} incomplete users");
                            }
                        }

                    }
                }

                Console.WriteLine($"Found {users.Count} users");
                users.ForEach(a => Console.WriteLine($"\t{a.Rank} {a.Username}"));

                foreach (var user in users)
                {
                    var results = await SearchUsername(user.Username);

                    if (results == default)
                    {
                        Console.WriteLine($"Unable to find result for {user.Username}");
                        continue;
                    }

                    UnoUserModel unoUser = results.FirstOrDefault();

                    if (unoUser == default)
                    {
                        //Console.WriteLine($"Unable to find result for {user.Username}");
                        continue;
                    }

                    if (results.Count == 1)
                    {
                        user.SetProfile(await GetProfile(unoUser.Username));
                    }
                    else
                    {
                        foreach (var result in results)
                        {
                            var noIdentifier = result.Username.Split('#')[0];
                            if (!noIdentifier.Equals(user.Username, StringComparison.CurrentCulture))
                            {
                                Console.WriteLine($"{noIdentifier} doesnt match {user.Username}");
                                continue;
                            }

                            var stats = await GetProfile(result.Username);

                            if (stats == default)
                            {
                                Console.WriteLine($"No stats found for {result.Username}");
                                continue;
                            }

                            // Not the user.
                            if (stats.Level < user.Rank)
                            {
                                continue;
                            }
                            else
                            {
                                // This needs some more work.
                                unoUser = result;
                                user.SetProfile(stats);
                                if (stats.Level == user.Rank)
                                {
                                    Console.WriteLine("Found user with exact level");
                                    break;
                                }
                            }
                        }
                    }

                    if (user.Profile == default)
                    {
                        Console.WriteLine($"Couldn't find a user with the same level or higher for {user.Username}");
                        continue;
                    }

                    Console.WriteLine($"{user.Username} stats");
                    Console.WriteLine($"\tRank: {user.Profile.Level}");
                    Console.WriteLine($"\tKill/Death Ratio: {Math.Round(user.Profile.Lifetime.All.Properties["kdRatio"], 2)}");
                    Console.WriteLine($"\tWin/Loss Ratio: {Math.Round(user.Profile.Lifetime.All.Properties["winLossRatio"], 2)}");
                    Console.WriteLine($"\tAccuracy: {user.Profile.Lifetime.All.Properties["accuracy"].ToString("0.00")}");
                    Console.WriteLine($"\tSPM: {Math.Round(user.Profile.Lifetime.All.Properties["scorePerMinute"], 2)}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR: {ex}");
            }

            Console.ReadLine();
        }

        static async Task<List<UnoUserModel>> SearchUsername(string searchTerm)
        {
            var encoded = HttpUtility.UrlEncode(searchTerm);
            var response = await httpClient.GetAsync($"https://www.callofduty.com/api/papi-client/crm/cod/v2/platform/uno/username/{encoded}/search");

            if (response.StatusCode != HttpStatusCode.OK)
            {
                return default;
            }

            var data = JsonConvert.DeserializeObject<UnoSearchResponseModel>(await response.Content.ReadAsStringAsync());

            if (data.Status != "success")
            {
                return default;
            }

            return data.Data;
        }

        static async Task<ModernWarfareProfileModel> GetProfile(string uno)
        {
            var encoded = HttpUtility.UrlEncode(uno);
            var response = await httpClient.GetAsync($"https://www.callofduty.com/api/papi-client/stats/cod/v1/title/mw/platform/uno/gamer/{encoded}/profile/type/mp?locale=en");

            if (response.StatusCode != HttpStatusCode.OK)
            {
                return default;
            }

            var data = JsonConvert.DeserializeObject<ModernWarfareProfileResponseModel>(await response.Content.ReadAsStringAsync());

            if (data.Status != "success")
            {
                return default;
            }

            return data.Data;
        }


        static async Task CodAuthenticatorAsync()
        {
            var email = Environment.GetEnvironmentVariable("ATVI_EMAIL") ?? throw new ArgumentException("Missing ATVI_EMAIL env");
            var password = Environment.GetEnvironmentVariable("ATVI_PASSWORD") ?? throw new ArgumentException("Missing ATVI_PASSWORD env");

            // To set cookies.
            await httpClient.GetAsync("https://profile.callofduty.com/cod/login");

            HttpResponseMessage response;

            var bytes = new byte[16];

            // Create random hash to spoof device id.
            using (var rand = new RNGCryptoServiceProvider())
            {
                rand.GetBytes(bytes);
            }

            var deviceId = BitConverter.ToString(bytes).Replace("-", string.Empty).ToLower();

            var deviceBody = JsonConvert.SerializeObject(new
            {
                deviceId
            });

            using (var content = new StringContent(deviceBody))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                response = await httpClient.PostAsync(registerDeviceUrl, content);
            }

            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception($"Bad status code '{response.StatusCode}' when registering device");
            }

            string contentString = await response.Content.ReadAsStringAsync();

            var data = (dynamic)JsonConvert.DeserializeObject(contentString);

            var authHeader = (string)data.data.authHeader;

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", authHeader);
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("x_cod_device_id", deviceId);

            var loginBody = JsonConvert.SerializeObject(new
            {
                email,
                password
            });

            using (var content = new StringContent(loginBody))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                response = await httpClient.PostAsync(loginUrl, content);
            }

            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception("Failed logging in");
            }
        }
    }
}
