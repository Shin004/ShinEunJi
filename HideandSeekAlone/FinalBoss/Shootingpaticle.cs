using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shootingpaticle : MonoBehaviour
{
    public ParticleSystem Ps;

    bool Re = true;
    Vector3 scail;
    void Start()
    {
        scail = gameObject.transform.localScale;
        gameObject.tag = "Untagged";
    }

    void Update()
    {
        if(gameObject.activeSelf&&Re)
        {
            Ps.gameObject.SetActive(true);
            Re = false;
            Ps.Play();
            
            Invoke("Scailing", 1f);
            Invoke("Activefalse", 2.8f);
        }
        if(!gameObject.activeSelf)
        {
            bool Re = true;
            gameObject.tag = "Untagged";
            transform.localScale = new Vector3(transform.localScale.x, 0.1169349f, 1);
        }
    }
    void Scailing()
    {
        gameObject.tag = "Enemy";
        transform.localScale = new Vector3(transform.localScale.x,transform.localScale.y+0.35f,transform.localScale.z);
    }
    void Activefalse()
    {
        gameObject.SetActive(false);
    }
}
