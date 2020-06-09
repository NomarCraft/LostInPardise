using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Console{
    public class CommandQuit : ConsoleCommand
    {
        public override string Name { get; protected set; }
        public override string Command { get; protected set; }
        public override string Description { get; protected set; }
        public override string Help { get; protected set; }

        public CommandQuit()
        {
            Name = "Quit";
            Command = "quit";
            Description = "Quits the application";
            Help = "Use this command with no arguments to force Unity to quit!";

            AddCommandToConsole();
        }

        public override void RunCommand(string[] _inputs)
        {
            if (Application.isEditor)
            {
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#endif
            }
            else
            {
                Application.Quit();
            }
        }

        public static CommandQuit CreateCommand()
        {
            return new CommandQuit();
        }
    }

    public class CommandTeleport : ConsoleCommand{
        public override string Name { get; protected set; }
        public override string Command { get; protected set; }
        public override string Description { get; protected set; }
        public override string Help { get; protected set; }

        public CommandTeleport()
        {
            Name = "Teleport";
            Command = "tp";
            Description = "Teleport the player";
            Help = "Use this command to teleport";

            AddCommandToConsole();
        }

        public override void RunCommand(string[] _inputs)
        {
            if(_inputs[1] != null && DeveloperConsole.Instance.varRef.teleportPoints.ContainsKey(_inputs[1])){
                Debug.Log("Woosh you've been teleported");
            }else{
                RunError();
            }
        }

        public static CommandTeleport CreateCommand()
        {
            return new CommandTeleport();
        }
    }
}
