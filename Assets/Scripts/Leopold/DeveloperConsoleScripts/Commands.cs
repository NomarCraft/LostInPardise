using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using Console;

#region Utils
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
        }else{
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
        Command = "teleport";
        Description = "Teleport the player";
        Help = "Use this command to teleport";

        AddCommandToConsole();
    }

    public override void RunCommand(string[] _inputs)
    {
        Dictionary<string, Transform> transforms = DeveloperConsole.Instance.varRef.teleportPoints;

        if(_inputs[1] != null && transforms.ContainsKey(_inputs[1]))
        {
            GameManager.Instance._chara.GetComponentsInParent<Transform>()[1].position = transforms[_inputs[1]].position;
        }else{
            RunError();
        }
    }

    public static CommandTeleport CreateCommand()
    {
        return new CommandTeleport();
    }
}

public class CommandHelp : ConsoleCommand{
        public override string Name { get; protected set; }
        public override string Command { get; protected set; }
        public override string Description { get; protected set; }
        public override string Help { get; protected set; }

        public CommandHelp()
        {
            Name = "Help";
            Command = "help";
            Description = "Give information about commands";
            Help = "Use this command without arguments to get a list of commands\nUse this command with the name of a command to get information about it";

            AddCommandToConsole();
        }

        public override void RunCommand(string[] _inputs)
        {
            if(_inputs.Length == 1){
                foreach (var item in DeveloperConsole.CommandDictionnary)
                {
                    Debug.Log("Write Help + a command to get more information");
                    Debug.Log(item.Value.Name);
                }
            }else if(_inputs.Length == 2){
                if(DeveloperConsole.CommandDictionnary.ContainsKey(_inputs[1])){
                    ConsoleCommand command = DeveloperConsole.CommandDictionnary[_inputs[1]];
                    Debug.Log(command.Name + "\n" + command.Description + "\n" + command.Help);
                }else{
                    RunError();
                }
            }else{
                RunError();
            }
        }

        public static CommandHelp CreateCommand()
        {
            return new CommandHelp();
        }
    }

#endregion

#region Inventory and Compendium
public class CommandAddItem : ConsoleCommand{
    public override string Name { get; protected set; }
    public override string Command { get; protected set; }
    public override string Description { get; protected set; }
    public override string Help { get; protected set; }

    public CommandAddItem()
    {
        Name = "AddItem";
        Command = "additem";
        Description = "Add a selected item to the inventory if possible";
        Help = "Use this command with the item id and amount to add it to the inventory";

        AddCommandToConsole();
    }

    public override void RunCommand(string[] _inputs)
    {
        if(_inputs[1] != null && _inputs[2] != null && _inputs.Length == 3)
        {
            int id = int.Parse(_inputs[1]);
            int amount = int.Parse(_inputs[2]);

            if(GameManager.Instance.comp.itemIDs.Contains(id)){
                
                GameManager.Instance.inv.AddItem(id, amount);
            }else{
                RunError();
            }
        }else{
            RunError();
        }
    }

    public static CommandAddItem CreateCommand()
    {
        return new CommandAddItem();
    }
}

public class CommandUnlockItem : ConsoleCommand{
    public override string Name { get; protected set; }
    public override string Command { get; protected set; }
    public override string Description { get; protected set; }
    public override string Help { get; protected set; }

    public CommandUnlockItem()
    {
        Name = "UnlockItem";
        Command = "unlockitem";
        Description = "Unlock the compendium entry of a selected item";
        Help = "Use this command with the item id to unlock it in the compendium";

        AddCommandToConsole();
    }

    public override void RunCommand(string[] _inputs)
    {
        if(_inputs[1] != null && _inputs.Length == 2)
        {
            int id = int.Parse(_inputs[1]);

            if(GameManager.Instance.comp.itemIDs.Contains(id)){
                
                GameManager.Instance.comp.UnlockItem(id);
            }else{
                RunError();
            }
        }else{
            RunError();
        }
    }

    public static CommandUnlockItem CreateCommand()
    {
        return new CommandUnlockItem();
    }
}

public class CommandUnlockRecipe : ConsoleCommand{
    public override string Name { get; protected set; }
    public override string Command { get; protected set; }
    public override string Description { get; protected set; }
    public override string Help { get; protected set; }

    public CommandUnlockRecipe()
    {
        Name = "UnlockLog";
        Command = "unlocklog";
        Description = "Unlock the compendium entry of a selected log";
        Help = "Use this command with the log id to unlock it in the compendium";

        AddCommandToConsole();
    }

    public override void RunCommand(string[] _inputs)
    {
        if(_inputs[1] != null && _inputs.Length == 2)
        {
            int id = int.Parse(_inputs[1]);

            if(GameManager.Instance.comp.recipeIDs.Contains(id)){
                
                GameManager.Instance.comp.UnlockRecipe(id);
            }else{
                RunError();
            }
        }else{
            RunError();
        }
    }

    public static CommandUnlockRecipe CreateCommand()
    {
        return new CommandUnlockRecipe();
    }
}

public class CommandUnlockLog : ConsoleCommand{
    public override string Name { get; protected set; }
    public override string Command { get; protected set; }
    public override string Description { get; protected set; }
    public override string Help { get; protected set; }

    public CommandUnlockLog()
    {
        Name = "UnlockRecipe";
        Command = "unlockrecipe";
        Description = "Unlock the compendium entry of a selected recipe";
        Help = "Use this command with the recipe id to unlock it in the compendium";

        AddCommandToConsole();
    }

    public override void RunCommand(string[] _inputs)
    {
        if(_inputs[1] != null && _inputs.Length == 2)
        {
            int id = int.Parse(_inputs[1]);

            if(GameManager.Instance.comp.logIDs.Contains(id)){
                
                GameManager.Instance.comp.UnlockLog(id);
            }else{
                RunError();
            }
        }else{
            RunError();
        }
    }

    public static CommandUnlockLog CreateCommand()
    {
        return new CommandUnlockLog();
    }
}
#endregion
    
namespace Console{
    public class EmptyCommand : ConsoleCommand{
        public override string Name { get; protected set; }
        public override string Command { get; protected set; }
        public override string Description { get; protected set; }
        public override string Help { get; protected set; }

        public EmptyCommand()
        {
            Name = "Empty";
            Command = "empty";
            Description = "Empty";
            Help = "Empty";

            AddCommandToConsole();
        }

        public override void RunCommand(string[] _inputs)
        {

        }

        public static EmptyCommand CreateCommand()
        {
            return new EmptyCommand();
        }
    }
}
