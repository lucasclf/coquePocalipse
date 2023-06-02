using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaJogador : MonoBehaviour, IMatavel {
    private Vector3 direcao;
    private MovimentoJogador movimentoJogadorScript;
    private AnimacaoPersonagem animacaoPersonagemScript;
    public Status statusJogador;
    public ControlaInterface ScriptInterface; 
    public AudioClip SomDano;

    public LayerMask MascaraChao;
    public GameObject TextoGameOver;

    void Start(){
        movimentoJogadorScript = GetComponent<MovimentoJogador>();
        animacaoPersonagemScript = GetComponent<AnimacaoPersonagem>();
        statusJogador = GetComponent<Status>();
    }
    // Update is called once per frame
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

        ControlaAudio.instanciaControleAudio.PlayOneShot(SomDano);
        if(statusJogador.Vida <= 0){
            Morrer();
        }
    }

    public void Morrer(){
        ScriptInterface.GameOver();
    }
}
