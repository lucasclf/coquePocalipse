using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaJogador : MonoBehaviour, IMatavel, ICuravel {
    private Vector3 direcao;
    private MovimentoJogador movimentoJogadorScript;
    private AnimacaoPersonagem animacaoPersonagemScript;
    private int skinDePreferencia;
    public Status statusJogador;
    public ControlaInterface ScriptInterface; 
    public AudioClip SomDano;
    public AudioClip SomCura;
    public LayerMask MascaraChao;
    public GameObject TextoGameOver;

    void Start(){
        skinDePreferencia = PlayerPrefs.GetInt("SkinDePreferencia");
        TrocandoSkinDoPlayer();
        movimentoJogadorScript = GetComponent<MovimentoJogador>();
        animacaoPersonagemScript = GetComponent<AnimacaoPersonagem>();
        statusJogador = GetComponent<Status>();
    }
    
    public void TrocandoSkinDoPlayer(){
        transform.GetChild(skinDePreferencia).gameObject.SetActive(true);
    }

    void Update(){
        float eixoX = Input.GetAxis("Horizontal");
        float eixoZ = Input.GetAxis("Vertical");

        direcao = new Vector3(eixoX, 0, eixoZ);
        animacaoPersonagemScript.Movimentar(direcao.magnitude);
    }

    void FixedUpdate(){
        movimentoJogadorScript.Movimentar(direcao, statusJogador.Velocidade);
        movimentoJogadorScript.RotacionarJogador(MascaraChao);
    }

    public void TomarDano(int dano){
        statusJogador.Vida -= dano;
        ScriptInterface.AtualizarSlideVidaJogador();

        ControlaAudio.InstanciaControleAudio.PlayOneShot(SomDano);
        if(statusJogador.Vida <= 0){
            Morrer();
        }
    }

    public void Morrer(){
        ScriptInterface.GameOver();
    }

    public void CurarVida(int cura){
        statusJogador.Vida += cura;
        if(statusJogador.Vida > statusJogador.VidaInicial){
            statusJogador.Vida = statusJogador.VidaInicial;
        }
        ScriptInterface.AtualizarSlideVidaJogador();

        ControlaAudio.InstanciaControleAudio.PlayOneShot(SomCura);
        
    }
}
