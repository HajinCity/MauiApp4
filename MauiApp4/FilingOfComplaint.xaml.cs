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
            RelationshipEntry.SelectedIndexChanged += OnRelationshipSelected;
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
                    Purok = PurokEntry.SelectedItem?.ToString() ?? string.Empty,
                    RespLastName = RespLastNameEntry.Text,
                    RespFirstName = RespFirstNameEntry.Text,
                    RespMiddleName = RespMiddleNameEntry.Text,
                    Relationship = GetRelationship(),
                    ComplaintDetails = ComplaintDetailsEditor.Text,
                    ComplaintDate = DateTime.UtcNow,
                    Status = "Pending"
                };

                bool success = await _firestoreService.SubmitComplaintAsync(complaint);

                if (success)
                {
                    await DisplayAlert("Success", $"Complaint submitted!\nCase ID: {caseId}", "OK");
                    await ClearForm();
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

        private string GetRelationship()
        {
            if (RelationshipEntry.SelectedItem?.ToString() == "Other (please specify)")
            {
                return OtherRelationshipEntry.Text;
            }
            return RelationshipEntry.SelectedItem?.ToString() ?? string.Empty;
        }

        private void OnRelationshipSelected(object sender, EventArgs e)
        {
            OtherRelationshipEntry.IsVisible = RelationshipEntry.SelectedItem?.ToString() == "Other (please specify)";
        }

        private async Task<string> GenerateCaseIDAsync()
        {
            return await Task.Run(() =>
            {
                return $"CFO-{DateTime.UtcNow.Year}-{DateTime.UtcNow:MMddHHmmss}";
            });
        }

        private bool IsFormValid()
        {
            return !string.IsNullOrWhiteSpace(LastNameEntry.Text) &&
                   !string.IsNullOrWhiteSpace(FirstNameEntry.Text) &&
                   !string.IsNullOrWhiteSpace(ContactEntry.Text) &&
                   !string.IsNullOrWhiteSpace(CityEntry.Text) &&
                   PurokEntry.SelectedItem != null &&
                   !string.IsNullOrWhiteSpace(RespLastNameEntry.Text) &&
                   !string.IsNullOrWhiteSpace(RespFirstNameEntry.Text) &&
                   RelationshipEntry.SelectedItem != null &&
                   (RelationshipEntry.SelectedItem.ToString() != "Other (please specify)" ||
                    !string.IsNullOrWhiteSpace(OtherRelationshipEntry.Text)) &&
                   !string.IsNullOrWhiteSpace(ComplaintDetailsEditor.Text);
        }

        private async Task ClearForm()
        {
            await Task.Run(() =>
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    LastNameEntry.Text = string.Empty;
                    FirstNameEntry.Text = string.Empty;
                    MiddleNameEntry.Text = string.Empty;
                    ContactEntry.Text = string.Empty;
                    CityEntry.Text = string.Empty;
                    PurokEntry.SelectedIndex = -1;
                    RespLastNameEntry.Text = string.Empty;
                    RespFirstNameEntry.Text = string.Empty;
                    RespMiddleNameEntry.Text = string.Empty;
                    RelationshipEntry.SelectedIndex = -1;
                    OtherRelationshipEntry.Text = string.Empty;
                    OtherRelationshipEntry.IsVisible = false;
                    ComplaintDetailsEditor.Text = string.Empty;
                    MaleRadio.IsChecked = false;
                    FemaleRadio.IsChecked = false;
                });
            });
        }
    }
}