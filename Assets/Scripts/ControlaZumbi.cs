using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaZumbi : MonoBehaviour{
    public GameObject Jogador;
    public float Velocidade = 5;
    private Rigidbody rigidbodyZumbi;
    private Animator animatorZumbi;
    
    // Start is called before the first frame update
    void Start(){
        Jogador = GameObject.FindWithTag("Jogador");
        int seedTipoZumbi = Random.Range(1, 28);
        transform.GetChild(seedTipoZumbi).gameObject.SetActive(true);
        rigidbodyZumbi = GetComponent<Rigidbody>();
        animatorZumbi = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update(){
        
    }

    void FixedUpdate(){
        float distancia = Vector3.Distance(transform.position, Jogador.transform.position);
        Vector3 direcao = Jogador.transform.position - transform.position;
        Quaternion rotacao = Quaternion.LookRotation(direcao);

        rigidbodyZumbi.MoveRotation(rotacao);

        if(distancia > 2.5){
            rigidbodyZumbi.MovePosition(
                rigidbodyZumbi.position + direcao.normalized * Velocidade * Time.deltaTime
            );
            
            animatorZumbi.SetBool("atacando", false);
        } else{
            animatorZumbi.SetBool("atacando", true);
        }
    }

    void AtacaJogador(){
        Time.timeScale = 0;
        Jogador.GetComponent<ControlaJogador>().TextoGameOver.SetActive(true); 
        Jogador.GetComponent<ControlaJogador>().vivo = false;
    }

}
