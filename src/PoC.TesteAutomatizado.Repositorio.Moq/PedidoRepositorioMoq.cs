using Moq;
using PoC.TesteAutomatizado.Dominio.Entidade;
using PoC.TesteAutomatizado.Interface.Repositorio;
using System.Collections.Generic;
using System.Linq;

namespace PoC.TesteAutomatizado.Repositorio.Moq
{
    public class PedidoRepositorioMoq : IPedidoRepositorio
    {
        private static List<Pedido> _listaPedido = new List<Pedido>();

        private readonly Mock<IPedidoRepositorio> mock = null;

        public PedidoRepositorioMoq()
        {
            mock = new Mock<IPedidoRepositorio>();
            
            mock.Setup(m => m.InserirPedido(It.IsAny<Pedido>())).Callback((Pedido pedido) =>
            {
                _listaPedido.Add(pedido);
            });

            mock.Setup(m => m.ObterListaPedidoPorContratoId(It.IsAny<int>())).Returns((int contratoId) =>
            {
                return _listaPedido.Where(p => p.ContratoId == contratoId).ToList();
            });
        }

        public void InserirPedido(Pedido pedido)
        {
            mock.Object.InserirPedido(pedido);
        }

        public IList<Pedido> ObterListaPedidoPorContratoId(int contratoId)
        {
            return mock.Object.ObterListaPedidoPorContratoId(contratoId);
        }
    }
}
