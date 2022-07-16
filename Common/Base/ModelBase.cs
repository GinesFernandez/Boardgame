using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace Common.Base
{
    public abstract class ModelBase : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;
        private static readonly Dictionary<Func<string>, PropertyChangedEventArgs> FuncNames = new Dictionary<Func<string>, PropertyChangedEventArgs>();

        private static readonly object lockObject = new object();

        /// <summary>
        /// Raises the property changed.  RaisePropertyChanged(() => Reg(() => Property));
        /// </summary>
        /// <param name="key">The key.</param>
        protected void RaisePropertyChanged(Func<string> key)
        {
            if (PropertyChanged != null)
            {
                lock (lockObject)
                {
                    PropertyChangedEventArgs propertyName;
                    if (!FuncNames.TryGetValue(key, out propertyName))
                    {
                        propertyName = new PropertyChangedEventArgs(key());

                        FuncNames.Add(key, propertyName);
                    }
                    PropertyChanged(this, propertyName);
                }
            }
        }

        /// <summary>
        /// Raises the property changed.
        /// </summary>
        /// <param name="propertyName">The property name.</param>
        public void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                lock (lockObject)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
                }
            }
        }

        /// <summary>
        /// Regs the specified property.
        /// </summary>
        /// <param name="property">The property.</param>
        /// <returns></returns>
        protected static string Reg<T>(Expression<Func<T>> property)
        {
            return ((MemberExpression)property.Body).Member.Name;
        }

        /// <summary>
        /// Regs the specified property name.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns></returns>
        protected static string Reg(string propertyName)
        {
            return propertyName;
        }

        #endregion
    }
}