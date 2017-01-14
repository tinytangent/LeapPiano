using UnityEngine;
using System.Collections;

public class SphereBehaviour : MonoBehaviour {

    public GameObject Cam;

    public int startCount;
    public static int startMax = 130;

    public int minusStep = 1;

	// Use this for initialization
	void Start () {
        startCount = 0;
        print(transform.position.x + " " + transform.position.y + " " + transform.position.z);
        print(GetComponent<SphereCollider>().radius);
        //GetComponent<Renderer>().material.shader. .EnableKeyword("_DETAIL_MULX2");
    }
	
    public int getStartCount ()
    {
        return startCount;
    }

    public void countToStart ()
    {
        startCount++;
        print("Counting to start!" + startCount);
    }

	// Update is called once per frame
	void Update () {
        if (startCount > startMax)
        {
            Cam.GetComponent<CameraBehaviour>().setAngle(CameraBehaviour.PIANO);
            minusStep = 5;
            startCount = startMax;
        } else
        {
            if (startCount < minusStep)
            {
                startCount = 0;
                minusStep = 1;
            }
            else
                startCount -= minusStep;
        }
        //GetComponent<Renderer>().material.color = new Color(255f, 255f-(255*startCount/startMax), 255f, 70f);
	}
}
