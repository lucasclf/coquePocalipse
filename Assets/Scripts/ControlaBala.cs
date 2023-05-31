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
        if(objetoDeColisao.tag == "Inimigo"){
            objetoDeColisao.GetComponent<ControlaZumbi>().TomarDano(1);
        }
        Destroy(gameObject);
    }
}
