using UnityEngine;
using System.Collections;

public class CollisionWall : MonoBehaviour {

    public CollisionPuzzle parent;

    public Color correctColor = Color.green;
    public Color normalColor = Color.white;

    public AudioClip m_onCollision;
    public AudioClip m_onWrong;

	// Use this for initialization
	void Start () {
	
	}
	
    public void Reset()
    {
        gameObject.transform.Find("Sprite").GetComponent<Renderer>().material.color = normalColor;
    }

	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if (!this.enabled) return;
        if (parent.onEnter(this.gameObject))
        {
            gameObject.transform.Find("Sprite").GetComponent<Renderer>().material.color = correctColor;
            if (m_onCollision != null)
            {
                SoundManager.instance.PlaySingle(m_onCollision);
            }
        }
        else if (m_onWrong != null)
        {
            SoundManager.instance.PlaySingle(m_onWrong);
        }
    }
}
