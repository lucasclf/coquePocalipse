using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ControlaInterface : MonoBehaviour
{
    private ControlaJogador scriptJogador;
    private float tempoPontuacaoSalvo;
    private int[] tempoAtual;
    public Slider SliderVidaJogador;
    public GameObject PainelGameOver;
    public AudioClip SomGameOver;
    public Text TextoRankTempo;
    public Text TextoRankTempoMáximo;
    public Text TextoTempoDeJogo;

    // Start is called before the first frame update
    void Start(){
        Time.timeScale = 1;
        scriptJogador = GameObject.FindWithTag("Jogador").GetComponent<ControlaJogador>();
        SliderVidaJogador.maxValue = scriptJogador.statusJogador.Vida;
        AtualizarSlideVidaJogador();
        tempoPontuacaoSalvo = PlayerPrefs.GetFloat("MelhorTempoSalvo");
    }

    void FixedUpdate(){
        MontarTextoTempo();
    }

    public void AtualizarSlideVidaJogador(){
        SliderVidaJogador.value = scriptJogador.statusJogador.Vida;
    }

    public void GameOver(){
        Time.timeScale = 0; 
        AudioSource audio = ControlaAudio.instanciaControleAudio.GetComponent<AudioSource>();
        audio.clip = SomGameOver;
        audio.Play();
        PainelGameOver.SetActive(true);

        AjustarPontuacao();
        MontarTextoGameOver();
    }

    public void Reiniciar(){
        SceneManager.LoadScene("menu");
    }

    void AjustarPontuacao(){
        if(Time.timeSinceLevelLoad > tempoPontuacaoSalvo){
            tempoPontuacaoSalvo = Time.timeSinceLevelLoad;
            PlayerPrefs.SetFloat("MelhorTempoSalvo", tempoPontuacaoSalvo);
        }
    }

    private void MontarTextoGameOver(){
        int minutosAtuais = tempoAtual[0];
        int segundosAtuais = tempoAtual[1];

        int minutosRecord = (int)(tempoPontuacaoSalvo / 60);
        int segundosRecord = (int)(tempoPontuacaoSalvo % 60);

        TextoRankTempo.text = minutosAtuais <= 0 ? 
            string.Format(
                "Você sobreviveu por {0} segundos!", segundosAtuais
            ) : 
            string.Format(
                "Você sobreviveu por {0} minutos e {1} segundos!", minutosAtuais, segundosAtuais
            );
        
        TextoRankTempoMáximo.text = minutosRecord <= 0 ?
        string.Format(
            "Seu melhor tempo é de {0}  segundos!", segundosRecord
        ) :
        string.Format(
            "Seu melhor tempo é de {0} minuots e {1} segundos!", minutosRecord, segundosRecord
        );
    }

    private void MontarTextoTempo(){
        tempoAtual = ConverterTempo(Time.timeSinceLevelLoad);
        string minutosAtuais = tempoAtual[0].ToString("00");
        string segundosAtuais = tempoAtual[1].ToString("00");

        TextoTempoDeJogo.text = string.Format("{0}:{1}", minutosAtuais, segundosAtuais);
    }

    private int[] ConverterTempo(float tempo){
        int minutos = (int)(tempo / 60);
        int segundos = (int)(tempo % 60);

        return new int[] {minutos, segundos};
    }

}
