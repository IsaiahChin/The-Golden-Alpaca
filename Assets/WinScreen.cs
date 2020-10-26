using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScreen : MonoBehaviour
{
    // Start is called before the first frame update
  void Start(){
      //Play Music
      FindObjectOfType<AudioManager>().PlayBGM("YouWinScene");
      StartCoroutine(wait());
  }
  IEnumerator wait()
    {   
        // wait two seconds otherwise player may immdiately move on the next scene
        yield return new WaitForSeconds(2);
    }

      void Update()
    {
        if (Input.anyKey)
        {
            SceneManager.LoadScene(0);
        }  
            
    }
}
