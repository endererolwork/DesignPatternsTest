using CommandPattern;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static CommandPattern.Command;

public class InputHandler : MonoBehaviour
{

    private Transform boxTransform;

    [SerializeField]
    public Stack<Command> previousCommands = new Stack<Command>();
    public List<RectTransform> inputTextTransform;
    public List<UnityEngine.UI.Button> uiButtons;

    public UnityEngine.UI.Button ShuffleButton, UndoButton;
    
    private Command buttonW, buttonA, buttonS, buttonD, buttonR;
    private List<Command> buttonList = new List<Command>();
    private List<Command> commandList = new List<Command>()
        {new MoveForward(), new MoveBackward(), new MoveRight(), new MoveLeft()};



    private ShuffleBag<Command> commandBag;

    private float _fiveSecondDelay = 0f;

    public int MovementRange = 4;  //gidebileceði max range
   

    private void Start()
    {
        boxTransform = GetComponent<Transform>();
        commandBag = new ShuffleBag<Command>(commandList.Count);
        foreach ( Command c in commandList)
        {
            commandBag.Add(c, 1);
        }

        ShuffleInputs(commandBag);
        buttonR = new UndoCommand();

        UndoButton.onClick.AddListener(() => { buttonR.Execute(boxTransform, buttonR); });
        ShuffleButton.onClick.AddListener(() => { ShuffleInputs(commandBag); });
    }


    private void Update()
    {
       
        ReadInput();



        _fiveSecondDelay += Time.deltaTime;
        if (_fiveSecondDelay >= 4)
        {
            _fiveSecondDelay = _fiveSecondDelay % 5f;
            ShuffleInputs(commandBag);
        }
    }

   

    void ReadInput()
    {

        if(Input.GetKeyDown(KeyCode.W))
        {
            buttonW.Execute(boxTransform, buttonW);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            buttonA.Execute(boxTransform, buttonA);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            buttonS.Execute(boxTransform, buttonS);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            buttonD.Execute(boxTransform, buttonD);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            buttonR.Execute(boxTransform, buttonR);
        }
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    ShuffleInputs(commandBag);
        //}

    }


    void ShuffleInputs(ShuffleBag<Command> bag)
    {
        
        buttonW = bag.Next();
        buttonA = bag.Next();
        buttonS = bag.Next();
        buttonD = bag.Next();
        UpdateUI();
    }


    void UpdateUI()
    {
        //update button onClick();
        ClearButtons();
        uiButtons[0].onClick.AddListener(() => { buttonW.Execute(boxTransform, buttonW); });
        uiButtons[1].onClick.AddListener(() => { buttonS.Execute(boxTransform, buttonS); });
        uiButtons[2].onClick.AddListener(() => { buttonA.Execute(boxTransform, buttonA); });
        uiButtons[3].onClick.AddListener(() => { buttonD.Execute(boxTransform, buttonD); });



        //update names
        inputTextTransform[0].GetComponent<TextMeshProUGUI>().SetText($"Input 1: {buttonW.GetType().Name}");
        inputTextTransform[1].GetComponent<TextMeshProUGUI>().SetText($"Input 2 : {buttonS.GetType().Name}");
        inputTextTransform[2].GetComponent<TextMeshProUGUI>().SetText($"Input 3 : {buttonA.GetType().Name}");
        inputTextTransform[3].GetComponent<TextMeshProUGUI>().SetText($"Input 4 : {buttonD.GetType().Name}");
    }


    void ClearButtons()
    {
        foreach (UnityEngine.UI.Button b in uiButtons)
        {
            b.onClick.RemoveAllListeners();
        }
    }


    void OnDestroy()
    {
        ClearButtons();
    }
}
