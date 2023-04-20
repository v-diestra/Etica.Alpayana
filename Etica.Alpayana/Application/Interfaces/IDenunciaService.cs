using Etica.Alpayana.Domain.Entity;

namespace Etica.Alpayana.Application.Interfaces
{
    public interface IDenunciaService
    {
        Task<List<Combo>> listarSede();
        Task<List<Combo>> listarTipoReporte();
        Response denuncia(Denuncia denuncia, IFormFileCollection files);
    }
}
