using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schoeppli.Interface
{
    public interface ISubView<T> : IView
    {
        void ShowAll(List<T> items);
    }
}
