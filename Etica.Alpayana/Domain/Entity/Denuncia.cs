namespace Etica.Alpayana.Domain.Entity
{
    public class Denuncia
    {
        public int IdDenuncia { get; set; }
        public string? TipoReporte { get; set; }
        public long CodigoDenuncia { get; set; }
        public string? DenunciadoNombre { get; set; }
        public string? DenunciadoSede { get; set; }
        public string? DenunciadoPuesto { get; set; }
        public int DenuncianteTipo { get; set; }
        public int DenuncianteIdentidad { get; set; }
        public string? DenuncianteNombre { get; set; }
        public string? DenuncianteSede { get; set; }
        public string? DenuncianteCorreo { get; set; }
        public string? DenuncianteTelefono { get; set; }
        public string? VictimaNombre { get; set; }
        public string? VictimaSede { get; set; }
        public string? LugarIncidente { get; set; }
        public string? Detalle { get; set; }
        public string? Evidencia { get; set; }
    }
}
