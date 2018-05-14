using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {

    [SerializeField]
    EventSystem eventSystem;

    [Header("Options")]
    [SerializeField]
    Transform optionsParent;
    [SerializeField]
    List<Button> buttons;

    [Header("Inventory")]
    [SerializeField]
    GameObject InventoryPanel;
    [SerializeField]
    GridLayoutGroup inventoryGrid;
    [SerializeField]
    List<Button> InventoryButtons;
    [SerializeField]
    Button backButton;

    [Header("WarningPanel")]
    [SerializeField]
    Button QuitButton;
    [SerializeField]
    GameObject warningPanel;
    [SerializeField]
    Button noOption;

    [Header("Audios")]
    [SerializeField] AudioSource audioEffects;
    [SerializeField] AudioClip Accept;
    [SerializeField] AudioClip Cancel;
    [SerializeField] AudioClip Move;


    /*[SerializeField]
    AudioController ac;*/


    protected void Awake()
    {
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

    public void Inventory()
    {
        InventoryPanel.SetActive(true);
        InventoryButtons[0].Select();
        audioEffects.clip = Accept;
        audioEffects.Play();
    }

    public void back()
    {
        InventoryPanel.SetActive(false);
        buttons[0].Select();
        audioEffects.clip = Cancel;
        audioEffects.Play();
    }

    public void showWarning()
    {
        warningPanel.SetActive(true);
        noOption.Select();
        audioEffects.clip = Accept;
        audioEffects.Play();
    }

    public void CancelQuit()
    {
        warningPanel.SetActive(false);
        QuitButton.Select();
        audioEffects.clip = Cancel;
        audioEffects.Play();
    }

    public void AcceptQuit()
    {
        audioEffects.clip = Accept;
        audioEffects.Play();
        warningPanel.SetActive(false);
        gameManager.instance.IsPaused = false;
        SceneManager.LoadScene(0);
        gameManager.instance.ChangeBackGroundMusic(0);
    }

    public void SaveGame()
    {
        audioEffects.clip = Accept;
        audioEffects.Play();
        gameManager.instance.saveGame();
    }

    public void moveSound()
    {
        audioEffects.clip = Move;
        audioEffects.Play();
    }

}
