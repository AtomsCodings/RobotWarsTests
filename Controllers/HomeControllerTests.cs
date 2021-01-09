using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RobotWars.Controllers;
using RobotWars.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RobotWars.Tests
{
    [TestClass()]
    public class HomeControllerTests
    {
        IPosition _position = new Position.Position();
        Models.RobotWars _robotWars = new Models.RobotWars();
        ILogger<HomeController> _logger; 

        //Tests from instructions
        [TestMethod()]

        public void ScenarioOneTest_ResultPositive()
        {
            //Arrange
            var controller = new HomeController(_logger);
            _robotWars.InitialPosition = "0,2,E";
            _robotWars.MovementInstructions = "MLMRMMMRMMRR";
            var expectedPenalty = 0;
            var expectedFinalPosition = "4, 1, N";

            //Action
            var Result = controller.Index(_robotWars);
            var actualPenalty = _robotWars.PenaltyCount;
            var actionFinalPosition = _robotWars.FinalPosition;

            //Assert
            Assert.AreEqual(expectedPenalty, actualPenalty);
            Assert.AreEqual(expectedFinalPosition, actionFinalPosition);
        }

        //Test from instructions
        [TestMethod]
        public void ScenarioTwoTest_ResultPositive()
        {
            //Arrange
            var controller = new HomeController(_logger);
            _robotWars.InitialPosition = "4,4,S";
            _robotWars.MovementInstructions = "LMLLMMLMMMRMM";
            var expectedPenalty = 1;
            var expectedFinalPosition = "0, 1, W";

            //Action
            var Result = controller.Index(_robotWars);
            var actualPenalty = _robotWars.PenaltyCount;
            var actionFinalPosition = _robotWars.FinalPosition;

            //Assert
            Assert.AreEqual(expectedPenalty, actualPenalty);
            Assert.AreEqual(expectedFinalPosition, actionFinalPosition);
        }

        //Test from instructions
        [TestMethod]
        public void ScenarioThreeTest_ResultPositive()
        {
            //Arrange
            var controller = new HomeController(_logger);
            _robotWars.InitialPosition = "2,2,W";
            _robotWars.MovementInstructions = "MLMLMLM RMRMRMRM";
            var expectedPenalty = 0;
            var expectedFinalPosition = "2, 2, N";

            //Action
            var Result = controller.Index(_robotWars);
            var actualPenalty = _robotWars.PenaltyCount;
            var actionFinalPosition = _robotWars.FinalPosition;

            //Assert
            Assert.AreEqual(expectedPenalty, actualPenalty);
            Assert.AreEqual(expectedFinalPosition, actionFinalPosition);
        }

        //Test from instructions, and lower entry to lower case
        [TestMethod]
        public void ScenarioFourTest_ResultPositive()
        {
            //Arrange
            var controller = new HomeController(_logger);
            _robotWars.InitialPosition = "1,3,N".ToLower();
            _robotWars.MovementInstructions = "MMLMMLMMMMM".ToLower();
            var expectedPenalty = 3;
            var expectedFinalPosition = "0, 0, S";

            //Action
            var Result = controller.Index(_robotWars);
            var actualPenalty = _robotWars.PenaltyCount;
            var actionFinalPosition = _robotWars.FinalPosition;

            //Assert
            Assert.AreEqual(expectedPenalty, actualPenalty);
            Assert.AreEqual(expectedFinalPosition, actionFinalPosition);
        }

        //Processing code fails so user is taken to the error view
        [TestMethod]
        public void ProcessingCodeFails_ResultErrorPage()
        {
            //Arrange
            var controller = new HomeController(_logger);
            _robotWars.InitialPosition = "x";
            _robotWars.MovementInstructions = "MMLMMLMMMMM";
            var expectedView = "Error";

            //Action
            var Result = controller.Index(_robotWars);
            var actualView = ((Microsoft.AspNetCore.Mvc.ViewResult)Result).ViewName;

            //Assert
            Assert.AreEqual(expectedView, actualView);
        }

        //Model state is not valid, so user is taken back to the index view
        [TestMethod]
        public void ModelStateNotValid_ResultTakenBackToIndexView()
        {
            //Arrange
            var controller = new HomeController(_logger);
            var expectedView = "Index";
            controller.ModelState.AddModelError("InitialPosition", "Required");

            //Action
            var Result = controller.Index(_robotWars);
            var actualView = ((Microsoft.AspNetCore.Mvc.ViewResult)Result).ViewName;

            //Assert
            Assert.AreEqual(expectedView, actualView);
        }
    }
}