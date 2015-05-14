using UnityEngine;
using System.Collections;

public class SelfDestruct : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (KeyCode.C)) {
			gameObject.AddComponent<Rigidbody> ();
			gameObject.tag = "destroyed";
		}
	
		if(transform.position.y < -1500f)
			Destroy(this.gameObject);
	}
}
