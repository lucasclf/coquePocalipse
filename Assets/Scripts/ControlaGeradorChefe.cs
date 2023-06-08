using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaGeradorChefe : MonoBehaviour
{
    private float tempoProximaGeracao = 0;
    public float tempoEntreGeracoes = 60;
    private float radiusGerador = 3;
    private ControlaInterface scriptControlaInterface;
    private Transform jogador;
    public GameObject ChefePrefab;
    public Transform[] SpanwPointPosicao;

    void Start(){
        tempoProximaGeracao = tempoEntreGeracoes;
        scriptControlaInterface = GameObject.FindObjectOfType(typeof(ControlaInterface)) as ControlaInterface;
        jogador = GameObject.FindWithTag("Jogador").transform;
    }

    void Update(){
        if(Time.timeSinceLevelLoad > tempoProximaGeracao){
            GerarChefe();
        }
    }

    void OnDrawGizmos(){
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, radiusGerador);
    }

    void GerarChefe(){
            Vector3 posicaoCriacao = CalcularPosicaoMaisDistante();
            Instantiate(ChefePrefab, posicaoCriacao, Quaternion.identity);
            tempoProximaGeracao = Time.timeSinceLevelLoad + tempoEntreGeracoes;
            scriptControlaInterface.AparecerTextoChefe("Shadows darken a new evil walks the earth!!!");
    }

    Vector3 CalcularPosicaoMaisDistante(){
        Vector3 posicaoMaisDistante = Vector3.zero;
        float maiorDistancia = 0;
        foreach(Transform posicao in SpanwPointPosicao){
            float distanciaEntreJogador = Vector3.Distance(posicao.position, jogador.position);
            if(distanciaEntreJogador > maiorDistancia){
                maiorDistancia = distanciaEntreJogador;
                posicaoMaisDistante = posicao.position;
            }
        }
        return posicaoMaisDistante;
    }

}
