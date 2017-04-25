using System;

namespace Common.Common.Converters.TransString.Token
{
    /// <summary>
    /// 문자와 문자를 구분짓는 실제로는 없는 문자
    /// </summary>
    public class SeparatorToken : TokenBase
    {
        String separator;
        public SeparatorToken() : this("")
        {
            
        }

        public SeparatorToken(String str)
        {
            separator = str;
        }


        public override string ToString()
        {
            return separator;
        }
    }
}
