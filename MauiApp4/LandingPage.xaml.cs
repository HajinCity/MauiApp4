using System;
using Microsoft.Maui.Controls;

namespace MauiApp4;

public partial class LandingPage : ContentPage
{
    public LandingPage()
    {
        InitializeComponent();
    }

    private async void OnFileComplaintTapped(object sender, EventArgs e)
    {
      

        await Shell.Current.GoToAsync(nameof(FilingOfComplaint));
    }




}
