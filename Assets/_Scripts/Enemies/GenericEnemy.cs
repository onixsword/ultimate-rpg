using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]

public class GenericEnemy : Combatant {

    [Header("Enemy IA Values")]
    [SerializeField] float patrolTime;
    [SerializeField] float visionRange;
    [SerializeField] float visionDistance;
    [SerializeField] float chaseDistance;
    [SerializeField] float attackDistance;

    NavMeshAgent Nav;

    public enum botStates { waiting, chase, attack };
    public botStates enemyState;

    private bool isPatrolling;

    protected override void Start()
    {
        base.Start();
        enemyState = botStates.waiting;
        Nav = GetComponent<NavMeshAgent>();
        Nav.speed = Speed;
    }

    private void Update()
    {
        //pausa
        if (gameManager.instance.IsPaused) return;

        //IA
        switch (enemyState)
        {
            case botStates.waiting:
                //inicia patrullaje en caso de no haberse activado ya
                if (!isPatrolling) StartCoroutine(patrol(patrolTime));
                //revisa constamente en busqueda del objetivo a atacar
                if (Vector3.Distance(gameManager.instance.player.position, transform.position) <= visionDistance)
                {
                    if (Vector3.Angle(gameManager.instance.player.position, transform.position) <= visionRange)
                    {
                        enemyState = botStates.chase;
                    }
                }
                break;
            case botStates.chase:
                if (!Aud.isPlaying) Aud.Play();
                if (Axis != Vector2.zero) Axis = Vector2.zero;
                if (Vector3.Distance(gameManager.instance.player.position, transform.position) <= chaseDistance)
                {
                    if (Vector3.Distance(gameManager.instance.player.position, transform.position) <= attackDistance)
                        enemyState = botStates.attack;
                    else
                    {
                        Nav.SetDestination(gameManager.instance.player.position);
                    }
                }
                else enemyState = botStates.waiting;
                break;
            case botStates.attack:
                Aud.Stop();
                if (!isAttacking)
                {
                    transform.LookAt(gameManager.instance.player);
                    StartCoroutine(attack(1));
                }
                break;
            default:
                Debug.Log("Error en update del objeto" + transform.name);
                break;
        }
    }

    //patruyaje
    IEnumerator patrol(float time)
    {
        isPatrolling = true;
        while (enemyState == botStates.waiting)
        {
            if (gameManager.instance.IsPaused)
            {
                isPatrolling = false;
                yield break;
            }
            if (Axis == Vector2.zero)
            {
                Axis = (new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f))).normalized;
                if (!Aud.isPlaying) Aud.Play();
            }
            else
            {
                Aud.Stop();
                Axis = Vector2.zero;
            }
            yield return new WaitForSeconds(time);
        }
        isPatrolling = false;
    }

    protected override IEnumerator attack(float animationTime)
    {
        isAttacking = true;

        //Anim.SetTrigger("attack");

        yield return new WaitForSeconds(animationTime / 2);
        //Desplega el damageBox
        Aud.PlayOneShot(audioAttack);
        damageBox.localPosition = new Vector3(0, 0, 0);
        damageBox.localScale = Vector3.one;


        yield return new WaitForSeconds(animationTime / 2);
        //reset damagebox
        damageBox.localScale = Vector3.zero;

        isAttacking = false;

        enemyState = botStates.chase;
    }

}
