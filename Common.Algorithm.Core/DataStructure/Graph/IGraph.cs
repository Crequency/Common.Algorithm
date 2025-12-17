using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Algorithm.Core.DataStructure.Graph;

internal interface IGraph
{
    bool IsTotallyConnected { get; set; }

    GraphTypes Type { get; set; }
}
