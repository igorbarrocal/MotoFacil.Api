using MotoFacilAPI.Application.Dtos;
using MotoFacilAPI.Application.Services;
using MotoFacilAPI.Domain.Entities;
using MotoFacilAPI.Domain.Enums;
using MotoFacilAPI.Domain.Repositories;
using Moq;
using Xunit;

public class MotoServiceTests
{
    [Fact]
    public async Task CreateAsync_DeveRetornarMotoDtoComId()
    {
        // Arrange
        var mockRepo = new Mock<IMotoRepository>();
        mockRepo.Setup(r => r.AddAsync(It.IsAny<Moto>()))
            .Callback<Moto>(m => m.GetType().GetProperty("Id")!.SetValue(m, 123));
        var service = new MotoService(mockRepo.Object);

        var dto = new MotoDto { Placa = "ABC1234", Modelo = ModeloMoto.MottuSport, UsuarioId = 1 };

        // Act
        var result = await service.CreateAsync(dto);

        // Assert
        Assert.Equal("ABC1234", result.Placa);
        Assert.Equal(ModeloMoto.MottuSport, result.Modelo);
        Assert.Equal(1, result.UsuarioId);
        Assert.Equal(123, result.Id);
    }
}