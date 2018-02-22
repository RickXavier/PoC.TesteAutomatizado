using System;

namespace PoC.TesteAutomatizado.Dominio.Entidade
{
    public class Contrato
    {
        public int ContratoId { get; set; }
        public float VolumeDisponivel { get; set; }
        public DateTime DataInicioVigencia { get; set; }
        public DateTime DataFimVigencia { get; set; }
        public bool Ativo { get; set; }

        public override bool Equals(object obj)
        {
            if (obj.GetType() != GetType())
                return false;

            Contrato c = (Contrato)obj;

            return VolumeDisponivel == c.VolumeDisponivel &&
                DataInicioVigencia == c.DataInicioVigencia &&
                DataFimVigencia == c.DataFimVigencia &&
                Ativo == c.Ativo;
        }

        public override int GetHashCode()
        {
            int hash = 13;

            hash = (hash * 7) + VolumeDisponivel.GetHashCode();
            hash = (hash * 7) + DataInicioVigencia.GetHashCode();
            hash = (hash * 7) + DataFimVigencia.GetHashCode();
            hash = (hash * 7) + Ativo.GetHashCode();

            return hash;
        }
    }
}
