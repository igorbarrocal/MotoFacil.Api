using Microsoft.ML;
using MotoFacilAPI.Application.Dtos;

namespace MotoFacilAPI.Application.Services
{
    public class PredictionService
    {
        private readonly MLContext _mlContext = new();

        // Simula um modelo de classificação binária
        public PredictionResponseDto Predict(PredictionRequestDto input)
        {
            // Lógica dummy: se a quilometragem for maior que 10000 ou meses desde revisão > 12, precisa manutenção
            bool precisa = input.Quilometragem > 10000 || input.MesesDesdeUltimaRevisao > 12 || input.NumeroServicosUltimoAno > 4;
            float score = precisa ? 0.92f : 0.13f;

            return new PredictionResponseDto
            {
                PrecisaManutencao = precisa,
                Score = score
            };
        }
    }
}