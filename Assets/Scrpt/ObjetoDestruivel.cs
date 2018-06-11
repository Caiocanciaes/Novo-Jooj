using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjetoDestruivel : MonoBehaviour {

	private float alturaDoPulo = 5;

	private void OnCollisionEnter2D(Collision2D colisao)
	{
		ControlaPersonagem personagem = colisao.collider.gameObject.GetComponent<ControlaPersonagem>();
		

		if (colisao.gameObject.CompareTag ("Player")&& personagem.PosicaoPes.transform.position.y> this.transform.position.y) {
			personagem.Rb.AddForce (Vector2.up * alturaDoPulo, ForceMode2D.Impulse);
			Destroy (this.gameObject);	

		}
		
	}
}
