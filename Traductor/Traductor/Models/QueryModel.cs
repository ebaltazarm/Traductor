namespace Traductor.Models
{
	public class QueryModel
	{
		private string param;
		private string value;

		public string Param
		{
			get => param;
			set => param = value;
		}

		public string Value
		{
			get => value;
			set => this.value = value;
		}
	}
}