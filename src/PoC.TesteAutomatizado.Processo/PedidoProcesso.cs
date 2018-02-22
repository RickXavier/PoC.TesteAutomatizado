using PoC.TesteAutomatizado.Dominio.Dto;
using PoC.TesteAutomatizado.Interface.Processo;
using PoC.TesteAutomatizado.Interface.Repositorio;
using PoC.TesteAutomatizado.Processo.Mapper;
using PoC.TesteAutomatizado.Util;
using System;
using System.Collections.Generic;

namespace PoC.TesteAutomatizado.Processo
{
    public class PedidoProcesso : IPedidoProcesso
    {

        private readonly IContratoRepositorio _contratoRepositorio;
        private readonly IPedidoRepositorio _pedidoRepositorio;

        public PedidoProcesso(IContratoRepositorio contratoRepositorio, IPedidoRepositorio pedidoRepositorio)
        {
            _contratoRepositorio = contratoRepositorio;
            _pedidoRepositorio = pedidoRepositorio;
        }

        public void InserirPedido(PedidoDto pedidoDto)
        {
            var pedido = pedidoDto.CriarEntidade(); 

            if (pedido.Volume < 1)
                throw new ExcecaoRegraNegocio(ExcecaoRegraNegocio.VOLUME_INVALIDO);

            if (pedido.DataPedido < DateTime.Now)
                throw new ExcecaoRegraNegocio(ExcecaoRegraNegocio.DATA_INVALIDA);

            var contrato = _contratoRepositorio.ObterContrato(pedido.ContratoId);

            if (!contrato.Ativo)
                throw new ExcecaoRegraNegocio(ExcecaoRegraNegocio.CONTRATO_INATIVO);

            if (pedido.Volume > contrato.VolumeDisponivel)
                throw new ExcecaoRegraNegocio(ExcecaoRegraNegocio.VOLUME_INDISPONIVEL);

            if (pedido.DataPedido > contrato.DataFimVigencia)
                throw new ExcecaoRegraNegocio(ExcecaoRegraNegocio.CONTRATO_NAO_VIGENTE);

            _contratoRepositorio.AtualizarVolume(contrato.ContratoId, contrato.VolumeDisponivel - pedido.Volume);
            _pedidoRepositorio.InserirPedido(pedido);
        }

        public IList<PedidoDto> ObterListaPedidoPorContratoId(int contratoId)
        {
            return _pedidoRepositorio.ObterListaPedidoPorContratoId(contratoId).CriarDto();
        }
    }
}
