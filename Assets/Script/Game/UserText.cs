using UnityEngine;

public class UserText : MonoBehaviour {
	GameObject Atk;
	GameObject Int;
	GameObject Def;
	GameObject Hp;
	GameObject Xien;
	GameObject Coin;
	GameObject DMG;
	void Start(){
		Atk = GameObject.Find ("Atk Gap");
		Int = GameObject.Find ("Int Gap");
		Def = GameObject.Find ("Def Gap");
		Hp = GameObject.Find ("Hp Gap");
		Xien = GameObject.Find ("Xien Gap");
		Coin = GameObject.Find ("Coin Gap");
		DMG = GameObject.Find ("DMG Gap");
		setStat();
	}
	public void setStat(){
		Atk.GetComponent<tk2dTextMesh>().text = UserData.Instance.Atk.ToString();
		Atk.GetComponent<tk2dTextMesh>().Commit();
		Int.GetComponent<tk2dTextMesh>().text = UserData.Instance.Int.ToString();
		Int.GetComponent<tk2dTextMesh>().Commit();
		Def.GetComponent<tk2dTextMesh>().text = UserData.Instance.Def.ToString();
		Def.GetComponent<tk2dTextMesh>().Commit();
		Hp.GetComponent<tk2dTextMesh>().text = UserData.Instance.Hp.ToString()+"/"+UserData.Instance.HpMax.ToString();
		Hp.GetComponent<tk2dTextMesh>().Commit();
		Xien.GetComponent<tk2dTextMesh>().text = UserData.Instance.Xien.ToString()+"/"+UserData.Instance.XienMax.ToString();
		Xien.GetComponent<tk2dTextMesh>().Commit();
		Coin.GetComponent<tk2dTextMesh>().text = UserData.Instance.Coin.ToString();
		Coin.GetComponent<tk2dTextMesh>().Commit();
	}
	public void setDMG(int Damage){
		DMG.GetComponent<tk2dTextMesh>().text = Damage.ToString();
		DMG.GetComponent<tk2dTextMesh>().Commit();
	}
	void settingUserStatus(){
		// ItemData
//		UserData.Instance.Atk = ItemData.Instance.Items[0][UserData.Instance.SwordLevel]["hack_damage"]+
//								ItemData.Instance.Items[7][UserData.Instance.HeadLevel]["hack_damage"];
//
//		UserData.Instance.Def = ItemData.Instance.Items[3][UserData.Instance.HelmetLevel]["def"]+
//								ItemData.Instance.Items[8][UserData.Instance.BodyLevel]["def"];
//
//		UserData.Instance.Int = ItemData.Instance.Items[0][UserData.Instance.SwordLevel]["int_damage"]+
//								ItemData.Instance.Items[7][UserData.Instance.HeadLevel]["int_damage"];
//
//		UserData.Instance.Hp = ItemData.Instance.Items[3][UserData.Instance.HelmetLevel]["HP"]+
//								ItemData.Instance.Items[8][UserData.Instance.BodyLevel]["HP"];

		setStat ();
	}
	void Update(){
		settingUserStatus();
	}
}
