using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeradorZumbis : MonoBehaviour {

    public GameObject Zumbi;
    public float TempoGeradorZumbi = 1;
    private float contadorTempo = 0;
    private float radiusGerador = 3;
    private float distanciaGeracao = 20;
    private GameObject jogador;
    private int quantidadeMaximaZumbi = 2;
    private int quantidadeAtualZumbi;
    private float tempoAumentoDificuldade = 10;
    private int dificuldadeAtual = 1;
    public LayerMask LayerZumbi;

    void Start(){
        jogador = GameObject.FindWithTag("Jogador");

        for(int i = 0; i < quantidadeMaximaZumbi; i++){
            StartCoroutine(GerarNovoZumbi());
        }

    }

    // Update is called once per frame
    void Update(){
        float distanciaJogador = Vector3.Distance(transform.position, jogador.transform.position);
        contadorTempo += Time.deltaTime;

        if(contadorTempo >= TempoGeradorZumbi && 
        distanciaJogador > distanciaGeracao &&
        quantidadeAtualZumbi < quantidadeMaximaZumbi){
            StartCoroutine(GerarNovoZumbi());
            contadorTempo = 0;
        }


        if(Time.timeSinceLevelLoad > tempoAumentoDificuldade * dificuldadeAtual){
            quantidadeMaximaZumbi += dificuldadeAtual;
            dificuldadeAtual++;
        }
    }

    void OnDrawGizmos(){
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position,radiusGerador);
    }

    IEnumerator GerarNovoZumbi(){
        Vector3 posicao = AleatorizarPosicao();
        Collider[] colisores = Physics.OverlapSphere(posicao, 1, LayerZumbi);

        while(colisores.Length > 0){
            posicao = AleatorizarPosicao();
            colisores = Physics.OverlapSphere(posicao, 1, LayerZumbi);
            yield return null;
        }

        ControlaZumbi zumbi = Instantiate(Zumbi, posicao, transform.rotation).GetComponent<ControlaZumbi>();
        zumbi.GeradorMae = this; 
        quantidadeAtualZumbi++;
    }

    public void DiminuirQuantidadeZumbisVivos(){
        quantidadeAtualZumbi--;
    }

    Vector3 AleatorizarPosicao(){
        Vector3 posicao = Random.insideUnitSphere * radiusGerador;
        posicao += transform.position;
        posicao.y = 0;

        return posicao;
    }
}
