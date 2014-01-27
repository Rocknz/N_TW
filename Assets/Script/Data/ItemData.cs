using UnityEngine;
using System.Collections;

public class ItemData : MonoBehaviour {
	string[] ItemType={"DeaSword","TaeSword","B_M_Sword","Helmet","L_Armor","NCL","BU","hea","bod","hand"};
	private LitJson.JsonData [] mItems = new LitJson.JsonData[10];

	private static ItemData ins;
	public static ItemData Instance {
		get { 
			return ins; 
		}
	}
	public LitJson.JsonData[] Items{
		get { return ins.mItems; }
	}
	// Use this for initialization
	void Start () {
		ins = new ItemData();

		for (int i = 0; i < 10; i++) {
			TextAsset textAsset = Resources.Load("json/"+ItemType[i]) as TextAsset;
			string text = textAsset.text;
			ins.mItems[i] = LitJson.JsonMapper.ToObject (text);
		}
		DontDestroyOnLoad(this);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
