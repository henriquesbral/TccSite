using Microsoft.AspNetCore.Mvc;
using TccSite.Domain.Entities;
using TccSite.Domain.Interfaces;
using TccSite.Web.ViewModels;

namespace TccSite.Controllers
{
    public class ConfiguracoesController : BaseController
    {
        private readonly IConfiguracoesRepository _context;

        public ConfiguracoesController(IConfiguracoesRepository context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            //var config = await _context.GetConfiguracaoAsync();

            //var vm = new ConfiguracoesViewModel
            //{
            //    LimiteAlertaBaixo = config.LimiteAlertaBaixo,
            //    LimiteAlertaMedio = config.LimiteAlertaMedio,
            //    LimiteAlertaAlto = config.LimiteAlertaAlto,
            //    FrequenciaCapturaMinutos = config.FrequenciaCapturaMinutos,
            //    NotificacaoEmail = config.NotificacaoEmail,
            //    NotificacaoWhatsapp = config.NotificacaoWhatsapp
            //};

            return View();
        }

        [HttpPost]
        public IActionResult Salvar(ConfiguracoesViewModel vm)
        {
            if (!ModelState.IsValid)
                return View("Index", vm);

            var config = new Configuracoes
            {
                Id = 1, // se sempre existir apenas um registro de configuração
                LimiteAlertaBaixo = vm.LimiteAlertaBaixo,
                LimiteAlertaMedio = vm.LimiteAlertaMedio,
                LimiteAlertaAlto = vm.LimiteAlertaAlto,
                LimiteAlertaCritico = vm.LimiteAlertaCritico,
                FrequenciaCaptura = vm.FrequenciaCaptura
            };

            _context.AtualizarConfiguracao(config);

            TempData["Mensagem"] = "Configurações salvas com sucesso!";
            return RedirectToAction("Index");
        }
    }

}
