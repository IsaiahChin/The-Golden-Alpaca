using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class CombatUnitTest1
    {
        
        //A Test behaves as an ordinary method
        [Test]
        public void InstantiateBullet()
        {
            GameObject bullet=null;
            bullet = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Combat/BulletTest"));
            Assert.IsNotNull(bullet, "Bullet Instantialized");
        }
        [Test]
        public void InstantiateBulletRed()
        {
            GameObject bulletred = null;
            bulletred = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Combat/BulletTestRed"));
            Assert.IsNotNull(bulletred, "Bullet Red Instantialized");
        }

        [Test]
        public void InstantiateAlpaca()
        {
            GameObject alpaca = null;
            alpaca = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Combat/AlpacaTest"));
            Assert.IsNotNull(alpaca, "Alpaca Instantialized");
        }

        [Test]
        public void InstantiateEnemy()
        {
            GameObject enemy = null;
            enemy = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Combat/EnemyTest"));
            Assert.IsNotNull(enemy, "Enemy Instantialized");
        }

        [Test]
        public void InstantiatePointLight()
        {
            GameObject pointLight = null;
            pointLight = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Combat/Point LightTest"));
            Assert.IsNotNull(pointLight, "Point Light Instantialized");
        }


    }
}
