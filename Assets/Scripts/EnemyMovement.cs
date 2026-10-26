using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public GameObject player;
    private NavMeshAgent navMeshAgent;
    // Start is called before the first frame update

    private float maxHealth = 5;
    private float health;
    [SerializeField] HealthBar healthBar;

    Animator animator;
    Transform playerC;

    bool STOP_MOVING = false;

    void Start()
    {
        health = maxHealth;
        healthBar.GetComponent<HealthBar>().HealthBarChange(health, maxHealth);
        healthBar.HealthBarChange(health, maxHealth);

        navMeshAgent = GetComponent<NavMeshAgent>();
        Transform[] trs = transform.GetComponentsInChildren<Transform>(true);
        foreach (Transform t in trs)
        {
            if (t.name == "Doctor")
            {
                Debug.Log($"Found {t.name}");
                animator = t.GetComponent<Animator>();
                animator.SetBool("PlayerDead", false);
                break;
            }
        }

        Transform[] trs2 = player.transform.GetComponentsInChildren<Transform>(true);
        foreach (Transform t in trs2)
        {
            if (t.name == "SWAT")
            {
                Debug.Log("Found SWAT(player.transform)");
                playerC = t;
                break;
            }
        }
    }

    public void HitBy_Fireball()
    {
        health -= 1;
        healthBar.HealthBarChange(health, maxHealth);
        if (health < 2)
            healthBar.HealthBarColorChange(Color.red);
        if (health <= 0) (player.GetComponent<SWAT_Controller>()).PlayerWin();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H)) {
            STOP_MOVING = !STOP_MOVING;
            GameManager.GOD_MODE = !GameManager.GOD_MODE;
        }
        if (GameManager.GOD_MODE && STOP_MOVING)
        {
            navMeshAgent.SetDestination(transform.position);
            return;
        }
        if (player.transform != null)
        {
            navMeshAgent.SetDestination(player.transform.position);
            // Debug.Log(player.transform.position);
        }

        if (playerC.GetComponent<SWAT_Controller>().isLose)
        {
            animator.SetBool("PlayerDead", true);
            // Debug.Log("GameOver");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        /*
        Debug.Log($"�I����F: {collision.gameObject.name}�A���ҬO: {collision.gameObject.tag}");
        if (collision.gameObject.CompareTag("Fireball")) {
            print("hit by fireball");
            health -= 1;
            healthBar.HealthBarChange(health, maxHealth);
            if (health < 2)
                healthBar.HealthBarColorChange(Color.blue);
        };
        */
    }
}
