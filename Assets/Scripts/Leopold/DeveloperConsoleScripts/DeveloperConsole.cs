using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

namespace Console
{
    public abstract class ConsoleCommand
    {
        public abstract string Name { get; protected set; }
        public abstract string Command { get; protected set; }
        public abstract string Description { get; protected set; }
        public abstract string Help { get; protected set; }

        public void AddCommandToConsole(){
            string addMessage = " command has been added to the console.";

            DeveloperConsole.AddCommandToConsole(Command, this);
            DeveloperConsole.AddStaticMessageToConsole(Name + addMessage);
        }

        public abstract void RunCommand();
    }

    public class DeveloperConsole : MonoBehaviour
    {
        public static  DeveloperConsole Instance { get; private set; }
        public static Dictionary<string, ConsoleCommand> Commands {get; private set;}

        [Header("UI Components")]
        public Canvas consoleCanvas;
        public ScrollRect scrollRect;
        public TextMeshProUGUI consoleText;
        public TextMeshProUGUI inputText;
        public TMP_InputField consoleInput;


        private void Awake(){
            if(Instance != null){
                return;
            }

            Instance = this;
            Commands = new Dictionary<string, ConsoleCommand>();
        }

        private void Start(){
            consoleCanvas.gameObject.SetActive(false);
            CreateCommands();
        }

        public void Update(){
            if(Input.GetKeyDown(KeyCode.Backspace)){
            consoleCanvas.gameObject.SetActive(!consoleCanvas.gameObject.activeInHierarchy);
            GameManager.Instance._gamePaused = consoleCanvas.gameObject.activeInHierarchy;
            }

            if(Input.GetKeyDown(KeyCode.Return)){
            if(inputText.text != " ")
            {
                AddMessageToConsole(inputText.text);
                ParseInput(inputText.text);
            }
            }
        }

        private void CreateCommands(){
            CommandQuit commandQuit = CommandQuit.CreateCommand();
        }

        public static void AddCommandToConsole(string _name, ConsoleCommand _command){
            if(!Commands.ContainsKey(_name)){
                Commands.Add(_name, _command);
            }
        }

        public void EnterCommand(InputAction.CallbackContext context){
            if(inputText.text != " ")
            {
                AddMessageToConsole(inputText.text);
                ParseInput(inputText.text);
            }
        }

        public void ShowConsole(InputAction.CallbackContext context){
            consoleCanvas.gameObject.SetActive(!consoleCanvas.gameObject.activeInHierarchy);
            GameManager.Instance._gamePaused = consoleCanvas.gameObject.activeInHierarchy;
        }

        private void AddMessageToConsole(string msg){
            consoleText.text += msg + "\n";
            scrollRect.verticalNormalizedPosition = 0f;
        }

        public static void AddStaticMessageToConsole(string msg){
            DeveloperConsole.Instance.consoleText.text += msg + "\n";
            DeveloperConsole.Instance.scrollRect.verticalNormalizedPosition = 0f;
        }

        //Check if we can run the command
        private void ParseInput(string input){
            //Split the string at each null character --> Space
            string[] _input = input.Split(null);

            //If there is no command --> return
            if(_input.Length == 0 || _input == null){
                AddMessageToConsole("Command not recognized");
                return;
            }

            //If the first word of the string is not in Commands --> return
            if(!Commands.ContainsKey(_input[0])){
                AddMessageToConsole("Command not recognized");
            }
            //If the command is registered then proceed
            else{
                Commands[_input[0]].RunCommand();
            }
        }
    }
}