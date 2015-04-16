#pragma strict
import System.Collections.Generic;
import UnityEngine;
import System.Collections;

// all augmon
var allAugmonArr : Augmon[];
var enemyAugmon  : Augmon;
var playerAugmon : Augmon;

var playerAugmonSpawnPoint : GameObject;
var enemyAugmonSpawnPoint  : GameObject;

//var theTrackable : TrackableBehaviour;

// player turn is 0, enemy turn is 1
var turn         : int;

var playerHitParticles : GameObject;
var enemyHitParticles  : GameObject;

var soundEffects : AudioClip[];

function Start()
{
	/*if(theTrackable == null)
    {
        Debug.Log("Warning: Trackable not set!");
    }*/

	turn = 0;
	playerHitParticles.SetActive(false);
	enemyHitParticles.SetActive(false);
}

function Update()
{
	if(turn == 0)
	{
		playerAttack();
	}
}

function playerAttack()
{
	Debug.Log("Player Attack");
	GameObject.Find("PlayerAugmon").animation.Play("Tackle");
	audio.clip = soundEffects[Random.Range(0, soundEffects.Length)];
	audio.Play();
	yield WaitForSeconds(0.1);
	GameObject.Find("EnemyAugmon").animation.Play("Hurt");
	turn = 1;
	enemyHitParticles.SetActive(true);
	enemyAttack();
}

function enemyAttack()
{
	yield WaitForSeconds(2);
	Debug.Log("Enemy Attack");
	GameObject.Find("EnemyAugmon").animation.Play("Tackle");
	audio.clip = soundEffects[Random.Range(0, soundEffects.Length)];
	audio.Play();
	yield WaitForSeconds(0.1);
	GameObject.Find("PlayerAugmon").animation.Play("Hurt");
	playerHitParticles.SetActive(true);
	yield WaitForSeconds(2);
	turn = 0;

	// reset particles
	playerHitParticles.SetActive(false);
	enemyHitParticles.SetActive(false);
}

// determines a random enemy augmon
function randomizeAugmon()
{
	// a list that will contain the enemy augmon to be chosen from
    var tempAugmonList : List.<Augmon> = new List.<Augmon>();
    var randomNum : int = Random.Range(0, 100);
    
    // roll a random augmon
    if(randomNum == 20)
    {
        for(var i = 0; i < allAugmonArr.Length; i++)
        {
            if(allAugmonArr[i].rarity == Rarity.rare)
            {
                tempAugmonList.Add(allAugmonArr[i]);
            }
        }
    }
    // roll a common augmon
    else
    {
        for(var j = 0; j < allAugmonArr.Length; j++)
        {
            if(allAugmonArr[j].rarity == Rarity.common)
            {
                tempAugmonList.Add(allAugmonArr[j]);
            }
        }
    }
    
    // select a random enemy augmon from tempAugmonList
    var newRandomNum = Random.Range(0, tempAugmonList.Count);
    enemyAugmon = tempAugmonList[newRandomNum];
    Debug.Log(enemyAugmon.name);
}