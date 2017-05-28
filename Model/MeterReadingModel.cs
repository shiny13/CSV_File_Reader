using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERM_TestTask.Model
{
    class MeterReadingModel
    {
        public string MeterPointCode { get; set; }
        public string SerialNumber { get; set; }
        public string PlantCode { get; set; }
        public string DataType { get; set; }
        public string Status { get; set; }
        public string Units { get; set; }

        public DateTime? DateTime { get; set; }
    }
}
