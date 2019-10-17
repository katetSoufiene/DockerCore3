using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using WebApiCore3.Controllers;
using WebApplicationCore.Entities;
using WebApplicationCore.Interfaces;

namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        List<Policy> Policies = new List<Policy>
          {
                new Policy
                {
                    Id = 739562,
                    PolicyHolder = new PolicyHolder
                        {
                            Name = "Bob Bo",
                            Age = 44,
                            Gender = Gender.Male
                        }
                }
               ,
                new Policy
                {
                    Id = 462946,
                    PolicyHolder =  new PolicyHolder {
                                Name = "Tom To",
                                Age = 38,
                                Gender = Gender.Male
                            }
                },
                new Policy
                {
                    Id = 355679,
                    PolicyHolder = new PolicyHolder
                        {
                            Name = "Tina Ti",
                            Age = 23,
                            Gender = Gender.Female
                        }
                },
          };

        [TestMethod]
        public void Get_Return_OkResult()
        {
            //Arrange  

            var mock = new Mock<IPolicyService>();

            mock.Setup(p => p.Get()).ReturnsAsync(Policies);

            var controller = new PoliciesController(mock.Object);

            //Act  
            var data = controller.Get().Result as OkObjectResult;

            //Assert  
            Assert.IsNotNull(data);
            Assert.AreEqual(200, data.StatusCode);
        }

        [TestMethod]
        public void Get_ById_Return_NotFoundResult()
        {
            //Arrage 
            var mockRepo = new Mock<IPolicyService>();
            mockRepo.Setup(repo => repo.Get(1))
               .ReturnsAsync((Policy)null);
            var controller = new PoliciesController(mockRepo.Object);

            //act
            var data = controller.Get(1).Result as NotFoundResult;

            //Assert  
            Assert.IsNotNull(data);
            Assert.AreEqual(404, data.StatusCode);
        }

        [TestMethod]
        public void Get_ById_Return_OkResult()
        {
            //Arrange  
            var mock = new Mock<IPolicyService>();

            mock.Setup(p => p.Get(739562)).ReturnsAsync(new Policy
            {
                Id = 739562,
                PolicyHolder = new PolicyHolder
                {
                    Name = "Dwayne Johnson",
                    Age = 44,
                    Gender = Gender.Male
                }
            });

            var controller = new PoliciesController(mock.Object);

            //Act  
            var data = controller.Get(739562).Result as OkObjectResult;

            //Assert  
            Assert.IsNotNull(data);
            Assert.AreEqual(200, data.StatusCode);
        }

        [TestMethod]
        public void Add_GivenInvalidModel_Returns_BadRequest()
        {
            // Arrange 
            var mockRepo = new Mock<IPolicyService>();
            var controller = new PoliciesController(mockRepo.Object);
            controller.ModelState.AddModelError("error", "some error");

            // Act
            var data = controller.Add(null).Result as BadRequestResult;

            // Assert
            Assert.IsNotNull(data);
            Assert.AreEqual(400, data.StatusCode);
        }

        [TestMethod]
        public void Add_Return_OKResult()
        {
            //Arrange  
            // var PolicyRepository = new PolicyRepository();
            var mock = new Mock<IPolicyService>();
            var controller = new PoliciesController(mock.Object);

            PolicyHolder _policyHolder1 = new PolicyHolder
            {
                Name = "Dwayne Johnson",
                Age = 44,
                Gender = Gender.Male
            };
            var policy = new Policy
            {
                Id = 739562,
                PolicyHolder = _policyHolder1
            };

            //Act  
            var data = controller.Add(policy).Result as OkResult;
            //Assert  
            Assert.IsNotNull(data);
            Assert.AreEqual(200, data.StatusCode);
        }

        [TestMethod]
        public void Update_GivenInvalidModel_Returns_BadRequest()
        {
            // Arrange 
            var mockRepo = new Mock<IPolicyService>();
            mockRepo.Setup(m => m.Get()).ReturnsAsync(new List<Policy>() { new Policy() { Id = 1 } });
            var controller = new PoliciesController(mockRepo.Object);
            controller.ModelState.AddModelError("error", "some error");

            // Act
            var data = controller.Update(1, null).Result as BadRequestObjectResult;

            // Assert
            Assert.IsNotNull(data);
            Assert.AreEqual(400, data.StatusCode);
        }

        [TestMethod]
        public void Update_Return_OKResult()
        {
            //Arrange  
            //var policyRepository = new PolicyRepository();
            var mock = new Mock<IPolicyService>();
            mock.Setup(m => m.Get()).ReturnsAsync(new List<Policy>() { new Policy() { Id = 739562 } });

            var controller = new PoliciesController(mock.Object);

            PolicyHolder _policyHolder1 = new PolicyHolder
            {
                Name = "Dwayne Johnson",
                Age = 45,
                Gender = Gender.Male
            };
            var policy = new Policy
            {
                Id = 739562,
                PolicyHolder = _policyHolder1
            };

            //Act  
            var data = controller.Update(739562, policy).Result as OkResult;
            //Assert  
            Assert.IsNotNull(data);
            Assert.AreEqual(200, data.StatusCode);
        }


        [TestMethod]
        public void Delete_Return_NotFound()
        {
            //Arrange  
            // var PolicyRepository = new PolicyRepository();
            var mock = new Mock<IPolicyService>();
            mock.Setup(m => m.Get()).ReturnsAsync(new List<Policy>() { new Policy() { Id = 1 } });

            var controller = new PoliciesController(mock.Object);

            //Act  
            var data = controller.Remove(739562).Result as NotFoundObjectResult;
            //Assert  
            Assert.IsNotNull(data);
            Assert.AreEqual(404, data.StatusCode);
        }

        [TestMethod]
        public void Delete_Return_OKResult()
        {
            //Arrange  
            // var PolicyRepository = new PolicyRepository();
            var mock = new Mock<IPolicyService>();
            mock.Setup(m => m.Get()).ReturnsAsync(new List<Policy>() { new Policy() { Id = 739562 } });

            var controller = new PoliciesController(mock.Object);

            //Act  
            var data = controller.Remove(739562).Result as OkResult;
            //Assert  
            Assert.IsNotNull(data);
            Assert.AreEqual(200, data.StatusCode);
        }
    }
}
