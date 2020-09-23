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

        [Test]
        public void Movement_On_Z_Axis_Works()
        {
            int speed = 5;
            Vector3 xAxisMovement = new Vector3(0, 0, 1);

            PlayerMovement movementTest = new PlayerMovement(speed);

            Assert.AreEqual(new Vector3(0, 0, 5), movementTest.CalculateMovement(xAxisMovement));
        }

        [Test]
        public void Movement_On_Daigonals_Work()
        {
            int speed = 5;
            Vector3 xAxisMovement = new Vector3(1, 0, 1);

            PlayerMovement movementTest = new PlayerMovement(speed);

            float normalizedSpeed = 5.0f / Mathf.Sqrt(50.0f);

            Assert.AreEqual(new Vector3(normalizedSpeed, 0, normalizedSpeed), movementTest.CalculateMovement(xAxisMovement));
        }
    }
}
