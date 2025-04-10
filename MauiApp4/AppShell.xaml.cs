namespace MauiApp4
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(LandingPage), typeof(LandingPage));
            Routing.RegisterRoute(nameof(FilingOfComplaint), typeof(FilingOfComplaint));

        }

    }
}
