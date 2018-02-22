using System.Configuration;
using System.Data.SQLite;

namespace PoC.TesteAutomatizado.Teste.Util
{
    public static class TesteUtil
    {
        private static string Diretorio
        {
            get
            {
                return System.AppContext.BaseDirectory;
            }
        }

        private static string CaminhoBancoDados
        {
            get
            {
                return ConfigurationManager.AppSettings["caminhoBancoDados"];
            }
        }

        private static SQLiteConnection CriarConexao()
        {
            return new SQLiteConnection(string.Format("Data Source={0}{1};Version=3;", Diretorio, CaminhoBancoDados));
        }

        private static void ExecutarComando(string sql)
        {
            using (var conexao = CriarConexao())
            {
                using (var comando = new SQLiteCommand(sql, conexao))
                    comando.Executar();
            }
        }

        private static void Executar(this SQLiteCommand comando)
        {
            if (comando.Connection.State == System.Data.ConnectionState.Closed)
                comando.Connection.Open();
            comando.ExecuteNonQuery();
        }

        public static void PrepararBanco()
        {
            ExecutarComando("DELETE FROM Contrato; UPDATE SQLITE_SEQUENCE SET seq = 0 WHERE name = 'Contrato'");
            ExecutarComando("DELETE FROM Pedido; UPDATE SQLITE_SEQUENCE SET seq = 0 WHERE name = 'Pedido'");
        }
    }
}
