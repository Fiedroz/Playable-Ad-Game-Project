using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionWallTrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Friendly") {
            other.gameObject.GetComponent<FriendlyBehavior>().movingToTower = true;
        }
    }
}
