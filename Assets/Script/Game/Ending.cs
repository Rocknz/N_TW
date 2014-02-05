using UnityEngine;
using System.Collections;
public class Ending : MonoBehaviour {
	GameObject EndingView;
	GameObject ending;
	GameObject Turn;
	GameObject Enemy;
	GameObject Cube;
	GameObject GoMenu;
	int TurnGap;
	int EnemyGap;
	bool Gotcha = false;
	void Start(){
		EndingView = GameObject.Find ("EndingView");
		ending = GameObject.Find ("Ending");
		Turn = GameObject.Find ("Turn Gap");
		Enemy = GameObject.Find ("Enemy Gap");
		Cube = GameObject.Find ("Cube");
		GoMenu = GameObject.Find ("Go Menu");
	}
	public void NowEnding(Tile[,] mTiles,int Turn,int DeadEnemyCount){
		EndingView.transform.localScale = new Vector3(0,0,0);
		EndingView.transform.localPosition = new Vector3(0,0,10);
		iTween.ScaleTo(EndingView, iTween.Hash(
			"x", 3000,
			"y", 5000,
			"easeType", "easeInCubic",
			"time", 1.0f,
			"oncomplete","LoadingStart",
			"oncompletetarget",gameObject,
			"oncompleteparams",EndingView));

		foreach(Tile thisTile in mTiles){
			thisTile.myHp.GetComponent<UILabel>().fontSize = 0;
			thisTile.myAttack.GetComponent<UILabel>().fontSize = 0;
		}
		TurnGap = Turn;
		EnemyGap = DeadEnemyCount;
	}
	public void LoadingStart(){
		ending.transform.localPosition = new Vector3(0,0,1);
		Turn.GetComponent<UILabel>().text = TurnGap.ToString();
		Enemy.GetComponent<UILabel>().text = EnemyGap.ToString();
		Gotcha = true;
		StartCoroutine("StartGotcha");
	}
	public void setCube(){
		int t;
		do{
			t = (int)(Random.value * 4.0f);
		}while(t == 4);
		
		switch(t){
		case 0: Cube.GetComponent<tk2dSprite>().SetSprite(Tile.datas,"Sword"); break;
		case 1: Cube.GetComponent<tk2dSprite>().SetSprite(Tile.datas,"Wand"); break;
		case 2: Cube.GetComponent<tk2dSprite>().SetSprite(Tile.datas,"Coin"); break;
		case 3: Cube.GetComponent<tk2dSprite>().SetSprite(Tile.datas,"Potion"); break;
		}
	}
	public IEnumerator StartGotcha(){
		while(Gotcha){
			setCube();
			yield return new WaitForSeconds(0.01f);
		}
	}
	void Update(){
		if(Input.GetButtonDown("Fire1")){
			Vector2 V2 = new Vector2(Input.mousePosition.x,Input.mousePosition.y);
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit = new RaycastHit();
			if(Physics.Raycast(ray, out hit)) {
				if(hit.transform == GoMenu.transform){
					Application.LoadLevel(1);
					Gotcha = false;
				}
			}
		}
	}
}
