using MauiApp4.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace MauiApp4.Services
{
    public class FirestoreService
    {
        private readonly HttpClient _httpClient = new();
        private const string ProjectId = "vawc-hestiaxisanpedro2025";
        private const string ApiKey = "AIzaSyDtRBmbhj36SeQ6e1Oj6h4hiGB7uqtE8Vo";

        public async Task<bool> SubmitComplaintAsync(Complaint complaint)
        {
            var url = $"https://firestore.googleapis.com/v1/projects/{ProjectId}/databases/(default)/documents/Complaints/{complaint.CaseID}?key={ApiKey}";

            var payload = new
            {
                fields = new
                {
                    CaseID = new { stringValue = complaint.CaseID },
                    LastName = new { stringValue = complaint.LastName },
                    FirstName = new { stringValue = complaint.FirstName },
                    MiddleName = new { stringValue = complaint.MiddleName },
                    Sex = new { stringValue = complaint.Sex },
                    Contact = new { stringValue = complaint.Contact },
                    City = new { stringValue = complaint.City },
                    Purok = new { stringValue = complaint.Purok },
                    RespLastName = new { stringValue = complaint.RespLastName },
                    RespFirstName = new { stringValue = complaint.RespFirstName },
                    RespMiddleName = new { stringValue = complaint.RespMiddleName },
                    Relationship = new { stringValue = complaint.Relationship },
                    ComplaintDetails = new { stringValue = complaint.ComplaintDetails },
                    ComplaintDate = new { timestampValue = complaint.ComplaintDate.ToString("yyyy-MM-ddTHH:mm:ssZ") },
                    Status = new { stringValue = complaint.Status }
                }
            };

            var request = new HttpRequestMessage(HttpMethod.Patch, url)
            {
                Content = JsonContent.Create(payload)
            };

            var response = await _httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"🔥 Firestore Error: {error}");
            }

            return response.IsSuccessStatusCode;
        }


    }
}
