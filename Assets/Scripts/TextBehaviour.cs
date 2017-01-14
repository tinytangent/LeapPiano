using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextBehaviour : MonoBehaviour {

    public GameObject Sphere;
    public Text text;
    public Color targetColor = new Color(34f/255, 80f/255, 1f);

    public int count;
    public int startMax;

	// Use this for initialization
	void Start () {
        startMax = SphereBehaviour.startMax;
        text = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        int cnt = Sphere.GetComponent<SphereBehaviour>().getStartCount();
        text.color = new Color(1f, 1f, 1f, 1f - ((float)cnt/ startMax));
    }
}
