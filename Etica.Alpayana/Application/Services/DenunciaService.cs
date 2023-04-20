using Etica.Alpayana.Application.Interfaces;
using Etica.Alpayana.Common;
using Etica.Alpayana.Domain.Entity;
using Etica.Alpayana.Domain.Interfaces;
using System.IO;
using System.Net;
namespace Etica.Alpayana.Application.Services
{
    public class DenunciaService: IDenunciaService
    {
        public readonly IDenunciaRepository _IDenunciaRepository;
        //public readonly IConfiguration _IConfiguration;

        public DenunciaService(IDenunciaRepository iDenunciaRepository)
        {
            _IDenunciaRepository = iDenunciaRepository;
        }

        public async Task<List<Combo>> listarSede()
        {
            return await _IDenunciaRepository.listarSede();
        }
        public async Task<List<Combo>> listarTipoReporte()
        {
            var lista = await _IDenunciaRepository.listarTipoReporte();
            lista.Insert(0, new Combo { Codigo = "0", Descripcion = "SELECCIONA EL TIPO DE DENUNCIA A REGISTRAR" });
            return lista;
        }
        public Response denuncia(Denuncia denuncia, IFormFileCollection files)
        {
            Response response = new Response();
            string path = "\\\\srvapplication.alpayana.net\\wwwroot\\AlmacenamientoDocumentosWeb\\Denuncias";

            try
            {
                int IdDenuncia = _IDenunciaRepository.registrarDenuncia(denuncia);
                string Url = new Helper().guardarArchivos(files, path, IdDenuncia);
                denuncia.Evidencia = Url;
                denuncia.IdDenuncia = IdDenuncia;
                bool actuaizado = _IDenunciaRepository.actualizarDenuncia(denuncia);

                response.code = HttpStatusCode.Created;
                response.message = "Se registró correctamente su denuncia.";
            }
            catch (Exception ex)
            {
                response.code = HttpStatusCode.InternalServerError;
                response.message = "Error al registrar su denuncia, intente nuevamente por favor." + ex.Message.ToString();
            }

            return response;
        }
    }
}
