using PoC.TesteAutomatizado.Dominio.Dto;
using PoC.TesteAutomatizado.Interface.Processo;
using PoC.TesteAutomatizado.Util;
using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;

namespace PoC.TesteAutomatizado.Api.Controllers
{   
    public class ContratoController : ApiController
    {
        private readonly IContratoProcesso _contratoProcesso;

        public ContratoController(IContratoProcesso contratoProcesso)
        {
            _contratoProcesso = contratoProcesso;
        }

        [HttpGet]
        [Route("contratos")]
        [ResponseType(typeof(IList<ContratoDto>))]
        public IHttpActionResult ObterListaContrato()
        {   
            try
            {
                return Ok(_contratoProcesso.ObterListaContrato());
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpPost]
        [Route("contratos")]
        [ResponseType(typeof(void))]
        public IHttpActionResult InserirContrato([FromBody] ContratoDto contratoDto)
        {
            try
            {
                _contratoProcesso.InserirContrato(contratoDto);
                return Ok();
            }
            catch (ExcecaoRegraNegocio e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

    }
}
