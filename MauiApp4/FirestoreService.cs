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

                    Complainant = new
                    {
                        mapValue = new
                        {
                            fields = new
                            {
                                LastName = new { stringValue = complaint.Complainant?.LastName ?? "" },
                                FirstName = new { stringValue = complaint.Complainant?.FirstName ?? "" },
                                MiddleName = new { stringValue = complaint.Complainant?.MiddleName ?? "" },
                                SexIdentification = new { stringValue = complaint.Complainant?.SexIdentification ?? "" },
                                CivilStatus = new { stringValue = complaint.Complainant?.CivilStatus ?? "" },
                                Birthdate = new { stringValue = complaint.Complainant?.Birthdate ?? "" },
                                Age = new { stringValue = complaint.Complainant?.Age ?? "" },
                                Religion = new { stringValue = complaint.Complainant?.Religion ?? "" },
                                CellNumber = new { stringValue = complaint.Complainant?.CellNumber ?? "" },
                                Nationality = new { stringValue = complaint.Complainant?.Nationality ?? "" },
                                Occupation = new { stringValue = complaint.Complainant?.Occupation ?? "" },
                                Address = new
                                {
                                    mapValue = new
                                    {
                                        fields = new
                                        {
                                            Purok = new { stringValue = complaint.Complainant?.Address?.Purok ?? "" },
                                            Barangay = new { stringValue = complaint.Complainant?.Address?.Barangay ?? "" },
                                            Municipality = new { stringValue = complaint.Complainant?.Address?.Municipality ?? "" },
                                            Province = new { stringValue = complaint.Complainant?.Address?.Province ?? "" },
                                            Region = new { stringValue = complaint.Complainant?.Address?.Region ?? "" }
                                        }
                                    }
                                }
                            }
                        }
                    },

                    Respondent = new
                    {
                        mapValue = new
                        {
                            fields = new
                            {
                                LastName = new { stringValue = complaint.Respondent?.LastName ?? "" },
                                FirstName = new { stringValue = complaint.Respondent?.FirstName ?? "" },
                                MiddleName = new { stringValue = complaint.Respondent?.MiddleName ?? "" },
                                Alias = new { stringValue = complaint.Respondent?.Alias ?? "" },
                                SexIdentification = new { stringValue = complaint.Respondent?.SexIdentification ?? "" },
                                CivilStatus = new { stringValue = complaint.Respondent?.CivilStatus ?? "" },
                                Birthdate = new { stringValue = complaint.Respondent?.Birthdate ?? "" },
                                Age = new { stringValue = complaint.Respondent?.Age ?? "" },
                                Religion = new { stringValue = complaint.Respondent?.Religion ?? "" },
                                CellNumber = new { stringValue = complaint.Respondent?.CellNumber ?? "" },
                                Nationality = new { stringValue = complaint.Respondent?.Nationality ?? "" },
                                Occupation = new { stringValue = complaint.Respondent?.Occupation ?? "" },
                                RelationshipToComplainant = new { stringValue = complaint.Respondent?.RelationshipToComplainant ?? "" },
                                Address = new
                                {
                                    mapValue = new
                                    {
                                        fields = new
                                        {
                                            Purok = new { stringValue = complaint.Respondent?.Address?.Purok ?? "" },
                                            Barangay = new { stringValue = complaint.Respondent?.Address?.Barangay ?? "" },
                                            Municipality = new { stringValue = complaint.Respondent?.Address?.Municipality ?? "" },
                                            Province = new { stringValue = complaint.Respondent?.Address?.Province ?? "" },
                                            Region = new { stringValue = complaint.Respondent?.Address?.Region ?? "" }
                                        }
                                    }
                                }
                            }
                        }
                    },

                    CaseDetails = new
                    {
                        mapValue = new
                        {
                            fields = new
                            {
                                CaseNumber = new { stringValue = complaint.CaseDetails?.CaseNumber ?? "" },
                                ComplaintDate = new { stringValue = complaint.CaseDetails?.ComplaintDate ?? "" },
                                VAWCCase = new { stringValue = complaint.CaseDetails?.VAWCCase ?? "" },
                                SubCase = new { stringValue = complaint.CaseDetails?.SubCase ?? "" },
                                CaseStatus = new { stringValue = complaint.CaseDetails?.CaseStatus ?? "" },
                                ReferredTo = new { stringValue = complaint.CaseDetails?.ReferredTo ?? "" },
                                IncidentDate = new { stringValue = complaint.CaseDetails?.IncidentDate ?? "" },
                                IncidentDescription = new { stringValue = complaint.CaseDetails?.IncidentDescription ?? "" },
                                PlaceOfIncident = new
                                {
                                    mapValue = new
                                    {
                                        fields = new
                                        {
                                            Purok = new { stringValue = complaint.CaseDetails?.PlaceOfIncident?.Purok ?? "" },
                                            Barangay = new { stringValue = complaint.CaseDetails?.PlaceOfIncident?.Barangay ?? "" },
                                            Municipality = new { stringValue = complaint.CaseDetails?.PlaceOfIncident?.Municipality ?? "" },
                                            Province = new { stringValue = complaint.CaseDetails?.PlaceOfIncident?.Province ?? "" },
                                            Region = new { stringValue = complaint.CaseDetails?.PlaceOfIncident?.Region ?? "" }
                                        }
                                    }
                                }
                            }
                        }
                    }
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
