using Xamarin.Forms;

namespace Traductor.ViewModels
{
	public class PrincipalViewModel : BaseViewModel
	{
		public Command PrincipalCommand { get; }

		public PrincipalViewModel()
		{
			PrincipalCommand = new Command(OnTraducirClicked);
		}

		private async void OnTraducirClicked(object obj)
		{
			
		}
	}
}