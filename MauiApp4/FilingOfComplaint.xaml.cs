using MauiApp4.Models;
using MauiApp4.Services;
using System;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace MauiApp4
{
    public partial class FilingOfComplaint : ContentPage
    {
        private readonly FirestoreService _firestoreService = new();

        public FilingOfComplaint()
        {
            InitializeComponent();
        }

        private async void OnCancelClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private async void OnFileNowClicked(object sender, EventArgs e)
        {
            if (!IsFormValid())
            {
                await DisplayAlert("Incomplete", "Please fill in all required fields.", "OK");
                return;
            }

            await SubmitComplaintAsync();
        }

        private async Task SubmitComplaintAsync()
        {
            try
            {
                string caseId = await GenerateCaseIDAsync();

                var complaint = new Complaint
                {
                    CaseID = caseId,
                    LastName = LastNameEntry.Text,
                    FirstName = FirstNameEntry.Text,
                    MiddleName = MiddleNameEntry.Text,
                    Sex = MaleRadio.IsChecked ? "Male" : FemaleRadio.IsChecked ? "Female" : "",
                    Contact = ContactEntry.Text,
                    City = CityEntry.Text,
                    Purok = PurokEntry.Text,
                    RespLastName = RespLastNameEntry.Text,
                    RespFirstName = RespFirstNameEntry.Text,
                    RespMiddleName = RespMiddleNameEntry.Text,
                    Relationship = RelationshipEntry.Text,
                    ComplaintDetails = ComplaintDetailsEditor.Text,
                    ComplaintDate = DateTime.UtcNow,
                    Status = "Pending"
                };

                bool success = await _firestoreService.SubmitComplaintAsync(complaint);

                if (success)
                {
                    await DisplayAlert("Success", $"Complaint submitted!\nCase ID: {caseId}", "OK");
                    ClearForm();
                }
                else
                {
                    await DisplayAlert("Error", "Failed to submit complaint. Check your network or API key.", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Submission error: {ex.Message}", "OK");
            }
        }

        private async Task<string> GenerateCaseIDAsync()
        {
            return $"CFO-{DateTime.UtcNow.Year}-{DateTime.UtcNow:MMddHHmmss}";
        }

        private bool IsFormValid()
        {
            return
                !string.IsNullOrWhiteSpace(LastNameEntry.Text) &&
                !string.IsNullOrWhiteSpace(FirstNameEntry.Text) &&
                !string.IsNullOrWhiteSpace(ContactEntry.Text) &&
                !string.IsNullOrWhiteSpace(CityEntry.Text) &&
                !string.IsNullOrWhiteSpace(PurokEntry.Text) &&
                !string.IsNullOrWhiteSpace(RespLastNameEntry.Text) &&
                !string.IsNullOrWhiteSpace(RespFirstNameEntry.Text) &&
                !string.IsNullOrWhiteSpace(RelationshipEntry.Text) &&
                !string.IsNullOrWhiteSpace(ComplaintDetailsEditor.Text);
        }

        private void ClearForm()
        {
            LastNameEntry.Text = "";
            FirstNameEntry.Text = "";
            MiddleNameEntry.Text = "";
            ContactEntry.Text = "";
            CityEntry.Text = "";
            PurokEntry.Text = "";
            RespLastNameEntry.Text = "";
            RespFirstNameEntry.Text = "";
            RespMiddleNameEntry.Text = "";
            RelationshipEntry.Text = "";
            ComplaintDetailsEditor.Text = "";
            MaleRadio.IsChecked = false;
            FemaleRadio.IsChecked = false;
        }
    }
}
