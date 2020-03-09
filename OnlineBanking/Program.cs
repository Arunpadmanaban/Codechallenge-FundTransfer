using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBanking
{
    public class Program
    {

        #region enum
        enum transcationtype
        {
            deposit = 1,
            withdrawal = 2,
            transfer = 3
        }
        enum currencytype
        {
            USD = 2,
            MXN = 3,
            CAD = 1
        }
        enum acctype
        {
            own = 1,
            otheraccount = 2
        }

        #endregion

        #region properties
        Customer accounttype = new Customer();
        //CustomerInfo model = new CustomerInfo();
        List<CustomerInfo> cust = new List<CustomerInfo>();
        string accountinformation = string.Empty;
        int transferredaccount = 0;
        #endregion
        static void Main(string[] args)
        {
            Program userinfo = new Program();
            userinfo.getcustomerinfo();
        }

        private void loadcustomerdata()
        {
            getuserdetails();
        }

        private void getuserinfo()
        {
            int transferacctno = 0;
            Program userinfo = new Program();
            Console.WriteLine("Enter Account No: ");
            int acctno = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter Customer Id: ");
            int custid = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter Transaction Type 1)Deposit 2)Withdrawal 3)Transfer: ");
            int transacationtype = Convert.ToInt32(Console.ReadLine());
            if (transacationtype == 3)
            {
                Console.WriteLine("Enter Account Number to Transfer");
                transferacctno = Convert.ToInt32(Console.ReadLine());
                transferredaccount = transferacctno;
            }
            Console.WriteLine("Enter Currency Type(Default-CAD) 1)CAD 2)USD 3)MXN: ");
            int currencytype = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter Amount");
            double amount = Convert.ToDouble(Console.ReadLine());
            usertransaction(acctno, custid, transacationtype, currencytype, amount, transferacctno);
            continuetranscation(acctno, transferredaccount);

        }

        private string getamountbalance(int accountno)
        {
            var customeracc = cust.Single(s => s.accountnumber == accountno);
            return Convert.ToString(customeracc.accountbalance);
        }

        private void continuetranscation(int acctno, int transferaccno)
        {
            StringBuilder sb = new StringBuilder();
            Console.WriteLine("Press 1 to continue the transactions");
            if (Convert.ToInt32(Console.ReadLine()) == 1)
            {
                getuserinfo();
            }
            else
            {
                sb.Append(string.Format("Account Number:{0} Balance:${1} CAD  ", acctno, getamountbalance(acctno)));
                if (transferaccno != 0)
                {
                    sb.Append(string.Format("Account Number:{0} Balance:${1} CAD  ", transferaccno, getamountbalance(transferaccno)));
                    transferredaccount = 0;
                }
                Console.WriteLine(sb.ToString());
                Console.ReadLine();
            }
        }
        private void getcustomerinfo()
        {
            loadcustomerdata();
            getuserinfo();
            Console.ReadLine();

        }

        private void usertransaction(int acctno, int custid, int transacationtype, int currencytype, double amount, int transferaccount = 0)
        {
            if (Convert.ToString((transcationtype)transacationtype) == Convert.ToString(transcationtype.deposit))
            {
                deposit(accounttype.filterdata(acctno, cust), custid, Convert.ToString((currencytype)currencytype), amount);
            }
            else if (Convert.ToString((transcationtype)transacationtype) == Convert.ToString(transcationtype.withdrawal))
            {
                withdrawal(accounttype.filterdata(acctno, cust), custid, Convert.ToString((currencytype)currencytype), amount);
            }
            else if (Convert.ToString((transcationtype)transacationtype) == Convert.ToString(transcationtype.transfer))
            {
                withdrawal(accounttype.filterdata(acctno, cust), custid, Convert.ToString((currencytype)currencytype), amount);
                deposit(accounttype.filterdata(transferaccount, cust), custid, Convert.ToString((currencytype)currencytype), amount);
            }
        }


        private void getuserdetails()
        {
            cust = customeraccountdetails();// accounttype.getuserdetails();
        }

        public List<CustomerInfo> customeraccountdetails()
        {
            return accounttype.getuserdetails();
        }

        public void updatecustomerinfo(int acctno, double acctbalance)
        {
            cust.Where(w => w.accountnumber == acctno).ToList().ForEach(s => s.accountbalance = acctbalance);
        }

        public double deposit(List<CustomerInfo> _customer, int custid, string _currencytype, double amount)
        {
            double _amount = 0.0;
            foreach (var customer in _customer)
            {
                if (customer.customerId == custid || custid == 0)
                {
                    customer.accountbalance = accounttype.depositamount(_currencytype, customer.accountbalance, amount);
                    updatecustomerinfo(customer.accountnumber, customer.accountbalance);
                }
                _amount = customer.accountbalance;
            }
            return _amount;

        }
        public double withdrawal(List<CustomerInfo> _customer, int custid, string _currencytype, double amount)
        {
            double _amount = 0.0;
            foreach (var customer in _customer)
            {
                if (customer.customerId == custid || custid == 0)
                {
                    customer.accountbalance = accounttype.withdrawalamount(_currencytype, customer.accountbalance, amount);
                    updatecustomerinfo(customer.accountnumber, customer.accountbalance);
                }
                _amount = customer.accountbalance;
            }
            return _amount;
        }

    }
}
