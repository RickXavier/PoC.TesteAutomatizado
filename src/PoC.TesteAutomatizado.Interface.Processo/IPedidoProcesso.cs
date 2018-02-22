using PoC.TesteAutomatizado.Dominio.Dto;
using System.Collections.Generic;

namespace PoC.TesteAutomatizado.Interface.Processo
{
    public interface IPedidoProcesso
    {
        void InserirPedido(PedidoDto pedidoDto);
        IList<PedidoDto> ObterListaPedidoPorContratoId(int contratoId);
    }
}
