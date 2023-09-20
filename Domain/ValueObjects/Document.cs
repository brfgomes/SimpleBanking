using System.Text.RegularExpressions;

namespace SimpleBanking.Domain
{
    public class Document
    {
        public Document(string code)
        {
            Code = code;

            var noPunctuationCode = Regex.Replace(code, "[!\"#$%&'()*+,-./:;<=>?@\\[\\]^_`{|}~]", string.Empty);
            
            if(code == ""){
                throw new Exception("CPF não pode ser em branco");
                
            }

            if(Regex.IsMatch(noPunctuationCode, @"^\d+$") == false){
                throw new Exception("O CPF deve conter apenas números");
            }

            if(noPunctuationCode.Length != 11){
                throw new Exception("CPF Invalido");
            }
        }

        public string Code { get; private set; }
    }
}