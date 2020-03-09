using System;
using System.Collections.Generic;
using System.Linq;
using OnlineBanking;
using BusinessLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FundTransferTesting
{


    [TestClass]
    public class UnitTest2
    {
        enum currencytype
        {
            USD,
            MXN,
            CAD
        }
        #region properties
        Program user = new Program();
        Customer customerinfo = new Customer();
        List<CustomerInfo> customer = new List<CustomerInfo>();
        #endregion

        private double getamountbalance(int accountno, List<CustomerInfo> cust)
        {
            var customeracc = cust.Single(s => s.accountnumber == accountno);
            return customeracc.accountbalance;
        }

        private void transfer(List<CustomerInfo> cust, int acctno,int custid, int transactionacctno, string currentcytype, double amount)
        {
            user.withdrawal(customerinfo.filterdata(acctno, cust), custid, currentcytype, amount);
            user.deposit(customerinfo.filterdata(transactionacctno, cust), custid, currentcytype, amount);
        }

        [TestMethod]
        public void TestMethod1()
        {
            customer = user.customeraccountdetails();
            double userbalance = user.deposit(customerinfo.filterdata(1234, customer),777, Convert.ToString(currencytype.USD), 300);
            Assert.AreEqual(Convert.ToDouble(700), userbalance);
        }
        [TestMethod]
        public void case2()
        {
            customer = user.customeraccountdetails();

            double userbalance = user.withdrawal(customerinfo.filterdata(2001, customer),504, Convert.ToString(currencytype.MXN), 5000);
            //  customer.Where(w => w.accountnumber == 2001).ToList().ForEach(s => s.accountbalance = userbalance);
            userbalance = user.withdrawal(customerinfo.filterdata(2001, customer),504, Convert.ToString(currencytype.USD), 12500);
            //customer.Where(w => w.accountnumber == 2001).ToList().ForEach(s => s.accountbalance = userbalance);
            userbalance = user.deposit(customerinfo.filterdata(2001, customer), 504,Convert.ToString(currencytype.CAD), 300);
            //customer.Where(w => w.accountnumber == 2001).ToList().ForEach(s => s.accountbalance = userbalance);
            Assert.AreEqual(Convert.ToDouble(9800), getamountbalance(2001, customer));
        }

        [TestMethod]
        public void case3()
        {
            customer = user.customeraccountdetails();
            double userbalance = user.withdrawal(customerinfo.filterdata(5500, customer), 002,Convert.ToString(currencytype.CAD), 5000);
            transfer(customer, 1010,0, 5500, Convert.ToString(currencytype.CAD), 7300);
            //userbalance = user.withdrawal(customerinfo.filterdata(1010, customer), Convert.ToString(currencytype.CAD), 7300);
            //userbalance = user.deposit(customerinfo.filterdata(1010, customer), Convert.ToString(currencytype.CAD), 7300);
            userbalance = user.deposit(customerinfo.filterdata(1010, customer),002, Convert.ToString(currencytype.MXN), 13726);
            var customeracc = customer.Single(s => s.accountnumber == 1010);
            Assert.AreEqual(Convert.ToDouble(1497.6), customeracc.accountbalance);

            var _customeracc = customer.Single(s => s.accountnumber == 5500);
            Assert.AreEqual(Convert.ToDouble(17300), _customeracc.accountbalance);
        }

        [TestMethod]
        public void case4()
        {
            customer = user.customeraccountdetails();
            double userbalance = user.withdrawal(customerinfo.filterdata(0123, customer),123, Convert.ToString(currencytype.USD), 70);
            userbalance = user.deposit(customerinfo.filterdata(0456, customer), 456,Convert.ToString(currencytype.USD), 23789);
            transfer(customer, 0456,0, 0123, Convert.ToString(currencytype.CAD), 23.75);

            var customeracc = customer.Single(s => s.accountnumber == 0123);
            Assert.AreEqual(Convert.ToDouble(33.75), customeracc.accountbalance);

            var _customeracc = customer.Single(s => s.accountnumber == 0456);
            Assert.AreEqual(Convert.ToDouble(112554.25), _customeracc.accountbalance);
        }

        [TestMethod]
        public void case5()
        {
            customer = user.customeraccountdetails();
            double userbalance = user.withdrawal(customerinfo.filterdata(1010, customer), 219, Convert.ToString(currencytype.USD), 100);

            var customeracc = customer.Single(s => s.accountnumber == 1010);
            Assert.AreEqual(Convert.ToDouble(7425), customeracc.accountbalance);

        }
    }
}
