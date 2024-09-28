namespace Sistema.Cadastro.CrossCutting.Common.Validators
{
    public class CPFValidator
    {
        private static readonly HashSet<string> Invalidos = new HashSet<string> { "00000000000", "22222222222", "33333333333", "44444444444", "55555555555", "66666666666", "77777777777", "88888888888", "99999999999" };

        public static bool Validar(string numero)
        {
            numero = new string(numero.Where(char.IsDigit).ToArray());

            if (numero.Length != 11 || Invalidos.Contains(numero))
                return false;

            if (!VerificarDigito(numero, 9))
                return false;

            if (!VerificarDigito(numero, 10))
                return false;

            return true;
        }

        private static bool VerificarDigito(string cpf, int posicao)
        {
            int soma = 0;
            int multiplicador = posicao + 1;

            for (int i = 0; i < posicao; i++)
            {
                soma += (cpf[i] - '0') * multiplicador--;
            }

            int resto = soma % 11;
            int digitoVerificador = resto < 2 ? 0 : 11 - resto;

            return cpf[posicao] - '0' == digitoVerificador;
        }
    }
}
