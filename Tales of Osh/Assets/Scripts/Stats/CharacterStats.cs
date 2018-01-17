using UnityEngine;

/* Contains all the stats for a character. */
/// <summary>
/// Contains all the stats for a character
/// Stores the max and current Health
/// The Damage the character deals 
/// and so on
/// </summary>
public class CharacterStats : MonoBehaviour {

	/// <summary>
	/// Maximum amount of health
	/// </summary>
	public Stat maxHealth;

	/// <summary>
	/// Current amount of health
	/// </summary>
	/// <returns>The current amount of health</returns>
	public int currentHealth {get;protected set;}

	/// <summary>
	/// The Attackspeed in seconds
	/// 
	/// TODO: Maybe better as 'Stat'-Object?
	/// </summary>
	/// <returns></returns>
	public float attackSpeed {get;protected set;}

	public Stat damage;				// not used yet
	public Stat armor;				//	not used yet

	public event System.Action OnHealthReachedZero;

	public virtual void Awake() {
		currentHealth = maxHealth.GetValue();
	}

	/// <summary>
	/// Deals Damage to the Character
	/// </summary>
	/// <param name="damage">The amount of damage to deal</param>
	public void TakeDamage (int damage) {

		// Subtract the armor value - Make sure damage doesn't go below 0.
		damage -= armor.GetValue();
		damage = Mathf.Clamp(damage, 0, int.MaxValue);

		// Subtract damage from health
		currentHealth -= damage;
		Debug.Log(transform.name + " takes " + damage + " damage.");

		// If we hit 0. Die.
		if (currentHealth <= 0) {
			if (OnHealthReachedZero != null) {
				OnHealthReachedZero ();
			}
		}
	}

	/// <summary>
	/// Heals the Character
	/// </summary>
	/// <param name="amount">The Amount of hitpoints to heal</param>
	public void Heal (int amount) {
		currentHealth += amount;
		currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth.GetValue());
	}



}
