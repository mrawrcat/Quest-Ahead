﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    const string PLAYER_RUN = "knight top run";
    const string PLAYER_ATTACK = "knight top slash";

    [Header("Ability Stuff")]
    public float Atk_Radius;
    public Transform Atk_Pos;
    public bool holding_atk;
    public float holding_atk_timer;
    public float hit_pause_dur;

    [SerializeField]
    private float attack_delay = 0.5f;

    [Header("Layers")]
    public LayerMask whatIsEnemy;

    [SerializeField]
    private ObjectPoolNS bullet_pool;

    [SerializeField]
    private Animator anim;

    private bool attacking;
    private string current_state;
    //private CamShake shake;
    //private bool isFrozen = false;
    // Start is called before the first frame update
    void Start()
    {
        //anim = FindObjectOfType<PlayerUpperAnim>().gameObject.GetComponent<Animator>();
        //shake = FindObjectOfType<CamShake>();
    }

    // Update is called once per frame
    void Update()
    {
        KeyboardInputAttack();
    }

    public void KeyboardInputAttack()
    {
        
        if (Input.GetKey(KeyCode.X))
        {
            if (!GameManager.manager.movingReset && !GameManager.manager.dead)
            {
                holding_atk_timer += Time.deltaTime;
                holding_atk = true;
            }
            else
            {
                holding_atk_timer = 0;
            }
        }
        if (Input.GetKeyUp(KeyCode.X))
        {
            if (!GameManager.manager.movingReset && !GameManager.manager.dead)
            {
                if (holding_atk_timer > 1f)
                {
                    Attack();
                    //Shoot_Bullet();
                    Debug.Log("holding power atk");
                }
                else
                {
                    Attack();
                    Debug.Log("regular attack");
                }

            }
            holding_atk_timer = 0;
            holding_atk = false;
        }

    }

    public void Shoot_Bullet()
    {
        //bullet_pool.SpawnProjectile(Atk_Pos, Quaternion.identity);
    }

    public void Attack()
    {
        if (!attacking)
        {
            attacking = true;
            Change_Anim_State(PLAYER_ATTACK);
            //attack_delay = anim.GetCurrentAnimatorStateInfo(0).length;
            Invoke("Complete_Attack", attack_delay);
            Collider2D[] hit_Enemies = Physics2D.OverlapCircleAll(Atk_Pos.position, Atk_Radius, whatIsEnemy);
            foreach (Collider2D enemy in hit_Enemies)
            {
                //shake.Shake();
                /*
                if (enemy.GetComponent<Barrel>() != null)
                {
                    //GameManager.manager.smash_sfx.Play();
                    if (!isFrozen)
                    {
                        StartCoroutine(hit_pause(hit_pause_dur));
                    }
                    enemy.GetComponent<Barrel>().Take_Dmg();
                }
                if (enemy.GetComponent<EnemyProjectile>() != null)
                {
                    //GameManager.manager.smash_sfx.Play();
                    if (!isFrozen)
                    {
                        StartCoroutine(hit_pause(hit_pause_dur));
                    }
                    Debug.Log("attacked enemy projectile");
                    enemy.GetComponent<EnemyProjectile>().Atk_Projectile();
                }
                if (enemy.GetComponent<IEnemy>() != null)
                {
                    SoundManager.sound_manager.smash_sfx.Play();
                    if (!isFrozen)
                    {
                        StartCoroutine(hit_pause(hit_pause_dur));
                    }
                    enemy.GetComponent<IEnemy>().Die();
                }

                */
            }
        }
    }
    private void Complete_Attack()
    {
        attacking = false;
        Change_Anim_State(PLAYER_RUN);
    }

    public IEnumerator hit_pause(float dur)//for pause when melee hit maybe bullet hit too?
    {
        //isFrozen = true;
        var original_timescale = Time.timeScale;
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(dur);
        Time.timeScale = original_timescale;
        //isFrozen = false;
    }

    private void Change_Anim_State(string newState)
    {
        if (current_state == newState)
        {
            return;
        }

        anim.Play(newState);
        current_state = newState;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(Atk_Pos.position, Atk_Radius);
    }
}
