namespace MauiApp4;

using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Devices.Sensors;
using System.Diagnostics;

public partial class SplashScreen : ContentPage
{
    public SplashScreen()
    {
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        var granted = await RequestLocationPermissionAsync();

        if (granted)
        {
            var location = await GetCurrentLocationAsync();

            if (location != null)
            {
                await Task.Delay(3000); // Splash delay
                await Shell.Current.GoToAsync(nameof(LandingPage));
            }
            else
            {
                await DisplayAlert("Location Required", "Please enable your device's location services.", "Exit");
                CloseApp();
            }
        }
        else
        {
            await DisplayAlert("Permission Denied", "Location permission is required to continue.", "Exit");
            CloseApp();
        }
    }

    private async Task<bool> RequestLocationPermissionAsync()
    {
        var status = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();

        if (status != PermissionStatus.Granted)
        {
            status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
        }

        return status == PermissionStatus.Granted;
    }

    private async Task<Location?> GetCurrentLocationAsync()
    {
        try
        {
            var location = await Geolocation.GetLastKnownLocationAsync();
            if (location == null)
            {
                location = await Geolocation.GetLocationAsync(new GeolocationRequest(GeolocationAccuracy.Medium));
            }

            return location;
        }
        catch
        {
            return null;
        }
    }

    private void CloseApp()
    {
#if ANDROID
        Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
#elif IOS
        // iOS does not allow programmatic exit; user must exit manually
        // You can optionally guide the user
        System.Diagnostics.Process.GetCurrentProcess().CloseMainWindow(); // not guaranteed to work on iOS
#elif WINDOWS
        Microsoft.UI.Xaml.Window.Current.Close();
#endif
    }
}
