using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFCommon.Common.Intents
{
    public interface IIntentFilter 
    {
        List<String> Actions { get; }
        List<String> Categorys { get; }
        List<String> MimeTypes { get; }
        Type Type { get; }

        

        void AddAction(String action);


        void AddCategory(String category);


        void AddMimeType(String mimeType);


        void SetType(Type p);

        object CreateObject();

 
     



    }
}
