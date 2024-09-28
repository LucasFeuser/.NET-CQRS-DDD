namespace Sistema.Cadastro.CrossCutting
{
    public static class Utils
    {
        public static bool PossuiNumero(this string s)
        {
            if (string.IsNullOrWhiteSpace(s))
                throw new ArgumentException(nameof(s));

            foreach (var c in s)
                if (char.IsDigit(c))
                    return true;

            return false;
        }
    }
}
