using System.Data.SQLite;

namespace PoC.TesteAutomatizado.Repositorio.Base
{
    public static class ConexaoBase
    {
        public static SQLiteConnection CriarConexao()
        {
            return new SQLiteConnection(string.Format("Data Source={0}bin\\db\\database.sqlite;Version=3;", System.AppContext.BaseDirectory));
        }

        public static void ExecutarComando(string sql)
        {
            using (var conexao = CriarConexao())
            {
                using (var comando = new SQLiteCommand(sql, conexao))
                    comando.Executar();
            }
        }

        public static void Executar(this SQLiteCommand comando)
        {
            if (comando.Connection.State == System.Data.ConnectionState.Closed)
                comando.Connection.Open();
            comando.ExecuteNonQuery();
        }

        public static SQLiteDataReader ExecutarLeitura(this SQLiteCommand comando)
        {
            if (comando.Connection.State == System.Data.ConnectionState.Closed)
                comando.Connection.Open();
            return comando.ExecuteReader();
        }
    }
}
