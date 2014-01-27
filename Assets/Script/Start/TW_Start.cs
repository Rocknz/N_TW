using UnityEngine;
using System.Collections;
public class TW_Start : MonoBehaviour {
	GameObject NewGame;
	void Start () {
		NewGame = GameObject.Find("NewGame");	
	}
	void Update () {
		if(Input.GetButtonDown ("Fire1")) {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit = new RaycastHit();
			if(Physics.Raycast(ray, out hit)) {
				if(NewGame.transform == hit.transform){
					Application.LoadLevel(2);
				}
			}
		}
	}
}