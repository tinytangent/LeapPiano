using UnityEngine;
using System.Collections;

public class CameraBehaviour : MonoBehaviour {

    public static int START = 0;
    public static int PIANO = 1;

    public static int TRANSSTART = 0;
    public static int INTRANS = 1;
    public static int COMPLETE = 2;

    public static Vector3 startPos = new Vector3(20f, 6.5f, -6.33f);
    public static Vector3 startEuler = new Vector3(0f, 65f, 0f);
    public static Vector3 pianoPos = new Vector3(0f, 6.5f, -6.33f);
    public static Vector3 pianoEuler = new Vector3(45f, 0f, 0f);

    public Vector3 deltaPos;
    public Vector3 deltaEuler;

    public int angle;
    public int inTransition;
    public int steptot = 20;
    public int stepi;

    public void setAngle(int a)
    {
        if (angle == a || inTransition != COMPLETE)
            return;
        angle = a;
        inTransition = TRANSSTART;
    }

	// Use this for initialization
	void Start () {
        angle = START;
        inTransition = COMPLETE;
        deltaEuler = Vector3.zero;
        deltaPos = Vector3.zero;
        transform.position = startPos;
        transform.eulerAngles = startEuler;
	}

    // Update is called once per frame
    void Update()
    {
        if (angle == START)
        {
            if (inTransition == TRANSSTART)
            {
                print("Go to Start!");
                deltaPos = startPos - pianoPos;
                deltaEuler = startEuler - pianoEuler;
                inTransition = INTRANS;
                stepi = 0;
            }
            if (stepi == steptot)
            {
                inTransition = COMPLETE;
                transform.position = startPos;
                transform.eulerAngles = startEuler;
                deltaEuler = Vector3.zero;
                deltaEuler = Vector3.zero;
            }
            if (inTransition == INTRANS)
            {
                transform.position += deltaPos / steptot;
                transform.eulerAngles += deltaEuler / steptot;
                stepi++;
            }
        }
        else
        {
            if (inTransition == TRANSSTART)
            {
                print("Go to Piano!");
                deltaPos = pianoPos - startPos;
                print(deltaPos);
                deltaEuler = pianoEuler - startEuler;
                inTransition = INTRANS;
                stepi = 0;
            }
            if (stepi == steptot)
            {
                inTransition = COMPLETE;
                transform.position = pianoPos;
                transform.eulerAngles = pianoEuler;
                deltaPos = Vector3.zero;
                deltaEuler = Vector3.zero;
            }
            if (inTransition == INTRANS)
            {
                transform.position += deltaPos / steptot;
                transform.eulerAngles += deltaEuler / steptot;
                stepi++;
            }
        }
    }
}
