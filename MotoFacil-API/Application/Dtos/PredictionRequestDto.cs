namespace MotoFacilAPI.Application.Dtos
{
    public class PredictionRequestDto
    {
        public int Quilometragem { get; set; }
        public int MesesDesdeUltimaRevisao { get; set; }
        public int NumeroServicosUltimoAno { get; set; }
    }
}