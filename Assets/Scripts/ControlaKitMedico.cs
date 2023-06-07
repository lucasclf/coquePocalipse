using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaKitMedico : MonoBehaviour{
    private int tempoDestruicao = 5;
    public int quantidadeCura = 15;
    
    private void Start(){
        Destroy(gameObject, tempoDestruicao);
    }

    void OnTriggerEnter(Collider objetoColisao){
        if(objetoColisao.tag == "Jogador"){
            objetoColisao.GetComponent<ControlaJogador>().CurarVida(quantidadeCura);
            Destroy(gameObject);
        }
    }
}
