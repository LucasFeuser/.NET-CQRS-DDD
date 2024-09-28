namespace Sistema.Cadastro.CrossCutting.Common.Validators
{
    public class DataValidator
    {
        public static bool ValidarDataNascimento(DateTime data)
        {
            return data < DateTime.Now.AddYears(-120);
        }
    }
}
