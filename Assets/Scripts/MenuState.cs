using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

public class MenuState
{
    public GameObject _buttonPrefab;
    public GameObject _textPrefab;
    public GameObject _sliderPrefab;
    public GameObject _imagePrefab;
    public GameObject _panelPrefab;
    public GameObject _inputPrefab;

    public MenuManager _menuManager;
    protected GameObject canvas;
    protected Transform canvasTrans;

    protected List<GameObject> menuItems = new List<GameObject>();

    protected delegate void SliderFunc(float sliderVal);
    protected delegate void ButtonFunc();
    protected delegate void InputFunc(string val);

    public virtual void Init(GameObject buttonPrefab, GameObject textPrefab, GameObject sliderPrefab, GameObject imagePrefab, GameObject panelPrefab, GameObject inputPrefab, MenuManager menuManager)
    {
        _buttonPrefab = buttonPrefab;
        _textPrefab = textPrefab;
        _sliderPrefab = sliderPrefab;
        _imagePrefab = imagePrefab;
        _panelPrefab = panelPrefab;
        _inputPrefab = inputPrefab;
        _menuManager = menuManager;
        canvas = GameObject.Find("Canvas");
        canvasTrans = canvas.transform;
    }
    public virtual void Enter()
    {
        menuItems.Clear();
        for (int i = 0; i < canvas.transform.childCount; i++)
        {
            GameObject.Destroy(canvas.transform.GetChild(i).gameObject);
        }
    }
    public virtual void Refresh()
    {

    }

    public void ChangeStateOnClick(string state)
    {
        _menuManager.ChangeState(state);
    }

    protected GameObject GenerateMenuButton(int index, string text, string changeState)
    {
        GameObject result = ((GameObject)GameObject.Instantiate(_buttonPrefab, Vector3.zero, Quaternion.identity));
        Text menuText;
        Button menuButton;
        result.transform.SetParent(canvasTrans);
        result.transform.localPosition = new Vector3(0, 0 - 50 * index, 0);
        result.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        menuText = result.transform.GetChild(0).GetComponent<Text>();
        menuText.text = text;
        menuButton = result.transform.GetComponent<Button>();
        menuButton.onClick.AddListener(() => ChangeStateOnClick(changeState));
        return result;
    }

    protected GameObject GenerateMenuButton(Vector3 position, string text, string changeState)
    {
        GameObject result = ((GameObject)GameObject.Instantiate(_buttonPrefab, Vector3.zero, Quaternion.identity));
        Text menuText;
        Button menuButton;
        result.transform.SetParent(canvasTrans);
        result.transform.localPosition = position;
        result.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        menuText = result.transform.GetChild(0).GetComponent<Text>();
        menuText.text = text;
        menuButton = result.transform.GetComponent<Button>();
        menuButton.onClick.AddListener(() => ChangeStateOnClick(changeState));
        return result;
    }

    protected GameObject GenerateMenuButton(int index, string text, ButtonFunc buttonOnClick)
    {
        GameObject result = ((GameObject)GameObject.Instantiate(_buttonPrefab, Vector3.zero, Quaternion.identity));
        Text menuText;
        Button menuButton;
        result.transform.SetParent(canvasTrans);
        result.transform.localPosition = new Vector3(0, 0 - 50 * index, 0);
        result.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        menuText = result.transform.GetChild(0).GetComponent<Text>();
        menuText.text = text;
        menuButton = result.transform.GetComponent<Button>();
        menuButton.onClick.AddListener(() => buttonOnClick());
        return result;
    }

    protected GameObject GenerateMenuButton(Vector3 position, string text, ButtonFunc buttonOnClick)
    {
        GameObject result = ((GameObject)GameObject.Instantiate(_buttonPrefab, Vector3.zero, Quaternion.identity));
        Text menuText;
        Button menuButton;
        result.transform.SetParent(canvasTrans);
        result.transform.localPosition = position;
        result.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        menuText = result.transform.GetChild(0).GetComponent<Text>();
        menuText.text = text;
        menuButton = result.transform.GetComponent<Button>();
        menuButton.onClick.AddListener(() => buttonOnClick());
        return result;
    }

    protected GameObject GenerateMenuText(Vector3 position, string displayText)
    {
        GameObject result = ((GameObject)GameObject.Instantiate(_textPrefab, Vector3.zero, Quaternion.identity));
        Text menuText;
        result.transform.SetParent(canvasTrans);
        result.transform.localPosition = position;
        result.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        menuText = result.transform.GetComponent<Text>();
        menuText.text = displayText;
        return result;
    }

    protected GameObject GenerateMenuText(Vector3 position, int fontSize, string displayText)
    {
        GameObject result = ((GameObject)GameObject.Instantiate(_textPrefab, Vector3.zero, Quaternion.identity));
        Text menuText;
        result.transform.SetParent(canvasTrans);
        result.transform.localPosition = position;
        result.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        menuText = result.transform.GetComponent<Text>();
        menuText.text = displayText;
        menuText.fontSize = fontSize;
        menuText.horizontalOverflow = HorizontalWrapMode.Overflow;
        return result;
    }

    protected GameObject GenerateMenuSlider(Vector3 position, Color sliderColour, SliderFunc slideFunc)
    {
        GameObject result = ((GameObject)GameObject.Instantiate(_sliderPrefab, Vector3.zero, Quaternion.identity));
        Slider menuSlider;
        Image highlightedFill;
        result.transform.SetParent(canvasTrans);
        result.transform.localPosition = position;
        result.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        menuSlider = result.transform.GetComponent<Slider>();
        menuSlider.onValueChanged.AddListener((x) => slideFunc(x));
        highlightedFill = menuSlider.transform.GetChild(1).transform.GetChild(0).GetComponent<Image>();
        highlightedFill.color = sliderColour;
        return result;
    }

