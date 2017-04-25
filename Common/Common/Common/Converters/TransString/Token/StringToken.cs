using System;

namespace Common.Common.Converters.TransString.Token
{
    /// <summary>
    /// 일반적인 원본 문자
    /// </summary>
    public class StringToken : TokenBase 
    {
        String str;
        public StringToken() : this("")
        {
            
        }

        public StringToken(String str)
        {
            this.str = str;
        }

        public void setString(string str)
        {
            this.str = str;
        }

        public override string ToString()
        {
            return str;
        }
    }
}
