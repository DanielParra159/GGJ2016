using UnityEngine;
using System.Collections;

public class pickUpObject : MonoBehaviour {

    public string objectName;

    public AudioClip m_onPickUp;


    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            print("pickup");
            other.gameObject.GetComponent<Objects>().addObject(objectName);
            if (m_onPickUp != null)
            {
                SoundManager.instance.PlaySingle(m_onPickUp);
            }
            Destroy(gameObject);
        }
    }
}
