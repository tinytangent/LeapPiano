using UnityEngine;
using System.Collections;

public class LeapMotionDebugBehaviour : MonoBehaviour
{

    protected Leap.Controller leapController;
    protected Leap.Frame lastFrame;
    public bool isConnected;
    public int handsCount;
    public float confidenceLeft;
    public float confidenceRight;
    public Leap.Vector leftNormal;
    public Leap.Vector rightNormal;
    public Leap.Vector leftPos;
    public Leap.Vector rightPos;
    public int leapFrame;
    public GameObject handIndicator;

    // Use this for initialization
    void Start()
    {
        leapController = new Leap.Controller();
    }

    // Update is called once per frame
    void Update()
    {
        isConnected = leapController.IsConnected;
        confidenceLeft = confidenceRight = 0.0f;
        if (!isConnected) return;
        lastFrame = leapController.Frame();
        handsCount = lastFrame.Hands.Count;
        if (handsCount != 2) return;
        for(int i = 0; i < 2; i++)
        {
            if(lastFrame.Hands[i].IsRight)
            {
                confidenceRight = lastFrame.Hands[i].Confidence;
                rightNormal = lastFrame.Hands[i].PalmNormal;
                rightPos = lastFrame.Hands[i].PalmPosition;
            }
            else
            {
                confidenceLeft = lastFrame.Hands[i].Confidence;
                leftNormal = lastFrame.Hands[i].PalmNormal;
                leftPos = lastFrame.Hands[i].PalmPosition;
            }
        }
        if(handIndicator != null)
        {
            Transform tr = handIndicator.GetComponent<Transform>();
            tr.position = new Vector3(leftPos.x/100.0f, leftPos.y/100.0f, leftPos.z/100.0f);
        }
    }

    void OnDestroy()
    {
        if(leapController != null)
        {
            leapController.StopConnection();
        }
    }
}
