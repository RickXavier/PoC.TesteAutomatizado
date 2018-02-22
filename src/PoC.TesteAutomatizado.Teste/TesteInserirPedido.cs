using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSTestExtensions;
using PoC.TesteAutomatizado.Dominio.Dto;
using PoC.TesteAutomatizado.Interface.Processo;
using PoC.TesteAutomatizado.Util;
using System;
using System.Linq;

namespace PoC.TesteAutomatizado.Teste
{
    [TestClass]
    public class TesteInserirPedido
    {
        private IPedidoProcesso _pedidoProcesso;
        private IContratoProcesso _contratoProcesso;
        
        [TestInitialize]
        public void IniciarTestes()
        {
            InjetorDependencias.InjetorDependencias.IniciarMock();

            _contratoProcesso = InjetorDependencias.InjetorDependencias.ObterInstancia<IContratoProcesso>();
            _pedidoProcesso = InjetorDependencias.InjetorDependencias.ObterInstancia<IPedidoProcesso>();
        }

        [TestMethod]
        public void TesteInserirPedido_Sucesso()
        {
            var contratoId = 1;
            var volume = 5;
            var dataPedido = DateTime.Now.AddDays(1);

            var contratoOriginal = _contratoProcesso.ObterListaContrato().FirstOrDefault(c => c.ContratoId == contratoId);

            _pedidoProcesso.InserirPedido(new PedidoDto()
            {
                ContratoId = contratoId,
                DataPedido = dataPedido,
                Volume = volume
            });
            
            var listaPedidoConsulta = _pedidoProcesso.ObterListaPedidoPorContratoId(contratoId);

            Assert.IsNotNull(listaPedidoConsulta);
            Assert.AreEqual(1, listaPedidoConsulta.Count);
            Assert.AreEqual(volume, listaPedidoConsulta.FirstOrDefault().Volume);
            Assert.AreEqual(dataPedido, listaPedidoConsulta.FirstOrDefault().DataPedido);

            var contratoAposCriacao = _contratoProcesso.ObterListaContrato().FirstOrDefault(c => c.ContratoId == contratoId);
            Assert.AreEqual(contratoOriginal.VolumeDisponivel, contratoAposCriacao.VolumeDisponivel + volume);
        }

        [TestMethod]
        public void TesteInserirPedido_Erro_VolumeInvalido()
        {
            var contratoId = 1;
            var volume = 0.5f;
            var dataPedido = DateTime.Now.AddDays(1);

            ExceptionAssert.Throws<ExcecaoRegraNegocio>(() =>
            {
                _pedidoProcesso.InserirPedido(new PedidoDto()
                {
                    ContratoId = contratoId,
                    DataPedido = dataPedido,
                    Volume = volume
                });
            }, ExcecaoRegraNegocio.VOLUME_INVALIDO);
        }

        [TestMethod]
        public void TesteInserirPedido_Erro_DataInvalida()
        {
            var contratoId = 1;
            var volume = 5;
            var dataPedido = DateTime.Now.AddDays(-1);

            ExceptionAssert.Throws<ExcecaoRegraNegocio>(() =>
            {
                _pedidoProcesso.InserirPedido(new PedidoDto()
                {
                    ContratoId = contratoId,
                    DataPedido = dataPedido,
                    Volume = volume
                });
            }, ExcecaoRegraNegocio.DATA_INVALIDA);
        }

        [TestMethod]
        public void TesteInserirPedido_Erro_ContratoInativo()
        {
            var contratoId = 2;
            var volume = 5;
            var dataPedido = DateTime.Now.AddDays(1);

            ExceptionAssert.Throws<ExcecaoRegraNegocio>(() =>
            {
                _pedidoProcesso.InserirPedido(new PedidoDto()
                {
                    ContratoId = contratoId,
                    DataPedido = dataPedido,
                    Volume = volume
                });
            }, ExcecaoRegraNegocio.CONTRATO_INATIVO);
        }

        [TestMethod]
        public void TesteInserirPedido_Erro_VolumeIndisponivel()
        {
            var contratoId = 1;
            var volume = 200;
            var dataPedido = DateTime.Now.AddDays(1);

            ExceptionAssert.Throws<ExcecaoRegraNegocio>(() =>
            {
                _pedidoProcesso.InserirPedido(new PedidoDto()
                {
                    ContratoId = contratoId,
                    DataPedido = dataPedido,
                    Volume = volume
                });
            }, ExcecaoRegraNegocio.VOLUME_INDISPONIVEL);
        }

        [TestMethod]
        public void TesteInserirPedido_Erro_ContratoNaoVigente()
        {
            var contratoId = 1;
            var volume = 10;
            var dataPedido = DateTime.Now.AddDays(20);

            ExceptionAssert.Throws<ExcecaoRegraNegocio>(() =>
            {
                _pedidoProcesso.InserirPedido(new PedidoDto()
                {
                    ContratoId = contratoId,
                    DataPedido = dataPedido,
                    Volume = volume
                });
            }, ExcecaoRegraNegocio.CONTRATO_NAO_VIGENTE);
        }
    }
}
