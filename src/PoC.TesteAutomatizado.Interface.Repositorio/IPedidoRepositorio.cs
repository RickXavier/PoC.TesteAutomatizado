using PoC.TesteAutomatizado.Dominio.Entidade;
using System.Collections.Generic;

namespace PoC.TesteAutomatizado.Interface.Repositorio
{
    public interface IPedidoRepositorio
    {   
        void InserirPedido(Pedido pedido);
        IList<Pedido> ObterListaPedidoPorContratoId(int contratoId);
    }
}
