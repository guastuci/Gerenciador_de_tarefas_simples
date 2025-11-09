using System;
using System.ComponentModel.DataAnnotations;

namespace GerenciadorDeTarefas.Validation
{
    /// <summary>
    /// Garante que a data seja estritamente futura (UTC independente).
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public sealed class DataFuturaAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext _)
        {
            if (value is null)
                return ValidationResult.Success;

            if (value is DateTime dataInformada)
            {
                var agora = DateTime.UtcNow;
                var data = dataInformada.Kind == DateTimeKind.Unspecified
                    ? DateTime.SpecifyKind(dataInformada, DateTimeKind.Utc)
                    : dataInformada.ToUniversalTime();

                if (data > agora)
                    return ValidationResult.Success;

                return new ValidationResult(ErrorMessage ?? "A data deve ser no futuro.");
            }

            return new ValidationResult("Formato de data inválido.");
        }
    }
}
