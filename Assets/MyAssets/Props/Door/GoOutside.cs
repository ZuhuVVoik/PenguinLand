using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoOutside : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(MainManager.HaveBackpack && MainManager.HaveScarf)
        {
            MainManager.sceneChanger.OpenOutsideScene();
        }
        else
        {
            MainManager.messenger.WriteMessage("Нужно надеть рюкзак и шарфик");
        }
    }
}
