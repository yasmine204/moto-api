using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using MotoRentAPI.Dtos;

namespace MotoRentAPI.Common.Swagger
{
    public static class ExamplesDictionary
    {
        public static readonly Dictionary<Type, OpenApiObject> Examples = new()
        {
            {
                typeof(RentalCreateDto),
                new OpenApiObject
                {
                    ["entregador_id"] = new OpenApiString("entregador123"),
                    ["moto_id"] = new OpenApiString("moto123"),
                    ["data_inicio"] = new OpenApiString("2024-01-01T00:00:00Z"),
                    ["data_termino"] = new OpenApiString("2024-01-07T23:59:59Z"),
                    ["data_previsao_termino"] = new OpenApiString("2024-01-07T23:59:59Z"),
                    ["plano"] = new OpenApiInteger(7),
                }
            },
            {
                typeof(RentalDto),
                new OpenApiObject
                {
                    ["identificador"] = new OpenApiString("a05d6e96-eadc-4b09-9c11-2dad37fec27d"),
                    ["valor_diaria"] = new OpenApiInteger(10),
                    ["entregador_id"] = new OpenApiString("entregador123"),
                    ["moto_id"] = new OpenApiString("moto123"),
                    ["data_inicio"] = new OpenApiString("2024-01-01T00:00:00Z"),
                    ["data_termino"] = new OpenApiString("2024-01-07T23:59:59Z"),
                    ["data_previsao_termino"] = new OpenApiString("2024-01-07T23:59:59Z"),
                    ["data_devolucao"] = new OpenApiString("2024-01-07T00:00:00Z"),
                }
            },
            {
                typeof(UpdateReturnDateDto),
                new OpenApiObject { ["data_devolucao"] = new OpenApiString("2024-01-07T00:00:00Z") }
            },
            {
                typeof(DeliveryDriverCreateDto),
                new OpenApiObject
                {
                    ["identificador"] = new OpenApiString("entregador123"),
                    ["nome"] = new OpenApiString("João da Silva"),
                    ["CNPJ"] = new OpenApiString("12345678901234"),
                    ["data_nascimento"] = new OpenApiString("1990-01-01T00:00:00Z"),
                    ["numero_cnh"] = new OpenApiString("12345678900"),
                    ["tipo_cnh"] = new OpenApiString("A"),
                    ["imagem_cnh"] = new OpenApiString("base64string"),
                }
            },
            {
                typeof(DriverLicenseUploadImageDto),
                new OpenApiObject { ["imagem_cnh"] = new OpenApiString("base64string") }
            },
            {
                typeof(MotorcycleCreateDto),
                new OpenApiObject
                {
                    ["identificador"] = new OpenApiString("moto123"),
                    ["ano"] = new OpenApiInteger(2020),
                    ["modelo"] = new OpenApiString("Mottu Sport"),
                    ["placa"] = new OpenApiString("CDX-0101"),
                }
            },
            {
                typeof(MotorcycleDto),
                new OpenApiObject
                {
                    ["identificador"] = new OpenApiString("moto123"),
                    ["ano"] = new OpenApiInteger(2020),
                    ["modelo"] = new OpenApiString("Mottu Sport"),
                    ["placa"] = new OpenApiString("CDX-0101"),
                }
            },
            {
                typeof(MotorcycleUpdateDto),
                new OpenApiObject { ["placa"] = new OpenApiString("ABC-1234") }
            },
        };
    }
}
