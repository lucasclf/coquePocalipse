using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaGeradorChefe : MonoBehaviour
{
    private float tempoProximaGeracao = 0;
    public float tempoEntreGeracoes = 60;
    private float radiusGerador = 3;
    public GameObject ChefePrefab;

    void Start(){
        tempoProximaGeracao = tempoEntreGeracoes;
    }

    void Update(){
        if(Time.timeSinceLevelLoad > tempoProximaGeracao){
            Instantiate(ChefePrefab, transform.position, Quaternion.identity);
            tempoProximaGeracao = Time.timeSinceLevelLoad + tempoEntreGeracoes;
        }
    }

    void OnDrawGizmos(){
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, radiusGerador);
    }


}
