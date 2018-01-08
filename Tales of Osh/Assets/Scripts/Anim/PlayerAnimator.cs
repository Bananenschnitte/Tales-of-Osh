using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerAnimator : CharacterAnimator {

	public WeaponAnimation[] weaponAnimations;
	private WeaponAnimation currentWeaponAnimation;

	void Awake() {
		currentWeaponAnimation = weaponAnimations [0];
	}

	protected override void Start() {
		base.Start ();
	}
		

	protected override void OnAttack() {
		if (currentWeaponAnimation != null) {
			int attackIndex = Random.Range (0, currentWeaponAnimation.numAnimations);
			animator.SetFloat ("Attack Index", attackIndex);
			animator.SetFloat ("Weapon Index", currentWeaponAnimation.weaponIndex);
		}

		base.OnAttack ();
	}


	[System.Serializable]
	public class WeaponAnimation {		
		public int weaponIndex;
		public int numAnimations;
	}
}
