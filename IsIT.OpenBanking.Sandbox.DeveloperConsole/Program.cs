using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace IsIT.OpenBanking.Sandbox.DeveloperConsole
{
    class Program
    {
        // UserApplication api key obtained from Developer Portal
        private static readonly string APIKEY = "[Place your ApiKey from the developer portal in here]";
        // User (PSU) identifier. Reserverd User from Developer Portal
        private static readonly string NATIONAL_REGISTRY_ID = "[Place your created user's national registry id from the developer portal in here]";
        // Base path to api
        private static readonly string IOBWS_BASE_PATH = "https://apigwsandbox.arionbanki.is/psd2/api/v1"; // for production, use https://apigw.arionbanki.is/psd2/api/v1

        // ID of the request, unique to the call, as determined by the initiating party.
        private static string REQUESTID = Guid.NewGuid().ToString();
        // Access Token created in Developer Portal from UserApplication and User.
        private static string ACCESS_TOKEN = "[Place your token from the developer portal in here]"; // To get access token on production, use https://curity-prod.arionbanki.is/oauth/v2/oauth-token
        // Wellknown OpenId enpoint on production can be found here: https://curity.arionbanki.is/oauth/v2/oauth-anonymous/.well-known/openid-configuration
        
        // Use previously created Consent or call Create Consent.
        private static string CONSENT_ID = "";

        static async Task Main(string[] args)
        {
            bool showMenu = true;
            while (showMenu)
            {
                showMenu = await MainMenu();
            }
        }

        private static async Task<bool> MainMenu()
        {
            Console.WriteLine("");
            Console.WriteLine("==== IOBWS 3.0 Payments and accounts ====");
            Console.WriteLine("1) Info");
            Console.WriteLine("2) Consents");
            Console.WriteLine("3) Accounts");
            Console.WriteLine("4) Confirmation Of Funds");
            Console.WriteLine("5) Payments");
            Console.WriteLine("6) Exit");
            Console.Write("\r\nSelect an option: ");

            switch (Console.ReadLine())
            {
                case "1":
                    Console.WriteLine($"ConsentId: {CONSENT_ID}");
                    Console.WriteLine($"ApiKey   : {APIKEY}");
                    Console.WriteLine($"RequestId: {REQUESTID}");
                    Console.WriteLine($"BasePath : {IOBWS_BASE_PATH}");

                    return true;
                case "2":
                    Console.WriteLine(" -- CONSENTS");
                    Console.WriteLine(" A) Create Consent");
                    Console.WriteLine(" B) Use Existing Consent");
                    Console.WriteLine(" C) Consent details");
                    Console.WriteLine(" D) Consent status");
                    Console.WriteLine(" E) Consent Auth");
                    Console.Write("\r\nSelect an option: ");

                    switch (Console.ReadLine().ToUpper())
                    {
                        case "A":
                            await Consent_Create();
                            return true;
                        case "B":
                            Console.Write("ConsentId: ");
                            CONSENT_ID = Console.ReadLine();
                            return true;
                        case "C":
                            await Consent_Details(CONSENT_ID);
                            return true;
                        case "D":
                            await Consent_Status(CONSENT_ID);
                            return true;
                        case "E":
                            await Consent_Authorizations(CONSENT_ID);
                            return true;
                        default:
                            return true;
                    }
                case "3":
                    Console.WriteLine("-- ACCOUNTS");
                    Console.WriteLine(" A) Read Accounts List");
                    Console.WriteLine(" B) Account Details");
                    Console.WriteLine(" C) Account Balance");
                    Console.WriteLine(" D) Account Transaction");
                    Console.WriteLine(" E) Account Transaction Details");
                    Console.WriteLine(" F) Card Accounts List");
                    Console.WriteLine(" G) Card Accounts Details");
                    Console.WriteLine(" H) Card Accounts Balance");
                    Console.WriteLine(" I) Card Accounts Transaction");
                    Console.Write("\r\nSelect an option: ");

                    switch (Console.ReadLine().ToUpper())
                    {
                        case "A":
                            await Accounts_List();
                            return true;
                        case "B":
                            Console.Write("\r\nResourceId: ");
                            await Accounts_Details(Console.ReadLine());
                            return true;
                        case "C":
                            Console.Write("\r\nResourceId: ");
                            await Accounts_Balance(Console.ReadLine());
                            return true;
                        case "D":
                            Console.Write("\r\nResourceId: ");
                            await Accounts_Transactions(Console.ReadLine());
                            return true;
                        case "E":
                            Console.Write("\r\nResourceId: ");
                            var resourceId = Console.ReadLine();
                            Console.Write("\r\nTransactionId: ");
                            var transId = Console.ReadLine();
                            await Accounts_TransactionDetails(resourceId, transId);
                            return true;
                        case "F":
                            await CardAccounts_List();
                            return true;
                        case "G":
                            Console.Write("\r\nResourceId: ");
                            await CardAccounts_Details(Console.ReadLine());
                            return true;
                        case "H":
                            Console.Write("\r\nResourceId: ");
                            await CardAccounts_Balance(Console.ReadLine());
                            return true;
                        case "I":
                            Console.Write("\r\nResourceId: ");
                            await CardAccounts_Transactions(Console.ReadLine());
                            return true;
                        default:
                            return true;
                    }
                case "4":
                    Console.WriteLine("-- CONFIRMATION OF FUNDS");
                    Console.Write("\r\nIban: ");
                    var iban = Console.ReadLine();
                    Console.Write("Amount to check: ");
                    var amount = Console.ReadLine();

                    await ConfirmationOfFunds(iban, amount);
                    return true;
                case "5":
                    Console.WriteLine("-- PAYMENTS");
                    Console.WriteLine(" A) Create Payment initiation");
                    Console.WriteLine(" B) Payment Status");
                    Console.WriteLine(" C) Payment Information");
                    Console.WriteLine(" D) Payment SCA Status");
                    Console.WriteLine(" E) PaymentID by RequestID");
                    Console.Write("\r\nSelect an option: ");

                    if (string.IsNullOrEmpty(CONSENT_ID))
                    {
                        Console.WriteLine("You need to create consent before creating payment");
                        return true;
                    }
                    else
                    {
                        switch (Console.ReadLine().ToUpper())
                        {
                            case "A":
                                await Payment_Initiate();
                                return true;
                            case "B":
                                Console.Write($"\r\nPaymentId: ");
                                await GetPaymentStatus(Console.ReadLine());
                                return true;
                            case "C":
                                Console.Write($"\r\nPaymentId: ");
                                await GetPaymentInfo(Console.ReadLine());
                                return true;
                            case "D":
                                Console.Write($"\r\nPaymentId: ");
                                var paymentId = Console.ReadLine();
                                Console.Write($"\r\nAuthorizationId: ");
                                var authorizationId = Console.ReadLine();
                                await GetPaymentAuthorizationScaStatus(paymentId, authorizationId);
                                return true;
                            case "E":
                                Console.Write($"\r\nRequestId: ");
                                await GetPaymentIdByRequestId(Console.ReadLine());
                                return true;
                            default:
                                return true;
                        }
                    }
                case "6":
                    return false;
                default:
                    return true;
            }
        }

        #region Consents

        private static async Task Consent_Create()
        {
            PrintLine($"- POST: {IOBWS_BASE_PATH}/consents", true, false);

            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("X-Request-ID", REQUESTID);                                          // Unique Request Id
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", APIKEY);                              // Azure Apim Subscription key
                client.DefaultRequestHeaders.Add("PSU-ID", NATIONAL_REGISTRY_ID);                                   // PSU ID (kennitala)
                client.DefaultRequestHeaders.Add("PSU-IP-Address", "127.0.0.1");                                      // PSU IP Address
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ACCESS_TOKEN); // PSU Bearer token

                // Consent request body
                var req = new
                {
                    access = new
                    {
                    },
                    recurringIndicator = true,              // True, if the consent is for recurring access to the account data. False for one time access.
                    validUntil = DateTime.Now.AddHours(4),
                    frequencyPerDay = 4,                    // This field indicates the requested maximum frequency for an access without PSU involvement per day.For a one - off access, this attribute is set to "1"
                    combinedServiceIndicator = false        // If true indicates that a payment initiation service will be addressed in the same "session"
                };

                var json = SerializeToJson(req);

                HttpResponseMessage response = await client.PostAsync($"{IOBWS_BASE_PATH}/consents", SetContent(json));
                var res = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == System.Net.HttpStatusCode.Created)
                {
                    Console.WriteLine("- Response:");
                    Console.WriteLine(res);

                    var consentResponse = JsonDocument.Parse(res);

                    CONSENT_ID = consentResponse.RootElement.GetProperty("consentId").GetString();

                    var scaLinks = consentResponse.RootElement.GetProperty("_links");
                    var scaRedirectLink = scaLinks.GetProperty("scaRedirect").GetProperty("href").GetString();

                    Console.WriteLine($"\r\nConsentId: {CONSENT_ID} created.");
                    Console.WriteLine($"ScaRedirect: {scaRedirectLink}");

                    OpenBrowser(scaRedirectLink);
                }
                else
                {
                    PrintLine("- Error:", false, true);
                    Console.WriteLine($"{response.StatusCode}, {res}");
                    Console.WriteLine(response.Headers.ToString());
                }

                PrintHeaders(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static async Task Consent_Details(string id)
        {
            PrintLine($"- GET: {IOBWS_BASE_PATH}/consents/{id}", true, false);

            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("X-Request-ID", REQUESTID);
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", APIKEY);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ACCESS_TOKEN);

                HttpResponseMessage response = await client.GetAsync($"{IOBWS_BASE_PATH}/consents/{id}");
                var res = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Console.WriteLine("- Response:");
                    Console.WriteLine(res);
                }
                else
                {
                    PrintLine("- Error:", false, true);
                    Console.WriteLine($"{response.StatusCode}, {res}");
                }

                PrintHeaders(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static async Task Consent_Status(string id)
        {
            PrintLine($"- GET: {IOBWS_BASE_PATH}/consents/{id}/status", true, false);

            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("X-Request-ID", REQUESTID);
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", APIKEY);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ACCESS_TOKEN);

                HttpResponseMessage response = await client.GetAsync($"{IOBWS_BASE_PATH}/consents/{id}/status");
                var res = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Console.WriteLine("- Response:");
                    Console.WriteLine(res);
                }
                else
                {
                    PrintLine("- Error:", false, true);
                    Console.WriteLine($"{response.StatusCode}, {res}");
                }

                PrintHeaders(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static async Task Consent_Authorizations(string id)
        {
            PrintLine($"- GET: {IOBWS_BASE_PATH}/consents/{id}/authorisations", true, false);

            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("X-Request-ID", REQUESTID);
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", APIKEY);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ACCESS_TOKEN);

                HttpResponseMessage response = await client.GetAsync($"{IOBWS_BASE_PATH}/consents/{id}/authorisations");
                var res = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Console.WriteLine("- Response:");
                    Console.WriteLine(res);
                }
                else
                {
                    PrintLine("- Error:", false, true);
                    Console.WriteLine($"{response.StatusCode}, {res}");
                }

                PrintHeaders(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        #endregion Consents

        #region Accounts

        public static async Task Accounts_List()
        {
            PrintLine($"- GET: {IOBWS_BASE_PATH}/accounts", true, false);

            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("X-Request-ID", REQUESTID);                                          // Unique Request Id 
                client.DefaultRequestHeaders.Add("Consent-ID", CONSENT_ID);                                          // Id of Consent.
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", APIKEY);                              // Azure Apim Subscription key
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ACCESS_TOKEN); // PSU Bearer token

                HttpResponseMessage response = await client.GetAsync($"{IOBWS_BASE_PATH}/accounts");
                var res = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Console.WriteLine("- Response:");

                    var accountsResponse = JsonDocument.Parse(res);
                    var accountList = accountsResponse.RootElement.GetProperty("accounts").EnumerateArray();

                    foreach (var a in accountList)
                    {
                        Console.WriteLine($"  [ResourceId]: {a.GetProperty("resourceId").GetString()}, [Name]: {a.GetProperty("name").GetString()}, [Iban]: {a.GetProperty("iban").GetString()}");
                    }
                }
                else
                {
                    PrintLine("- Error:", false, true);
                    Console.WriteLine($"{response.StatusCode}, {res}");
                }

                PrintHeaders(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static async Task Accounts_Details(string resourceId)
        {
            PrintLine($"- GET: {IOBWS_BASE_PATH}/accounts/{resourceId}", true, false);

            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("X-Request-ID", REQUESTID);                                          // Unique Request Id 
                client.DefaultRequestHeaders.Add("Consent-ID", CONSENT_ID);                                          // Id of Consent.
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", APIKEY);                              // Azure Apim Subscription key
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ACCESS_TOKEN); // PSU Bearer token

                HttpResponseMessage response = await client.GetAsync($"{IOBWS_BASE_PATH}/accounts/{resourceId}");
                var res = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Console.WriteLine("- Response:");
                    Console.WriteLine(res);
                }
                else
                {
                    PrintLine("- Error:", false, true);
                    Console.WriteLine($"{response.StatusCode}, {res}");
                }

                PrintHeaders(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static async Task Accounts_Balance(string resourceId)
        {
            PrintLine($"- GET: {IOBWS_BASE_PATH}/accounts/{resourceId}/balances", true, false);

            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("X-Request-ID", REQUESTID);                                          // Unique Request Id 
                client.DefaultRequestHeaders.Add("Consent-ID", CONSENT_ID);                                          // Id of Consent.
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", APIKEY);                              // Azure Apim Subscription key
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ACCESS_TOKEN); // PSU Bearer token

                HttpResponseMessage response = await client.GetAsync($"{IOBWS_BASE_PATH}/accounts/{resourceId}/balances");
                var res = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Console.WriteLine("- Response:");

                    var accountsResponse = JsonDocument.Parse(res);
                    var balanceList = accountsResponse.RootElement.GetProperty("balances").EnumerateArray();

                    foreach (var b in balanceList)
                    {
                        Console.WriteLine($"  [Currency]: {b.GetProperty("balanceAmount").GetProperty("currency").GetString()}, [Amount]: {b.GetProperty("balanceAmount").GetProperty("amount").GetString()}, [Date]: {b.GetProperty("referenceDate").GetString()}");
                    }
                }
                else
                {
                    PrintLine("- Error:", false, true);
                    Console.WriteLine($"{response.StatusCode}, {res}");
                }

                PrintHeaders(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static async Task Accounts_Transactions(string resourceId)
        {
            PrintLine($"- GET: {IOBWS_BASE_PATH}/accounts/{resourceId}/transactions?bookingStatus=both", true, false);

            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("X-Request-ID", REQUESTID);                                          // Unique Request Id 
                client.DefaultRequestHeaders.Add("Consent-ID", CONSENT_ID);                                          // Id of Consent.
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", APIKEY);                              // Azure Apim Subscription key
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ACCESS_TOKEN); // PSU Bearer token

                HttpResponseMessage response = await client.GetAsync($"{IOBWS_BASE_PATH}/accounts/{resourceId}/transactions?bookingStatus=both");
                var res = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Console.WriteLine("- Response:");

                    var transactionResponse = JsonDocument.Parse(res);
                    var transactionList = transactionResponse.RootElement.GetProperty("transactions").GetProperty("booked").EnumerateArray();

                    foreach (var t in transactionList)
                    {
                        Console.WriteLine($"  [TransactionId]: {t.GetProperty("transactionId").GetString()}, [Amount]: {t.GetProperty("transactionAmount").GetProperty("amount").GetString()}, [CreditorName]: {t.GetProperty("creditorName").GetString()}");
                    }
                }
                else
                {
                    PrintLine("- Error:", false, true);
                    Console.WriteLine($"{response.StatusCode}, {res}");
                }

                PrintHeaders(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static async Task Accounts_TransactionDetails(string resourceId, string transactionId)
        {
            PrintLine($"- GET: {IOBWS_BASE_PATH}/accounts/{resourceId}/transactions/{transactionId}", true, false);

            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("X-Request-ID", REQUESTID);                                          // Unique Request Id 
                client.DefaultRequestHeaders.Add("Consent-ID", CONSENT_ID);                                          // Id of Consent.
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", APIKEY);                              // Azure Apim Subscription key
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ACCESS_TOKEN); // PSU Bearer token

                HttpResponseMessage response = await client.GetAsync($"{IOBWS_BASE_PATH}/accounts/{resourceId}/transactions/{transactionId}");
                var res = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Console.WriteLine("- Response:");
                    Console.WriteLine(res);
                }
                else
                {
                    PrintLine("- Error:", false, true);
                    Console.WriteLine($"{response.StatusCode}, {res}");
                }

                PrintHeaders(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        #endregion Accounts

        #region Card Accounts

        public static async Task CardAccounts_List()
        {
            PrintLine($"- GET: {IOBWS_BASE_PATH}/card-accounts", true, false);

            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("X-Request-ID", REQUESTID);                                          // Unique Request Id 
                client.DefaultRequestHeaders.Add("Consent-ID", CONSENT_ID);                                          // Id of Consent.
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", APIKEY);                              // Azure Apim Subscription key
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ACCESS_TOKEN); // PSU Bearer token

                HttpResponseMessage response = await client.GetAsync($"{IOBWS_BASE_PATH}/card-accounts");
                var res = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Console.WriteLine("- Response:");

                    var accountsResponse = JsonDocument.Parse(res);
                    var accountList = accountsResponse.RootElement.GetProperty("cardAccounts").EnumerateArray();

                    foreach (var a in accountList)
                    {
                        Console.WriteLine($"  [ResourceId]: {a.GetProperty("resourceId").GetString()}, [Name]: {a.GetProperty("name").GetString()}, [MaskedPan]: {a.GetProperty("maskedPan").GetString()}");
                    }
                }
                else
                {
                    PrintLine("- Error:", false, true);
                    Console.WriteLine($"{response.StatusCode}, {res}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static async Task CardAccounts_Details(string resourceId)
        {
            PrintLine($"- GET: {IOBWS_BASE_PATH}/card-accounts/{resourceId}", true, false);

            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("X-Request-ID", REQUESTID);                                          // Unique Request Id 
                client.DefaultRequestHeaders.Add("Consent-ID", CONSENT_ID);                                          // Id of Consent.
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", APIKEY);                              // Azure Apim Subscription key
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ACCESS_TOKEN); // PSU Bearer token

                HttpResponseMessage response = await client.GetAsync($"{IOBWS_BASE_PATH}/card-accounts/{resourceId}");
                var res = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Console.WriteLine("- Response:");
                    Console.WriteLine(res);
                }
                else
                {
                    PrintLine("- Error:", false, true);
                    Console.WriteLine($"{response.StatusCode}, {res}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static async Task CardAccounts_Balance(string resourceId)
        {
            PrintLine($"- GET: {IOBWS_BASE_PATH}/card-accounts/{resourceId}/balances", true, false);

            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("X-Request-ID", REQUESTID);                                          // Unique Request Id 
                client.DefaultRequestHeaders.Add("Consent-ID", CONSENT_ID);                                          // Id of Consent.
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", APIKEY);                              // Azure Apim Subscription key
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ACCESS_TOKEN); // PSU Bearer token

                HttpResponseMessage response = await client.GetAsync($"{IOBWS_BASE_PATH}/card-accounts/{resourceId}/balances");
                var res = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Console.WriteLine("- Response:");
                    Console.WriteLine(res);
                }
                else
                {
                    PrintLine("- Error:", false, true);
                    Console.WriteLine($"{response.StatusCode}, {res}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static async Task CardAccounts_Transactions(string resourceId)
        {
            PrintLine($"- GET: {IOBWS_BASE_PATH}/card-accounts/{resourceId}/transactions?bookingStatus=both", true, false);

            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("X-Request-ID", REQUESTID);                                          // Unique Request Id 
                client.DefaultRequestHeaders.Add("Consent-ID", CONSENT_ID);                                          // Id of Consent.
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", APIKEY);                              // Azure Apim Subscription key
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ACCESS_TOKEN); // PSU Bearer token

                HttpResponseMessage response = await client.GetAsync($"{IOBWS_BASE_PATH}/card-accounts/{resourceId}/transactions?bookingStatus=both");
                var res = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Console.WriteLine("- Response:");
                    Console.WriteLine(res);
                }
                else
                {
                    PrintLine("- Error:", false, true);
                    Console.WriteLine($"{response.StatusCode}, {res}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        #endregion Card Acccounts

        #region Confirmation Of Funds

        public static async Task ConfirmationOfFunds(string iban, string amount)
        {
            PrintLine($"- GET: {IOBWS_BASE_PATH}/funds-confirmations", true, false);

            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("X-Request-ID", REQUESTID);                                          // Unique Request Id 
                client.DefaultRequestHeaders.Add("Consent-ID", CONSENT_ID);                                          // Id of Consent.
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", APIKEY);                              // Azure Apim Subscription key
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ACCESS_TOKEN); // PSU Bearer token

                // Access object empty. Will be decieded by Arion banki.
                var req = new
                {
                    cardNumber = "",
                    account = new
                    {
                        iban = iban,
                        bban = "string",
                        pan = "string",
                        maskedPan = "string",
                        msisdn = "string",
                        currency = "ISK"
                    },
                    payee = "string",
                    instructedAmount = new
                    {
                        amount = amount,
                        currency = "ISK"
                    }
                };

                var json = SerializeToJson(req);

                HttpResponseMessage response = await client.PostAsync($"{IOBWS_BASE_PATH}/funds-confirmations", SetContent(json));
                var res = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Console.WriteLine("- Response:");
                    Console.WriteLine(res);
                }
                else
                {
                    PrintLine("- Error:", false, true);
                    Console.WriteLine($"{response.StatusCode}, {res}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        #endregion Confirmation Of Funds

        #region Payments

        private static async Task Payment_Initiate()
        {
            PrintLine($"- POST: {IOBWS_BASE_PATH}/payments/credit-transfers", true, false);

            var sourceIban = "IS260370261000871508021480";
            var destIban = "IS050367266585622808081480";

            // Payment Initiation Request body
            var req = new
            {
                EndToEndIdentification = Guid.NewGuid().ToString("N"),
                DebtorAccount = new
                {
                    Iban = sourceIban
                },
                DebtorId = "1912631469",
                CreditorAccount = new
                {
                    Iban = destIban
                },
                CreditorId = "1234567890",
                InstructedAmount = new
                {
                    Amount = "10",
                    Currency = "EUR"
                },
                RemittanceInformationUnstructured = "my description"
            };

            var json = SerializeToJson(req);

            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("X-Request-ID", REQUESTID);                                          // Unique Request Id 
                client.DefaultRequestHeaders.Add("Consent-ID", CONSENT_ID);                                          // Id of Consent.
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", APIKEY);                              // Azure Apim Subscription key
                client.DefaultRequestHeaders.Add("PSU-IP-Address", "127.0.0.1");                                      // PSU IP Address
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ACCESS_TOKEN); // PSU Bearer token

                HttpResponseMessage response = await client.PostAsync($"{IOBWS_BASE_PATH}/payments/credit-transfers", SetContent(json));
                var res = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == System.Net.HttpStatusCode.Created)
                {
                    Console.WriteLine("- Response:");
                    Console.WriteLine(res);

                    var paymentResponse = JsonDocument.Parse(res);

                    var paymentId = paymentResponse.RootElement.GetProperty("paymentId").GetString();

                    var scaLinks = paymentResponse.RootElement.GetProperty("_links");
                    var scaRedirectLink = scaLinks.GetProperty("scaRedirect").GetProperty("href").GetString();

                    Console.WriteLine($"\r\nPaymentId: {paymentId} created.");
                    Console.WriteLine($"ScaRedirect: {scaRedirectLink}");

                    OpenBrowser(scaRedirectLink);
                }
                else
                {
                    PrintLine("- Error:", false, true);
                    Console.WriteLine($"{response.StatusCode}, {res}");
                }

                PrintHeaders(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static async Task GetPaymentStatus(string paymentId)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("X-Request-ID", REQUESTID);
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", APIKEY);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ACCESS_TOKEN);

                HttpResponseMessage response = await client.GetAsync($"{IOBWS_BASE_PATH}/payments/credit-transfers/{paymentId}/status");
                var res = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Console.WriteLine("- Response:");
                    Console.WriteLine(res);
                }
                else
                {
                    PrintLine("- Error:", false, true);
                    Console.WriteLine($"{response.StatusCode}, {res}");
                }

                PrintHeaders(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static async Task GetPaymentInfo(string paymentId)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("X-Request-ID", REQUESTID);
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", APIKEY);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ACCESS_TOKEN);

                HttpResponseMessage response = await client.GetAsync($"{IOBWS_BASE_PATH}/payments/credit-transfers/{paymentId}");
                var res = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Console.WriteLine("- Response:");
                    Console.WriteLine(res);
                }
                else
                {
                    PrintLine("- Error:", false, true);
                    Console.WriteLine($"{response.StatusCode}, {res}");
                }

                PrintHeaders(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static async Task GetPaymentAuthorizationScaStatus(string paymentId, string authorizationId)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("X-Request-ID", REQUESTID);
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", APIKEY);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ACCESS_TOKEN);

                HttpResponseMessage response = await client.GetAsync($"{IOBWS_BASE_PATH}/payments/credit-transfers/{paymentId}/authorisations/{authorizationId}");
                var res = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Console.WriteLine("- Response:");
                    Console.WriteLine(res);
                }
                else
                {
                    PrintLine("- Error:", false, true);
                    Console.WriteLine($"{response.StatusCode}, {res}");
                }

                PrintHeaders(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static async Task GetPaymentIdByRequestId(string requestId)
        {
            PrintLine($"- GET: {IOBWS_BASE_PATH}/payments/credit-transfers/info/{requestId}", true, false);

            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("X-Request-ID", REQUESTID);
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", APIKEY);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ACCESS_TOKEN);

                HttpResponseMessage response = await client.GetAsync($"{IOBWS_BASE_PATH}/payments/credit-transfers/info/{requestId}");
                var res = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Console.WriteLine("- Response:");
                    Console.WriteLine(res);
                }
                else
                {
                    PrintLine("- Error:", false, true);
                    Console.WriteLine($"{response.StatusCode}, {res}");
                }

                PrintHeaders(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        #endregion Payments

        #region Helpers

        public static void PrintLine(string text, bool isTitle, bool isError)
        {
            if (isError)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(text);
                Console.ResetColor();
            }
            else if (isTitle)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(text);
                Console.ResetColor();
            }
            else
            {
                Console.WriteLine(text);
            }
        }

        public static string SerializeToJson<TAnything>(TAnything value)
        {
            return System.Text.Json.JsonSerializer.Serialize(value);
        }

        private static StringContent SetContent(string content)
        {
            string requestBody = (string.IsNullOrEmpty(content)) ? "" : content;
            return new StringContent(requestBody, Encoding.UTF8, "application/json");
        }

        public static void OpenBrowser(string url)
        {
            try
            {
                Process.Start(url);
            }
            catch
            {
                // hack because of this: https://github.com/dotnet/corefx/issues/10361
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    url = url.Replace("&", "^&");
                    Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    Process.Start("xdg-open", url);
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    Process.Start("open", url);
                }
                else
                {
                    throw;
                }
            }
        }

        #endregion Helpers
    }
}
