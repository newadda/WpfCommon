using System;

namespace WPFCommon.Common.Converters.TransString
{
    /// <summary>
    /// 원본 문자열에 대한 정보
    /// </summary>
    public class OriginalString
    {
        string str;
        int cursor;
        public OriginalString()
        {
            this.str = "";
            this.cursor = 0;
        }

        public void SetCursor(int cursor)
        {
            this.cursor = cursor;
        }

        public int GetCursor()
        {
            return this.cursor;
        }

        public void SetString(String str)
        {
            this.str = str;
        }

        public String GetString()
        {
            return this.str;
        }

        public override string ToString()
        {
            return str;
        }

    }
}
