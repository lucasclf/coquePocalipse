using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 

public class ControlaMenu : MonoBehaviour
{
    private int numeroSkin = 1;
    private bool muted = false;
    public GameObject botaoSair; 
    public Transform player;
    public Text SoundText;
    
    private void Start(){
        #if UNITY_STANDALONE || UNITY_EDITOR
            botaoSair.SetActive(true);
        #endif
        ControlaTextoAudio();
    }

    public void JogarJogo(){
        PlayerPrefs.SetInt("SkinDePreferencia",numeroSkin);
        StartCoroutine(MudarCena("Level01"));
    }
    public void TrocarSkin()
    {
        player.GetChild(numeroSkin).gameObject.SetActive(false);
        numeroSkin++;

        if (numeroSkin >= player.childCount-1)
        { 
            numeroSkin = 1;
        }
        player.GetChild(numeroSkin).gameObject.SetActive(true);
    }

    public void ControlSound(){
        muted = !muted;
        ControlaTextoAudio();
    }

    IEnumerator MudarCena(string name){
        yield return new WaitForSeconds(0.3f);
        SceneManager.LoadScene(name);
    }

    public void SairDoJogo(){
        StartCoroutine(Sair());
    }

    IEnumerator Sair(){
        yield return new WaitForSeconds(0.3f);
                Application.Quit();
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

    private void ControlaTextoAudio(){
        PlayerPrefs.SetString("MutedOption", muted.ToString());
        if(muted == false){
            SoundText.text = "Desligar Som";
        } else{
            SoundText.text = "Ligar Som";
        }
    }
}
