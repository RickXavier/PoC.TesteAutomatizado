using Moq;
using PoC.TesteAutomatizado.Dominio.Entidade;
using PoC.TesteAutomatizado.Interface.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PoC.TesteAutomatizado.Repositorio.Moq
{
    public class ContratoRepositorioMoq : IContratoRepositorio
    {
        private static List<Contrato> _listaContrato = new List<Contrato>
        {
            new Contrato()
            {
                ContratoId = 1,
                DataInicioVigencia = DateTime.Now.Date.AddDays(-10),
                DataFimVigencia = DateTime.Now.Date.AddDays(10),
                VolumeDisponivel = 100,
                Ativo = true
            },
            new Contrato()
            {
                ContratoId = 2,
                DataInicioVigencia = DateTime.Now.Date.AddDays(-10),
                DataFimVigencia = DateTime.Now.Date.AddDays(10),
                VolumeDisponivel = 100,
                Ativo = false
            }
        };

        private readonly Mock<IContratoRepositorio> mock = null;

        public ContratoRepositorioMoq()
        {
            mock = new Mock<IContratoRepositorio>();

            mock.Setup(m => m.ObterContrato(It.IsAny<int>())).Returns((int contratoId) =>
            {
                return _listaContrato.FirstOrDefault(C => C.ContratoId == contratoId);
            });

            mock.Setup(m => m.ObterListaContrato()).Returns(() =>
            {
                return _listaContrato;
            });

            mock.Setup(m => m.InserirContrato(It.IsAny<Contrato>())).Callback((Contrato contrato) =>
            {
                contrato.ContratoId = _listaContrato.Count + 1;
                _listaContrato.Add(contrato);
            });

            mock.Setup(m => m.AtualizarVolume(It.IsAny<int>(), It.IsAny<float>())).Callback((int contratoId, float novoVolume) =>
            {
                var contrato = _listaContrato.FirstOrDefault(c => c.ContratoId == contratoId);
                contrato.VolumeDisponivel = novoVolume;
            });
        }

        public void AtualizarVolume(int contratoId, float novoVolume)
        {
            mock.Object.AtualizarVolume(contratoId, novoVolume);
        }

        public void InserirContrato(Contrato contrato)
        {
            mock.Object.InserirContrato(contrato);
        }

        public Contrato ObterContrato(int contratoId)
        {
            return mock.Object.ObterContrato(contratoId);
        }

        public IList<Contrato> ObterListaContrato()
        {
            return mock.Object.ObterListaContrato();
        }
    }
}
