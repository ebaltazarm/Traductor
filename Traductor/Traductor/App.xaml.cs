using System;
using MediaManager;
using Newtonsoft.Json;
using Traductor.Models.Usuario;
using Traductor.Services;
using Traductor.Views;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Traductor
{
    public partial class App : Application
    {
        public App()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NzI3MjA1QDMyMzAyZTMyMmUzMEw1WVM2Szg5WFBWQTY0azFzdEUzOGpNMTdlNkdEVWJxUUhyV0RpSlA1b1E9");
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new AppShell();
           
        }

        protected override void OnStart()
        {
            try
            {
                var usuario = JsonConvert.DeserializeObject<UsuarioModel>(Preferences.Get("Usuario", "{}"));

                if (usuario.RegionId == 0)
                {
                    Shell.Current.GoToAsync("//Login");
                }
                else
                {
                    Shell.Current.GoToAsync("//Principal");
                }
            }
            catch (Exception e)
            {
                //ignore
            }
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
