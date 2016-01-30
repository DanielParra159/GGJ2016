using UnityEngine;
using System.Collections;

public class ColorWall : MonoBehaviour {

    [System.Serializable]
    public class ColorRange
    {
        public float distance;
        public Color color = Color.white;
    }
    public ColorRange [] ranges;

    protected Color currentColor;

    protected Renderer renderer;

    public AudioClip m_onCollision;

	// Use this for initialization
	void Start () {
        renderer = GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void collision(float distance)
    {
        if (m_onCollision != null)
        {
            SoundManager.instance.PlaySingle(m_onCollision);
        }
        for (int i = 0; i <ranges.Length; ++i)
        {
            if (distance < ranges[i].distance )
            {
                renderer.material.color = ranges[i].color;
                currentColor = ranges[i].color;
                break;
            }
        }
    }

    public Color getColor()
    {
        return currentColor;
    }
}
