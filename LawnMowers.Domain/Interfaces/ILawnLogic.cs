using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawnMowers.Domain.Interfaces
{
    public interface ILawnLogic
    {
        void BuildLawnAndMowers(string text);

        string MoveLawnMowers();
    }
}
