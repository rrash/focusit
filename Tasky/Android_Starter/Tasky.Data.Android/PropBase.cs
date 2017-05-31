using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;


namespace Tasky.Base.Android
{
    public interface IProperty
    {
        object Value { get; set; }
        bool IsMandatory { get; set; }
        string Name { get; set; }
        Type Type { get; }

    }
    
    public interface IProperty<T> : IProperty
        {
            new T Value { get; set; }

        }
    public class Property<T> : IProperty<T>
    {
        
        
        public T Value { get; set; }
        public Type Type { get { return typeof(T); } }
        public bool IsMandatory { get; set; } = false;
        public string Name { get; set; }
       
        object IProperty.Value
        {
            get { return Value; }
            set { Value = (T)value; }
        }


    }        

}

