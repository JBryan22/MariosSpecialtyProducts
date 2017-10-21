using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using MariosSpeciality.Models;
using MariosSpeciality.Controllers;
using Moq;
using System.Linq;
using MarioSpecialityTests.Models;
using Microsoft.AspNetCore.Mvc;

namespace MarioSpecialityTests.ControllersTest
{
    [TestClass]
    public class ReviewsControllerTest : IDisposable
    {
        Mock<IReviewRepository> mock = new Mock<IReviewRepository>();
        EFReviewRepository db = new EFReviewRepository(new TestDbContext());
        EFProductRepository dbp = new EFProductRepository(new TestDbContext());

        public void Dispose()
        {
            db.DeleteAll();
            dbp.DeletAll();
        }
        private void DbSetup()
        {
            var querableReviewArray = new Review[] 
            {
                new Review(){ReviewId = 1, Author = "Mike", ContntBody = "Awesome", Rating = 4, ProductId = 1},
                new Review(){ReviewId = 2, Author = "Kaili", ContntBody = "Awesome", Rating = 5, ProductId = 1},
                new Review(){ReviewId = 3, Author = "Jesse", ContntBody = "Awesome", Rating = 3, ProductId = 1}

            }.AsQueryable();
            mock.Setup(m => m.Reviews).Returns(querableReviewArray);
        }

        [TestMethod]
        public void Mock_GetViewResultIndex_Test()
        {
            DbSetup();
            //Arrange
            var reviewController = new ReviewsController(mock.Object);
            //Act
            var result = reviewController.Index();

            //Assert
            Assert.IsInstanceOfType(result, typeof(IActionResult));
        }

        [TestMethod]
        public void Mock_GetListOfReviewIndex_Test()
        {
            DbSetup();
            //Arrange
            var reviewController = new ReviewsController(mock.Object);
            var indexViewResult = reviewController.Index() as ViewResult;
            //Act
            var result = indexViewResult.ViewData.Model as List<Review>;

            //Assert
            Assert.IsInstanceOfType(result, typeof(List<Review>));
        }

        [TestMethod]
        public void Mock_ConfirmEntryIndex_Test()
        {
            DbSetup();
            //Arrange
            var reviewController = new ReviewsController(mock.Object);
            var indexViewResult = reviewController.Index() as ViewResult;
            var testReview = new Review() { ReviewId = 1, Author = "Mike", ContntBody = "Awesome", Rating = 4, ProductId = 1 };

            //Act
            var collection = indexViewResult.ViewData.Model as List<Review>;

            //Assert
            CollectionAssert.Contains(collection, testReview);
        }

        [TestMethod]
        public void DB_CreateEntryGET_Test()
        {
            //Arrange
            var reviewController = new ReviewsController(db);

            //Act
            var indexViewResult = reviewController.Create() as ViewResult;

            //Assert
            Assert.IsInstanceOfType(indexViewResult, typeof(IActionResult));
        }

        [TestMethod]
        public void DB_CreateEntryPOST_Test()
        {
            //Arrange
            var productController = new ProductsController(dbp);
            var testProduct = new Product()
            {
                Name = "Kitfo",
                Cost = 10,
                CountryOfOrigin = "Ethiopia",
                ProductImg = null
            };
            productController.Create(testProduct);
            var productId = dbp.Products.ToList()[0].ProductId;
            var reviewController = new ReviewsController(db);
            var testReview = new Review() { Author = "Mike", ContntBody = "Awesome", Rating = 4, ProductId = productId };

            //Act
            reviewController.Create(testReview);
            var collection = db.Reviews.ToList();

            //Assert
            CollectionAssert.Contains(collection, testReview);
        }

        [TestMethod]
        public void DB_EditEntryGET_Test()
        {
            CreateReview();
            //Arrange

            var reviewController = new ReviewsController(db);
            var reviewId = db.Reviews.ToList()[0].ReviewId;
            //Act
            var viewResult = reviewController.Edit(reviewId) as ViewResult;
            var reviewToEdit = viewResult.ViewData.Model as Review;
            var collection = db.Reviews.ToList();

            //Assert
            CollectionAssert.Contains(collection, reviewToEdit);
        }

        [TestMethod]
        public void DB_EditEntryPOST_Test()
        {
            CreateReview();
            //Arrange

            var reviewController = new ReviewsController(db);
            var reviewId = db.Reviews.ToList()[0].ReviewId;
            //Act
            var viewResult = reviewController.Edit(reviewId) as ViewResult;
            var reviewToEdit = viewResult.ViewData.Model as Review;
            reviewToEdit.Author = "Wihib";
            reviewController.Edit(reviewToEdit);
            var collection = db.Reviews.ToList();

            //Assert
            CollectionAssert.Contains(collection, reviewToEdit);
        }

        [TestMethod]
        public void DB_DeleteEntryGET_Test()
        {
            CreateReview();
            //Arrange

            var reviewController = new ReviewsController(db);
            var reviewId = db.Reviews.ToList()[0].ReviewId;
            //Act
            var viewResult = reviewController.Delete(reviewId) as ViewResult;
            var reviewToDelete = viewResult.ViewData.Model as Review;
            var collection = db.Reviews.ToList();

            //Assert
            CollectionAssert.Contains(collection, reviewToDelete);
        }

        [TestMethod]
        public void DB_DeleteConfirmedEntryPOST_Test()
        {
            CreateReview();
            //Arrange

            var reviewController = new ReviewsController(db);
            var reviewId = db.Reviews.ToList()[0].ReviewId;
            //Act
            var viewResult = reviewController.Delete(reviewId) as ViewResult;
            var reviewToDelete = viewResult.ViewData.Model as Review;
            reviewController.DeleteConfirmed(reviewId);
            var collection = db.Reviews.ToList();

            //Assert
            CollectionAssert.DoesNotContain(collection, reviewToDelete);
        }

        private void CreateReview()
        {
            var productController = new ProductsController(dbp);
            var reviewController = new ReviewsController(db);

            var testProduct = new Product()
            {
                Name = "Kitfo",
                Cost = 10,
                CountryOfOrigin = "Ethiopia",
                ProductImg = null
            };
            productController.Create(testProduct);
            var productId = dbp.Products.ToList()[0].ProductId;
            var testReview = new Review() { Author = "Mike", ContntBody = "Awesome", Rating = 4, ProductId = productId };
            reviewController.Create(testReview);
        }
    }
}
