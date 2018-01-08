using UnityEngine;
using UnityEngine.AI;
using System.Collections;

/**
 * Controles the Enemey.
 * So This can be seen as the AI.
 */
[RequireComponent(typeof(CharacterCombat))]
public class EnemyController : MonoBehaviour {

	public float lookRadius = 10f;

	private Transform target;
	[SerializeField]
	private NavMeshAgent agent;
	[SerializeField]
	private CharacterCombat combatManager;

	void Start() {
		target = Player.instance.transform;
		agent = GetComponent<NavMeshAgent>();
		combatManager = GetComponent<CharacterCombat>();
	}

	void Update () {
		// Get the distance to the player
		float distance = Vector3.Distance(target.position, transform.position);

		// If inside the radius
		if (distance <= lookRadius)	{
			// Move towards the player
			agent.SetDestination(target.position);
			if (distance <= agent.stoppingDistance)	{
				// Attack
				// combatManager.Attack(Player.instance.playerStats);
				FaceTarget();
			}
		}
	}

	/// <summary>
	/// Makes sure, that the enemy, is facing always towards the player.
	/// </summary>
	void FaceTarget () {
		Vector3 direction = (target.position - transform.position).normalized;
		Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
		transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
	}

	/// <summary>
	/// Draws a gizmo if the object is selected.
	/// It's used to show the look-range of the enemy
	/// </summary>
	void OnDrawGizmosSelected () {
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, lookRadius);
	}

}
