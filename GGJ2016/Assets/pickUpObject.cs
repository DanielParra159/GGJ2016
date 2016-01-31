using UnityEngine;
using System.Collections;

public class pickUpObject : MonoBehaviour {

    public string objectName;

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            print("pickup");
            other.gameObject.GetComponent<Objects>().addObject(objectName);
          
            Destroy(gameObject);
        }
    }
}
