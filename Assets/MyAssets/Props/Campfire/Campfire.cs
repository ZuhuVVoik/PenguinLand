using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Campfire : MonoBehaviour
{
    Penguin1Move player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Penguin1Move>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Player")
        {
            MainManager.IsInWarmPlace = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            MainManager.IsInWarmPlace = false;
        }
    }
}
