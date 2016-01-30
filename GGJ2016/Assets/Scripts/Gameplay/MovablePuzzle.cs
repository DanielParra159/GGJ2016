using UnityEngine;
using System.Collections;

public class MovablePuzzle : MonoBehaviour {

    public int triggerNeeded = 2;
    private int currentTriggersActived = 0;

    public Door[] doorsToOpen;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void activeTrigger()
    {
        if (++currentTriggersActived == triggerNeeded)
        {
            //completado
            for (int i = 0; i < doorsToOpen.Length; ++i)
            {
                doorsToOpen[i].open();
            }
            gameObject.SetActive(false);
        }
        

    }
}
