using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERM_TestTask.Model;

namespace ERM_TestTask
{
    interface IReader
    {
        void ReadFiles(string path, string filename, out List<LpModel> lpItems, out List<TouModel> touItems);
    }
}
