namespace Traductor.Models.Usuario
{
	public class UsuarioModel
	{
		public int    Id             { get; set; }
		public string Nombres        { get; set; }
		public string Apellidos      { get; set; }
		public string NombreCompleto { get; set; }
		public string Email          { get; set; }
		public int    RegionId       { get; set; }
		public string Region         { get; set; }
		public string Telefono       { get; set; }
	}
}