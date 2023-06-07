using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private ControlaAudio controlaAudioScript;
    public Text SoundText;
    public AudioClip somTiro;

    public void Awake(){
            SoundText.text = "Ligar Som";
    }

    public void Start(){
        controlaAudioScript = GameObject.FindObjectOfType(typeof(ControlaAudio)) as ControlaAudio;
        ControlaAudio.Muted = true;
    }

    public void PlayGame(){
        SceneManager.LoadScene("Level01"); 
    }

    public void SoundControl(){
        if(ControlaAudio.Muted == true){
            SoundText.text = "Desligar Som";
            ControlaAudio.Muted = false;
        } else{
            SoundText.text = "Ligar Som";
            ControlaAudio.Muted = true;
        }
        controlaAudioScript.AlteraVolume();
    }
}
