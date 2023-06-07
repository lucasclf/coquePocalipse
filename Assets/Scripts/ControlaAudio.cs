using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaAudio : MonoBehaviour
{
    private AudioSource controleAudio;
    public static bool Muted;
    public static AudioSource InstanciaControleAudio;

    void Awake(){
        controleAudio = GetComponent<AudioSource>();
        InstanciaControleAudio = controleAudio;
        Debug.Log("ControlaAudio Awake muted: " + Muted);
    }

    void Start(){
        AlteraVolume();
    }

    void FixedUpdate(){
        
    }

    public void AlteraVolume(){
        Debug.Log("ControlaAudio AlteraVolume MUTED: " + Muted);
        if(Muted == true){
            controleAudio.volume = 0;
        } else {
            controleAudio.volume = 1;
        }
    }
}
