using PoC.TesteAutomatizado.Dominio.Entidade;
using PoC.TesteAutomatizado.Interface.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PoC.TesteAutomatizado.Repositorio.Mock
{
    public class ContratoRepositorioMock : IContratoRepositorio
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
        
        public void AtualizarVolume(int contratoId, float novoVolume)
        {
            _listaContrato.FirstOrDefault(c => c.ContratoId == contratoId).VolumeDisponivel = novoVolume;
        }

        public void InserirContrato(Contrato contrato)
        {
            contrato.ContratoId = _listaContrato.Count + 1;
            _listaContrato.Add(contrato);
        }

        public Contrato ObterContrato(int contratoId)
        {
            return _listaContrato.FirstOrDefault(c => c.ContratoId == contratoId);
        }

        public IList<Contrato> ObterListaContrato()
        {
            return _listaContrato;
        }
    }
}
