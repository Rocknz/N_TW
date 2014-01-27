using UnityEngine;
using System.Collections;

public class Login : MonoBehaviour {

	// Use this for initialization
	GameObject Logo;
	void Start () {
		Logo = GameObject.Find ("CJCJ");
		Logo.transform.localScale = new Vector3(0,0,1);

		iTween.ScaleTo(Logo, iTween.Hash(
			"x", 0.15f,
			"y", 0.15f,
			"easeType", "easeOutQuad",
			"time", 2.0f,
			"oncomplete","LoadEnd",
			"oncompletetarget",gameObject,
			"oncompleteparams",Logo));
	}
	void LoadEnd(){
		Application.LoadLevel(1);

	}
	// Update is called once per frame
	void Update () {
	
	}
}
