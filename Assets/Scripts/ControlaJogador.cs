using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlaJogador : MonoBehaviour
{
    public float Velocidade = 10;
    public LayerMask MascaraChao;
    public GameObject TextoGameOver;
    public bool vivo = true;
    private Vector3 direcao;
    private Rigidbody rigidbodyJogador;
    private Animator animatorJogador;

    void Start(){
        rigidbodyJogador = GetComponent<Rigidbody>();
        animatorJogador = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update(){
        float eixoX = Input.GetAxis("Horizontal");
        float eixoZ = Input.GetAxis("Vertical");

        direcao = new Vector3(eixoX, 0, eixoZ);

        reiniciaJogo();
    }

    void FixedUpdate(){
        movimento();
        rotacao();
        
    }
    
    void movimento(){

        //rigidbodyJogador.MovePosition
        //(rigidbodyJogador.position + direcao * Velocidade * Time.deltaTime);

        rigidbodyJogador.velocity = direcao.normalized * Velocidade;

        if(direcao != Vector3.zero){
            animatorJogador.SetBool("Movendo", true);
        } else {
            animatorJogador.SetBool("Movendo", false);
        }
    }


    void rotacao(){
        Ray raio = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(raio.origin, raio.direction *100, Color.red);

        RaycastHit impacto;

        if(Physics.Raycast(raio, out impacto, 100, MascaraChao)){
            Vector3 posicaoMiraJogador = impacto.point - transform.position;
            posicaoMiraJogador.y = transform.position.y;

            Quaternion novaRotacao = Quaternion.LookRotation(posicaoMiraJogador);
            rigidbodyJogador.MoveRotation(novaRotacao); 
        };
    }

    void reiniciaJogo(){
        if(vivo == false && Input.GetButtonDown("Fire1")){
            SceneManager.LoadScene("Menu");
            Time.timeScale = 1;
        }
    }
}
