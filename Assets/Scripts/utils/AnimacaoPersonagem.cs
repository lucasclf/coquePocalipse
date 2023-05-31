using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimacaoPersonagem : MonoBehaviour
{
    private Animator animatorPersonagem;
    // Start is called before the first frame update
    void Awake()
    {
        animatorPersonagem = GetComponent<Animator>();
    }

    public void Atacar(bool estado){
        animatorPersonagem.SetBool("atacando", estado);
    }
    
    public void Movimentar(float valorMovimento){
        animatorPersonagem.SetFloat("Movendo", valorMovimento);
    }
}
