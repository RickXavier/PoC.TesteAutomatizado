using PoC.TesteAutomatizado.Dominio.Entidade;
using PoC.TesteAutomatizado.Interface.Repositorio;
using System.Collections.Generic;
using System.Linq;

namespace PoC.TesteAutomatizado.Repositorio.Mock
{
    public class PedidoRepositorioMock : IPedidoRepositorio
    {
        private static List<Pedido> _listaPedido = new List<Pedido>();

        public PedidoRepositorioMock()
        {

        }

        public void InserirPedido(Pedido pedido)
        {
            pedido.PedidoId = _listaPedido.Count + 1;
            _listaPedido.Add(pedido);
        }

        public IList<Pedido> ObterListaPedidoPorContratoId(int contratoId)
        {
            return _listaPedido.Where(p => p.ContratoId == contratoId).ToList();
        }
    }
}
