using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;

namespace Tests{

    public class MainMenuUnitTest
    {
        [Test]
        public void InstantiatePlayButton(){
            GameObject playButton = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Menu/Play Button"));
            Assert.IsNotNull(playButton, "Play Button Instantialized");
        }

        [Test]
        public void InstantiateOptionsButton(){
            GameObject optionButton = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Menu/Option Button"));
            Assert.IsNotNull(optionButton, "Option Button Instantialized");
        }
        
        [Test]
        public void InstantiateQuitButton(){
            GameObject quitButton = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Menu/Quit Button"));
            Assert.IsNotNull(quitButton, "Quit Instantialized");
        }
       
    }

}
