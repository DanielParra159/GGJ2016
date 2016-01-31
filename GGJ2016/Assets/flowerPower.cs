using UnityEngine;
using System.Collections;

public class flowerPower : MonoBehaviour {


    public GameObject door;


    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Destroy(door);

        }
    }
}
