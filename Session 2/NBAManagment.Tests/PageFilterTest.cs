using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NBAManagement.Filter;
using System.Linq;

namespace NBAManagment.Tests
{
    [TestClass]
    public class PageFilterTest
    {  
        [TestMethod]
        public void Use_5PageSize1CurrentPageNumCollectionSequence1To40_Return1To5()
        {
            // Arrange
            var pageFilter = new PageFilter<int>(5, 1);
            var squence1to40 = Enumerable.Range(1, 40);

            // Act
            var res = pageFilter.Use(squence1to40);

            // Assert
            var expectedResult = Enumerable.Range(1, 5);
            CollectionAssert.AreEqual(res.ToArray(), expectedResult.ToArray(), 
                String.Join(", ", res.Select(item => item.ToString()).ToArray()));
        }

        [TestMethod]
        public void Use_2PageSize4CurrentPageNumCollectionSequence1To40_Return8to9()
        {
            // Arrange
            var pageFilter = new PageFilter<int>(2, 4);
            var squence1to40 = Enumerable.Range(1, 40);

            // Act
            var res = pageFilter.Use(squence1to40);

            // Assert
            var expectedResult = Enumerable.Range(7, 2);
            CollectionAssert.AreEqual(res.ToArray(), expectedResult.ToArray(),
                String.Join(", ", res.Select(item => item.ToString()).ToArray()));
        }
    }
}
