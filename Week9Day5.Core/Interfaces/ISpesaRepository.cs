using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Week9Day5.Core.Entities;

namespace Week9Day5.Core.Interfaces
{
    public interface ISpesaRepository: IRepository<Spesa>
    {
        public List<Spesa> FetchSpesaWithoutApproval();
    }
}
