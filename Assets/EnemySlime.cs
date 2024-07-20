using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySlime : MonoBehaviour
{
    // Start is called before the first frame update

    // Update is called once per frame
    [SerializeField] public Animator animator;
    public float timeBtwAttack = 2f;
    [SerializeField] public Transform ShootPos;
    [SerializeField] public GameObject SlimeShoot;
    [SerializeField] private EnemyMaster enemyMaster;
    [SerializeField] public float startTimeBtwAttack = 4f;


    private void Start()
    {
        enemyMaster = GetComponent<EnemyMaster>();
        timeBtwAttack = 0;
    }
    void Update()
    {
        Shoot();
    }

    public void Shoot()
    {
        if (timeBtwAttack <= 0)
        {
            SoundManager.PlaySound(SoundManager.Sound.SlimeShoot);
            animator.SetBool("isShooting", true);
            Instantiate(SlimeShoot, ShootPos.position, ShootPos.rotation);
            timeBtwAttack = startTimeBtwAttack;
        }
        else
        {
            timeBtwAttack -= Time.deltaTime;
            animator.SetBool("isShooting", false);
        }
    }
}
