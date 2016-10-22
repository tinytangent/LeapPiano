using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LeapMotionDataSourceBehaviour : MonoBehaviour {

    protected Leap.Controller leapController;
    protected Leap.Frame lastFrame;

    // Use this for initialization
    void Start ()
    {
        leapController = new Leap.Controller();
    }
	
	// Update is called once per frame
	void Update () {
        UpdateLeapMotionData();
    }

    void OnDestroy()
    {
        if (leapController != null)
        {
            leapController.StopConnection();
        }
    }

    void UpdateLeapMotionData()
    {
        lastFrame = leapController.Frame();
    }

    public List<Leap.Hand> GetHands()
    {
        return lastFrame.Hands;
    }
}
