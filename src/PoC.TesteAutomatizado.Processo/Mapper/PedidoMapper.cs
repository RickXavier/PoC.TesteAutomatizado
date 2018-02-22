using PoC.TesteAutomatizado.Dominio.Dto;
using PoC.TesteAutomatizado.Dominio.Entidade;
using System.Collections.Generic;
using System.Linq;

namespace PoC.TesteAutomatizado.Processo.Mapper
{
    public static class PedidoMapper
    {
        public static PedidoDto CriarDto(this Pedido entidade)
        {
            return new PedidoDto()
            {
                PedidoId = entidade.PedidoId,
                ContratoId = entidade.ContratoId,
                Volume = entidade.Volume,
                DataPedido = entidade.DataPedido
            };
        }

        public static IList<PedidoDto> CriarDto(this IList<Pedido> entidade)
        {
            return entidade.ToList().ConvertAll(e => e.CriarDto());
        }

        public static Pedido CriarEntidade(this PedidoDto dto)
        {
            return new Pedido()
            {
                PedidoId = dto.PedidoId,
                ContratoId = dto.ContratoId,
                Volume = dto.Volume,
                DataPedido = dto.DataPedido
            };
        }
    }
}
