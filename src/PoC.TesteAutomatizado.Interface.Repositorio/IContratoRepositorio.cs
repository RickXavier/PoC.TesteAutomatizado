using PoC.TesteAutomatizado.Dominio.Entidade;
using System.Collections.Generic;

namespace PoC.TesteAutomatizado.Interface.Repositorio
{
    public interface IContratoRepositorio
    {
        IList<Contrato> ObterListaContrato();
        void InserirContrato(Contrato contrato);
        Contrato ObterContrato(int contratoId);        
        void AtualizarVolume(int contratoId, float novoVolume);
    }
}
