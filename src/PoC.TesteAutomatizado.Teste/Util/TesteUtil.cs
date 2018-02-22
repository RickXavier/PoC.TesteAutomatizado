using PoC.TesteAutomatizado.Util;
using System;
using System.Data.SQLite;

namespace PoC.TesteAutomatizado.Teste.Util
{
    public static class TesteUtil
    {   
        public static void CriarBanco()
        {
            SQLiteConnection.CreateFile("tmp_database.sqlite");

            var sqlCreateTableContrato = @"CREATE TABLE ""Contrato"" ( `ContratoId` INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, `VolumeDisponivel` NUMERIC NOT NULL, `DataInicioVigencia` INTEGER NOT NULL, `DataFimVigencia` INTEGER NOT NULL, `Ativo` INTEGER NOT NULL )";
            var sqlCreateTablePedido = @"CREATE TABLE ""Pedido"" ( `PedidoId` INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, `ContratoId` INTEGER NOT NULL, `Volume` NUMERIC NOT NULL, `DataPedido` INTEGER NOT NULL )";

            ConexaoBase.Executar(sqlCreateTableContrato);
            ConexaoBase.Executar(sqlCreateTablePedido);
        }

        public static void PrepararBanco()
        {
            ConexaoBase.Executar("DELETE FROM Contrato; UPDATE SQLITE_SEQUENCE SET seq = 0 WHERE name = 'Contrato'");
            ConexaoBase.Executar("DELETE FROM Pedido; UPDATE SQLITE_SEQUENCE SET seq = 0 WHERE name = 'Pedido'");
            
            var dataInicio = DateTime.Now.Date.AddDays(-10).Ticks;
            var dataFim = DateTime.Now.Date.AddDays(10).Ticks;

            ConexaoBase.Executar(string.Format("INSERT INTO Contrato (VolumeDisponivel, DataInicioVigencia, DataFimVigencia, Ativo) VALUES (10, {0}, {1}, 1)", dataInicio, dataFim));
            ConexaoBase.Executar(string.Format("INSERT INTO Contrato (VolumeDisponivel, DataInicioVigencia, DataFimVigencia, Ativo) VALUES (10, {0}, {1}, 0)", dataInicio, dataFim));
            
        }
    }
}
