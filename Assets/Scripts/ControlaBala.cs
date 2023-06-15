using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ControlaBala : MonoBehaviour
{
    public float Velocidade = 20;
    public AudioClip SomMorteZumbi;
    private Rigidbody rigidbodyBala;
    // Update is called once per frame
    void Start(){
        rigidbodyBala = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        rigidbodyBala.MovePosition(rigidbodyBala.position + transform.forward * Velocidade * Time.deltaTime);
    }

    void OnTriggerEnter(Collider objetoDeColisao){
        Quaternion rotacaoOpostaBala = Quaternion.LookRotation(-transform.forward);
        switch(objetoDeColisao.tag){
            case "Inimigo":
                ControlaZumbi zumbi = objetoDeColisao.GetComponent<ControlaZumbi>();
                zumbi.TomarDano(1);
                zumbi.GerarSangue(transform.position, rotacaoOpostaBala);
                break;
            case "Chefe":
                ControlaChefe chefe = objetoDeColisao.GetComponent<ControlaChefe>();
                chefe.TomarDano(1);
                chefe.GerarSangue(transform.position, rotacaoOpostaBala);
                break;
        }
        if(objetoDeColisao.tag == "Inimigo"){
            
        }
        Destroy(gameObject);
    }
}
