using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/* This class just makes it faster to get certain components on the player. */

 public class Player : MonoBehaviour {

	public CharacterStats playerStats;
	#region Singleton
	
	public static Player instance;

	void Awake ()
	{
		instance = this;
	}

	#endregion

	public CharacterCombat playerCombatManager;	

	void Start() {
		
	}

	void Die() {
		
	}
}
