using PoC.TesteAutomatizado.Dominio.Dto;
using PoC.TesteAutomatizado.Interface.Processo;
using PoC.TesteAutomatizado.Interface.Repositorio;
using PoC.TesteAutomatizado.Processo.Mapper;
using PoC.TesteAutomatizado.Util;
using System;
using System.Collections.Generic;

namespace PoC.TesteAutomatizado.Processo
{
    public class ContratoProcesso : IContratoProcesso
    {

        private readonly IContratoRepositorio _contratoRepositorio;

        public ContratoProcesso(IContratoRepositorio contratoRepositorio)
        {
            _contratoRepositorio = contratoRepositorio;
        }

        public void InserirContrato(ContratoDto contratoDto)
        {
            var contrato = contratoDto.CriarEntidade();

            if (contrato.VolumeDisponivel < 1)
                throw new ExcecaoRegraNegocio(ExcecaoRegraNegocio.VOLUME_INVALIDO);

            if (contrato.DataFimVigencia < DateTime.Now)
                throw new ExcecaoRegraNegocio(ExcecaoRegraNegocio.DATA_INVALIDA);

            _contratoRepositorio.InserirContrato(contrato);
        }

        public IList<ContratoDto> ObterListaContrato()
        {
            return _contratoRepositorio.ObterListaContrato().CriarDto();
        }
    }
}
