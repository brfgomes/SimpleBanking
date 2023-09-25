using System.Text.RegularExpressions;
using SimpleBanking.Domain.Exceptions;

namespace SimpleBanking.Domain
{
    public class Document
    {
        public Document(string code)
        {
            var noPunctuationCode = Regex.Replace(code, "[!\"#$%&'()*+,-./:;<=>?@\\[\\]^_`{|}~]", string.Empty);

            EmptyException.Throw(code, "CPF não informado");
            CompareValuesException.IsEqualsThan(Regex.IsMatch(noPunctuationCode, @"^\d+$"), false, "O CPF deve conter apenas números");
            CompareValuesException.IsOtherThan(noPunctuationCode.Length, 11, "Quantidade de caracteres do CPF invalida.");

            Code = code;
        }

        public string Code { get; private set; }
    }
}