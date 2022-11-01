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
	public partial class Comentarios : ContentPage
	{
		private UsuarioModel _usuario = null;
		
		public Comentarios()
		{
			InitializeComponent();
			_usuario = JsonConvert.DeserializeObject<UsuarioModel>(Preferences.Get("Usuario", "{}"));
		}
		
		public async void Guardar(object sender, EventArgs e)
		{
			try
			{
				var model = new
				            {
					            RegionId  = _usuario.RegionId,
					            UsuarioId = _usuario.Id,
					            Mensaje   = this.Mensaje.Text,
				            };

				var response = await Http.Post("Palabras/CreateRecomendacion", JsonConvert.SerializeObject(model));
				var data     = JsonConvert.DeserializeObject<Response>(response);
				if (data.code == 200)
				{
					await DisplayAlert("Alerta", "Su comentario fue agregado", "Aceptar");
					this.Mensaje.Text = "";
				}
				else
				{
					await DisplayAlert("Error", data.errors.Error ,"Aceptar");
				}
			}
			catch (Exception exception)
			{
				await DisplayAlert("Alerta", exception.Message, "Aceptar");
			}
		}
	}
}