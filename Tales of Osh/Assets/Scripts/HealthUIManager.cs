using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manager Class to create HealthUI elements
/// </summary>
public class HealthUIManager : MonoBehaviour {

	public HealthUI healthUIPrefab;
	public static HealthUIManager instance;

	void Awake() {
		instance = this;
	}

	public void Create(Transform target, CharacterStats stats) {
		if (healthUIPrefab == null) {
			Debug.LogError("[HealthUIManager] No HealthUI-Prefab");
			return;
		}
		HealthUI newUI = Instantiate (healthUIPrefab, transform) as HealthUI;
		newUI.Init (target, stats);
	}
}
