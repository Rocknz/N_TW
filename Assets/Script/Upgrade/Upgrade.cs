using UnityEngine;
using System.Collections;

public class Upgrade : MonoBehaviour {
	GameObject Back;
	GameObject[] Click = new GameObject[UpgradeText.UC];
	GameObject DoUpgrade;
	
	// Use this for initialization
	void Start () {
		Back = GameObject.Find ("Back");
		Click[0] = GameObject.Find ("U_Fencing");
		Click[1] = GameObject.Find ("U_Health");
		Click[2] = GameObject.Find ("U_Concentration");
		DoUpgrade = GameObject.Find ("DoUpgrade");
	}

	// Update is called once per frame
	int Selected = -1;
	public void NowSelected(int n){
		if(n != Selected){
			if(Selected != -1){
				this.GetComponent<UpgradeText>().NotSelect (Selected);
				Click[Selected].transform.localScale = new Vector3(30,30,0.1f);
			}
			Selected = n;
			this.GetComponent<UpgradeText>().Select (Selected);
			Click[Selected].transform.localScale = new Vector3(40,40,0.1f);
		}
		else if(n == Selected){
			this.GetComponent<UpgradeText>().NotSelect(Selected);
			Click[Selected].transform.localScale = new Vector3(30,30,0.1f);
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
				else if(DoUpgrade.transform == hit.transform){
					this.GetComponent<UpgradeText>().Up (Selected);
					Click[Selected].transform.localScale = new Vector3(30,30,0.1f);
					Selected = -1;
				}
				else{
					int i;
					for(i=0;i<UpgradeText.UC;i++){
						if(Click[i].transform == hit.transform){
							NowSelected (i);
						}
					}
				}
			}
		}
	}
}
