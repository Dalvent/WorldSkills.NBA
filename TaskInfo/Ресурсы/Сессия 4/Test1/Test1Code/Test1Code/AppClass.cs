using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test1Code
{
    public class AppClass
    {
        /// <summary>
        /// Login
        /// </summary>
        /// <returns>true/false</returns>
        public static bool Login(string userName, string password)
        {
            if (userName != string.Empty && password != string.Empty)
            {
                if (userName == "admin" && password == "123456")
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// get the total of two int number
        /// </summary>
        /// <param name="a">the int number one</param>
        /// <param name="b">the int number two</param>
        /// <returns>return the total</returns>
        public static int Add(int a, int b)
        {
            return a + b;
        }

        /// <summary>
        /// get the minus result of two int number
        /// </summary>
        /// <param name="a">the int number one</param>
        /// <param name="b">the int number two</param>
        /// <returns>return the minus result</returns>
        public static int Minus(int a, int b)
        {
            return a - b;
        }

        /// <summary>
        /// get the dividing result of two int number
        /// </summary>
        /// <param name="a">the int number one</param>
        /// <param name="b">the int number two</param>
        /// <returns>return the dividing result of two int number</returns>
        public static int Divide(int a, int b)
        {
            return a / b;
        }

        /// <summary>
        /// get the multiple result of two int number
        /// </summary>
        /// <param name="a">the int number one</param>
        /// <param name="b">the int number two</param>
        /// <returns>return the multiple result of two int number</returns>
        public static double Multiply(double a, double b)
        {
            return a * b;
        }

    }
}
