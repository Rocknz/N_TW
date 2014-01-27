using UnityEngine;
using System.Collections;

public class MainLogic : MonoBehaviour {
	public enum TILETYPE{ Enemy, Sword, Wand, Potion, Coin};
	public static int TILE_SIZE = 7; 
	GameObject player;
	Stack PathStack;
	bool NowBreaking;
	int FallingCount;
	int Turn = 0;
	int Damage_now = 0;

	// Use this for initialization
	Tile[,] main_Tile = new Tile[TILE_SIZE,TILE_SIZE];
	bool[,] touch_Check = new bool[TILE_SIZE,TILE_SIZE];

	void Start () {
		player = GameObject.Find ("Player");

		PathStack = new Stack();
		int i,j;
		for(i=0;i<TILE_SIZE;i++){
			for(j=0;j<TILE_SIZE;j++){
				main_Tile[i,j] = new Tile(i,j,GameObject.Find ("AllTiles"));
				touch_Check[i,j] = false;
			}
		}
		NowBreaking = false;
	}

	// Update is called once per frame
	void setDamage_now(int x){
		if(x == 0){
			// use Xien;
			Damage_now = UserData.Instance.Xien + 1;
		}
		else if(x == 1){
			Damage_now += UserData.Instance.Atk;
		}
		else if(x == -1){
			Damage_now -= UserData.Instance.Atk;
		}
		GameObject.Find ("UserText").GetComponent<UserText>().setDMG (Damage_now);
	}
	void Update () {
		if(PathStack.Count == 0){
			setDamage_now(0);
		}
		if(!NowBreaking){
			if(Input.touchCount != 0){
				Vector2 V2 = Input.GetTouch(0).position;
				Ray ray = Camera.main.ScreenPointToRay(new Vector3(V2.x,V2.y,0));
//			if(Input.GetButtonDown("Fire1")){
//				Vector2 V2 = new Vector2(Input.mousePosition.x,Input.mousePosition.y);
//				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				RaycastHit hit = new RaycastHit();
				if(Physics.Raycast(ray, out hit)) {
					if(player.transform == hit.transform){
						Application.LoadLevel(3);
					}
					foreach(Tile tiles in main_Tile){
						if(tiles.myTile.transform == hit.transform){
							Add(new Vector2(tiles.myStatus.myX,tiles.myStatus.myY));
						}
					}
				}
			}
			else{
			//else if(Input.GetButtonDown("Fire2")){
				int count = PathStack.Count;
				TILETYPE type = new TILETYPE();
				bool isEnemyAttacked = false;
				while(PathStack.Count != 0){
					Vector2 now = (Vector2)PathStack.Pop();
					int nx,ny;
					nx = (int)now.x;
					ny = (int)now.y;
					main_Tile[ny,nx].SetScale (0.9f);
					if(count >= 3){
						if(!(type == TILETYPE.Sword && 
						   main_Tile[ny,nx].myStatus.myType == TILETYPE.Enemy) ){
							type = main_Tile[ny,nx].myStatus.myType;
						}
						if(main_Tile[ny,nx].myStatus.myType == TILETYPE.Enemy){
							isEnemyAttacked = true;
						}
						main_Tile[ny,nx].myStatus.Attacked (Damage_now);
					}
					touch_Check[ny,nx] = false;
				}
				DestroyTile();
				if(count >= 3){
					if(isEnemyAttacked){
						UserData.Instance.Xien = 0;
					}
					GameObject.Find ("ComboBox").GetComponent<ComboLogic>().AddCombo(type);
					if(type == TILETYPE.Coin){
						UserData.Instance.Coin += count;
					}
					else if(type == TILETYPE.Potion){
						UserData.Instance.Hp += count;
					}
					else if(type == TILETYPE.Wand){
						UserData.Instance.Xien += count;
					}
					GameObject.Find ("UserText").GetComponent<UserText>().setStat();
				}
			}
		}
	}
	private void DestroyTile(){
		// hP ㄱㅏ 0ㅂㅗㄷㅏ ㅈㅏㄱㅇㅡㄴ ㅌㅏㅇㅣㄹ ㅈㅔㄱㅓ
		int i,j,cnt=0;
		for(i=0;i<TILE_SIZE;i++){
			for(j=0;j<TILE_SIZE;j++){
				if(main_Tile[i,j].myStatus.myHp <= 0){
					cnt ++;
				}
			}
		}

		if(cnt != 0){
			NowBreaking = true;

			//MonsterAttackTurn.
			for(i=0;i<TILE_SIZE;i++){
				for(j=0;j<TILE_SIZE;j++){
					main_Tile[i,j].myStatus.myTurn ++;
				}
			}

			for(i=0;i<TILE_SIZE;i++){
				for(j=0;j<TILE_SIZE;j++){
					if(main_Tile[i,j].myStatus.myHp <= 0){
						//ㅈㅔㄱㅓ
						cnt --;
						if(cnt != 0){
							iTween.ScaleTo(main_Tile[i,j].myTile, iTween.Hash(
								"x", 0,
								"y", 0,
								"easeType", "easeInCubic",
								"time", 0.5f));
						}
						else{
							iTween.ScaleTo(main_Tile[i,j].myTile, iTween.Hash(
								"x", 0,
								"y", 0,
								"easeType", "easeInCubic",
								"time", 0.5f,
								"oncomplete","FallingTile",
								"oncompletetarget",gameObject,
								"oncompleteparams",main_Tile[i,j]));
						}
					}
				}
			}
		}
	}
	private void FallingTile(){
		int i,j,y;
		for(j=0;j<TILE_SIZE;j++){
			y = 0;
			for(i=0;i<TILE_SIZE;i++){
				while(true){
					if(y >= TILE_SIZE){
						main_Tile[i,j].myStatus = new TileStatus(y,j);
						y ++;
						break;
					}
					else{
						if(main_Tile[y,j].myStatus.myHp <= 0){
							y++;
						}
						else{
							main_Tile[i,j].myStatus = main_Tile[y,j].myStatus;
							y++;
							break;
						}
					}
				}
				main_Tile[i,j].SetTileByStatus();
			}
		}
		FallingCount = 0;
		for(i=0;i<TILE_SIZE;i++){
			for(j=0;j<TILE_SIZE;j++){
				if(main_Tile[i,j].myStatus.myY != i){
					main_Tile[i,j].myStatus.myY = i;
					FallingCount ++;
					Vector3 v3 = Tile.Position(i,j);
					iTween.MoveTo(main_Tile[i,j].myTile, iTween.Hash(
						"x", v3.x,
						"y", v3.y,
						"easeType", "easeOutQuad",
						"speed", 10,
						"oncomplete","FallingEnd",
						"oncompletetarget",gameObject,
						"oncompleteparams",main_Tile[i,j]));
				}
			}
		}
	}
	public void FallingEnd(Tile tile){
		FallingCount --;
		tile.SetTileByStatus();
		if(FallingCount == 0){
			MonsterAttack();
		}
	}
	public void MonsterAttack(){
		// ㅁㅗㄴㅅㅡㅌㅓ ㄱㅗㅇㄱㅕㄱ
		Turn ++;
		FallingCount = 1;

		int i,j;
		for(i=0;i<TILE_SIZE;i++){
			for(j=0;j<TILE_SIZE;j++){
				if(main_Tile[i,j].myStatus.myType == TILETYPE.Enemy && 
				   main_Tile[i,j].myStatus.myTurn != 0){

					FallingCount ++;
					iTween.ScaleFrom(main_Tile[i,j].myTile, iTween.Hash(
						"x", 2.5f,
						"y", 2.5f,
						"easeType", "easeOutQuad",
						"time", 0.5,
						"oncomplete","MonsterAttackEnd",
						"oncompletetarget",gameObject,
						"oncompleteparams",main_Tile[i,j]));

					UserData.Instance.Hp -= main_Tile[i,j].myStatus.myAttack;
					GameObject.Find ("UserText").GetComponent<UserText>().setStat();
				}
			}
		}
		MonsterAttackEnd();
	}
	public void MonsterAttackEnd(){
		FallingCount --;
		if(FallingCount == 0){
			NowBreaking = false;
		}
	}
	private void Add(Vector2 newSelectedTile){
		// Path ㅇㅔ ㅍㅛ ㅅㅣ!
		int nx,ny;
		ny = (int)newSelectedTile.y;
		nx = (int)newSelectedTile.x;
		if(PathStack.Count == 0){
			PathStack.Push(newSelectedTile);
			main_Tile[ny,nx].SetScale (0.4f);
			if(main_Tile[ny,nx].myStatus.myType == TILETYPE.Sword){
				setDamage_now(1);
			}
			touch_Check[ny,nx] = true;
		}
		else{
			Vector2 nowSelectedTile = (Vector2)PathStack.Peek();
			int nx1,ny1;
			nx1 = (int)nowSelectedTile.x;
			ny1 = (int)nowSelectedTile.y;
			if(!touch_Check[ny,nx]){
				if(Vector2.Distance(newSelectedTile,nowSelectedTile) < 2 &&
				   TileStatus.EqualType(main_Tile[ny,nx].myStatus.myType,
				                        main_Tile[ny1,nx1].myStatus.myType)){

					// ㄱㅏㄹ ㅅㅜ ㅇㅣㅆ ㅇㅡㄹ ㄸㅐ.
					PathStack.Push(newSelectedTile);
					main_Tile[ny,nx].SetScale (0.4f);
					if(main_Tile[ny,nx].myStatus.myType == TILETYPE.Sword){
						setDamage_now(1);
					}
					touch_Check[ny,nx] = true;
				}
			}
			else{
				//ㄱㅣㅈㅗㄴㅇㅔ ㅈㅗㄴㅈㅐ ㅎㅏㄴㄷㅏㅁㅕㄴ
				while(!PathStack.Peek().Equals(newSelectedTile)){
					Vector2 delpath = (Vector2)PathStack.Pop();
					int dx,dy;
					dx = (int)delpath.x;
					dy = (int)delpath.y;
					main_Tile[dy,dx].SetScale(0.9f);
					if(main_Tile[ny,nx].myStatus.myType == TILETYPE.Sword){
						setDamage_now (-1);
					}
					touch_Check[dy,dx] = false;
				}
			}
		}
	}
}
