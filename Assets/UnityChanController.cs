using UnityEngine;
using System.Collections;

public class UnityChanController : MonoBehaviour {


	//アニメーションするためのコンポーネントを入れる
	Animator animator;

	//Unityちゃんを移動させるコンポーネントを入れる
	Rigidbody2D rigid2D;


	//地面の位置
	private float groundLevel =-3.0f;

	//ジャンプの速度の減衰
	private float dump = 0.8f;

	//ジャンプの速度
	private float jumpVelocity = 20.0f;

	//ゲームオーバーになる位置
	private float deadLine = -9.0f;

	// Use this for initialization
	void Start () {

		//アニメータのコンポーネントを取得
		this.animator = GetComponent<Animator>();

		//Rigidbody2Dのコンポーネントを取得
		this.rigid2D = GetComponent<Rigidbody2D>();

	
	}
	
	// Update is called once per frame
	void Update () {
	
		//走るアニメーションを再生するために、Animatorのパラメータを調整する
		this.animator.SetFloat("Horizontal", 1);


		//着地しているかどうか調べる
		bool isGround = (transform.position.y > this.groundLevel) ? false : true;
		this.animator.SetBool ("isGround", isGround);


		//ジャンプ状態の時にボリュームを0にする
		GetComponent<AudioSource>().volume = (isGround)? 1: 0;



		//着地でクリックされた場合
		if (Input.GetMouseButtonDown (0) && isGround) {
			//上方向に力をかける
			this.rigid2D.velocity = new Vector2 (0,this.jumpVelocity);

		}

		//クリックをやめたら上方向への速度を減速する
		if (Input.GetMouseButton (0) == false) {
			if (this.rigid2D.velocity.y > 0) {
				this.rigid2D.velocity *= this.dump;
			}

		}

		//デッドラインを超えたらゲームオーバーにする

		if (transform.position.x < this.deadLine) {
			//UIControllerのGameOver関数を呼び出して画面上に表示する
			GameObject.Find("Canvas").GetComponent<UIController>().GameOver();

			//Unityちゃんを破棄する
			Destroy(gameObject);




		}



	

	}
}
