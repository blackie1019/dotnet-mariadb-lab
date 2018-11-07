using System;
using System.Threading.Tasks;
using System.Transactions;
using Mariadb.Lab.DataAccessLayer;
using MySql.Data.MySqlClient;
using NUnit.Framework;

namespace Mariadb.Lab.Test
{
    public class TransactionRepositoryTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test_GetUsers()
        {
            // Arrange

            // Act
            var userName = UserSpRepository.Instance.GetNewUser().Result;
            
            // Assert
            Assert.IsFalse(string.IsNullOrEmpty(userName));
        }
        
        [Test]
        public async Task Test_AddNewUsers()
        {
            // Arrange
            var userName = $"CT_{DateTime.Now:yyyyMMddHHmmss}";
            string result;
            string currentUserName;

            // Act
            using (var conn =
                new MySqlConnection(
                    "Server=localhost;User ID=root;Password=pass.123;Database=Test;IgnoreCommandTransaction=true;"))
            {
                await conn.OpenAsync();
                using (var transaction = conn.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted))
                {
                    // Start a local transaction.
                    await UserSpRepository.Instance.AddNewUser(userName, conn,transaction);
                    result = await UserSpRepository.Instance.GetNewUser(conn,transaction);
                    
                    transaction.Rollback();
                }
            }

            currentUserName = UserSpRepository.Instance.GetNewUser().Result;

            // Assert
            Assert.AreEqual(userName,result);
            Assert.AreNotEqual(currentUserName,result);
        }
        
        [Test]
        public async Task Test_TransactionScope()
        {
            // Arrange
            var postfix = DateTime.Now.ToString("yyyyMMddHHmmss");
            var userName = $"CT_{postfix}";
            var productName = $"Product_{postfix}";
            string resultUserName;
            
            var transactionOption = new TransactionOptions();
            transactionOption.IsolationLevel = IsolationLevel.ReadUncommitted;

            // Act
            using (var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOption,
                TransactionScopeAsyncFlowOption.Enabled))
            {
                await UserSpRepository.Instance.AddNewUser(userName);
                await ProductSpRepository.Instance.AddNewProduct(productName);
                
                transactionScope.Dispose();
            }
            resultUserName = UserSpRepository.Instance.GetNewUser().Result;


            // Assert
            Assert.AreNotEqual(userName,resultUserName);
        }
    }
}