    protected GameObject GenerateMenuImage(Vector3 position)
    {
        GameObject result = ((GameObject)GameObject.Instantiate(_imagePrefab, Vector3.zero, Quaternion.identity));
        Image menuImage;
        result.transform.SetParent(canvasTrans);
        result.transform.localPosition = position;
        result.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        menuImage = result.transform.GetComponent<Image>();
        menuImage.color = Color.red;
        return result;
    }

    protected GameObject GenerateMenuPanel(Vector3 position, Vector2 size)
    {
        GameObject result = ((GameObject)GameObject.Instantiate(_panelPrefab, Vector3.zero, Quaternion.identity));
        RectTransform rectSize;
        result.transform.SetParent(canvasTrans);
        result.transform.localPosition = position;
        result.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        rectSize = result.transform.GetComponent<RectTransform>();
        rectSize.sizeDelta = size;
        return result;
    }

    protected GameObject GenerateMenuInput(Vector3 position, InputFunc inputFunc)
    {
        GameObject result = ((GameObject)GameObject.Instantiate(_inputPrefab, Vector3.zero, Quaternion.identity));
        InputField menuInput;
        result.transform.SetParent(canvasTrans);
        result.transform.localPosition = position;
        result.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        menuInput = result.transform.GetComponent<InputField>();
        menuInput.onValueChanged.AddListener((x) => inputFunc(x));
        return result;
    }
}


public class MainMenuState : MenuState
{
    string input;
    public override void Enter()
    {
        base.Enter();
        GenerateMenuButton(0, "Play", "PlayMenu");
        GenerateMenuButton(1, "Options", "CustomisationMenu");
        GenerateMenuButton(2, "Credits", "CreditsMenu");
        GenerateMenuButton(3, "Quit", QuitGame);
        GenerateMenuText(new Vector3(0, 150, 0), 20, "Main Menu");
    }

    protected void QuitGame()
    {
        Application.Quit();
    }
}


public class PlayMenuState : MenuState
{
    GameManager gameManager;
    public override void Enter()
    {
        base.Enter();
        gameManager = GameObject.Find("Main Camera").GetComponent<GameManager>() as GameManager;
        if (gameManager != null)
            gameManager.BeginGame();
        menuItems.Add(GenerateMenuText(new Vector3(0, 150, 0), 20, "Score: " + gameManager.Score));
        menuItems.Add(GenerateMenuText(new Vector3(0, 130, 0), 20, "Press Esc to return to main menu"));
        menuItems.Add(GenerateMenuText(new Vector3(0,20,0),20,"GAME OVER!"));
    }
    public override void Refresh()
    {
        menuItems[0].GetComponent<Text>().text = "Score: " + gameManager.Score;
        if (!gameManager.GameOver) menuItems[2].gameObject.SetActive(false);
        else menuItems[2].gameObject.SetActive(true);
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameManager.StopGame();
            gameManager.CancelInvoke();
            _menuManager.ChangeState("MainMenu");
        }
        base.Refresh();
    }
}


public class CustomisationMenuState : MenuState
{
    Color boxColour = Color.black;
    public override void Enter()
    {
        base.Enter();
        menuItems.Add(GenerateMenuText(new Vector3(0, 100, 0), 20, "Options"));
        menuItems.Add(GenerateMenuSlider(new Vector3(0, 0, 0), Color.red, RedSliderFunc));
        menuItems.Add(GenerateMenuSlider(new Vector3(0, -50, 0), Color.green, GreenSliderFunc));
        menuItems.Add(GenerateMenuSlider(new Vector3(0, -100, 0), Color.blue, BlueSliderFunc));
        menuItems.Add(GenerateMenuImage(new Vector3(150, 0, 0)));
        menuItems.Add(GenerateMenuButton(5, "Back", "MainMenu"));
        menuItems[1].GetComponent<Slider>().value = PlayerPrefs.GetFloat("PlayerR");
        menuItems[2].GetComponent<Slider>().value = PlayerPrefs.GetFloat("PlayerG");
        menuItems[3].GetComponent<Slider>().value = PlayerPrefs.GetFloat("PlayerB");
        menuItems[4].GetComponent<Image>().color = boxColour;
    }

    protected void RedSliderFunc(float x)
    {
        boxColour.r = x;
    }

    protected void GreenSliderFunc(float x)
    {
        boxColour.g = x;
    }

    protected void BlueSliderFunc(float x)
    {
        boxColour.b = x;
    }

    public override void Refresh()
    {
        menuItems[4].GetComponent<Image>().color = boxColour;
        PlayerPrefs.SetFloat("PlayerR", boxColour.r);
        PlayerPrefs.SetFloat("PlayerG", boxColour.g);
        PlayerPrefs.SetFloat("PlayerB", boxColour.b);
        base.Refresh();
    }
}

public class CreditsMenuState : MenuState
{
    public override void Enter()
    {
        base.Enter();
        GenerateMenuButton(5, "Back", "MainMenu");
        GenerateMenuText(new Vector3(0, 100, 0), 25, "Credits");
        GenerateMenuText(new Vector3(10, 60, 0), 20, "Lead Developer: Conor Wood");
    }
    public override void Refresh()
    {

    }
}