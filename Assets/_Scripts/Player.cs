using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : Combatant {

    [Header("UI")]
    [SerializeField] private Image CharImage;
    [SerializeField] private Slider HealthBar;
    [SerializeField] private Slider ManaBar;
    [SerializeField] public TextBox textBox;
    [SerializeField] private GameObject PausePanel;

    public Interactuable interactionTarget;

    protected override void Awake()
    {
        base.Awake();
        gameManager.instance.player = this.transform;
    }

    private void Update()
    {
        //Controles dentro del juego
        if (!gameManager.instance.IsPaused)
        {
            changeAxis();
            if (Rigid.velocity.magnitude > 0f)
            {
                if(!Aud.isPlaying) Aud.Play();
            }
            else
            {
                Aud.Stop();
            }

            //interactua con objeto
            if (Input.GetButtonDown("Fire1") && interactionTarget != null)
            {
                gameManager.instance.IsPaused = true;
                gameManager.instance.InScene = true;
                interactionTarget.nextDialogue();
            } else if (Input.GetButtonDown("Fire1") && !isAttacking) StartCoroutine(attack(1));

            if (Input.GetButtonDown("Cancel"))
            {
                if (!gameManager.instance.InScene) pauseGame();
            }
        }
        //controles de escena
        else if (gameManager.instance.InScene)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                interactionTarget.nextDialogue();
            }
            if (Input.GetButtonDown("Cancel"))
            {
                interactionTarget.endConversation();
            }
        }
        //Controles de pausa
        else if (Input.GetButtonDown("Cancel"))
        {
            if (!gameManager.instance.InScene) pauseGame();
        }

        if (Health <= 0) SceneManager.LoadScene(0);
    }

    private void changeAxis()
    {
        if (!isAttacking) Axis = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        else Axis = Vector2.zero;
    }

    private void pauseGame()
    {
        gameManager.instance.IsPaused = !gameManager.instance.IsPaused;
        PausePanel.SetActive(!PausePanel.activeSelf);

    }

    protected override IEnumerator attack(float animationTime)
    {
        isAttacking = true;

        //Anim.SetTrigger("attack");

        yield return new WaitForSeconds(animationTime / 2);
        //Desplega el damageBox
        Aud.PlayOneShot(audioAttack);
        damageBox.localPosition = Vector3.zero;
        damageBox.localScale = Vector3.one;


        yield return new WaitForSeconds(animationTime / 2);
        //reset damagebox
        damageBox.localScale = Vector3.zero;

        isAttacking = false;
    }

    protected override void GetHurt(int inDamageValue)
    {
        base.GetHurt(inDamageValue);
        HealthBar.value = (float)Health / (float)maxHealth;
    }

    /*public void AlterStats()
    {
        gameManager.instance.GameData = new GameData();
    }*/
}
