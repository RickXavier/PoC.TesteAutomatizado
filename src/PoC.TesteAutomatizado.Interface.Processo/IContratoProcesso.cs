using PoC.TesteAutomatizado.Dominio.Dto;
using System.Collections.Generic;

namespace PoC.TesteAutomatizado.Interface.Processo
{
    public interface IContratoProcesso
    {
        void InserirContrato(ContratoDto contratoDto);
        IList<ContratoDto> ObterListaContrato();
    }
}
