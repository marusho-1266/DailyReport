using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace DailyReportApp.Services
{
    public class ChatworkService
    {
        private readonly string _apiKey;
        private readonly string _roomId;

        public ChatworkService(string apiKey, string roomId)
        {
            _apiKey = apiKey;
            _roomId = roomId;
        }

        public async Task<bool> SendMessage(string message)
        {
            var client = new RestClient("https://api.chatwork.com/v2");
            var request = new RestRequest($"/rooms/{_roomId}/messages", Method.Post);
            request.AddHeader("X-ChatWorkToken", _apiKey);
            request.AddParameter("body", message);

            var response = await client.ExecuteAsync(request);

            return response.IsSuccessful;
        }
    }
}
