using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class PlayerMovementTests
    {
        [Test]
        public void Movement_On_X_Axis_Works()
        {
            int speed = 5;
            Vector3 xAxisMovement = new Vector3(1, 0, 0);

            PlayerMovement movementTest = new PlayerMovement(speed);

            Assert.AreEqual(new Vector3(5, 0, 0), movementTest.CalculateMovement(xAxisMovement));
        }
    }
}
