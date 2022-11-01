using System.Collections.Generic;

namespace Traductor.Models
{
	public class Response
	{
		public object data          { get; set; }
		public int?   MaxJsonLength { get; set; }
		public int?   code          { get; set; }
		public Errors errors        { get; set; }
	}

	public class Errors
	{
		public string Error { get; set; }
	}
}