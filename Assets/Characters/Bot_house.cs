using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot_house : MonoBehaviour
{
    public AudioSource voice;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "Player")
        {
            voice.Play();
            animator.Play("Mask");
        }
    }
}
