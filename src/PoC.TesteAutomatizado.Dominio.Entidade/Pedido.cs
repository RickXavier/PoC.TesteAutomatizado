using System;

namespace PoC.TesteAutomatizado.Dominio.Entidade
{
    public class Pedido
    {
        public int PedidoId { get; set; }
        public int ContratoId { get; set; }
        public float Volume { get; set; }
        public DateTime DataPedido { get; set; }
    }
}
