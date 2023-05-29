using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlaJogador : MonoBehaviour
{
    public float Velocidade = 10;
    public LayerMask MascaraChao;
    public GameObject TextoGameOver;
    private Vector3 direcao;
    private Rigidbody rigidbodyJogador;
    private Animator animatorJogador;
    public int Vida = 100;
    public ControlaInterface ScriptInterface; 
    public AudioClip SomDano;
    public AudioClip SomGameOver;

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
        if(Vida <= 0 && Input.GetButtonDown("Fire1")){
            SceneManager.LoadScene("Menu");
            Time.timeScale = 1;
        }
    }

    public void TomarDano(int dano){
        Vida -= dano;
        ScriptInterface.AtualizarSlideVidaJogador();

        ControlaAudio.instanciaControleAudio.PlayOneShot(SomDano);
        if(Vida <= 0){
            Time.timeScale = 0; 
            AudioSource audio = ControlaAudio.instanciaControleAudio.GetComponent<AudioSource>();
            audio.clip = SomGameOver;
            audio.Play();
            TextoGameOver.SetActive(true);
        }
    }
}
