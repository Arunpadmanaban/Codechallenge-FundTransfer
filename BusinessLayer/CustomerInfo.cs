using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer
{
    public class CustomerInfo
    {
        public string customername
        { get; set; }

        public int customerId
        { get; set; }

        public int accountnumber
        { get; set; }

        public double accountbalance
        { get; set; }

        public double USDexchangerate
        {
            get { return 0.5; }
        }

        public string accounttype { get; set; }
        public double MXNexchangerate
        {
            get { return 10.0; }
        }
    }
}
