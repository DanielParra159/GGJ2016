using UnityEngine;
using System.Collections;

public class TalkToGoddes : MonoBehaviour {


    public Door doorRight;
    public Door doorLeft;


    void OnTriggerEnter(Collider other)
    {
        print("hablar");
        
        if (other.tag == "Player")
        {
            print("hablar");
            gameObject.GetComponent<Animator>().Play("ConversacionConBruja");

        }
    }

    void openDoors()
    {
        doorRight.open();
        doorLeft.open();
    }
}
