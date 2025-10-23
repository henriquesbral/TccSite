using Microsoft.AspNetCore.Mvc;
using TccSite.Application.Interfaces;
using TccSite.Domain.Entities;
using TccSite.Web.ViewModels;

namespace TccSite.Controllers
{
    public class ConfiguracoesController : BaseController
    {
        private readonly IConfiguracoesService _configuracoesService;

        public ConfiguracoesController(IConfiguracoesService configuracoesService)
        {
            _configuracoesService = configuracoesService;
        }

        public IActionResult Index()
        {
            var config = _configuracoesService.GetConfiguracao();

            var configuracoes = new ConfiguracoesViewModel();

            if (config != null)
            {
                configuracoes.LimiteAlertaBaixo = config.LimiteAlertaBaixo;
                configuracoes.LimiteAlertaMedio = config.LimiteAlertaMedio;
                configuracoes.LimiteAlertaAlto = config.LimiteAlertaAlto;
                configuracoes.FrequenciaCaptura = config.FrequenciaCaptura;
                configuracoes.NotificarEmail = config.NotificarEmail;
                configuracoes.NotificacaoWhatsapp = config.NotificacaoWhatsapp;
            }

            return View(configuracoes);
        }

        [HttpPost]
        public IActionResult Salvar(ConfiguracoesViewModel newConfig)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View("Index", newConfig);

                var config = new Configuracoes
                {
                    LimiteAlertaBaixo = newConfig.LimiteAlertaBaixo,
                    LimiteAlertaMedio = newConfig.LimiteAlertaMedio,
                    LimiteAlertaAlto = newConfig.LimiteAlertaAlto,
                    LimiteAlertaCritico = newConfig.LimiteAlertaCritico,
                    FrequenciaCaptura = newConfig.FrequenciaCaptura,
                    NotificacaoWhatsapp = newConfig.NotificacaoWhatsapp,
                    NotificarEmail = newConfig.NotificarEmail
                };

                _configuracoesService.AtualizarConfiguracao(config);

                return View();
            }
            catch (Exception ex) 
            {
                return Json(new {success = false, message = $"Ocorreu um erro ao salvar as configurações: {ex.Message}" });
            }
        }
    }

}
