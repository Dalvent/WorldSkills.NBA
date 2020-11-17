using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Test1Code;

namespace Test1Project
{
    /*
     * You should complete the follow tests.
     * 1.Test the Login method and write the testing report.
     * 2.Test the Add method and write the testing report.
     * 3.Test the Minus method and write the testing report.
     * 4.Test the Divide method and write the testing report.
     * 5.Test the Multiply method and write the testing report.
     */

    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void LoginMethod()
        {
            bool a = AppClass.Login("test1", "test2");
           
        }
    }
}
