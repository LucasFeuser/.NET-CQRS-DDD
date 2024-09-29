namespace Sistema.Cadastro.CrossCutting.Common.Validators
{
    public class TelefoneValidator
    {
        private static readonly HashSet<string> InvalidNumbers = new() { "000000000", "1111111111", "222222222", "333333333", "444444444", "555555555", "666666666", "777777777", "888888888", "999999999"};

        private static readonly HashSet<string> ValidDdds = new()
        {
            "11", "12", "13", "14", "15", "16", "17", "18", "19", // SP
            "21", "22", "24", // RJ
            "27", "28", // ES
            "31", "32", "33", "34", "35", "37", "38", // MG
            "71", "73", "74", "75", "77", // BA
            "82", // AL
            "85", "88", // CE
            "98", "99", // MA
            "83", // PB
            "81", "87", // PE
            "86", "89", // PI
            "84", // RN
            "79", // SE
            "61", // DF
            "62", "64", // GO
            "65", "66", // MT
            "67", // MS
            "68", // AC
            "92", "97", // AM
            "96", // AP
            "91", "93", "94", // PA
            "69", // RO
            "95", // RR
            "63", // TO
            "41", "42", "43", "44", "45", "46", // PR
            "51", "53", "54", "55", // RS
            "47", "48", "49" // SC
        };

        public static bool ValidarNumeroTelefone(string numero)
        {
            if (numero.Length < 11) return false;

            if (!ValidDdds.Contains(numero.Substring(0, 2))) return false;

            if(InvalidNumbers.Contains(numero.Substring(2, 9))) return false;

            return true;
        }

    }
}
