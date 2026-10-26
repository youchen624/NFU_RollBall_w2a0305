using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.InputSystem;
using TMPro;

public class SWAT_Controller : MonoBehaviour
{

    float moveAmount;
    float turnAmount;
    float turnSpeed = 100;
    private Animator animator;

    [SerializeField] private GameObject SWAT_exp;
    [SerializeField] private GameObject Doctor_exp;

    public bool isLose = false;
    //GameManager gm;

    float currenHealth_v;
    float maxHealth_v = 4;
    [SerializeField] HealthBar healthBar;

    public int winCount = 5;
    public GameObject MysteryBoxParticlePrefab;
    public GameObject BumpParticlePrefab;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI winText;
    public GameObject sfx;
    private int count = 0;

    bool isFiring = false;
    [SerializeField] GameObject fireball;
    [SerializeField] GameObject FireSpawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        //gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        currenHealth_v = maxHealth_v;
        healthBar.GetComponent<HealthBar>().HealthBarChange(currenHealth_v, maxHealth_v);
    }

    // Update is called once per frame
    void Update()
    {
        moveAmount = Input.GetAxis("Vertical");
        turnAmount = Input.GetAxis("Horizontal");
        animator.SetFloat("MoveAmount", moveAmount);
        animator.SetBool("IsBackwalking", (moveAmount < 0));

        // rotation actions
        transform.Rotate(Vector3.up, turnAmount * turnSpeed * Time.deltaTime, Space.Self);

        // into running
        animator.SetBool("IsRunning", (Input.GetKey(KeyCode.LeftShift) && moveAmount > 0));

        if (moveAmount == 0)    // rotation animes
        {
            animator.SetBool("IsLeftTurning", (turnAmount < 0));
            animator.SetBool("IsRightTurning", (turnAmount > 0));
        }
        else
        {
            animator.SetBool("IsLeftTurning", false);
            animator.SetBool("IsRightTurning", false);
        }

        if (Input.GetKeyDown(KeyCode.LeftControl) && !isFiring) {
            if (!CheckDistanceOK(2f)) return;
            isFiring = true;
            animator.SetBool("IsRunning", false);
            animator.SetBool("IsBackwalking", false);
            animator.SetBool("IsFireball", true);
            StartCoroutine(shootFireball());
        }

        // goto spawn
        if (GameManager.GOD_MODE && Input.GetKeyDown(KeyCode.J)) transform.position = new Vector3(0, 0.75f, 0);

        // ray drawing
        Ray ray = new Ray(transform.position + new Vector3(0, 0.75f, 0), transform.forward);
        Debug.DrawRay(ray.origin, transform.forward * 10, Color.magenta);
    }

    private bool CheckDistanceOK(float dist)
    {
        Ray ray = new Ray(transform.position + new Vector3(0, 0.75f, 0), transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, dist))
        {
            if (hit.collider.gameObject.CompareTag("Obstacle") || hit.collider.gameObject.CompareTag("Enemy"))
                return false;
        }
        return true;
    }

    IEnumerator shootFireball() {
        while (!animator.GetCurrentAnimatorStateInfo(0).IsName("Fireball"))
        {
            yield return new WaitForSeconds(0.001f);
        }
        yield return new WaitForSeconds(1.0f);
        sfx.GetComponent<SFXManager>().playFirecracker_fire();
        yield return new WaitForSeconds(1.0f);
        Instantiate(fireball, FireSpawnPoint.transform.position, FireSpawnPoint.transform.rotation);
        yield return new WaitForSeconds(0.5f);
        animator.SetBool("IsFireball", false);
        isFiring = false;
    }

    public void PlayerWin()
    {
        winText.gameObject.SetActive(true);
        sfx.GetComponent<SFXManager>().playWin();

        GameManager.gameOver_lose = false;   // won
        GameManager.shouldGameOver = true;

        StartCoroutine(DelyayWin(2));
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MysteryBox"))
        {
            other.gameObject.SetActive(false);
            GameObject obj = Instantiate(MysteryBoxParticlePrefab, other.transform.position, Quaternion.identity);
            Destroy(obj, 2);
            sfx.GetComponent<SFXManager>().playEating();
            ++count;
            scoreText.text = "count = " + count + " / " + winCount;
            if (count >= winCount)  // win
            {
                PlayerWin();
                /*
                winText.gameObject.SetActive(true);
                sfx.GetComponent<SFXManager>().playWin();
                //GameManager.gm.gameOver_lose = false;   // won
                //GameManager.gm.shouldGameOver = true;

                GameManager.gameOver_lose = false;   // won
                GameManager.shouldGameOver = true;

                StartCoroutine(DelyayWin(2));
                if (!GameObject.FindGameObjectWithTag("Enemy")) return;
                sfx.GetComponent<SFXManager>().playExp2();
                GameObject obj_enemy = Instantiate(Doctor_exp, GameObject.FindGameObjectWithTag("Enemy").transform.position, Quaternion.identity);
                Destroy(obj_enemy, 2);
                GameObject.FindGameObjectWithTag("Enemy")?.SetActive(false);
                */
            }
            // Debug.Log($"count = {count}");
        }
    }
    private void lose()
    {

        winText.text = "YOU LOSE";
        winText.gameObject.SetActive(true);
        GameObject obj = Instantiate(SWAT_exp, transform.position, Quaternion.identity);
        Destroy(obj, 2);
        //sfx.GetComponent<SFXManager>().playEating();
        //isLose = true;
        //if (GameManager.gm)
        //{
        //    GameManager.gm.gameOver_lose = true;
        //    GameManager.gm.shouldGameOver = true;
        //}
        GameManager.gameOver_lose = true;
        GameManager.shouldGameOver = true;
        GameManager.shouldReturn = true;

        GameManager.gm.SwitchToVirtualCamera("VCam2");
        // else Debug.Log("FUCK GM");
        sfx.GetComponent<SFXManager>().playLose();
        sfx.GetComponent<SFXManager>().playDoctorExpSound();
        gameObject.SetActive(false);
    }

    IEnumerator DelyayWin(float waitTime)
    {
        if (GameManager.gm==null) Debug.LogError("GM not found");
        GameManager.gm.SwitchToVirtualCamera("VCam2");
        yield return new WaitForSeconds(waitTime);

        //if (!GameObject.FindGameObjectWithTag("Enemy")) return;
        sfx.GetComponent<SFXManager>().playExp2();
        GameObject obj_enemy = Instantiate(Doctor_exp, GameObject.FindGameObjectWithTag("Enemy").transform.position, Quaternion.identity);
        Destroy(obj_enemy, 2);
        GameObject.FindGameObjectWithTag("Enemy")?.SetActive(false);
        StartCoroutine(DelyayRetrunMenu(waitTime));
    }

    IEnumerator DelyayRetrunMenu(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        GameManager.shouldReturn = true;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (GameManager.GOD_MODE) return;
        if (collision.gameObject.CompareTag("Enemy"))   // lose
        {
            lose();
            /*
            winText.text = "YOU LOSE";
            winText.gameObject.SetActive(true);
            GameObject obj = Instantiate(SWAT_exp, transform.position, Quaternion.identity);
            Destroy(obj, 2);
            //sfx.GetComponent<SFXManager>().playEating();
            //isLose = true;
            //if (GameManager.gm)
            //{
            //    GameManager.gm.gameOver_lose = true;
            //    GameManager.gm.shouldGameOver = true;
            //}
            GameManager.gameOver_lose = true;
            GameManager.shouldGameOver = true;
            // else Debug.Log("FUCK GM");
            sfx.GetComponent<SFXManager>().playLose();
            sfx.GetComponent<SFXManager>().playDoctorExpSound();
            gameObject.SetActive(false);
            */
        }
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            currenHealth_v -= 1;
            healthBar.HealthBarChange(currenHealth_v, maxHealth_v);
            if (currenHealth_v < 2)
                healthBar.HealthBarColorChange(Color.red);
            if ((currenHealth_v < 1) && (!GameManager.GOD_MODE))
            {
                GameManager.gm.SwitchToVirtualCamera("VCam2");
                lose();
            }
            GameObject obj = Instantiate(BumpParticlePrefab, transform.position, Quaternion.identity);
            Destroy(obj, 2);
            sfx.GetComponent<SFXManager>().playBumping();
        }
    }
}
