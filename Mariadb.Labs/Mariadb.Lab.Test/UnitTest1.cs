using System;
using System.Linq;
using Mariadb.Lab.DataAccessLayer;
using Mariadb.Lab.DataAccessLayer.DataTransferObjects;
using Mariadb.Lab.DataAccessLayer.Entities;
using NUnit.Framework;

namespace Tests
{
    public class Tests
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
            var users = UserRepository.Instance.GetUsers().Result;
            
            // Assert
            Assert.IsTrue(users.Any());
        }
        
        [Test]
        public void Test_GetUserById()
        {
            // Arrange
            var id = 1;
            
            // Act
            var user = UserRepository.Instance.GetUserById(1).Result;
            
            // Assert
            Assert.AreEqual("Blackie",user.Name);
        }
        
        [Test]
        public void Test_CreateUser()
        {
            // Arrange
            UserDto dto = new UserDto();
            dto.Name = $"Jen_{DateTime.Now.ToString("yyyyMMddhhmmss")}";
            
            var currentUserCount = UserRepository.Instance.GetUsers().Result.Count();
            var expected = currentUserCount + 1;
            
            // Act
            UserRepository.Instance.CreateUser(dto).Wait();
            
            // Assert
            currentUserCount = UserRepository.Instance.GetUsers().Result.Count();
            Assert.AreEqual(expected,currentUserCount);
        }
        
        [Test]
        public void Test_UpdateUser()
        {
            // Arrange
            Random randomInstance = new Random();
            UserDto dto;
            dto.Id = 2;
            dto.Name = $"Jen_{DateTime.Now.ToString("yyyyMMddhhmmss")}";
            dto.BalanceAmount =  Convert.ToDecimal(randomInstance.NextDouble() *111.111);
            var expectedUser = UserRepository.Instance.GetUserById(dto.Id).Result;

            // Act
            UserRepository.Instance.UpdateUser(dto).Wait();
            
            // Assert
            var currentUser = UserRepository.Instance.GetUserById(dto.Id).Result;
            
            Assert.AreEqual(expectedUser.Id,currentUser.Id);
            Assert.AreEqual(expectedUser.DateCreated, currentUser.DateCreated);
            Assert.AreNotEqual(expectedUser.Name,currentUser.Name);
            Assert.AreNotEqual(expectedUser.BalanceAmount,currentUser.BalanceAmount);
            Assert.AreNotEqual(expectedUser.DateUpdated,currentUser.DateUpdated);
        }
        
        [Test]
        public void Test_DeleteUser()
        {
            // Arrange
            var expectedUserCount = UserRepository.Instance.GetUsers().Result.Count();
            UserDto dto = new UserDto();
            dto.Name = $"Jen_{DateTime.Now.ToString("yyyyMMddhhmmss")}";
            var targetDeleteId = expectedUserCount + 1;
            UserRepository.Instance.CreateUser(dto).Wait();
            
            // Act
            
            UserRepository.Instance.DeleteUser(targetDeleteId).Wait();
            
            // Assert
            var currentUserCount = UserRepository.Instance.GetUsers().Result.Count();
            
            Assert.AreEqual(expectedUserCount,currentUserCount);
        }
         
        [Test]
        public void Test_GetUserCountBySPWithReturnValue()
        {
            // Arrange
            var totalAmount  = UserRepository.Instance.GetUsers().Result.Count();
            
            // Act
            var result = UserRepository.Instance.GetUserCountBySPWithReturnValue().Result;
            
            // Assert
            Assert.AreEqual(totalAmount,result);
        }
    }
}