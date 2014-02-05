using UnityEngine;
using System.Collections;

public class Upgrade : MonoBehaviour {
	GameObject Back;
	GameObject Fencing;
	GameObject Healthing;
	GameObject DoUpgrade;
	
	// Use this for initialization
	void Start () {
		Back = GameObject.Find ("Back");
		Fencing = GameObject.Find ("U_Fencing");
		Healthing = GameObject.Find ("U_Healthing");
		DoUpgrade = GameObject.Find ("DoUpgrade");
	}

	// Update is called once per frame
	int Selected = -1;
	public void NowSelected(int n){
		if(n != Selected){
			if(Selected != -1){
				this.GetComponent<UpgradeText>().NotSelect (Selected);
			}
			Selected = n;
			this.GetComponent<UpgradeText>().Select (n);
		}
		else if(n == Selected){
			this.GetComponent<UpgradeText>().NotSelect(n);
			Selected = -1;
		}
	}
	void Update () {
		if(Input.GetButtonDown ("Fire1")) {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit = new RaycastHit();
			if(Physics.Raycast(ray, out hit)) {
				if(Back.transform == hit.transform){
					Application.LoadLevel(2);
				}
				else if(Fencing.transform == hit.transform){
					NowSelected (0);
				}
				else if(Healthing.transform == hit.transform){
					NowSelected (1);
				}
				else if(DoUpgrade.transform == hit.transform){
					this.GetComponent<UpgradeText>().Up (Selected);
				}
			}
		}
	}
}
