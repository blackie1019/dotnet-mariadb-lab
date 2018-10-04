using System;
using System.Linq;
using Mariadb.Lab.DataAccessLayer;
using Mariadb.Lab.DataAccessLayer.DataTransferObjects;
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
            UserDto dto;
            dto.Name = $"Jen_{DateTime.Now.ToString("yyyyMMddhhmmss")}";
            var currentUserCount = UserRepository.Instance.GetUsers().Result.Count();
            var expected = currentUserCount + 1;
            
            // Act
            UserRepository.Instance.CreateUser(dto).Wait();
            
            // Assert
            currentUserCount = UserRepository.Instance.GetUsers().Result.Count();
            Assert.AreEqual(expected,currentUserCount);
        }
    }
}