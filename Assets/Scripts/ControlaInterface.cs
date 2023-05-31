using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlaInterface : MonoBehaviour
{
    private ControlaJogador scriptJogador;
    public Slider SliderVidaJogador;
    // Start is called before the first frame update
    void Start(){
        scriptJogador = GameObject.FindWithTag("Jogador").GetComponent<ControlaJogador>();
        SliderVidaJogador.maxValue = scriptJogador.statusJogador.Vida;
        AtualizarSlideVidaJogador();
    }

    // Update is called once per frame
    void Update(){
        
    }

    public void AtualizarSlideVidaJogador(){
        SliderVidaJogador.value = scriptJogador.statusJogador.Vida;
    }
}
