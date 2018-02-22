using PoC.TesteAutomatizado.Dominio.Dto;
using PoC.TesteAutomatizado.Dominio.Entidade;
using System.Collections.Generic;
using System.Linq;

namespace PoC.TesteAutomatizado.Processo.Mapper
{
    public static class ContratoMapper
    {
        public static ContratoDto CriarDto(this Contrato entidade)
        {
            return new ContratoDto()
            {
                ContratoId = entidade.ContratoId,
                VolumeDisponivel = entidade.VolumeDisponivel,
                DataInicioVigencia = entidade.DataInicioVigencia,
                DataFimVigencia = entidade.DataFimVigencia,
                Ativo = entidade.Ativo
            };
        }

        public static IList<ContratoDto> CriarDto(this IList<Contrato> entidade)
        {
            return entidade.ToList().ConvertAll(e => e.CriarDto());
        }

        public static Contrato CriarEntidade(this ContratoDto dto)
        {
            return new Contrato()
            {
                ContratoId = dto.ContratoId,
                VolumeDisponivel = dto.VolumeDisponivel,
                DataInicioVigencia = dto.DataInicioVigencia,
                DataFimVigencia = dto.DataFimVigencia,
                Ativo = dto.Ativo
            };
        }
    }
}
