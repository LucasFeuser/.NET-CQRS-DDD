namespace Sistema.Cadastro.CrossCutting.Common.CQRS.Views
{
    public class ResponseErroView : View
    {
        protected ResponseErroView()
        {   }

        public ResponseErroView(string codigo, string mensagem)
        {
            Codigo = codigo;
            Mensagem = mensagem;
        }

        public string Codigo { get; private set; }
        public string Mensagem { get; private set; }
    }
}
