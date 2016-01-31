using UnityEngine;
using System.Collections;

public class Objects : MonoBehaviour {


    public bool fairyDust = false;
    public bool Flower = false;
    public bool Play = false;
    public bool Pocion = false;




    public void addObject(string nombre)
    {
        if(nombre == "FairyDust" )
        {
            fairyDust = true;
        }
        else if (nombre == "Flower")
        {
            Flower = true;
        }
        else if (nombre == "Play")
        {
            Play = true;

        }
        else if (nombre == "Pocion")
        {
            Pocion = true;

        }
        else
        {
            print("nombre de objeto erroneo");
        }
    }
}
