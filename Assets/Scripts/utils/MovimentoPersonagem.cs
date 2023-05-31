using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentoPersonagem : MonoBehaviour
{
    private Rigidbody rigidbodyPersonagem;
    
    void Awake(){
        rigidbodyPersonagem = GetComponent<Rigidbody>();
    }

    public void Movimentar(Vector3 direcao, float velocidade){
        rigidbodyPersonagem.MovePosition(
                rigidbodyPersonagem.position + direcao.normalized * velocidade * Time.deltaTime
            );
    }

    public void Rotacionar(Vector3 direcao){
        Quaternion rotacao = Quaternion.LookRotation(direcao);
        rigidbodyPersonagem.MoveRotation(rotacao);
    }
}
