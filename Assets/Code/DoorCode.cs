using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCode : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("key"))
        {
            GameManager.instance.SetKeyHasBeenSet = true;
            GameManager.instance.ActivateDoorOpening();
            print("AAAAAAAAAAAAAAAA");
        }
    }
}
