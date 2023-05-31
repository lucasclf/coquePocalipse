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
    public LayerMask LayerZumbi;

    void Start(){
        jogador = GameObject.FindWithTag("Jogador");
    }

    // Update is called once per frame
    void Update(){
        float distanciaJogador = Vector3.Distance(transform.position, jogador.transform.position);
        contadorTempo += Time.deltaTime;

        if(contadorTempo >= TempoGeradorZumbi && distanciaJogador > distanciaGeracao){
            StartCoroutine(GerarNovoZumbi());
            contadorTempo = 0;
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

        Instantiate(Zumbi, posicao, transform.rotation);
    }

    Vector3 AleatorizarPosicao(){
        Vector3 posicao = Random.insideUnitSphere * radiusGerador;
        posicao += transform.position;
        posicao.y = 0;

        return posicao;
    }
}
