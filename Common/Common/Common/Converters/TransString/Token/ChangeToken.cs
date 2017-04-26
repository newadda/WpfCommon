using System;

namespace WPFCommon.Common.Converters.TransString.Token
{
    /// <summary>
    /// 원본문자의 변경문자
    /// </summary>
    public class ChangeToken : TokenBase
    {
        String originStr;
        String changeStr;

        public ChangeToken()
        {
            changeStr = "";
            originStr = "";
        }
        public ChangeToken(string origin,string change)
        {
            this.originStr = origin;
            this.changeStr = change;
        }

        public void SetChangeString(string str)
        {
            this.changeStr = str;
        }

        public void SetOriginString(string str)
        {
            this.originStr = str;
        }

        public string GetOriginString()
        {
            return originStr;
        }

        public override string ToString()
        {
            return changeStr;
        }

        
    }
}
