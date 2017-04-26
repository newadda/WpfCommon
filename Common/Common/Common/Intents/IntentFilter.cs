using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace WPFCommon.Common.Intents
{
    public class IntentFilter : IIntentFilter
    {
        private List<String> _Actions;
        private List<String> _Categorys;
        private List<String> _MimeTypes;
        private Type _Type;

        public List<string> Actions
        {
            get
            {
                return _Actions;
            }
        }

        public List<string> Categorys
        {
            get
            {
                return _Categorys;
            }
        }

        public List<string> MimeTypes
        {
            get
            {
                return _MimeTypes;
            }
        }

        public Type Type
        {
            get
            {
                return _Type;
            }
        }

        public IntentFilter()
        {
            _Actions = new List<string>();
            _Categorys = new List<String>();
            _MimeTypes = new List<string>();
            _Type = null;


        }

        public void AddAction(String action)
        {
            _Actions.Add(action);
        }

        public void AddCategory(String category)
        {
            _Categorys.Add(category);
        }

        public void AddMimeType(String mimeType)
        {
            _MimeTypes.Add(mimeType);
        }

        public void SetType(Type p)
        {
            _Type = p;
        }

        public object CreateObject()
        {
            Object instance = (Object)Activator.CreateInstance(this.Type);
            return instance;
        }







        public override bool Equals(object obj)
        {
            IIntent intent = obj as IIntent;
            bool isEquals = false;

            if (intent != null)
            {
                
                return EqualsIntent(intent);
            }

            return base.Equals(obj);

        }

        private bool EqualsIntent(IIntent target)
        {
            bool isEquals = false;
            String action = target.Action;
            if (action != null)
            {
                if (this.Actions.Count == 0)
                {
                    return false;
                }

                isEquals = this.Actions.Contains(action);
                if (isEquals == false)
                {
                    return isEquals;
                }
            }

            String category = target.Category;
            if (category != null)
            {
                if (this.Categorys.Count == 0)
                {
                    return false;
                }

                isEquals = this.Categorys.Contains(category);
                if (isEquals == false)
                {
                    return isEquals;
                }
            }

            String mimeType = target.MimeType;
            if (mimeType != null)
            {
                if (this.MimeTypes.Count == 0)
                {
                    return false;
                }


                isEquals = this.MimeTypes.Contains(mimeType);
                if (isEquals == false)
                {
                    return isEquals;
                }
            }

            Type type = target.Type;
            if (type != null)
            {
                if (this.Type == null)
                {
                    return false;
                }

                isEquals = (type == this.Type);
                if (isEquals == false)
                {
                    return isEquals;
                }
            }

            return true;
        }


        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        


    }
}
