using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
	public partial class CambiarInformacion : ContentPage
	{
		private UsuarioModel _user = null;
		public CambiarInformacion()
		{
			InitializeComponent();
			Application.Current.PageAppearing += OnPageAppearing;
		}

		private async void OnPageAppearing(object sender, Page e)
		{
			if (e is CambiarInformacion)
			{
				_user = JsonConvert.DeserializeObject<UsuarioModel>(Preferences.Get("Usuario", "{}"));
				ObtenerRegiones(_user);

				this.Nombres.Text   = _user.Nombres;
				this.Apellidos.Text = _user.Apellidos;
				this.Telefono.Text  = _user.Telefono;
			}
		}

		public async void Guardar(object sender, EventArgs e)
		{
			try
			{
				var region = (RegionModel)RegionId.SelectedItem;
				var model = new UsuarioModel
				            {
					            Apellidos = Apellidos.Text,
					            Nombres   = Nombres.Text,
					            RegionId  = region.Id,
					            Telefono  = Telefono.Text,
					            Id        = _user.Id
				            };

				var response = await Http.Post("Palabras/UpdateUsuario", JsonConvert.SerializeObject(model));
				var data     = JsonConvert.DeserializeObject<Response>(response);
				if (data.code == 200)
				{
					_user = JsonConvert.DeserializeObject<UsuarioModel>(data.data.ToString());

					Preferences.Set("Usuario", JsonConvert.SerializeObject(_user));
					
					await DisplayAlert("Alerta", "Se edito correctamente", "Aceptar");
				}
				else
				{
					await DisplayAlert("Error", data.errors.Error , "Aceptar");
				}
			}
			catch (Exception exception)
			{
				await DisplayAlert("Alerta", exception.Message, "Aceptar");
			}
		}

		public void ObtenerRegiones(UsuarioModel user)
		{
			try
			{
				var response = Http.GetHttpData("Palabras/ObtenerRegiones");

				var data  = JsonConvert.DeserializeObject<Response>(response);
				var items = JsonConvert.DeserializeObject<List<RegionModel>>(data.data.ToString());

				this.RegionId.DataSource        = items;
				this.RegionId.DisplayMemberPath = "Nombre";
				if (items.Count > 0)
				{
					this.RegionId.SelectedItem = new
					                              {
						                              Id     = user.RegionId,
						                              Nombre = user.Region
					                              };
				}
			}
			catch (Exception e)
			{
				//
			}
		}
	}
}