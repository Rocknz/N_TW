using UnityEngine;
using System.Collections;

public class UpgradeText : MonoBehaviour {
	public static int UC = 3;
	public static int UMax = 21;
	// UPGRADE [i,j,k], i = Kind, j = Level, z = (0 = atk,1 = def,2 = int,3 = hpmax,4 = xienmax; 
	public int[,,] UPGRADE = new int[UC,UMax,5];
	public int[,] UPGRADECOST = new int[UC,UMax];
	void SettingUPGRADE(){
		int i,j,k;
		for(i=0;i<UC;i++){
			for(j=0;j<UMax;j++){
				for(k=0;k<5;k++){
					UPGRADE[i,j,k] = 0;
				}
				UPGRADECOST[i,j] = 0;
			}
		}
		for(j=1;j<UMax;j++){
			UPGRADE[0,j,0] = j * 1;
			UPGRADE[0,j,2] = j * 1;

			UPGRADE[1,j,1] = j * 1;
			UPGRADE[1,j,3] = j * 1;

			UPGRADE[2,j,2] = j * 1;
			UPGRADE[2,j,3] = j * 1;
			UPGRADE[2,j,4] = j * 1;


			UPGRADECOST[0,j] = j * 10;
			UPGRADECOST[1,j] = j * 10;
			UPGRADECOST[2,j] = j * 10;
		}
	}

	GameObject[] UpgradeLevel = new GameObject[UC];  
	GameObject[] UpgradeCost = new GameObject[UC];
	GameObject[] UpgradeName = new GameObject[UC];
	GameObject Atk,Def,Int,HpMax,XienMax,Coin;
	void Start () {
		SettingUPGRADE();
		UpgradeLevel[0] = GameObject.Find ("U_Fencing_Gap");
		UpgradeLevel[1] = GameObject.Find ("U_Health_Gap");
		UpgradeLevel[2] = GameObject.Find ("U_Concentration_Gap");
		UpgradeCost[0] = GameObject.Find ("U_Fencing_Cost");
		UpgradeCost[1] = GameObject.Find ("U_Health_Cost");
		UpgradeCost[2] = GameObject.Find ("U_Concentration_Cost");
		UpgradeName[0] = GameObject.Find ("U_Fencing_A");
		UpgradeName[1] = GameObject.Find ("U_Health_A");
		UpgradeName[2] = GameObject.Find ("U_Concentration_A");

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
		for(i=0;i<UC;i++){
			UpgradeLevel[i].GetComponent<tk2dTextMesh>().text = UserData.Instance.UpgradeLevel[i].ToString ();
			UpgradeLevel[i].GetComponent<tk2dTextMesh>().Commit ();
		}
		for(i=0;i<UC;i++){
			UpgradeCost[i].GetComponent<tk2dTextMesh>().text = UPGRADECOST[i,UserData.Instance.UpgradeLevel[i]].ToString ();
			UpgradeCost[i].GetComponent<tk2dTextMesh>().Commit ();
		}

		// NOT depends on UC;
		UpgradeName[0].GetComponent<tk2dTextMesh>().color = new Color(255,0,0);
		UpgradeName[0].GetComponent<tk2dTextMesh>().Commit();
		UpgradeLevel[0].GetComponent<tk2dTextMesh>().color = new Color(255,0,0);
		UpgradeLevel[0].GetComponent<tk2dTextMesh>().Commit();
		
		UpgradeName[1].GetComponent<tk2dTextMesh>().color = new Color(0,255,0);
		UpgradeName[1].GetComponent<tk2dTextMesh>().Commit();
		UpgradeLevel[1].GetComponent<tk2dTextMesh>().color = new Color(0,255,0);
		UpgradeLevel[1].GetComponent<tk2dTextMesh>().Commit();
		
		UpgradeName[2].GetComponent<tk2dTextMesh>().color = new Color(0,0,255);
		UpgradeName[2].GetComponent<tk2dTextMesh>().Commit();
		UpgradeLevel[2].GetComponent<tk2dTextMesh>().color = new Color(0,0,255);
		UpgradeLevel[2].GetComponent<tk2dTextMesh>().Commit();

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

		
		UpgradeName[n].GetComponent<tk2dTextMesh>().color = new Color(255,255,255);
		UpgradeName[n].GetComponent<tk2dTextMesh>().Commit();
		UpgradeLevel[n].GetComponent<tk2dTextMesh>().color = new Color(255,255,255);
		UpgradeLevel[n].GetComponent<tk2dTextMesh>().Commit();
	}
	public void NotSelect(int n){
		settingText ();
	}
	public void Up(int n){
		if(n != -1 && UserData.Instance.UpgradeLevel[n] != UMax-1){
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
