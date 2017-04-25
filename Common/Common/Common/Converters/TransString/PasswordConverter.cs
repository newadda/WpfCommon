using Common.Common.Converters.TransString.Token;
using System.Collections.Generic;



namespace Common.Common.Converters.TransString
{
    /// <summary>
    /// 문자열을 패스워드(마지막은 보이게) 바꾼다.
    /// </summary>
    class PasswordConverter : StringTransformer
    {
        protected override List<TokenBase> Convert(string originStr)
        {
            List<TokenBase> list = new List<TokenBase>();
            for(int i = 0; i < originStr.Length; i++)
            {
                if ((i + 1) == originStr.Length)
                {
                    list.Add(new StringToken(originStr[i].ToString()));
                }
                else
                {
                    list.Add(new ChangeToken(originStr[i].ToString(), "*"));
                }

            }

            return list;
        }
    }
}
