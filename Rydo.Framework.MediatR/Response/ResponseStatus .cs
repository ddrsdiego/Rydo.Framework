using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rydo.Framework.MediatR.Response
{
    /// <summary>
    /// Status do Response para a API
    /// Por default quando esse objeto for instanciado será adicionado o código de sucesso aonde será igual 1
    /// Toda tratamento de resposta deve ser e sempre ser diferente de 1.
    /// </summary>
    public class ResponseStatus
    {
        //"Requisição processada com sucesso."
        public const int SUCCESS_STATUS = 0;
        public const int GENERIC_ERROR = 99;
        private const string SUCCESS_MESSAGE = "Requisição processada com sucesso.";
        private readonly IDictionary<int, string> mDictionaryRetutn = new ConcurrentDictionary<int, string>();

        public ResponseStatus()
        {
            mDictionaryRetutn.Add(SUCCESS_STATUS, SUCCESS_MESSAGE);
        }

        /// <summary>
        /// Verifica se a erros registrados, caso uma mensagem de OK tenha sido registrada, todos os erros são ignorados.
        /// </summary>
        public bool Sucesso => !mDictionaryRetutn.Any() || mDictionaryRetutn.ContainsKey(SUCCESS_STATUS);

        /// <summary>
        /// Adiciona erros ao dicionário de Erros e Mensagens
        /// </summary>
        /// <param name="returnCode"></param>
        public void AddReturnCode(ReturnCode returnCode)
        {
            if (returnCode.Code != SUCCESS_STATUS && mDictionaryRetutn.ContainsKey(SUCCESS_STATUS))
                mDictionaryRetutn.Remove(SUCCESS_STATUS);

            if (!mDictionaryRetutn.ContainsKey(returnCode.Code))
                mDictionaryRetutn.Add(returnCode.Code, returnCode.Message);
        }

        public IEnumerable<string> Messages
        {
            get
            {
                var listErros = new List<string>();

                if (mDictionaryRetutn != null && mDictionaryRetutn.Any())
                    listErros.AddRange(mDictionaryRetutn.Select(x => x.Value.ToString()));

                return listErros;
            }
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            if (mDictionaryRetutn != null && mDictionaryRetutn.Count > 0)
            {
                mDictionaryRetutn.ToList().ForEach(x =>
                {
                    sb.AppendLine(string.Format("{0} - {1}", x.Key, x.Value));
                    sb.AppendLine();
                });
            }
            return base.ToString();
        }
    }
}
