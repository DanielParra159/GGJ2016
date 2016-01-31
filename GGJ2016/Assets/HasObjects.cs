using UnityEngine;
using System.Collections;

public class HasObjects : MonoBehaviour {


    public int ObjectsDelivered = 0;

    public bool DeliveredFlower = false;
    public bool DeliveredFairyDust = false;
    public bool DeliveredPlay = false;
    public bool DeliveredPocion = false;

    public GameObject Flower;
    public GameObject FairyDust;
    public GameObject Play;
    public GameObject Pocion;

    public AudioClip bossMusic; 



    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (other.GetComponent<Objects>().Flower && !DeliveredFlower)
            {
                ObjectsDelivered++;
                DeliveredFlower = true;
                Flower.SetActive(true);
            }
             if (other.GetComponent<Objects>().fairyDust && !DeliveredFairyDust)
            {
                ObjectsDelivered++;
                DeliveredFairyDust = true;
                FairyDust.SetActive(true);

            }
             if (other.GetComponent<Objects>().Play && !DeliveredPlay)
            {
                ObjectsDelivered++;
                DeliveredPlay = true;
                Play.SetActive(true);

            }
             if (other.GetComponent<Objects>().Pocion && !DeliveredPocion)
            {
                ObjectsDelivered++;
                DeliveredPocion = true;
                Pocion.SetActive(true);

            }
        }
        if(ObjectsDelivered == 4)
        {
            Destroy(gameObject);
            if (bossMusic != null)
            {
                SoundManager.instance.SetMusic(bossMusic);
            }
        }

    }
}
