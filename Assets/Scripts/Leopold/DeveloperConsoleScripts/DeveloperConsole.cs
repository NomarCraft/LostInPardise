using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Console
{
    public abstract class ConsoleCommand
    {
        public abstract string Name { get; protected set; }
        public abstract string Command { get; protected set; }
        public abstract string Description { get; protected set; }
        public abstract string Help { get; protected set; }

        public void AddCommandToConsole()
        {
            string addMessage = " command has been added to the console.";

            DeveloperConsole.AddCommandsToConsole(Command, this);
            Debug.Log(Name + addMessage);
        }

        public void RunError(){
            Debug.LogWarning(Name + " arguments not found");
        }

        public void RunError(string text){
            Debug.LogWarning(Name + ": " + text);
        }

        public abstract void RunCommand(string[] _inputs);
    }

    public class DeveloperConsole : MonoBehaviour
    {
        public static DeveloperConsole Instance { get; private set; }
        public static Dictionary<string, ConsoleCommand> CommandDictionnary { get; private set; }

        [Header("UI Components")]
        public Canvas consoleCanvas;
        public Text consoleText;
        public Text inputText;
        public Text placeholderText;
        public InputField consoleInput;
        public ConsoleVar varRef;

        private void Awake()
        {
            if(Instance != null)
            {
                return;
            }

            Instance = this;
            CommandDictionnary = new Dictionary<string, ConsoleCommand>();
        }

        private void Start()
        {
            consoleCanvas.gameObject.SetActive(false);
            CreateCommands();
        }

        private void OnEnable()
        {
            Application.logMessageReceived += HandleLog;
        }

        private void OnDisable()
        {
            Application.logMessageReceived -= HandleLog;
        }

        private void HandleLog(string logMessage, string stackTrace, LogType type)
        {
            string _message = "[" + type.ToString() + "] " + logMessage;
            AddMessageToConsole(_message);
        }

        private void CreateCommands()
        {
            CommandQuit.CreateCommand();
            CommandTeleport.CreateCommand();
            CommandHelp.CreateCommand();
            CommandAddItem.CreateCommand();
            CommandUnlockItem.CreateCommand();
            CommandUnlockRecipe.CreateCommand();
            CommandUnlockLog.CreateCommand();
        }

        public static void AddCommandsToConsole(string _name, ConsoleCommand _command)
        {
            if(!CommandDictionnary.ContainsKey(_name))
            {
                CommandDictionnary.Add(_name, _command);
            }
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.BackQuote))
            {
                consoleCanvas.gameObject.SetActive(!consoleCanvas.gameObject.activeInHierarchy);
                GameManager.Instance._gamePaused = consoleCanvas.gameObject.activeInHierarchy;
                consoleText.text = "Starting Developer Console ...\n";
                inputText.text = "";
                consoleInput.text = "";
                placeholderText.enabled = true;
                consoleInput.ActivateInputField();
            }

            if(consoleCanvas.gameObject.activeInHierarchy)
            {
                if(Input.GetKeyDown(KeyCode.Return))
                {
                    if(inputText.text != "")
                    {
                        AddMessageToConsole(inputText.text);
                        ParseInput(inputText.text.ToLower());
                    }
                }
            }
        }

        private void AddMessageToConsole(string msg)
        {
            consoleText.text += msg + "\n";
        }

        private void ParseInput(string input)
        {
            string[] _input = input.Split(null);

            if (_input.Length == 0 || _input == null)
            {
                Debug.LogWarning("Command not recognized.");
                return;
            }

            if (!CommandDictionnary.ContainsKey(_input[0]))
            {
                Debug.LogWarning("Command not recognized.");
            }
            else
            {
                CommandDictionnary[_input[0]].RunCommand(_input);
            }
        }
    }
}
