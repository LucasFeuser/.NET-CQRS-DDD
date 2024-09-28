using System.ComponentModel.DataAnnotations;

namespace Sistema.Cadastro.CrossCutting.Messages.Integrations
{
    public class ResponseMessage : Message
    {
        public ResponseMessage(ValidationResult validationResult)
        {
            ValidationResult = validationResult;
        }

        public ValidationResult ValidationResult { get; set; }
    }
}
