using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pokeball : MonoBehaviour
{
    Animator anim;
    Trainer train;
    public GameObject lucario;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        train = GetComponentInParent<Trainer>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Throw() {
        anim.enabled = true;
    }

    public void Spawn()
    {
        Instantiate(lucario, lucario.transform);
    }
}
