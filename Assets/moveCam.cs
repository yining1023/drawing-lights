using UnityEngine;
using System.Collections;

public class moveCam : MonoBehaviour {
	public float moveSpeed =  5.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += transform.forward*moveSpeed*Input.GetAxis("Vertical")*Time.deltaTime;
		transform.position += transform.right*moveSpeed*Input.GetAxis("Horizontal")*Time.deltaTime;
	}
}
