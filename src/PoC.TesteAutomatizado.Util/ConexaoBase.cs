using System.Configuration;
using System.Data.SQLite;

namespace PoC.TesteAutomatizado.Util
{
    public static class ConexaoBase
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

        public static SQLiteConnection CriarConexao()
        {
            return new SQLiteConnection(string.Format("Data Source={0}{1};Version=3;", Diretorio, CaminhoBancoDados));
        }

        public static void Executar(string sql)
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
