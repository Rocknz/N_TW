using UnityEngine;

public class ComboLogic : MonoBehaviour {
	public static int COMBO_LEN = 6;
	GameObject[] Combo = new GameObject[COMBO_LEN];
	GameObject[] Check = new GameObject[COMBO_LEN];
	MainLogic.TILETYPE[] type = new MainLogic.TILETYPE[COMBO_LEN];

	int length;
	int complete;
	private Vector3 ComboPosition(int x,int y,int z){
		return new Vector3(x*320.0f,0,z);
	}
	void Start() {
		int i;
		for(i=0;i<COMBO_LEN;i++){
			Combo[i] = new GameObject("Combo"+i.ToString());
			Combo[i].AddComponent<tk2dSprite>();
			Combo[i].transform.parent = this.transform;
			Combo[i].transform.localPosition = ComboPosition(i,0,0);

			Check[i] = new GameObject("Check"+i.ToString());
			Check[i].AddComponent<tk2dSprite>().SetSprite(Tile.datas,"Check");
			Check[i].transform.parent = this.transform;
			Check[i].transform.localPosition = ComboPosition(i,0,-1);
		}
		NewComboSetting();
	}

	public void AddCombo(MainLogic.TILETYPE ntype){
		if(ntype == type[complete]){
//			iTween.ScaleTo(Combo[complete], iTween.Hash(
//				"x", 0.5f,
//				"y", 0.5f,
//				"easeType", "easeOutQuad",
//				"time", 1,
//				"oncomplete","AddComboEnd",
//				"oncompletetarget",gameObject,
//				"oncompleteparams",Combo[complete]));
			iTween.ScaleTo(Check[complete], iTween.Hash(
				"x", 15.0f,
				"y", 15.0f,
				"easeType", "easeOutQuad",
				"time", 0.5,
				"oncomplete","AddComboEnd",
				"oncompletetarget",gameObject,
				"oncompleteparams",Combo[complete]));
			complete ++;
		}
		else{
			NewComboSetting ();
		}
	}
	void AddComboEnd(){
		if(complete >= length){
			//Effect ! 
			NewComboSetting ();
		}
	}
	void NewComboSetting(){
		int i;
		for(i=0;i<COMBO_LEN;i++){
			Combo[i].transform.localScale = new Vector3(0,0,0);
			Check[i].transform.localScale = new Vector3(0,0,0);
		}
		//length random ㅠㅠ ㅎㅐ
		complete = 0;
		do{
			length = (int)(Random.value * ((float)(COMBO_LEN-2)))+3;
		}while(length == COMBO_LEN+1);

		int t;
		for(i=0;i<length;i++){
			do{
				t = (int)(Random.value * 4.0f);
			}while(t == 4);
			tk2dSprite sprite = Combo[i].GetComponent<tk2dSprite>(); 
			switch(t){
				case 0: 
					type[i] = MainLogic.TILETYPE.Sword;
					sprite.SetSprite(Tile.datas,"Sword"); 
					sprite.scale = new Vector3(90.0f,90.0f,1.0f);
					break;
				case 1: 
					type[i] = MainLogic.TILETYPE.Wand; 
					sprite.SetSprite(Tile.datas,"Wand");
					sprite.scale = new Vector3(90.0f,90.0f,1.0f);
					break;
				case 2: 
					type[i] = MainLogic.TILETYPE.Coin; 
					sprite.SetSprite(Tile.datas,"Coin");
					sprite.scale = new Vector3(90.0f,90.0f,1.0f);
					break;
				case 3: 
					type[i] = MainLogic.TILETYPE.Potion; 
					sprite.SetSprite(Tile.datas,"Potion");
					sprite.scale = new Vector3(90.0f,90.0f,1.0f);
					break;
			}
			Combo[i].transform.localScale = new Vector3(1.0f,1.0f,0);
		}
	}
}

