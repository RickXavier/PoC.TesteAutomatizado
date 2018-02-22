using PoC.TesteAutomatizado.Dominio.Entidade;
using PoC.TesteAutomatizado.Interface.Repositorio;
using PoC.TesteAutomatizado.Repositorio.Mapper;
using PoC.TesteAutomatizado.Util;
using System.Collections.Generic;
using System.Data.SQLite;

namespace PoC.TesteAutomatizado.Repositorio
{
    public class PedidoRepositorio : IPedidoRepositorio
    {
        public void InserirPedido(Pedido pedido)
        {
            var sql = @"INSERT INTO Pedido (ContratoId, Volume, DataPedido)
                    VALUES (@ContratoId, @Volume, @DataPedido)";

            using (var conexao = ConexaoBase.CriarConexao())
            {
                using (var comando = new SQLiteCommand(sql, conexao))
                {
                    comando.Parameters.AddWithValue("ContratoId", pedido.ContratoId);
                    comando.Parameters.AddWithValue("Volume", pedido.Volume);
                    comando.Parameters.AddWithValue("DataPedido", pedido.DataPedido.Ticks);

                    comando.Executar();
                }
            }
        }

        public IList<Pedido> ObterListaPedidoPorContratoId(int contratoId)
        {
            var listaRetorno = new List<Pedido>();

            string sql = "SELECT * FROM Pedido WHERE ContratoId = @ContratoId";
            
            using (var conexao = ConexaoBase.CriarConexao())
            {
                using (var comando = new SQLiteCommand(sql, conexao))
                {
                    comando.Parameters.AddWithValue("ContratoId", contratoId);

                    using (var consulta = comando.ExecutarLeitura())
                    {
                        while (consulta.Read())
                            listaRetorno.Add(consulta.CriarPedido());
                    }
                }
            }

            return listaRetorno;
        }
    }
}
