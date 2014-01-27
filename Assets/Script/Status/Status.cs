using UnityEngine;
using System.Collections;
using LitJson;

/*
 * json's keys
 * t = ['limit_lv','limit_stat','name','durability','kyungdo','stab_damage',
     'hack_damage','def','int_damage','magic_def','accuracy',
     'dex','agility','critical','delay','intensity','slot']
*/
public class Status : MonoBehaviour {
	int money;
	GameObject Money;
	GameObject[] ViewTable;
	GameObject[,] ViewItem = new GameObject[4,11];
	GameObject[] ViewPresentItem = new GameObject[4];
	GameObject[] ViewNextItem = new GameObject[5];
	GameObject ViewMessage;

	/*
	 * 0 : ㄷㅐㄱㅓㅁ
	 * 1 : ㅌㅐㄷㅗ
	 * 2 : ㅍㅕㅇㄷㅗ
	 * 3 : ㅌㅜㄱㅜ
	 * 4 : ㄱㅏㅂㅇㅗㅅ
	 * 5 : ㄴㅓㅋㅡㄹ
	 * 6 : ㅇㅏㅁㄹㅣㅅ
	 * 7 : ㅁㅓㄹㅣ
	 * 8 : ㅁㅗㅁ
	 * 9 : ㅅㅗㄴ
	 */
	//					0			1			2			3		4		5		6	7		8	9
	string[] ItemType={"DeaSword","TaeSword","B_M_Sword","Helmet","L_Armor","NCL","BU","hea","bod","hand"};
	string[,] ViewItemType = {{"Helmet","Helmet_Name","Helmet_Def","Helmet_HP","Helmet_Next"},
		{"Head","Head_Name","Head_IntDamage","Head_HackDamage","Head_Next"},
		{"Sword","Sword_Name","Sword_HackDamage","Sword_IntDamage","Sword_Next"},
		{"Body","Body_Name","Body_HP","Body_Def","Body_Next"} };
	string[,] JsonItemType = {{"","name","def","HP",""},
		{"","name","int_damage","hack_damage",""},
		{"","name","hack_damage","int_damage",""},
		{"","name","HP","def",""}};
	string[,] ShowItemType = {{"","name","def","HP",""},
		{"","name","int","hack",""},
		{"","name","hack","int",""},
		{"","name","HP","def",""}};

	string[] ViewPresentItemType = {"Present","Present_Name","Present_1","Present_2"};
	string[] ViewNextItemType = {"Next","Next_Name","Next_1","Next_2","Next_Price"};

	int [] hash = {3,7,0,8};
	//public enum ITEMTYPE {Sword,TAESWORD,B_M_SWORD,Helmet,Armor,NCL,Armlet,Head,Body,Hand};

	LitJson.JsonData [] Items = new LitJson.JsonData[10];
    
	// Use this for initialization
	void Start () {
		ViewMessage = GameObject.Find ("Message");
		money = UserData.Instance.Coin;

		Money = GameObject.Find ("Money");
		TextMesh textMesh = Money.GetComponent<TextMesh> ();
		textMesh.text = "Seed : " + money.ToString ();

		ViewTable = new GameObject[6];
	
		for ( int i = 0 ; i < 6 ; i++ ) 
			ViewTable[i] = GameObject.Find ("ViewTable"+(i+1).ToString());
		ViewTable[4].transform.position = new Vector3(0,1,10); // NextItem table
		ViewTable[5].transform.position = new Vector3(0,1,10); // Message table

	Items = ItemData.Instance.Items;
	

		for ( int i = 0 ; i < 4 ; i++ ) 
			for ( int j = 0 ; j < 5 ; j++ ) 
				ViewItem[i,j] = GameObject.Find (ViewItemType[i,j]);

		for ( int i = 0 ; i < 5 ; i++ ) {
			if ( i < 4 ) ViewPresentItem[i] = GameObject.Find (ViewPresentItemType[i]);
			ViewNextItem[i] = GameObject.Find (ViewNextItemType[i]);
		}

		//showWindows ("Start Call!");
		DrawItemTable ();
	}
	
	// Update is called once per frame
	public static int LastSelectItem;
	void Update () {
		money = UserData.Instance.Coin;
		if ( Input.GetButtonDown ("Fire1") ) {
			//Vector2 V2 = new Vector2(Input.mousePosition.x,Input.mousePosition.y);
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit = new RaycastHit();
			if(Physics.Raycast(ray, out hit)) {
				closeWindows();
				if ( hit.collider.name == "select" ) {
					Application.LoadLevel(2);
				}
				if ( hit.collider.name == ViewTable[0].name ) {
					if ( DrawNextItem(0) ) {
						LastSelectItem = 0;
						ViewTable[4].transform.position = new Vector3(0,1.1f,-1.3f);
					}
					else isLastItem (0);
				}
				else if ( hit.collider.name == ViewTable[1].name ) {
					if ( DrawNextItem (1) ) {
						LastSelectItem = 1;
						ViewTable[4].transform.position = new Vector3(0,1.1f,-1.3f);
					}
					else isLastItem(1);
				}
				else if ( hit.collider.name == ViewTable[2].name ) {
					if ( DrawNextItem (2) ) { 
						LastSelectItem = 2;
						ViewTable[4].transform.position = new Vector3(0,1.1f,-1.3f);
					}
					else isLastItem (2);
				}
				else if ( hit.collider.name == ViewTable[3].name ) {
					if ( DrawNextItem (3) ) { 
						LastSelectItem = 3;
						ViewTable[4].transform.position = new Vector3(0,1.1f,-1.3f);
					}
					else isLastItem (3);
				}
				else if ( hit.collider.name == ViewTable[5].name ) {
				//	closeWindows ();
				}
				else if ( hit.collider.name == "Bye" ) {
					ViewTable[4].transform.position = new Vector3(0,1.1f,10);
				}
				else if ( hit.collider.name == "Buy" ) {
					BuyItem (LastSelectItem);
					DrawItemTable ();
					//Debug.Log (isPossibleBuyItem(LastSelectItem));
					//Debug.Log (LastSelectItem);
					ViewTable[4].transform.position = new Vector3(0,1.1f,10);
				}
			//	else if ( hit.collider.name == ViewTable[4].name )
			//		ViewTable[4].transform.position = new Vector3(0,1,10);
			//	Debug.Log (hit.collider.name);
            }

			TextMesh textMesh = Money.GetComponent<TextMesh>();
			textMesh.text = "Seed : "+money.ToString ();
		}
		//Debug.Log ("asdf");
	}

