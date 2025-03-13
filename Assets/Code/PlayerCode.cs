using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCode : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Final"))
        {
            GameManager.instance.ShowVictoryCanvasAndStopExplosion();
        }
    }
}
