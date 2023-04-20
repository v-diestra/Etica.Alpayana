using Etica.Alpayana.Domain.Entity;

namespace Etica.Alpayana.Domain.Interfaces
{
    public interface IDenunciaRepository
    {
        Task<List<Combo>> listarSede();
        Task<List<Combo>> listarTipoReporte();
        int registrarDenuncia(Denuncia denuncia);
        bool actualizarDenuncia(Denuncia denuncia);
    }
}
