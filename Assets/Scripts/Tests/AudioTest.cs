
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;

namespace Tests{
    public class AudioTest 
    {
        private float sVolume = 1f;
        private float sPitch = 1f;
        [Test]
        public void VolumeInRange(){
            float max = 1f;
            float min = 0f;
            Assert.IsTrue (sVolume <= max && sVolume>=min);
        }
       [Test]
       public void PitchInRange(){
            float max = 3f;
            float min = 0.1f;
            Assert.IsTrue (sPitch <= max && sPitch>=min);
        }
        [Test]
        public void IsPlaying(){
            AudioSource testSource = new AudioSource();
            Assert.IsTrue(testSource.isPlaying);
        }
    }
}
