using System;
using System.Collections.Generic;
using MariosSpeciality.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MariosSpeciality.Models;
using Moq;
using MarioSpecialityTests.Models;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace MarioSpecialityTests.ControllersTest
{
    [TestClass]
    public class ProductsControllerTest : IDisposable
    {
        Mock<IProductRepository> mock = new Mock<IProductRepository>();
        EFProductRepository db = new EFProductRepository(new TestDbContext());
        public void Dispose()
        {
            db.DeletAll();
        }

        private void DbSetup()
        {
            var querableProductArray = new Product[]
            {
                new Product(){ProductId = 1, Name = "Kitfo", Cost = 10, CountryOfOrigin = "Ethiopia", ProductImg = null},
                new Product(){ProductId = 2, Name = "Borittos", Cost = 12, CountryOfOrigin = "Mexico", ProductImg = null},
                new Product(){ProductId = 3, Name = "Sushi", Cost = 15, CountryOfOrigin = "Japan", ProductImg = null}
            }.AsQueryable();
            mock.Setup(m => m.Products).Returns(querableProductArray);
        }

        [TestMethod]
        public void Mock_GetViewResultIndex_Test()
        {
            DbSetup();
            //Arange
            var productController = new ProductsController(mock.Object);

            //Act
            var result = productController.Index();
            //Assert
            Assert.IsInstanceOfType(result, typeof(IActionResult));
        }

        [TestMethod]
        public void Mock_IndexListOfProducts_Test()
        {
            DbSetup();
            //Arange
            var productController = new ProductsController(mock.Object);
            var indexViewResult = productController.Index() as ViewResult;

            //Act
            var result = indexViewResult.ViewData.Model;
            //Assert
            Assert.IsInstanceOfType(result, typeof(List<Product>));
        }

        [TestMethod]
        public void Mock_ConfirmEntry_Test()
        {
            DbSetup();
            //Arange
            var productController = new ProductsController(mock.Object);
            var indexViewResult = productController.Index() as ViewResult;
            var testProduct = new Product()
            {   ProductId = 1,
                Name = "Kitfo",
                Cost = 10,
                CountryOfOrigin = "Ethiopia",
                ProductImg = null
            };

            //Act
            var collection = indexViewResult.ViewData.Model as List<Product>;
            //Assert
            CollectionAssert.Contains(collection, testProduct);
        }

        [TestMethod]
        public void DB_CreateNewEntryGET_Test()
        {
            //Arange
            var productController = new ProductsController(db);
            //Act
            var productViewResult = productController.Create();
            //Assert
            Assert.IsInstanceOfType(productViewResult, typeof(IActionResult));
        }

        [TestMethod]
        public void DB_CreateNewEntryPOST_Test()
        {
            //Arange
            var productController = new ProductsController(db);
            var testProduct = new Product()
            {
                Name = "Kitfo",
                Cost = 10,
                CountryOfOrigin = "Ethiopia",
                ProductImg = null
            };

            //Act
            productController.Create(testProduct);

            var indexViewResult = productController.Index() as ViewResult;
            var collection = indexViewResult.ViewData.Model as List<Product>;
            //Assert
            CollectionAssert.Contains(collection, testProduct);
        }



        [TestMethod]
        public void DB_EditEntryGET_Test()
        {
            CreateProduct();
            //Arange
            var productController = new ProductsController(db);
            int productId = db.Products.ToList()[0].ProductId;

            //Act
            var viewResult = productController.Edit(productId) as ViewResult;
            var productToEdit = viewResult.ViewData.Model;

            var indexViewResult = productController.Index() as ViewResult;
            var collection = indexViewResult.ViewData.Model as List<Product>;
            //Assert
            CollectionAssert.Contains(collection, productToEdit);
        }

        [TestMethod]
        public void DB_EditEntryPOST_Test()
        {
            CreateProduct();
            //Arange
            var productController = new ProductsController(db);
            int productId = db.Products.ToList()[0].ProductId;
            var viewResult = productController.Edit(productId) as ViewResult;
            var productToEdit = viewResult.ViewData.Model as Product;


            //Act
            productToEdit.CountryOfOrigin = "Mexico";
            productController.Edit(productToEdit);
            var indexViewResult = productController.Index() as ViewResult;
            var collection = indexViewResult.ViewData.Model as List<Product>;
            //Assert
            CollectionAssert.Contains(collection, productToEdit);
        }

        [TestMethod]
        public void DB_DeleteEntryGET_Test()
        {
            CreateProduct();
            //Arange
            var productController = new ProductsController(db);

            int productId = db.Products.ToList()[0].ProductId;
            var viewResult = productController.Delete(productId) as ViewResult;
            var productToDelete = viewResult.ViewData.Model as Product;

            //Act
            var indexViewResult = productController.Index() as ViewResult;
            var collection = indexViewResult.ViewData.Model as List<Product>;
            //Assert
            CollectionAssert.Contains(collection, productToDelete);
        }

        [TestMethod]
        public void DB_DeleteConfirmedEntryPOST_Test()
        {
            CreateProduct();
            //Arange
            var productController = new ProductsController(db);
            int productId = db.Products.ToList()[0].ProductId;
            var viewResult = productController.Delete(productId) as ViewResult;
            var productToDelete = viewResult.ViewData.Model as Product;

            //Act
            productController.DeleteConfirmed(productId);
            var indexViewResult = productController.Index() as ViewResult;
            var collection = indexViewResult.ViewData.Model as List<Product>;
            //Assert
            CollectionAssert.DoesNotContain(collection, productToDelete);
        }

        private Product CreateProduct()
        {
            var productController = new ProductsController(db);
            var testProduct = new Product()
            {
                Name = "Kitfo",
                Cost = 10,
                CountryOfOrigin = "Ethiopia",
                ProductImg = null
            };
            productController.Create(testProduct);
            return testProduct;
        }
    }
}
