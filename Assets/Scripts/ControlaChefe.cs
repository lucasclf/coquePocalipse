using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class ControlaChefe : MonoBehaviour, IMatavel
{
    private Transform jogador;
    private NavMeshAgent agente;
    private Status statusChefe;
    private AnimacaoPersonagem animacaoChefe;
    private MovimentoPersonagem movimentoChefe;
    private bool morto = false;
    private ControlaInterface controlaInterfaceScript;
    public GameObject KitMedico;
    public Slider sliderVidaChef;
    public Image imageSlider;
    public Color CorVidaMaxima, CorVidaMinima;

    void Start(){
        jogador = GameObject.FindWithTag("Jogador").transform;
        agente = GetComponent<NavMeshAgent>();
        statusChefe = GetComponent<Status>();
        agente.speed = statusChefe.Velocidade;
        animacaoChefe = GetComponent<AnimacaoPersonagem>();
        movimentoChefe = GetComponent<MovimentoPersonagem>();
        controlaInterfaceScript = GameObject.FindObjectOfType(typeof(ControlaInterface)) as ControlaInterface;
        sliderVidaChef.maxValue = statusChefe.VidaInicial;
        AtualizarInterface();
    }

    void Update(){
        agente.SetDestination(jogador.position);
        animacaoChefe.Movimentar(agente.velocity.magnitude);
        float distancia = Vector3.Distance(transform.position, jogador.position);
        
        if(agente.hasPath){
            bool proximoAoJogador = agente.remainingDistance <= agente.stoppingDistance;
            if(proximoAoJogador){
                animacaoChefe.Atacar(true);
                Vector3 direcao = jogador.position - transform.position;
                movimentoChefe.Rotacionar(direcao);
            } else{
                animacaoChefe.Atacar(false);
            }
        }

        if(morto && distancia > 20){
            Destroy(gameObject);
        }
    }

    void AtacaJogador(){
        int dano = Random.Range(30, 40);
        jogador.GetComponent<ControlaJogador>().TomarDano(dano);
    }

    public void TomarDano(int dano){
        statusChefe.Vida -= dano;
        AtualizarInterface();
        if(statusChefe.Vida <= 0){
            Morrer();
        }
    }

    public void Morrer(){
        animacaoChefe.Morrer();
        movimentoChefe.Morrer();
        controlaInterfaceScript.AtualizarContadorDeMortos();
        this.enabled = false;
        agente.enabled = false;
        DerrubarItem();
    }

    void DerrubarItem(){
        Vector3 distanciaGeracao = transform.position;
        distanciaGeracao.x += 1;
        distanciaGeracao.z += 1;
        Instantiate(KitMedico, distanciaGeracao, Quaternion.identity);
    }

    void AtualizarInterface(){
        sliderVidaChef.value = statusChefe.Vida;
        float porcentagemVida = (float)statusChefe.Vida / statusChefe.VidaInicial;
        imageSlider.color = Color.Lerp(CorVidaMinima, CorVidaMaxima, porcentagemVida);
    }
}
