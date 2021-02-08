using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetBananas : MonoBehaviour
{
    public IKHandController hand;
    private void OnTriggerEnter(Collider other)
    {
        if(MainManager.IsBananaPickedUp)
        {
            Debug.Log(1);
            hand.inHand.parent = null;
            Destroy(MainManager.CurrentBanana);
            MainManager.IsBananaPickedUp = false;
            MainManager.BananasCount++;
        }

    }
}
