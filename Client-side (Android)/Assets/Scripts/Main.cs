using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {

	public GameObject[] allAugmonArr;
	public GameObject   playerAugmonModel;
	public GameObject   enemyAugmonModel;
	
	public GameObject   playerAugmonSpawnPoint;
	public GameObject   enemyAugmonSpawnPoint;
	
	public int          turn;
	public GameObject   playerHitParticles;
	public GameObject   enemyHitParticles;
	
	public AudioClip[]  soundEffects;
	/*
	public AudioClip[]  music;
	public AudioSource  battleTheme;
	*/
	public GameObject   victoryTheme;
	
	private float       currPlayerAugmonHP;
	private float       currEnemyAugmonHP;
	private float       maxPlayerAugmonHP = 5;
	private float       maxEnemyAugmonHP = 5;
	
	public GameObject   playerAugmonHealthBar;
	public GameObject   enemyAugmonHealthBar;
	public float        maxPlayerAugmonHealthBarLength;
	public float        maxEnemyAugmonHealthBarLength;
	
	public GameObject   victoryMsg;
	
	//-----------------------------------------------------------------------------
	void Awake()
	{
		victoryTheme = GameObject.Find ("victory");
		victoryTheme.SetActive (false);
		victoryMsg.SetActive (false);
	}
	
	void Start()
	{
		turn = 0;
		playerHitParticles.SetActive(false);
		enemyHitParticles.SetActive(false);
		
		spawnAugmon();
		maxPlayerAugmonHealthBarLength = playerAugmonHealthBar.transform.localScale.x;
		maxEnemyAugmonHealthBarLength = enemyAugmonHealthBar.transform.localScale.x;
		currPlayerAugmonHP = maxPlayerAugmonHP;
		currEnemyAugmonHP = maxEnemyAugmonHP;
		Debug.Log ("original: " + maxPlayerAugmonHealthBarLength);
		
		// Debug.Log ("VICTORY: " + victoryTheme.clip.name);
		/*
		sounds = gameObject.AddComponent<AudioSource>();
		sounds.clip = music[0];
		sounds.Play ();
		*/
		
		// battleTheme.Play ();
	}
	
	//-----------------------------------------------------------------------------
	void Update()
	{
		/*if(turn == 0)
		{
			if(!(currPlayerAugmonHP > 0 ^ currEnemyAugmonHP > 0))
				playerAttack();
		}
		if(currEnemyAugmonHP <= 0)
		{
			DoVictory();
		}
		else if(currPlayerAugmonHP <= 0)
		{
			DoDefeat();
		}*/
		
		if(currPlayerAugmonHP > 0 ^ currEnemyAugmonHP > 0)
		{
		/*
			Debug.Log ("PLAYER: " + currPlayerAugmonHP);
			Debug.Log ("ENEMY : " + currEnemyAugmonHP);
			Debug.Log ("TURN  : " + turn);
		*/
			if(currEnemyAugmonHP <= 0)
			{
				DoVictory();
			}
			else if(currPlayerAugmonHP <= 0)
			{
				DoDefeat();
			}
		}
		else
		{
		/*
			Debug.Log ("PLAYER: " + currPlayerAugmonHP);
			Debug.Log ("ENEMY : " + currEnemyAugmonHP);
			Debug.Log ("TURN  : " + turn);
		*/
			if(turn == 0)
			{
				playerAttack();
			}
		}
		
	}
	
	//-----------------------------------------------------------------------------
	IEnumerator Wait()
	{
		yield return new WaitForSeconds(2.0f);
		enemyAttack ();
		
		yield return new WaitForSeconds(2.0f);
		// reset particles
		playerHitParticles.SetActive(false);
		enemyHitParticles.SetActive(false);
		turn = 0;
		
		
	}
	
	//-----------------------------------------------------------------------------
	void playerAttack()
	{
		if(turn != 2)
		{
			Debug.Log("TURN : " + turn + "Player Attack");
			GameObject.Find("PlayerAugmon").animation.Play("Tackle");
			audio.PlayOneShot(soundEffects[Random.Range(0, soundEffects.Length)]);
			GameObject.Find("EnemyAugmon").animation.Play("Hurt");
			turn = 1;
			
			enemyHitParticles.SetActive(true);
			
			currEnemyAugmonHP -= 1;
			// Debug.Log ("Enemy HP: " + enemyAugmonHP);
			Vector3 barScale = enemyAugmonHealthBar.transform.localScale;
			barScale.x = maxEnemyAugmonHealthBarLength * (currEnemyAugmonHP / maxEnemyAugmonHP);
			enemyAugmonHealthBar.transform.localScale = barScale;
			
			StartCoroutine (Wait ());
		}
	}
	
	//-----------------------------------------------------------------------------
	void enemyAttack()
	{
		if(turn != 2)
		{
				Debug.Log("TURN : " + turn + "Enemy Attack");
				GameObject.Find("EnemyAugmon").animation.Play("Tackle");
				audio.PlayOneShot(soundEffects[Random.Range(0, soundEffects.Length)]);
				GameObject.Find("PlayerAugmon").animation.Play("Hurt");
				
				playerHitParticles.SetActive(true);
				
				currPlayerAugmonHP -= 1;
				// Debug.Log ("Player HP: " + playerAugmonHP);
				Vector3 barScale = playerAugmonHealthBar.transform.localScale;
				Debug.Log ("Precalculation: " + barScale.x);
				barScale.x = maxPlayerAugmonHealthBarLength * (currPlayerAugmonHP / maxPlayerAugmonHP);
				Debug.Log ("Postcaluclation: " + barScale.x);
				playerAugmonHealthBar.transform.localScale = barScale;
		}
	}
	
	//-----------------------------------------------------------------------------
	void spawnAugmon()
	{
		int randomNum1 = Random.Range(0, allAugmonArr.Length);
		playerAugmonModel  = (GameObject)Instantiate(allAugmonArr[randomNum1], playerAugmonSpawnPoint.transform.position, playerAugmonSpawnPoint.transform.rotation);
		playerAugmonModel.name = "PlayerAugmon";
		playerAugmonModel.transform.parent = playerAugmonSpawnPoint.transform;
		int randomNum2 = Random.Range(0, allAugmonArr.Length);
		enemyAugmonModel = (GameObject)Instantiate(allAugmonArr[randomNum2], enemyAugmonSpawnPoint.transform.position, enemyAugmonSpawnPoint.transform.rotation);
		enemyAugmonModel.name = "EnemyAugmon";
		enemyAugmonModel.transform.parent = enemyAugmonSpawnPoint.transform;
	}
	
	//-----------------------------------------------------------------------------
	void DoVictory()
	{
		turn = 2; // arbitrary value to end fight loop
		enemyAugmonModel.renderer.enabled = false;
		victoryMsg.SetActive (true);
		
		audio.Stop ();
		victoryTheme.SetActive (true);
		Debug.Log ("AUDIO :" + victoryTheme.name);
		GameObject.Find ("EnemyFrame").renderer.enabled = false;
	}
	
	//-----------------------------------------------------------------------------
	void DoDefeat()
	{
		playerAugmonModel.renderer.enabled = false;
		GameObject.Find ("PlayerFrame").renderer.enabled = false;
	}
}
