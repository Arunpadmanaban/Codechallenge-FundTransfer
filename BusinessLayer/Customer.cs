using System;
using System.Collections.Generic;

namespace BusinessLayer
{
    public class Customer
    {
        #region enums
        enum currencytype
        {
            USD,
            MXN,
            CAD
        }
        enum acctype
        {
            chequing = 1,
            saving = 1,
            multiple = 2,
            joint = 3
        }
        enum transactiontype
        {
            deposit = 1,
            withdrawal = 2
        }
        #endregion
        CustomerInfo cust = new CustomerInfo();
        #region balanceinformation
        private double getbalance(string usercurreny, double depositamount)
        {
            double amount = 0.0;
            if (usercurreny == Convert.ToString(currencytype.USD))
            {
                amount += (depositamount / cust.USDexchangerate);
            }
            else if (usercurreny == Convert.ToString(currencytype.MXN))
            {
                amount += (depositamount / cust.MXNexchangerate);
            }
            else if (usercurreny == Convert.ToString(currencytype.CAD))
            {
                amount = depositamount;
            }
            return amount;
        }
        private double getaccountbalance(string usercurreny, double accountbalance, double depositamount, int type)
        {
            switch (type)
            {
                case 1:
                    accountbalance += getbalance(usercurreny, depositamount);
                    break;
                case 2:
                    accountbalance -= getbalance(usercurreny, depositamount);
                    break;
            }
            return accountbalance;
        }
        public double depositamount(string usercurreny, double accountbalance = 0.0, double depositamount = 0.0)
        {
            return getaccountbalance(usercurreny, accountbalance, depositamount, (int)transactiontype.deposit);
        }

        public double withdrawalamount(string usercurreny, double accountbalance = 0.0, double depositamount = 0.0)
        {
            return getaccountbalance(usercurreny, accountbalance, depositamount, (int)transactiontype.withdrawal);
        }

        public List<CustomerInfo> getuserdetails()
        {
            List<CustomerInfo> cust = new List<CustomerInfo>();
            cust.Add(new CustomerInfo { customername = "Stewie Griffin", accounttype = "chequing", customerId = 777, accountbalance = 100.00, accountnumber = 1234 });
            cust.Add(new CustomerInfo { customername = "Glenn Quagmire", accounttype = "chequing", customerId = 504, accountbalance = 35000.00, accountnumber = 2001 });
            cust.Add(new CustomerInfo { customername = "Joe Swanson", accounttype = "chequing", customerId = 002, accountbalance = 7425.00, accountnumber = 1010 });
            cust.Add(new CustomerInfo { customername = "Joe Swanson", accounttype = "chequing", customerId = 002, accountbalance = 15000.00, accountnumber = 5500 });
            cust.Add(new CustomerInfo { customername = "Peter Griffin", accounttype = "chequing", customerId = 123, accountbalance = 150.00, accountnumber = 0123 });
            cust.Add(new CustomerInfo { customername = "Lois Griffin", accounttype = "chequing", customerId = 456, accountbalance = 65000.00, accountnumber = 0456 });
            return cust;
        }

        public List<CustomerInfo> filterdata(int acctno, List<CustomerInfo> cust)
        {
            return cust.FindAll(e => e.accountnumber == acctno);
        }
        #endregion

    }

}
