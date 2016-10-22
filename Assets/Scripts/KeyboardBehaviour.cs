using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class KeyboardBehaviour : MonoBehaviour
{

    public GameObject PitchIntervalTemplate;

    protected float left;
    protected float right;

    protected List<GameObject> pitchIntervalObjects = new List<GameObject>();

    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            GameObject newObject = Instantiate(PitchIntervalTemplate);
            newObject.transform.localPosition = new Vector3(i * 7 * 0.26f, 0, 0);
            //newObject.transform.localScale = new Vector3(0.02f, 0.1f, 0.1f);
            newObject.transform.parent = this.transform;
            newObject.name = "" + i;
            pitchIntervalObjects.Add(newObject);
        }
        PitchIntervalTemplate.SetActive(false);
        setScale(-10.0f, 10.0f, 4);
    }

    void setScale(float left, float right, int numPitchInterval)
    {
        this.left = left;
        this.right = right;
        transform.localPosition = new Vector3((left + right) / 2, 0, 0);
        float pitchIntervalWidth = (right - left) / 4;
        for(int i = 0; i < 4; i++)
        {
            float intervalLeft = left + pitchIntervalWidth * i;
            float intervalRight = intervalLeft + pitchIntervalWidth;
            pitchIntervalObjects[i].transform.localPosition = 
                new Vector3((intervalLeft + intervalRight) / 2, 0, 0);
            pitchIntervalObjects[i].GetComponent<KeyboardPitchIntervalBehaviour>().
                SetScale(intervalLeft, intervalRight, 0.1f, 0.9f);
        }
    }

    public void SetPressedPositions(List<float> positions)
    {
        for (int i = 0; i < 4; i++)
        {
            pitchIntervalObjects[i].GetComponent<KeyboardPitchIntervalBehaviour>().SetPressedPositions(positions);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
