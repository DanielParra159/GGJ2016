using UnityEngine;
using System.Collections;

public class MovablePuzzle : MonoBehaviour {

    public int triggerNeeded = 2;
    protected int currentTriggersActived = 0;

    private bool solved = false;
    
    public Door[] doorsToOpen;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void complete()
    {
        for (int i = 0; i < doorsToOpen.Length; ++i)
        {
            doorsToOpen[i].open();
        }
        gameObject.SetActive(false);
    }
    public void activeTrigger()
    {
        if (++currentTriggersActived == triggerNeeded)
        {
            //completado
            complete();
        }
    }
}
