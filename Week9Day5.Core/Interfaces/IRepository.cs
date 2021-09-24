using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week9Day5.Core.Interfaces
{
    public interface IRepository<T>
    {
        List<T> Fetch();
        void Update(T item);

        
    }
}
