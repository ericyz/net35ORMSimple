using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleORM.Util;

namespace SimpleORM.Tests {
    [TestClass]
    public class SimpleORMTest {
        #region ClearTestOutcome
        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void ClearTestOutcomeTest() {

        }

        #endregion
        #region SinglePrimaryKeyTest
        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void AutoGenerateSinglePrimaryKeyClassTest() {
        }
        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void InsertSinglePrimaryKeyRecordTest() {
            // Input
            // Expected Output
        }
        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void UpdateSinglePrimaryKeyRecordTest() {
            // Input
            // Expected Output
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void DeleteSinglePrimaryKeyRecordTest() {
        }
        #endregion
        #region CompositeKeyTest
        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void AutoGenerateCompositeKeyClassTest() {
            // Input
            // Expected Output
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void InsertCompositeKeyRecordTest() {
            // Input
            // Expected Output
        }
        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void UpdateCompositeKeyRecordTest() {
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void DeleteCompositeKeyRecordTest() {
        }
        #endregion

        [TestMethod]
        public void GenerateClassTest()
        {
            string connectionString = @"";
            string sql = "SELECT * FROM Employee ";
            SimpleORM.Util.ORMUtil.GenerateClass(connectionString,sql);
        }
    }
}
