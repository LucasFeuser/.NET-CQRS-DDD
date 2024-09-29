namespace Sistema.Cadastro.CrossCutting.Common.Validators
{
    public class DataValidator
    {
        private const int IdadeMaxima = 120;
        private const int IdadeMinima = 0;

        public static bool ValidarDataNascimento(DateTime data)
        {
            DateTime atual = DateTime.Now;

            if (data > atual)
            {
                return false;
            }

            if (data < atual.AddYears(-IdadeMaxima))
            {
                return false;
            }

            if (data > atual.AddYears(-IdadeMinima))
            {
                return false;
            }

            return true;
        }
    }
}
