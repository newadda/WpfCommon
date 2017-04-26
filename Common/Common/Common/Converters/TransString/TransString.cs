using WPFCommon.Common.Converters.TransString.Token;
using System.Collections.Generic;
using System.Text;

namespace WPFCommon.Common.Converters.TransString
{
    /// <summary>
    /// 변경된 문자열에 대한 클래스
    /// </summary>
    public class TransString
    {
        List<TokenBase> tokenList;
        int cursor;

        public TransString()
        {
            this.tokenList = new List<TokenBase>();
            cursor = 0;
        }

        public void SetTokenList(List<TokenBase> tokenList)
        {
            this.tokenList = tokenList;
        }

        public List<TokenBase> GetTokenList()
        {
            return this.tokenList;
        }

        public void SetCursor(int cursor)
        {
            this.cursor = cursor;
        }

        public int GetCursor()
        {
            return this.cursor;
        }

        /// <summary>
        /// 원본 문자열을 반환
        /// </summary>
        /// <returns></returns>
        public string ToOriginalString()
        {
            if (tokenList == null || tokenList.Count == 0)
            {
                return "";
            }

            StringBuilder strBuilder = new StringBuilder();

            foreach (var str in tokenList)
            {
                if(str is StringToken)
                {
                    strBuilder.Append(str.ToString());
                }else if(str is ChangeToken)
                {
                    var temp = str as ChangeToken;
                    strBuilder.Append(temp.GetOriginString());
                }
                


            }

            return strBuilder.ToString();
        }


        /// <summary>
        /// 변경된 문자열을 반환
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if(tokenList==null || tokenList.Count == 0)
            {
                return "";
            }

            StringBuilder strBuilder = new StringBuilder();
            
            foreach(var str in tokenList)
            {
                strBuilder.Append(str.ToString());


            }

            return strBuilder.ToString();

        }

    }
}
