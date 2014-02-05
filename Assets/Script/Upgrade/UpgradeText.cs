using UnityEngine;
using System.Collections;

public class UpgradeText : MonoBehaviour {

	// UPGRADE [i,j,k], i = Kind, j = Level, z = (0 = atk,1 = def,2 = int,3 = hpmax,4 = xienmax; 
	public int[,,] UPGRADE = new int[4,11,5];
	public int[,] UPGRADECOST = new int[4,11];
	void SettingUPGRADE(){
		int i,j,k;
		for(i=0;i<4;i++){
			for(j=0;j<11;j++){
				for(k=0;k<5;k++){
					UPGRADE[i,j,k] = 0;
				}
				UPGRADECOST[i,j] = 0;
			}
		}
		for(j=1;j<=10;j++){
			UPGRADE[0,j,0] = j * 1;
			UPGRADE[0,j,2] = j * 1;
			UPGRADE[1,j,1] = j * 1;
			UPGRADE[1,j,3] = j * 1;
			UPGRADE[1,j,4] = j * 1;

			UPGRADECOST[0,j] = j * 20;
			UPGRADECOST[1,j] = j * 20;
		}
	}

	GameObject[] UpgradeLevel = new GameObject[4];  
	GameObject[] UpgradeCost = new GameObject[4];
	GameObject Atk,Def,Int,HpMax,XienMax,Coin;
	void Start () {
		UserData.Instance.Coin = 100;

		SettingUPGRADE();
		UpgradeLevel[0] = GameObject.Find ("U_Fencing_Gap");
		UpgradeLevel[1] = GameObject.Find ("U_Healthing_Gap");
		UpgradeCost[0] = GameObject.Find ("U_Fencing_Cost");
		UpgradeCost[1] = GameObject.Find ("U_Healthing_Cost");

		Atk = GameObject.Find ("AtkGap");
		Def = GameObject.Find ("DefGap");
		Int = GameObject.Find ("IntGap");
		HpMax = GameObject.Find ("HpMaxGap");
		XienMax = GameObject.Find ("XienMaxGap");
		Coin = GameObject.Find ("CoinGap");
		settingText();
	}
	public void settingText(){
		Atk.GetComponent<tk2dTextMesh>().text = UserData.Instance.Atk.ToString();
		Atk.GetComponent<tk2dTextMesh>().Commit ();
		Def.GetComponent<tk2dTextMesh>().text = UserData.Instance.Def.ToString();
		Def.GetComponent<tk2dTextMesh>().Commit ();
		Int.GetComponent<tk2dTextMesh>().text = UserData.Instance.Int.ToString();
		Int.GetComponent<tk2dTextMesh>().Commit ();
		HpMax.GetComponent<tk2dTextMesh>().text = UserData.Instance.HpMax.ToString();
		HpMax.GetComponent<tk2dTextMesh>().Commit ();
		XienMax.GetComponent<tk2dTextMesh>().text = UserData.Instance.XienMax.ToString();
		XienMax.GetComponent<tk2dTextMesh>().Commit ();
		Coin.GetComponent<tk2dTextMesh>().text = UserData.Instance.Coin.ToString();
		Coin.GetComponent<tk2dTextMesh>().Commit ();
		int i;
		for(i=0;i<2;i++){
			UpgradeLevel[i].GetComponent<tk2dTextMesh>().text = UserData.Instance.UpgradeLevel[i].ToString ();
			UpgradeLevel[i].GetComponent<tk2dTextMesh>().Commit ();
		}
		for(i=0;i<2;i++){
			UpgradeCost[i].GetComponent<tk2dTextMesh>().text = UPGRADECOST[i,UserData.Instance.UpgradeLevel[i]].ToString ();
			UpgradeCost[i].GetComponent<tk2dTextMesh>().Commit ();
		}
	}
	public void Select(int n){
		Atk.GetComponent<tk2dTextMesh>().text += "(+" + UPGRADE[n,UserData.Instance.UpgradeLevel[n],0].ToString()  + ")";
		Atk.GetComponent<tk2dTextMesh>().Commit ();
		Def.GetComponent<tk2dTextMesh>().text += "(+" + UPGRADE[n,UserData.Instance.UpgradeLevel[n],1].ToString()  + ")";
		Def.GetComponent<tk2dTextMesh>().Commit ();
		Int.GetComponent<tk2dTextMesh>().text += "(+" + UPGRADE[n,UserData.Instance.UpgradeLevel[n],2].ToString()  + ")";
		Int.GetComponent<tk2dTextMesh>().Commit ();
		HpMax.GetComponent<tk2dTextMesh>().text += "(+" + UPGRADE[n,UserData.Instance.UpgradeLevel[n],3].ToString()  + ")";
		HpMax.GetComponent<tk2dTextMesh>().Commit ();
		XienMax.GetComponent<tk2dTextMesh>().text += "(+" + UPGRADE[n,UserData.Instance.UpgradeLevel[n],4].ToString()  + ")";
		XienMax.GetComponent<tk2dTextMesh>().Commit ();
	}
	public void NotSelect(int n){
		settingText ();
	}
	public void Up(int n){
		if(n != -1){
			if(UserData.Instance.Coin >= UPGRADECOST[n,UserData.Instance.UpgradeLevel[n]]){
				UserData.Instance.Coin -= UPGRADECOST[n,UserData.Instance.UpgradeLevel[n]];
				UserData.Instance.Atk += UPGRADE[n,UserData.Instance.UpgradeLevel[n],0];
				UserData.Instance.Def += UPGRADE[n,UserData.Instance.UpgradeLevel[n],1];
				UserData.Instance.Int += UPGRADE[n,UserData.Instance.UpgradeLevel[n],2];
				UserData.Instance.HpMax += UPGRADE[n,UserData.Instance.UpgradeLevel[n],3];
				UserData.Instance.XienMax += UPGRADE[n,UserData.Instance.UpgradeLevel[n],4];
				UserData.Instance.UpgradeLevel[n] ++;
			}
		}
		settingText();
	}
}
