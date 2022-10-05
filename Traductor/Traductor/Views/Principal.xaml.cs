using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Cloud.Speech.V1;
using MediaManager;
using MediaManager.Playback;
using Newtonsoft.Json;
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
		
		public Principal()
		{
			InitializeComponent();
			this.BindingContext = new PrincipalViewModel();
			_usuario = JsonConvert.DeserializeObject<UsuarioModel>(Preferences.Get("Usuario", "{}"));
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
			
			await CrossMediaManager.Current.Play(palabras);
		}
		
		public string obtnerRuta(string ruta)
		{
			return Servicios.Http.UrlContenido() + ruta;
		}

		private void ReconocimientoDeVoz(object sender, EventArgs e)
		{
			try
			{
				var speech = SpeechClient.Create();
				var config = new RecognitionConfig
				             {
					             Encoding        = RecognitionConfig.Types.AudioEncoding.Flac,
					             SampleRateHertz = 16000,
					             LanguageCode    = LanguageCodes.Spanish.Guatemala
				             };
				var audio = RecognitionAudio.FromStorageUri("gs://cloud-samples-tests/speech/brooklyn.flac");         

				var response = speech.Recognize(config, audio);

				var resultado = "";

				foreach (var result in response.Results)
				{
					foreach (var alternative in result.Alternatives)
					{
						resultado += alternative.Transcript;
					}
				}

				if (resultado == null)
				{
				
				}
			}
			catch (Exception exception)
			{
				
			}
		}
	}
}