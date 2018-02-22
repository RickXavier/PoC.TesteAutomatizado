using PoC.TesteAutomatizado.Dominio.Dto;
using PoC.TesteAutomatizado.Interface.Processo;
using PoC.TesteAutomatizado.Util;
using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;

namespace PoC.TesteAutomatizado.Api.Controllers
{
    public class PedidoController : ApiController
    {
        private readonly IPedidoProcesso _pedidoProcesso;

        public PedidoController(IPedidoProcesso pedidoProcesso)
        {
            _pedidoProcesso = pedidoProcesso;
        }

        [HttpGet]
        [Route("contratos/{contratoId:int}/pedidos")]
        [ResponseType(typeof(IList<PedidoDto>))]
        public IHttpActionResult ObterListaPedido(int contratoId)
        {
            try
            {
                return Ok(_pedidoProcesso.ObterListaPedidoPorContratoId(contratoId));
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpPost]
        [Route("contratos/{contratoId:int}/pedidos")]
        [ResponseType(typeof(void))]
        public IHttpActionResult InserirPedido(int contratoId, [FromBody] PedidoDto pedidoDto)
        {
            try
            {
                _pedidoProcesso.InserirPedido(pedidoDto);
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