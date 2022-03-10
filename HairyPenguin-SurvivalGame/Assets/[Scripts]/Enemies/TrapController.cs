
using UnityEngine;
using UnityEngine.AI;

public class TrapController : MonoBehaviour
{
    [SerializeField]
    public float ForwardForce = 10f;

    [SerializeField]
    public float UpForce = 1.5f;

    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;

    //States
    public float attackRange;
    public bool playerInAttackRange;

    //Attacking
    [SerializeField] public int attackTimeLow = 1;
    [SerializeField] public int attackTimeHigh = 15;
    private float currentAttackTime;

    bool alreadyAttacked = true;
    public GameObject projectile;

    private void Awake()
    {
        player = GameObject.Find("Elf").transform;
        currentAttackTime = Random.Range(attackTimeLow, attackTimeHigh);
        Invoke(nameof(ResetAttack), currentAttackTime);
    }

    private void Update()
    {
        //Check for sight and attack range
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        
        if (playerInAttackRange) AttackPlayer();
    }

    private void AttackPlayer()
    {
        transform.LookAt(player);
        if (!projectile.gameObject)
        {
            return;
        }

        
        // Attack the player
        if (!alreadyAttacked)
        {
            ///Attack code here
            Rigidbody rb = Instantiate(projectile, transform.position + new Vector3(0f, 0.2f, 0f), Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * ForwardForce, ForceMode.Impulse);
            rb.AddForce(transform.up * UpForce, ForceMode.Impulse);
            ///End of attack code

            alreadyAttacked = true;
            currentAttackTime = Random.Range(attackTimeLow, attackTimeHigh);
            Invoke(nameof(ResetAttack), currentAttackTime);
        }
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
