using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class HealthUnitTest
    {
        float maxHealth = 3.0f;
        float currentHealth = 0.0f;

        [Test]
        public void InstantiateEmptyHeartObject()
        {
            GameObject emptyHeart = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Health/Empty Heart"));
            Assert.IsNotNull(emptyHeart, "Empty Heart Instantialized");
        }

        [Test]
        public void InstantiateHalfHeartObject()
        {
            GameObject halfHeart = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Health/Half Heart"));
            Assert.IsNotNull(halfHeart, "Half Heart Instantialized");
        }

        [Test]
        public void InstantiateFullHeartObject()
        {
            GameObject fullHeart = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Health/Full Heart"));
            Assert.IsNotNull(fullHeart, "Full Heart Instantialized");
        }

        [Test]
        public void InstantiateLastHalfHeartObject()
        {
            GameObject lastHalfHeart = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Health/Last Half Heart"));
            Assert.IsNotNull(lastHalfHeart, "Last Half Heart Instantialized");
        }

        [Test]
        public void InstantiateGameOverText()
        {
            GameObject gameOverText = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Health/Game Over Text"));
            Assert.IsNotNull(gameOverText, "Game Over Text Instantialized");
        }

        [Test]
        public void InitializeCurrentHealthToMaxHealth()
        {
            currentHealth = maxHealth;
            Assert.AreEqual(currentHealth, maxHealth);
        }

        [Test]
        public void IncreaseHealthByOne()
        {
            float oldHealth = 0.0f;
            currentHealth += 1;
            Assert.Greater(currentHealth, oldHealth);
        }

        [Test]
        public void DecreaseHealthByOne()
        {
            float oldHealth = 0.0f;
            currentHealth -= 1;
            Assert.Less(currentHealth, oldHealth);
        }
    }
}
