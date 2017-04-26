using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace WPFCommon.Common.Intents
{
    /// <summary>
    /// 안드로이드 Intent 비슷하게 만들었다. 상호 통신용이다.
    /// </summary>
    public class Intent : IIntent
    {
        String _Action;
        String _Category;
        String _MimeType;
        Type _Type;

        public string Action
        {
            get
            {
                return _Action;
            }
        }

        public string Category
        {
            get
            {
                return _Category;
            }
        }

        public string MimeType
        {
            get
            {
                return _MimeType;
            }
        }

        public Type Type
        {
            get
            {
                return _Type;
            }
        }

        public void SetAction(String action)
        {
            this._Action = action;
        }

        public void SetCategory(String category)
        {
            this._Category = category;
        }

        public void SetMimeType(String mimeType)
        {
            this._MimeType = mimeType;
        }

        public void SetType(Type type)
        {
            this._Type = type;
        }

        

    }
}
