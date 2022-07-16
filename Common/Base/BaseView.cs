using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using Microsoft.Practices.ServiceLocation;

namespace Common.Base
{
    public abstract class BaseView<T> : UserControl where T : ViewModelBase
    {
        protected BaseView()
        {
            Language = XmlLanguage.GetLanguage(Thread.CurrentThread.CurrentCulture.Name);

            var vm = ServiceLocator.Current.GetInstance<T>();
            ViewModel = vm;
        }

        public T ViewModel
        {
            get
            {
                return GetDataContext();
            }
            set
            {
                InitComponents();
                SetDataContext(value);
                PostViewModelOperations();
            }
        }

        protected abstract T GetDataContext();

        protected abstract void SetDataContext(T value);

        protected virtual void PostViewModelOperations()
        {
            Init();
        }

        protected virtual void InitComponents()
        {
            var initmethod = GetType().GetMethod("InitializeComponent");
            initmethod.Invoke(this, new object[] { });
        }

        public void Init()
        {
        }

        protected static void SyncProperty<TProp>(Func<TProp> assignFunc, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue.GetType() == typeof(T))
            {
                assignFunc();
            }
        }
    }
}