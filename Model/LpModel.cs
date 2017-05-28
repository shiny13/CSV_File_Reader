using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERM_TestTask.Model
{
    class LpModel : MeterReadingModel
    {
        public decimal DataValue { get; set; }

        public LpModel()
        {
            DataValue = 0M;

            DateTime = null;
        }
    }
}
