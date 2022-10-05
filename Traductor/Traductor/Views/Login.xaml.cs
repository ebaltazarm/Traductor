using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Traductor.Models;
using Traductor.Models.Usuario;
using Traductor.Servicios;
using Traductor.ViewModels;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Traductor.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Login : ContentPage
	{
		public Login()
		{
			InitializeComponent();
			var response = Traductor.Servicios.Http.GetHttpData("Palabras/ObtenerRegiones");

			var data  = JsonConvert.DeserializeObject<Response>(response);
			var items = JsonConvert.DeserializeObject<List<RegionModel>>(data.data.ToString());

			this.RegionId.DataSource        = items;
			this.RegionId.DisplayMemberPath = "Nombre";
			if (items.Count > 0)
			{
				this.RegionId.SelectedIndex = 0;
			}
		}

		public async void ButtonLogin(object sender, EventArgs e)
		{
			try
			{
				var region = (RegionModel)RegionId.SelectedItem;
				var model = new UsuarioModel
				            {
					            Email     = Email.Text,
					            Apellidos = Apellidos.Text,
					            Nombres   = Nombres.Text,
					            RegionId  = region.Id,
					            Telefono  = Telefono.Text,
				            };

				var response = await Http.Post("Palabras/CreateUsuario", JsonConvert.SerializeObject(model));
				var data     = JsonConvert.DeserializeObject<Response>(response);
				if (data.code == 200)
				{
					var usuario = JsonConvert.DeserializeObject<UsuarioModel>(data.data.ToString());
				
					Preferences.Set("Usuario", JsonConvert.SerializeObject(usuario));
					await Shell.Current.GoToAsync("//Principal");
				}
				else
				{
					
				}
			}
			catch (Exception exception)
			{
				await DisplayAlert("Alerta", exception.Message, "Aceptar");
			}
		}
	}
}