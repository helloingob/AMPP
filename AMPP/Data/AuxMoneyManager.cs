using System;
using System.Collections.Generic;
using System.Threading;
using RestSharp;

namespace AMPP.Data
{
    public class AuxMoneyManager
    {
        private readonly RestClient _restClient = new("https://www.auxmoney.com/");

        public readonly bool IsLoggedIn;

        public AuxMoneyManager(string username, string password)
        {
            Cookies = new Dictionary<string, string>();

            var request = new RestRequest("login_check");
            request.AddParameter("login[loginUsername]", username);
            request.AddParameter("login[loginPassword]", password);
            var response = _restClient.Post(request);

            foreach (var cookie in response.Cookies) Cookies.Add(cookie.Name, cookie.Value);

            IsLoggedIn = response.Content.Contains("<a href=\"/logout\">Logout</a>");
        }

        private Dictionary<string, string> Cookies { get; }

        public List<AuxMoneyProject> Get()
        {
            List<AuxMoneyProject> auxMoneyProjects = new();
            var detailUrls = GetDetailUrls();
            Console.WriteLine("Found " + detailUrls.Count + " project details.");
            for (var i = 0; i < detailUrls.Count; i++)
            {
                Console.WriteLine("[" + (i + 1) + "/" + detailUrls.Count + "] " + detailUrls[i]);
                auxMoneyProjects.Add(GetTransactionsFromDetailUrl(detailUrls[i]));
                
                //Floodprotection
                Thread.Sleep(new Random().Next(500, 1000));
            }
            return auxMoneyProjects;
        }

        private List<string> GetDetailUrls()
        {
            var request = new RestRequest("anlegercockpit/projects");
            foreach (var cookie in Cookies) request.AddCookie(cookie.Key, cookie.Value);
            var response = _restClient.Get(request);
            var extractDetailUrls = Parser.ExtractDetailUrls(response.Content);
            return extractDetailUrls;
        }

        private AuxMoneyProject GetTransactionsFromDetailUrl(string url)
        {
            var request = new RestRequest(url);
            foreach (var cookie in Cookies) request.AddCookie(cookie.Key, cookie.Value);
            var response = _restClient.Get(request);
            return Parser.ExtractTransactions(response.Content);
        }
    }
}