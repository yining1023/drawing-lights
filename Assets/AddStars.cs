using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AddStars : MonoBehaviour {

	public Transform starPrefab;
	public Transform linePrefab;

	public class Line {
		public Vector3	start, end;
		public Transform prefab;
		public GameObject line_gameObject;
		public bool drawn;


		public Line(Vector3 _start, Vector3 _end, Transform _prefab){
			start = _start;
			end = _end;
			prefab = _prefab;
			drawn = false;
		}

		public void draw() {

			if (!(float.IsNaN (start.x) || float.IsNaN (start.y) || float.IsNaN (start.z)  || float.IsNaN (end.x) || float.IsNaN (end.y) || float.IsNaN (end.x))) {
				Transform g2 = (Transform)Instantiate (prefab, start, Quaternion.identity);
				line_gameObject = g2.gameObject;

				if (!(line_gameObject == null)) {
					Vector3 direction = end - start;
			
					line_gameObject.transform.position += direction / 2; //offset
					line_gameObject.transform.rotation = Quaternion.LookRotation (direction);
					line_gameObject.transform.localScale = new Vector4 (1, 1, direction.magnitude);
				}
				drawn = true;

			}
		}
	}


	GameObject[] allStars, allLines, allDestroyed;
	List<Line> lines = new List<Line>();

	// Use this for initialization
	void Start () {
		Physics.gravity *= 3;
	}
	
	// Update is called once per frame
	void Update () {

		allStars = GameObject.FindGameObjectsWithTag ("star");
		allLines = GameObject.FindGameObjectsWithTag ("line");
		allDestroyed = GameObject.FindGameObjectsWithTag ("destroyed");

		foreach (Line l in lines) {
			if(!l.drawn)
				l.draw();
		}


		var mousePos = Input.mousePosition;
		mousePos.z = Random.Range (50, 300);

		if (Input.GetMouseButtonDown (0)) {
			Vector3 star_pos = Camera.main.ScreenToWorldPoint (mousePos);
			if(!( float.IsNaN(star_pos.x) || float.IsNaN(star_pos.y) || float.IsNaN(star_pos.z)))
			{
				//Instantiate function returns a Transform Object that contains the gameobject that was just created!
				Transform g = (Transform)Instantiate (starPrefab, star_pos, Quaternion.identity);

				//Get the game object that was just created above ^ and store it in a variable called star
				GameObject star = g.gameObject;

				Color c = new Color (Random.Range (0.0f, 1.0f), Random.Range (0.0f, 1.0f), Random.Range (0.0f, 1.0f));
				star.GetComponent<Renderer> ().material.SetColor ("_Color", c);
				star.GetComponent<Renderer> ().material.SetColor ("_EmissionColor", c);

				if (allStars.Length > 0)
					lines.Add (new Line (star_pos, allStars [allStars.Length - 1].transform.position, linePrefab));
			}
		}


		if(Input.GetKeyDown(KeyCode.X)){
			foreach(GameObject s in allStars)
				Destroy(s);
			foreach(GameObject l in allLines)
				Destroy(l);
			foreach(GameObject d in allDestroyed)
				Destroy(d);
			allLines = null;
			allStars = null;
			lines.Clear();
		}



	}
}
