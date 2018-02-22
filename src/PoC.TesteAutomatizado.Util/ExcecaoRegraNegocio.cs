using System;

namespace PoC.TesteAutomatizado.Util
{
    public class ExcecaoRegraNegocio : Exception
    {
        public ExcecaoRegraNegocio(string mensagem) : base(mensagem) { }

        public const string VOLUME_INVALIDO = "volume_invalido";
        public const string DATA_INVALIDA = "data_invalida";
        public const string CONTRATO_INATIVO = "contrato_inativo";
        public const string VOLUME_INDISPONIVEL = "volume_indisponivel";
        public const string CONTRATO_NAO_VIGENTE = "contrato_nao_vigente";
    }
}
