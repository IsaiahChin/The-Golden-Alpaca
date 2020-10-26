using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class EnemyCounterTests
    {
        private int enemyCounter;
        private int maxEnemies;
        private int minEnemies;

        private void ResetValues()
        {
            enemyCounter = 0;
            maxEnemies = 100;
            minEnemies = 0;
        }

        [Test]
        public void EnemyCounterIncreaseByOne()
        {
            ResetValues();
            int oldValue = enemyCounter;
            increaseCount();
            Assert.Greater(enemyCounter, oldValue);
        }

        [Test]
        public void EnemyCounterDecreaseByOne()
        {
            ResetValues();
            enemyCounter = 5;
            int oldValue = enemyCounter;
            decreaseCount();
            Assert.Less(enemyCounter, oldValue);
        }

        [Test]
        public void EnemyCounterCannotGoUnderUnderMinEnemies()
        {
            ResetValues();
            for (int i = 0; i < 150; i++)
            {
                decreaseCount();
            }
            Assert.AreEqual(enemyCounter, minEnemies);
        }

        [Test]
        public void EnemyCounterCannotGoOverMaxEnemies()
        {
            ResetValues();
            for (int i = 0; i < 150; i++)
            {
                increaseCount();
            }
            Assert.AreEqual(enemyCounter, maxEnemies);
        }

        private void increaseCount()
        {
            if (enemyCounter >= maxEnemies)
            { enemyCounter = maxEnemies; }
            else
            { enemyCounter += 1; }
        }
        private void decreaseCount()
        {
            if (enemyCounter <= minEnemies)
            { enemyCounter = minEnemies; }
            else
            { enemyCounter -= 1; }
        }
    }
}
