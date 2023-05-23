using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaCamera : MonoBehaviour
{
    public GameObject Jogador;
    private Vector3 distCameraJogador;
    // Start is called before the first frame update
    void Start()
    {
        distCameraJogador = transform.position - Jogador.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Jogador.transform.position + distCameraJogador ;
    }
}