	void DrawItemTable() {
		int [] lv = new int[4];
		lv[0] = UserData.Instance.HelmetLevel;
		lv[1] = UserData.Instance.HeadLevel;
		lv[2] = UserData.Instance.SwordLevel;
		lv[3] = UserData.Instance.BodyLevel;


		for ( int i = 0 ; i < 4 ; i++ ) 
			for ( int j = 1 ; j < 4 ; j++ ) {
			//if ( JsonItemType[i,j].ToString () == "HP" ) continue;
				TextMesh textMesh = ViewItem[i,j].GetComponent<TextMesh>();
				if ( j == 1 ) {
					textMesh.text = Items[hash[i]][lv[i]][JsonItemType[i,j]].ToString();
					textMesh.fontSize = 10;
				}
				else textMesh.text = ShowItemType[i,j]+" : "+Items[hash[i]][lv[i]][JsonItemType[i,j]].ToString ();
			}
	}
	bool DrawNextItem(int ItemType) {
		int [] lv = new int[4];
		lv [0] = UserData.Instance.HelmetLevel;
		lv [1] = UserData.Instance.HeadLevel;
		lv [2] = UserData.Instance.SwordLevel;
		lv [3] = UserData.Instance.BodyLevel;

		if ( lv[ItemType] == Items[hash[ItemType]].Count-1 ) 
			return false;

		for ( int j = 1 ; j < 4 ; j++ ) { 
			TextMesh textMesh = ViewPresentItem[j].GetComponent<TextMesh>();
			if ( j == 1 ) {
				textMesh.text = Items[hash[ItemType]][lv[ItemType]][JsonItemType[ItemType,j]].ToString ();
				textMesh.fontSize = 10;
			}
			else textMesh.text = ShowItemType[ItemType,j]+" : "+Items[hash[ItemType]][lv[ItemType]][JsonItemType[ItemType,j]].ToString ();
		}
		int nextLv = lv[ItemType]+1;
		for ( int j = 1 ; j < 4 ; j++ ) {
			TextMesh textMesh = ViewNextItem[j].GetComponent<TextMesh>();
			if ( j == 1 ) { 
				textMesh.text = Items[hash[ItemType]][nextLv][JsonItemType[ItemType,j]].ToString();
				textMesh.fontSize = 10;
			}
			else textMesh.text = ShowItemType[ItemType,j]+" : "+Items[hash[ItemType]][nextLv][JsonItemType[ItemType,j]].ToString ();
		}
		TextMesh price = ViewNextItem[4].GetComponent<TextMesh>();
		price.text = "Price\t:\t" + Items [hash [ItemType]] [nextLv] ["price"].ToString ();
		return true;
	}
	void isLastItem(int ItemType) {
		Debug.Log ("last item");
	}
	bool isPossibleBuyItem(int ItemType) {
		int [] lv = new int[4];
		lv [0] = UserData.Instance.HelmetLevel;
		lv [1] = UserData.Instance.HeadLevel;
		lv [2] = UserData.Instance.SwordLevel;
		lv [3] = UserData.Instance.BodyLevel;

		return money >= (int)Items [hash [ItemType]] [lv [ItemType] + 1] ["price"];
	}
	void BuyItem(int ItemType) {
		int [] lv = new int[4];
		lv [0] = UserData.Instance.HelmetLevel;
		lv [1] = UserData.Instance.HeadLevel;
		lv [2] = UserData.Instance.SwordLevel;
		lv [3] = UserData.Instance.BodyLevel;
		if ( !isPossibleBuyItem (ItemType) ) 
			showWindows("is not enough Seed!");
		else {
			money -= (int)Items[hash[ItemType]][lv[ItemType]+1]["price"];
			UserData.Instance.Coin = money;

			if ( ItemType == 0 ) UserData.Instance.HelmetLevel++;
			else if ( ItemType == 1 ) UserData.Instance.HeadLevel++;
			else if ( ItemType == 2 ) UserData.Instance.SwordLevel++;
			else if ( ItemType == 3 ) UserData.Instance.BodyLevel++;
			showWindows ("success");
		}
	}
	void showWindows(string s) {
		ViewTable [5].transform.position = new Vector3 (0, 1, -1.5f);
		TextMesh message = ViewMessage.GetComponent<TextMesh> ();
		message.text = s;
	}
	void closeWindows() {
		ViewTable [5].transform.position = new Vector3 (0, 1, 10);
	}
}
