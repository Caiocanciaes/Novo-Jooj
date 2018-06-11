using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlaPersonagem : MonoBehaviour {
	
	private Rigidbody2D rb;
	[SerializeField]private float velocidade = 5;
	private Animator anim;
	private bool viradoParaDireita;
	[SerializeField] private float altura = 6;
	private bool estaNoChao;
	[SerializeField] private LayerMask mascaraChao;
	[SerializeField] private Transform posicaoPes;
	private bool movimento;

	public Transform PosicaoPes{
		get{
			return posicaoPes;

		}


	}

	public Rigidbody2D Rb{
	
		get{
			return rb;
		}
	}

	void Awake(){
		rb = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
	}
	void Start () {
		viradoParaDireita = true;
		movimento = true;
	}
	
	// Update is called once per frame
	void Update () {
		estaNoChao = Physics2D.OverlapCircle(posicaoPes.position, .2f, mascaraChao);

		anim.SetFloat("Velocidade", Mathf.Abs(rb.velocity.x));
		anim.SetBool ("estaNoChao", estaNoChao);
		if(Input.GetButtonDown("Jump")){
			Pular();
		}
		Recomeçar ();
		//RecomeçarCaindo ();
	}
	void FixedUpdate(){
		if (movimento == true) {
			float movHorizontal = Input.GetAxis ("Horizontal");
			//Vector2 direcao = new Vector2(movHorizontal, 0);
			//rb.AddForce(direcao*velocidade);
			rb.velocity = new Vector2 (movHorizontal * velocidade, rb.velocity.y);
			VirarPersonagem (movHorizontal);
		}
	}
	void VirarPersonagem(float movHorizontal){
		if (viradoParaDireita && movHorizontal < 0 || !viradoParaDireita && movHorizontal > 0) {
			transform.localScale = new Vector3 (transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
			viradoParaDireita = !viradoParaDireita; 

		}
	}
	void Pular(){
		if (estaNoChao == true && movimento == true){
			rb.AddForce(Vector2.up * altura, ForceMode2D.Impulse);
		}
	}
	private void OnDrawGizmos(){
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere (posicaoPes.position, .2f);
	}

	void OnTriggerEnter2D (Collider2D colidiu)
	{
		if (colidiu.gameObject.CompareTag ("Espinhos")) {
			Morre();
		}
		if (colidiu.gameObject.CompareTag("Caiu")){
			RecomeçarCaindo ();
			print("Morreu");
	}
	}

	private void Morre(){
		anim.SetTrigger ("Morreu");
		movimento = false;
		rb.velocity = Vector2.zero;
	}

	private void Recomeçar(){
		if(movimento == false && Input.GetKeyDown(KeyCode.R)){
			SceneManager.LoadScene (0);
		}
	}
		private void RecomeçarCaindo(){
		//if (gameObject.CompareTag("Caiu")){
		SceneManager.LoadScene (0);	
		//}
	}
}