
namespace Common.Base
{
    public abstract class PageBaseView<T> : BaseView<T> where T : ViewModelBase
    {
        protected override T GetDataContext()
        {
            return DataContext as T;
        }

        protected override void SetDataContext(T value)
        {
            DataContext = value;
        }
    }
}