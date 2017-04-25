using Common.Common.Converters.TransString.Token;
using System.Collections.Generic;
using System.Text;


namespace Common.Common.Converters.TransString
{

    /// <summary>
    /// 기본 문자열을 폰번호 포맷으로 바꾼다.
    /// </summary>
    public class PhoneNumberConverter : StringTransformer
    {
        protected override List<TokenBase> Convert(string originStr)
        {
            string format = "###-####-####";
            if (originStr.Length <= 7)
            {
                format = "###-####";
            }
            else if (originStr.Length < 10)
            {
                format = "##-###-####";
            }
            else if (originStr.Length < 11)
            {
                format = "###-###-####";
            }

            StringBuilder strB = new StringBuilder();
            List<TokenBase> list = new List<TokenBase>();

            int i = 0;
            foreach(var ch in format)
            {
                if(i>=originStr.Length)
                {
                    break;
                }

                if(ch.Equals('#'))
                {
                    strB.Append(originStr[i]);
                    i++;
                }else if(ch.Equals('-'))
                {
                    if(strB.Length > 0)
                    {
                        list.Add(new StringToken(strB.ToString()));
                        strB.Clear();
                        

                    }
                    list.Add(new SeparatorToken("-"));
                    
                }
                
            }
            if(strB.Length>0)
            {
                list.Add(new StringToken(strB.ToString()));
            }

            return list;
            
        }
    }
}
