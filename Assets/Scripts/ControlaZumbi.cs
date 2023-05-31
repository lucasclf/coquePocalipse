using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaZumbi : MonoBehaviour, IMatavel{
    private Rigidbody rigidbodyZumbi;
    private Animator animatorZumbi;
    private MovimentoPersonagem movimentoInimigo;
    private AnimacaoPersonagem animacaoInimigo;
    private Vector3 posicaoAleatoria;
    private Vector3 direcao;
    private float contadorVagar;
    private float tempoEntrePosicoesVagar = 4;
    public GameObject Jogador;    
    public AudioClip SomMorteZumbi;

    public Status StatusZumbi;
    // Start is called before the first frame update
    void Start(){
        Jogador = GameObject.FindWithTag("Jogador");
        aleatorizarZumbi();
        movimentoInimigo = GetComponent<MovimentoPersonagem>();
        animacaoInimigo = GetComponent<AnimacaoPersonagem>();
        StatusZumbi = GetComponent<Status>();
    }

    void FixedUpdate(){
        float distancia = Vector3.Distance(transform.position, Jogador.transform.position);
        
        movimentoInimigo.Rotacionar(direcao);
        animacaoInimigo.Movimentar(direcao.magnitude);

        if(distancia > 20){
            Vagar();
            animacaoInimigo.Atacar(false);
        } else  if(distancia > 2.5){
            direcao = Jogador.transform.position - transform.position;
            movimentoInimigo.Movimentar(direcao, StatusZumbi.Velocidade);
            animacaoInimigo.Atacar(false);
        } else{
            animacaoInimigo.Atacar(true);
        }
    }

    void Vagar(){
        contadorVagar -= Time.deltaTime;
        if(contadorVagar <= 0){
            posicaoAleatoria = AleatorizarPosicao();
            contadorVagar += tempoEntrePosicoesVagar;
        }
        
        bool pertoSuficiente = Vector3.Distance(transform.position, posicaoAleatoria) <= 0.05;

        if(pertoSuficiente == false){
            direcao = posicaoAleatoria - transform.position;
            movimentoInimigo.Movimentar(direcao, StatusZumbi.Velocidade);
        }
    }

    Vector3 AleatorizarPosicao(){
        Vector3 posicao =  Random.insideUnitSphere * 10;
        posicao += transform.position;
        posicao.y = transform.position.y;

        return posicao;

    }

    void AtacaJogador(){
        int dano = Random.Range(20, 30);
        Jogador.GetComponent<ControlaJogador>().TomarDano(dano);
    }

    void aleatorizarZumbi(){
        int seedTipoZumbi = Random.Range(1, 28);
        transform.GetChild(seedTipoZumbi).gameObject.SetActive(true);
    }

    public void TomarDano(int dano)
    {
        StatusZumbi.Vida -= dano;
        if(StatusZumbi.Vida <= 0){
            Morrer();
        }
    }

    public void Morrer()
    {
        ControlaAudio.instanciaControleAudio.PlayOneShot(SomMorteZumbi);
        Destroy(gameObject);
    }
}
