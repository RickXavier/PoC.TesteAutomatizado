using PoC.TesteAutomatizado.Dominio.Entidade;
using PoC.TesteAutomatizado.Interface.Repositorio;
using PoC.TesteAutomatizado.Repositorio.Mapper;
using PoC.TesteAutomatizado.Util;
using System.Collections.Generic;
using System.Data.SQLite;

namespace PoC.TesteAutomatizado.Repositorio
{
    public class ContratoRepositorio : IContratoRepositorio
    {
        public void AtualizarVolume(int contratoId, float novoVolume)
        {
            string sql = @"UPDATE Contrato
                    SET VolumeDisponivel = @VolumeDisponivel
                    WHERE ContratoId = @ContratoId";

            using (var conexao = ConexaoBase.CriarConexao())
            {
                using (var comando = new SQLiteCommand(sql, conexao))
                {
                    comando.Parameters.AddWithValue("VolumeDisponivel", novoVolume);
                    comando.Parameters.AddWithValue("ContratoId", contratoId);

                    comando.Executar();
                }
            }
        }

        public void InserirContrato(Contrato contrato)
        {
            string sql = @"INSERT INTO Contrato (VolumeDisponivel, DataInicioVigencia, DataFimVigencia, Ativo)
                    VALUES (@VolumeDisponivel, @DataInicioVigencia, @DataFimVigencia, @Ativo)";

            using (var conexao = ConexaoBase.CriarConexao())
            {
                using (var comando = new SQLiteCommand(sql, conexao))
                {
                    comando.Parameters.AddWithValue("VolumeDisponivel", contrato.VolumeDisponivel);
                    comando.Parameters.AddWithValue("DataInicioVigencia", contrato.DataInicioVigencia.Ticks);
                    comando.Parameters.AddWithValue("DataFimVigencia", contrato.DataFimVigencia.Ticks);
                    comando.Parameters.AddWithValue("Ativo", contrato.Ativo);

                    comando.Executar();
                }
            }
        }

        public Contrato ObterContrato(int contratoId)
        {
            string sql = "SELECT * FROM Contrato WHERE ContratoId = @ContratoId";

            using (var conexao = ConexaoBase.CriarConexao())
            {
                using (var comando = new SQLiteCommand(sql, conexao))
                {
                    comando.Parameters.AddWithValue("ContratoId", contratoId);
                    using (var consulta = comando.ExecutarLeitura())
                    {
                        if (consulta.Read())
                            return consulta.CriarContrato();
                        else
                            return null;
                    }
                }
            }
        }

        public IList<Contrato> ObterListaContrato()
        {
            var listaRetorno = new List<Contrato>();

            string sql = "SELECT * FROM Contrato";

            using (var conexao = ConexaoBase.CriarConexao())
            {
                using (var comando = new SQLiteCommand(sql, conexao))
                {
                    using (var consulta = comando.ExecutarLeitura())
                    {
                        while (consulta.Read())
                            listaRetorno.Add(consulta.CriarContrato());
                    }
                }
            }

            return listaRetorno;
        }
    }
}
