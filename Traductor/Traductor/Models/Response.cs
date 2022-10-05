namespace Traductor.Models
{
	public class Response
	{
		public object data          { get; set; }
		public int?   MaxJsonLength { get; set; }
		public int?   code          { get; set; }
	}
}