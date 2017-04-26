using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFCommon.Common.Intents
{
    public interface IIntent
    {
        String Action { get; }
        String Category { get; }
        String MimeType { get; }
        Type Type { get; }


        void SetAction(String action);


        void SetCategory(String category);


        void SetMimeType(String mimeType);


        void SetType(Type type);
       

    }
}
