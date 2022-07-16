using System.Windows;
using Common.Exceptions;

namespace Common.Base
{
    public abstract class ControlBaseView<T> : BaseView<T> where T : ViewModelBase
    {
        protected override T GetDataContext()
        {
            return ((FrameworkElement)Content).DataContext as T;
        }

        protected override void SetDataContext(T value)
        {
            if (value is IPage) //is a page
            {
                throw new BoardGameException("ControlBaseView can not be inherited by a Page, instead inherit PageBaseView");
            }

            var layoutroot = Content as FrameworkElement;
            if (layoutroot != null)
            {
                layoutroot.DataContext = value;
                return;
            }

            throw new BoardGameException("Can't not find Layoutroot grid in UserControl");
        }
    }
}