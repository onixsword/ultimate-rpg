using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combatant : Actor {

    [Header("Audios")]
    [SerializeField] protected AudioClip audioAttack;
    [SerializeField] protected AudioClip audioDamage;
    [SerializeField] protected AudioClip audioDeath;
    [SerializeField] protected AudioClip audioHit;
    [SerializeField] protected AudioClip audioWalk;

    [Header("Stats")]
    [SerializeField] protected int maxHealth;
    [SerializeField] public int damage;
    [SerializeField] protected int defense;
    protected int Health;

    [Header("DamageBox")]
    [Tooltip("Its required a pivot with the boxTrigger inside it")]
    [SerializeField] protected Transform damageBox;
    protected Vector3 damageBoxStartPosition;
    protected Vector3 damageBoxMaxScale;

    protected bool isAttacking;

    private bool grougi;
    private bool invulnerable;

	// Use this for initialization
	protected override void Start () {
        base.Start();
        Health = maxHealth;

        damageBoxStartPosition = damageBox.position;
        damageBoxMaxScale = damageBox.localScale;
        damageBox.localScale = Vector3.zero;
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DamageBox"))
        {
            GetHurt(other.transform.parent.parent.GetComponent<Combatant>().damage);    
        }
    }

    protected virtual void GetHurt(int inDamageValue)
    {
        if (!invulnerable)
        {
            Health -= (inDamageValue - defense);
            if (Health > 0)
            {
                //Anim.SetTrigger("Hurt");
                Aud.PlayOneShot(audioDamage);
            }
            else
            {
                //Anim.SetTrigger("Die");
                Aud.PlayOneShot(audioDeath);
                Destroy(gameObject, audioDeath.length);
            }

            invulnerable = true;
            StartCoroutine(ModifierCountDown(1, (x) => invulnerable = x));
        }
        
    }

    //Se inicializa con
    //StartCoroutine(ModifierCountDown(time, (x) => modifier = x));
    protected IEnumerator ModifierCountDown(float time, System.Action<bool> modifier)
    {
        yield return new WaitForSeconds(time);
        modifier(false);
    }

    protected virtual IEnumerator attack(float animationTime)
    {
        //Anim.SetTrigger("attack");

        Aud.PlayOneShot(audioAttack);
        yield return new WaitForSeconds(animationTime / 2);
        
        //Desplega el damageBox
        damageBox.localPosition = new Vector3(0, 0, 0);
        damageBox.localScale = Vector3.one;


        yield return new WaitForSeconds(animationTime / 2);
        //reset damagebox
        damageBox.localScale = Vector3.zero;
    }
}
