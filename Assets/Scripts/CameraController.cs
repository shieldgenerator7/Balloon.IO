using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public GameObject player;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (player != null)
        {
            transform.position = player.transform.position + new Vector3(0, 0, -10);//to keep it focused on the player but still above the player
        }
	}
}
