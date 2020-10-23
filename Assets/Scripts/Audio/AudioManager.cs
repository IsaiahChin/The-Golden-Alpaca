using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    
    public Sound[] sounds;
    public static Sound nowPlaying;
    public static AudioManager instance;   
    
    //Sounds set up before the start() method
    void Awake(){
        if (instance == null){
            instance = this;
        }
        else{
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

        

        foreach(Sound s in sounds){
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            
            //routes audio source to Audio Mixer
            s.source.outputAudioMixerGroup = s.group;
        }
    }
  
    //Method to play background music
    public void PlayBGM(string name){

        foreach(Sound s in sounds){
            //nowPlaying is Sound variable to make sure only one song is playing
            //checks if there are any songs in nowPlaying
            if (s.name == name){
                if(nowPlaying!=null){
                    //if there was a song playing before it stops
                    nowPlaying.source.Stop();
                }
                nowPlaying = s;
                //play new BGM
                nowPlaying.source.Play(); 
                ResetBGM();
            }
        }
        
        
    }
    //resets BFM pitch and volume to 1
    public void ResetBGM(){
        nowPlaying.source.volume = 1;
        nowPlaying.source.pitch = 1;
    }
    
    //Method to play effects audio
    public void Play(string name){
        //Check if name is valid
        bool soundFound = false;
    
        // check each sound for a matching name and then play audio
        
        foreach(Sound s in sounds){
            if (s.name == name){
                s.source.Play();
                soundFound = true;    
            }
        }
        if (soundFound == false) {
            Debug.LogWarning("Sound: " + name + " not found.");
            return;
        }
    }
    
}
