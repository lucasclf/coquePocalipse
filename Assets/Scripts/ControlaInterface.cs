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
    private int contadorDeMortes = 0;
    public Slider SliderVidaJogador;
    public GameObject PainelGameOver;
    public AudioClip SomGameOver;
    public Text TextoRankTempo;
    public Text TextoRankTempoMáximo;
    public Text TextoTempoDeJogo;
    public Text TextoContadorMortes;
    public Text TextoChefe;

    // Start is called before the first frame update
    void Start(){
        Time.timeScale = 1;
        scriptJogador = GameObject.FindWithTag("Jogador").GetComponent<ControlaJogador>();
        SliderVidaJogador.maxValue = scriptJogador.statusJogador.Vida;
        AtualizarSlideVidaJogador();
        tempoPontuacaoSalvo = PlayerPrefs.GetFloat("MelhorTempoSalvo");
        MontarTextoContadorMortes();
    }

    void FixedUpdate(){
        MontarTextoTempo();
    }

    public void AtualizarSlideVidaJogador(){
        SliderVidaJogador.value = scriptJogador.statusJogador.Vida;
    }

    public void GameOver(){
        Time.timeScale = 0; 
        AudioSource audio = ControlaAudio.InstanciaControleAudio.GetComponent<AudioSource>();
        audio.clip = SomGameOver;
        audio.Play();
        PainelGameOver.SetActive(true);

        AjustarPontuacao();
        MontarTextoGameOver();
    }

    public void Reiniciar(){
        SceneManager.LoadScene("Menu");
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
                "Você sobreviveu por {0} segundos e matou {1} criaturas!", segundosAtuais, contadorDeMortes
            ) : 
            string.Format(
                "Você sobreviveu por {0} minutos e {1} segundos e matou {2} criaturas!", minutosAtuais, segundosAtuais, contadorDeMortes
            );
        
        TextoRankTempoMáximo.text = minutosRecord <= 0 ?
        string.Format(
            "Seu melhor tempo é de {0}  segundos!", segundosRecord
        ) :
        string.Format(
            "Seu melhor tempo é de {0} minutos e {1} segundos!", minutosRecord, segundosRecord
        );
    }

    private void MontarTextoTempo(){
        tempoAtual = ConverterTempo(Time.timeSinceLevelLoad);
        string minutosAtuais = tempoAtual[0].ToString("00");
        string segundosAtuais = tempoAtual[1].ToString("00");

        TextoTempoDeJogo.text = string.Format("{0}:{1}", minutosAtuais, segundosAtuais);
    }

    private void MontarTextoContadorMortes(){
        TextoContadorMortes.text = string.Format("x {0}", contadorDeMortes);
    }

    private int[] ConverterTempo(float tempo){
        int minutos = (int)(tempo / 60);
        int segundos = (int)(tempo % 60);

        return new int[] {minutos, segundos};
    }

    public void AtualizarContadorDeMortos(){
        contadorDeMortes++;
        MontarTextoContadorMortes();
    }

    public void AparecerTextoChefe(string texto){
        TextoChefe.text = texto;
        StartCoroutine(DesapareceTexto(5, TextoChefe));
    }

    IEnumerator DesapareceTexto(float tempoSumico, Text textoParaSumir){
        Color corTexto = textoParaSumir.color;
        corTexto.a = 1;
        textoParaSumir.color = corTexto;
        yield return new WaitForSeconds(1);
        float contador = 0;
        textoParaSumir.gameObject.SetActive(true);
        while(textoParaSumir.color.a > 0){
            contador += Time.deltaTime / tempoSumico;
            corTexto.a = Mathf.Lerp(1, 0, contador);
            textoParaSumir.color = corTexto;

            if(textoParaSumir.color.a < 0){
                textoParaSumir.gameObject.SetActive(false);
            }

            yield return null;
        }
    }

}
