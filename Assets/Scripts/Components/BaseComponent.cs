using Isekai.Systems;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Isekai.Components
{
    public class PropertyValueChangedEventArgs : EventArgs
    {
        public string PropertyName;
        public object Value;

        public PropertyValueChangedEventArgs(string propertyName, object value)
        {
            PropertyName = propertyName;
            Value = value;
        }
    }

    public delegate void PropertyValueChangedEventHandler(object sender, PropertyValueChangedEventArgs e);
    public abstract class BaseComponent:IComponent
    {
        public event PropertyValueChangedEventHandler PropertyValueChanged;

        public abstract void Initialize(BaseComponentSystem system);

        protected bool ChangePropertyAndNotify<T>(ref T currentValue, T newValue, [CallerMemberName] string propertyName = null)
        {
            if (newValue == null && currentValue == null)
            {
                return false;
            }

            if (newValue != null && newValue.Equals(currentValue))
            {
                return false;
            }

            currentValue = newValue;

            RaisePropertyChanged(propertyName, newValue);

            return true;
        }
        protected bool ChangePropertyAndNotify<T>(T currentValue, T newValue, [CallerMemberName] string propertyName = null)
        {
            if (newValue == null && currentValue == null)
            {
                return false;
            }

            if (newValue != null && newValue.Equals(currentValue))
            {
                return false;
            }

            currentValue = newValue;

            RaisePropertyChanged(propertyName, newValue);

            return true;
        }

        protected virtual void RaisePropertyChanged(string propertyName, object value = null)
        {

            PropertyValueChanged?.Invoke(this, new PropertyValueChangedEventArgs(propertyName, value));
        }
    }
}

