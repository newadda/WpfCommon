using Common.Common.Converters.TransString.Token;
using System.Collections.Generic;


namespace Common.Common.Converters.TransString
{

    /// <summary>
    /// 원본 문자열을 돈형식으로 변환
    /// </summary>
    class MoneyConverter : StringTransformer
    {
        protected override List<TokenBase> Convert(string originStr)
        {
            List<TokenBase> list = new List<TokenBase>();
            const int separatorCnt = 3;

            for(int i=0;i<originStr.Length;)
            {
                int addCount = 0;
                if(originStr.Length-i >= 3)
                {

                    int lastIndex = (originStr.Length - 1) - i;
                    int startIndex = lastIndex - (separatorCnt -1 );
                    string addStr = originStr.Substring(startIndex, lastIndex - startIndex + 1);
                    addCount = addStr.Length;

                    list.Insert(0,new StringToken(addStr));
  
                }
                else
                {
                    int lastIndex = (originStr.Length -1) - i;
                    int startIndex = lastIndex - ((originStr.Length - 1 - i));
                    string addStr = originStr.Substring(startIndex,lastIndex-startIndex +1 );
                    addCount = addStr.Length;
                 
                    list.Insert(0,new StringToken(addStr));
                    
                }
                i += addCount;
                if (i < originStr.Length)
                {
                    list.Insert(0, new SeparatorToken(","));
                }

            }
            
            return list;
        }
    }
}
