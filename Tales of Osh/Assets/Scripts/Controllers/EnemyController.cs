using UnityEngine;
using UnityEngine.AI;
using System.Collections;

/// <summary>
/// Controles the Enemey.
/// So This can be seen as the AI.
/// 
/// 
/// TODO: Class-Hirachy for splitting EnemyControllerMelee and EnemyControllerRange
/// </summary>
[RequireComponent(typeof(CharacterCombat))]
[RequireComponent(typeof(NavMeshAgent))]
public class EnemyController : MonoBehaviour {

	/// <summary>
	/// Distance the Enemy can Look.
	/// If the Player is in this range the enemy starts attacking him.
	/// The Range will be displayed as gizmo in the Szene-View when dragged
	/// </summary>
	public float lookRadius = 10f;

	/// <summary>
	/// The Range / Distance the Enemy-Character can attack
	/// </summary>
	public float attackRange = 1f;


	/// <summary>
	/// The Target to attack.
	/// This will be the player, and it wil lbe set on sTart automatically
	/// </summary>
	private Transform target;

	/// <summary>
	/// Agent to walk the Enemy
	/// </summary>
	[SerializeField]
	private NavMeshAgent agent;

	/// <summary>
	/// Do i really need this?
	/// Wouldn't it be better if the COmbat siystem is "global" ?
	/// </summary>
	[SerializeField]
	private CharacterCombat combatManager;

	void Start() {
		target = Player.instance.transform;
		agent = GetComponent<NavMeshAgent>();
		combatManager = GetComponent<CharacterCombat>();

		if (target == null) {
			Debug.Log("Target (Player) not found");
		}

		//	Set the Attackrange as Stop-Distance
		agent.stoppingDistance = attackRange;
	}

	/// <summary>
	/// Called one a frame.
	/// 
	/// Checks if the target is in sight-range.
	/// If so it starts to attack.
	/// </summary>
	void Update () {
		
		// Get the distance to the player
		float distance = Vector3.Distance(target.position, transform.position);
		// Debug.Log("Distance to Player: " + distance);

		// If inside the radius
		if (distance <= lookRadius)	{

			// Move towards the player
			agent.destination = target.position;
			
			if (distance <= agent.stoppingDistance)	{

				FaceTarget();

				// Attack
				attack();
			}
		}
	}

	/// <summary>
	/// Makes sure, that the enemy, is facing towards the player.
	/// </summary>
	private void FaceTarget () {
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

		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(transform.position, attackRange);
	}
	
	/// <summary>
	/// Attacks the target.
	/// 
	/// </summary>
	private void attack () {
		Debug.Log("Attack....");
		combatManager.Attack(Player.instance.playerStats);
	}
}
