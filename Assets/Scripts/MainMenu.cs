using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public static bool sound = true;
    public Text SoundText;
    public AudioClip somTiro;
    public void PlayGame(){
        SceneManager.LoadScene("Level01"); 
    }

    public void Start(){
        if(sound == true){
            SoundText.text = "Desligar Som";
        } else{
            SoundText.text = "Ligar Som";
        }
    }

    public void SoundControl(){
        if(sound == true){
            SoundText.text = "Ligar Som";
            sound = false;
        } else{
            SoundText.text = "Desligar Som";
            sound = true;
        }
    }
}
