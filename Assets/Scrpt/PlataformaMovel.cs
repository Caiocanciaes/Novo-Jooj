using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformaMovel : MonoBehaviour {

	[SerializeField] private Transform pontoA;
	[SerializeField] private Transform pontoB;
	[SerializeField] private float velocidade;
	private bool moverParaPontoA;
	private bool moverParaPontoB;

	void Start(){
		moverParaPontoA = true;

	}


	void Update(){
		if (moverParaPontoA == true) {
			transform.position = Vector2.MoveTowards (transform.position, pontoA.position, velocidade * Time.deltaTime);
		}
		if (transform.position.y == pontoA.position.y) {
			moverParaPontoA = false;
			moverParaPontoB = true;
		}
		if (moverParaPontoB == true) {
			transform.position = Vector2.MoveTowards (transform.position, pontoB.position, velocidade * Time.deltaTime);
		}
		if (transform.position.y == pontoB.position.y) {
			moverParaPontoA = true;
			moverParaPontoB = false;
		}
	}

		private void OnCollisionEnter2D(Collision2D collision){
			if(collision.gameObject.CompareTag("Player")){
				collision.transform.SetParent(this.gameObject.transform);
		}
	}
		private void OnCollisionExit2D(Collision2D Collision){
			Collision.transform.SetParent (null);
		
		}

	
}
