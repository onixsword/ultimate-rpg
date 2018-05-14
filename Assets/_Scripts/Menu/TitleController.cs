using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleController : MonoBehaviour {

    [SerializeField]
    Transform optionsParent;
    [SerializeField]
    EventSystem eventSystem;
    [SerializeField]
    List<Button> buttons;

        
    [SerializeField]
    GameObject warningPanel;
    [SerializeField]
    Button newGameButton;

    [SerializeField]
    Button noOption;

    [SerializeField]
    GameObject continueButton;

    [SerializeField]
    AudioSource audioEffects;
    [SerializeField]
    AudioClip accept;
    [SerializeField]
    AudioClip cancel;
    [SerializeField]
    AudioClip move;


    protected void Start()
    {
        continueButton.SetActive(gameManager.instance.existData);
            
        InitializeMenu();

        //check menu
        eventSystem.firstSelectedGameObject = buttons[0].gameObject;
        buttons[0].Select();
    }

    public void InitializeMenu()
    {

        for (int i = 0; i < optionsParent.childCount; i++)
        {
            Button button = optionsParent.GetChild(i).GetComponent<Button>();
            if (button.gameObject.activeSelf)
            {
                buttons.Add(button);
            }
        }

        Button first = buttons[0];
        Button last = buttons.Last<Button>();
        Navigation btnFisrtNav = first.navigation;
        Navigation btnLasttNav = last.navigation;
        btnFisrtNav.mode = Navigation.Mode.Explicit;
        btnLasttNav.mode = Navigation.Mode.Explicit;
        btnFisrtNav.selectOnUp = last;
        btnFisrtNav.selectOnDown = buttons[1];
        btnLasttNav.selectOnUp = buttons[buttons.Count - 2];
        btnLasttNav.selectOnDown = first;
        first.navigation = btnFisrtNav;
        last.navigation = btnLasttNav;
    }

    public void NewGame()
    {
        if (gameManager.instance.existData)
        {
            warningPanel.SetActive(true);
            noOption.Select();
        }
        else
        {
            //ac.PlayClipOnce(0);
            gameManager.instance.GameData = new GameData("0", 0, new Vector3(0, 0, 0), 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
            gameManager.instance.saveGame();

            //load
            gameManager.instance.loadGame();
            SceneManager.LoadScene(1);
            gameManager.instance.ChangeBackGroundMusic(1);
        }

        audioEffects.clip = accept;
        audioEffects.Play();
    }

    public void CancelNewGame()
    {
        audioEffects.clip = cancel;
        audioEffects.Play();
        warningPanel.SetActive(false);
        newGameButton.Select();

    }

    public void AcceptNewGame()
    {
        warningPanel.SetActive(false);
        gameManager.instance.GameData = new GameData("0", 0, new Vector3(0, 0, 0), 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
        gameManager.instance.saveGame();

        //load
        audioEffects.clip = accept;
        audioEffects.Play();
        gameManager.instance.loadGame();
        SceneManager.LoadScene(1);
        gameManager.instance.ChangeBackGroundMusic(1);
    }

    public void Continue()
    {
        //load
        audioEffects.clip = accept;
        audioEffects.Play();
        gameManager.instance.loadGame();
        SceneManager.LoadScene(1);
        gameManager.instance.ChangeBackGroundMusic(1);
    }

    public void moveSound()
    {
        audioEffects.clip = move;
        audioEffects.Play();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
