using System;
using System.Threading;
using System.Windows;
using System.Windows.Markup;
using Microsoft.Practices.ServiceLocation;

namespace Common.Base
{
    public abstract class WindowBaseView<T> : Window where T : ViewModelBase
    {
        protected WindowBaseView()
        {
            Language = XmlLanguage.GetLanguage(Thread.CurrentThread.CurrentCulture.Name);

            var vm = ServiceLocator.Current.GetInstance<T>();
            ViewModel = vm;
        }

        public virtual T ViewModel
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

        protected T GetDataContext()
        {
            return ((FrameworkElement)Content).DataContext as T;
        }

        protected void SetDataContext(T value)
        {
            DataContext = value;
        }

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