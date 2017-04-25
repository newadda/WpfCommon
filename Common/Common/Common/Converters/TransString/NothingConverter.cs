using Common.Common.Converters.TransString.Token;
using System.Collections.Generic;


namespace Common.Common.Converters.TransString
{
    /// <summary>
    /// 원본문자열을 포맷팅하지않는다. 기본임
    /// </summary>
    class NothingConverter : StringTransformer
    {
        protected override List<TokenBase> Convert(string originStr)
        {
            var list = new List<TokenBase>();
            list.Add(new StringToken(originStr));
            return list;
        }
    }
}
