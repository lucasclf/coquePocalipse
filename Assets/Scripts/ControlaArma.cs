using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaArma : MonoBehaviour
{
    public GameObject Bala;
    public GameObject CanoDaArma;
    public float Delta = 0.5F;
    public AudioClip SomTiro;
    private float proximoTiro = 10F;
    private float tempoInicial = 0.0F;


    // Start is called before the first frame update
    void Start()
    {
    }

    void FixedUpdate(){
        tempoInicial = tempoInicial + Delta;
    }
    // Update is called once per frame
    void Update(){
        if(Input.GetButton("Fire1")){
            if(tempoInicial > proximoTiro) {
                ControlaAudio.InstanciaControleAudio.PlayOneShot(SomTiro);
                atirar();
            }
        }
    }

    void atirar(){
        Instantiate(Bala, CanoDaArma.transform.position, CanoDaArma.transform.rotation);
        tempoInicial = 0.0F;
    }
}
