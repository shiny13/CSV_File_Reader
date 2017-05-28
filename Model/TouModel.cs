using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERM_TestTask.Model
{
    class TouModel : MeterReadingModel
    {
        public string Period { get; set; }
        public string Rate { get; set; }

        public bool DlsActive { get; set; }

        public int BillingResetCount { get; set; }

        public decimal Energy { get; set; }
        public decimal MaximumDemand { get; set; }

        public DateTime? TimeOfMaxDemand { get; set; }
        public DateTime? BillingResetDateTime { get; set; }

        public TouModel()
        {
            Period = "";
            Rate = "";

            DlsActive = false;
            BillingResetCount = 0;
            Energy = 0M;
            MaximumDemand = 0M;

            TimeOfMaxDemand = null;
            BillingResetDateTime = null;
            DateTime = null;
        }
    }
}
