using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaAudio : MonoBehaviour
{
    private AudioSource controleAudio;
    private string muted = "False";
    public static bool MutedDeprecated;
    public static AudioSource InstanciaControleAudio;

    void Awake(){
        controleAudio = GetComponent<AudioSource>();
        InstanciaControleAudio = controleAudio;
    }

    void Update(){
        AlteraVolume();
    }

    public void AlteraVolume(){
        muted = PlayerPrefs.GetString("MutedOption");
        if(muted == "True"){
            controleAudio.volume = 0;
        } else {
            controleAudio.volume = 1;
        }
    }
    public void AlteraVolumeDeprecated(){
        if(MutedDeprecated == true){
            controleAudio.volume = 0;
        } else {
            controleAudio.volume = 1;
        }
    }
}
