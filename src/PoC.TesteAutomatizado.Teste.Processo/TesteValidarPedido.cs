using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MSTestExtensions;
using PoC.TesteAutomatizado.Dominio.Entidade;
using PoC.TesteAutomatizado.Interface.Repositorio;
using PoC.TesteAutomatizado.Processo;
using PoC.TesteAutomatizado.Util;
using System;

namespace PoC.TesteAutomatizado.Teste.Processo
{
    [TestClass]
    public class TesteValidarPedido
    {
        private PedidoProcesso _pedidoProcesso;
        private Mock<IPedidoRepositorio> _pedidoRepositorio;
        private Mock<IContratoRepositorio> _contratoRepositorio;
        
        [TestInitialize]
        public void IniciarTestes()
        {
            _pedidoRepositorio = new Mock<IPedidoRepositorio>();
            _contratoRepositorio = new Mock<IContratoRepositorio>();

            _contratoRepositorio.Setup(m => m.ObterContrato(It.Is<int>(id => id == 1))).Returns(new Contrato()
            {
                ContratoId = 1,
                DataInicioVigencia = DateTime.Now.Date.AddDays(-10),
                DataFimVigencia = DateTime.Now.Date.AddDays(10),
                VolumeDisponivel = 100,
                Ativo = true
            });

            _contratoRepositorio.Setup(m => m.ObterContrato(It.Is<int>(id => id == 2))).Returns(new Contrato()
            {
                ContratoId = 2,
                DataInicioVigencia = DateTime.Now.Date.AddDays(-10),
                DataFimVigencia = DateTime.Now.Date.AddDays(10),
                VolumeDisponivel = 100,
                Ativo = false
            });

            _pedidoProcesso = new PedidoProcesso(_contratoRepositorio.Object, _pedidoRepositorio.Object);
        }

        [TestMethod]
        public void TesteValidarPedido_Erro_VolumeInvalido()
        {
            var contratoId = 1;
            var volume = 0.5f;
            var dataPedido = DateTime.Now.AddDays(1);

            ExceptionAssert.Throws<ExcecaoRegraNegocio>(() =>
            {
                var pedido = new Pedido()
                {
                    ContratoId = contratoId,
                    Volume = volume,
                    DataPedido = dataPedido
                };
                _pedidoProcesso.ValidarPedido(pedido);
            }, ExcecaoRegraNegocio.VOLUME_INVALIDO);
        }

        [TestMethod]
        public void TesteValidarPedido_Erro_DataInvalida()
        {
            var contratoId = 1;
            var volume = 5;
            var dataPedido = DateTime.Now.AddDays(-1);

            ExceptionAssert.Throws<ExcecaoRegraNegocio>(() =>
            {
                var pedido = new Pedido()
                {
                    ContratoId = contratoId,
                    Volume = volume,
                    DataPedido = dataPedido
                };
                _pedidoProcesso.ValidarPedido(pedido);
            }, ExcecaoRegraNegocio.DATA_INVALIDA);
        }

        [TestMethod]
        public void TesteValidarPedido_Erro_ContratoInativo()
        {
            var contratoId = 2;
            var volume = 5;
            var dataPedido = DateTime.Now.AddDays(1);

            ExceptionAssert.Throws<ExcecaoRegraNegocio>(() =>
            {
                var pedido = new Pedido()
                {
                    ContratoId = contratoId,
                    Volume = volume,
                    DataPedido = dataPedido
                };
                var contrato = _contratoRepositorio.Object.ObterContrato(contratoId);
                _pedidoProcesso.ValidarContrato(pedido, contrato);
            }, ExcecaoRegraNegocio.CONTRATO_INATIVO);
        }

        [TestMethod]
        public void TesteValidarPedido_Erro_VolumeIndisponivel()
        {
            var contratoId = 1;
            var volume = 200;
            var dataPedido = DateTime.Now.AddDays(1);

            ExceptionAssert.Throws<ExcecaoRegraNegocio>(() =>
            {
                var pedido = new Pedido()
                {
                    ContratoId = contratoId,
                    Volume = volume,
                    DataPedido = dataPedido
                };
                var contrato = _contratoRepositorio.Object.ObterContrato(contratoId);
                _pedidoProcesso.ValidarContrato(pedido, contrato);
            }, ExcecaoRegraNegocio.VOLUME_INDISPONIVEL);
        }

        [TestMethod]
        public void TesteValidarPedido_Erro_ContratoNaoVigente()
        {
            var contratoId = 1;
            var volume = 10;
            var dataPedido = DateTime.Now.AddDays(20);

            ExceptionAssert.Throws<ExcecaoRegraNegocio>(() =>
            {
                var pedido = new Pedido()
                {
                    ContratoId = contratoId,
                    Volume = volume,
                    DataPedido = dataPedido
                };
                var contrato = _contratoRepositorio.Object.ObterContrato(contratoId);
                _pedidoProcesso.ValidarContrato(pedido, contrato);
            }, ExcecaoRegraNegocio.CONTRATO_NAO_VIGENTE);
        }
    }
}
