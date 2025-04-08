namespace MauiApp4;

public partial class SplashScreen : ContentPage
{
	public SplashScreen()
	{
		InitializeComponent();
	}
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await Task.Delay(3000); // waits for 3 seconds
        Application.Current.MainPage = new LandingPage(); // navigate to Landing Page
    }

}