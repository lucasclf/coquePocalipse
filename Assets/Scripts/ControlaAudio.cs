using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaAudio : MonoBehaviour
{
    private AudioSource controleAudio;
    public static AudioSource instanciaControleAudio;

    void Awake(){
        controleAudio = GetComponent<AudioSource>();
        instanciaControleAudio = controleAudio;
    }

    void FixedUpdate(){
        if(MainMenu.sound == false){
            controleAudio.volume = 0;
        } else {
            controleAudio.volume = 1;
        }
    }
}
