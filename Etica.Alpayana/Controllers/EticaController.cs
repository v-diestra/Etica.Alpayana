using Etica.Alpayana.Application.Interfaces;
using Etica.Alpayana.Application.Services;
using Etica.Alpayana.Domain.Entity;
using Microsoft.AspNetCore.Mvc;

namespace Etica.Alpayana.Controllers
{
    public class EticaController : Controller
    {
        public readonly IDenunciaService _IDenunciaService;
        public EticaController(IDenunciaService iDenunciaService)
        {
            _IDenunciaService = iDenunciaService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult guardarDenuncia(Denuncia denuncia, IFormFileCollection files)
        {
            return new JsonResult(_IDenunciaService.denuncia(denuncia, files));
        }

        [HttpGet]
        public async Task<ActionResult> listarSede()
        {
            return new JsonResult(await _IDenunciaService.listarSede());
        }
        [HttpGet]
        public async Task<ActionResult> listarTipoReporte()
        {
            return new JsonResult(await _IDenunciaService.listarTipoReporte());
        }
    }
}
