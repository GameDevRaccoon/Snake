using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MenuManager : MonoBehaviour
{


    public Dictionary<string, MenuState> states = new Dictionary<string, MenuState>();
    public MenuState currentState;
    public GameObject menuButton, menuText, menuSlider, menuImage, menuPanel, menuInput;

    void Awake()
    {
        states.Add("MainMenu", new MainMenuState());
        states.Add("PlayMenu", new PlayMenuState());
        states.Add("CreditsMenu", new CreditsMenuState());
        states.Add("CustomisationMenu", new CustomisationMenuState());
        foreach (KeyValuePair<string, MenuState> i in states)
        {
            states[i.Key].Init(menuButton, menuText, menuSlider, menuImage, menuPanel, menuInput, this);
        }
        ChangeState("MainMenu");
    }

    public void ChangeState(string state)
    {
        if (states.ContainsKey(state))
        {
            currentState = states[state];
            currentState.Enter();
        }
    }

    // Update is called once per frame
    void Update()
    {
        currentState.Refresh();
    }
}
