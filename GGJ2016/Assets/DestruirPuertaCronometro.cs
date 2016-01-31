using UnityEngine;
using System.Collections;

public class DestruirPuertaCronometro : MonoBehaviour {
public GameObject door;


    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Destroy(door);

        }
    }
}
