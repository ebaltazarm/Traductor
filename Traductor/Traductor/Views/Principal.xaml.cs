using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Cloud.Speech.V1;
using MediaManager;
using MediaManager.Playback;
using Newtonsoft.Json;
using Plugin.CrossSpeechToText.Stt;
using Traductor.Models;
using Traductor.Models.Usuario;
using Traductor.ViewModels;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace Traductor.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Principal : ContentPage
	{
		private UsuarioModel _usuario = null;
		private bool         _speak   = false;
		
		public Principal()
		{
			Application.Current.PageAppearing += OnPageAppearing;
			InitializeComponent();
			this.BindingContext               =  new PrincipalViewModel();
			_usuario                          =  JsonConvert.DeserializeObject<UsuarioModel>(Preferences.Get("Usuario", "{}"));
		}
		
		private async void OnPageAppearing(object sender, Page e)
		{
			if (e is Principal && !this._speak)
			{
				this.videoView.IsVisible = false;
				this.oracion.Text               = "";
				//this.videoView.VideoPlaceholder = ImageSource.FromResource(obtnerRuta("/Images/Traductor/9k6CfbovDNsRNR4QKw0J.mp4"));
			}
		}

		private async void Traducir(object sender, EventArgs e)
		{
			try
			{
				var texto = oracion.Text;
				if (!string.IsNullOrEmpty(texto))
				{
					var query = new List<QueryModel>();
					query.Add(new QueryModel { Param = "region", Value    = _usuario.RegionId.ToString() });
					query.Add(new QueryModel { Param = "oracion", Value   = texto });
					query.Add(new QueryModel { Param = "usuarioId", Value = _usuario.Id.ToString() });
					var response     = Traductor.Servicios.Http.GetHttpData("Palabras/BuscarCoincidencias", query);
					
					var data     = JsonConvert.DeserializeObject<Response>(response);
					var palabras = JsonConvert.DeserializeObject<List<PalabrasGeneradas>>(data.data.ToString());
					
					ListarVideos(palabras.Select(x => obtnerRuta(x.Contenido)).ToList());
				}
			}
			catch (Exception exception)
			{
				
			}
		}

		public async void ListarVideos(List<string> palabras)
		{
			if (palabras.Count == 0)
			{
				await DisplayAlert("Alerta", "No se ha encontrado información", "Aceptar");
			}
			
			this.videoView.IsVisible = true;
			await CrossMediaManager.Current.Play(palabras);
		}

		public async void VideoInicial(string video)
		{
			await CrossMediaManager.Current.Play(video);
		}
		
		public string obtnerRuta(string ruta)
		{
			return Servicios.Http.UrlContenido() + ruta;
		}

		private async void ReconocimientoDeVoz(object sender, EventArgs e)
		{
			try
			{
				this._speak = true;
				var resultado = await CrossSpeechToText.StartVoiceInput("Hable");
				this.oracion.Text = resultado;
				this._speak       = false;
			}
			catch (Exception exception)
			{
				//ignore
			}
		}
	}
}