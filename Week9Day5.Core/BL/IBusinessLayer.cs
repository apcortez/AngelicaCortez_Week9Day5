using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Week9Day5.Core.Entities;

namespace Week9Day5.Core.BL
{
    public interface IBusinessLayer
    {
        void EseguiApprovazione();
        Dipendente GetDipendenteByNome(string nome);

        void Accedi(Dipendente d);
    }
}
