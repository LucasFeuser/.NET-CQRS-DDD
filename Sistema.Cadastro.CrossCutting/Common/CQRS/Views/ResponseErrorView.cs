
namespace Sistema.Cadastro.CrossCutting.Common.CQRS.Views
{
    public class ResponseErroView : View
    {
        protected ResponseErroView()
        {   }

        public ResponseErroView(string codigo, string grupo, string mensagem)
        {
            Codigo = codigo;
            Grupo = grupo;
            Mensagem = mensagem;
        }

        public string Codigo { get; }
        public string Mensagem { get; }
        public string Grupo { get; }

    }
}
