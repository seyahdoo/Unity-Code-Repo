using UnityEngine;
using System.Collections.Generic;
using System;

/// <summary>
/// Must be attached to an object to be worked...
/// </summary>
public class Console : MonoBehaviour {

    public bool IsListening = false;
    public string cmd = "";

    void Update()
    {
        //if get key "  listen to the keystokes, if " again ignore, if [Enter] Excecute.
        if (Input.GetKeyDown(KeyCode.BackQuote) && !IsListening)
        {
            IsListening = true;
            cmd = "";
        }
        else if (Input.GetKeyDown(KeyCode.BackQuote) && IsListening)
        {
            IsListening = false;
            cmd = "";
        }
        else if (Input.GetKeyDown(KeyCode.Return) && IsListening)
        {
            ConsoleController.Execute(cmd);
            IsListening = false;
        }
        else if (Input.GetKeyDown(KeyCode.Backspace) && IsListening)
        {
            //delete last character from cmd
            cmd = cmd.Remove(cmd.Length - 1);
        }
        else if (Input.GetKeyDown(KeyCode.Tab) && IsListening)
        {
            //TODO
            Debug.Log("Intellisense");
        }
        else if (Input.anyKeyDown && IsListening)
        {
            if (Input.inputString.Length > 0)
            {
                cmd += Input.inputString;
            }
        }
        
    }

    void OnGUI()
    {
        if (IsListening)
        {
            GUI.Label(new Rect(0, 0, Screen.width, Screen.height), cmd);
        }
    }


}

public partial class ConsoleController
{
    #region Static Controller Thingies

    private static ConsoleController _instance;

    public static ConsoleController instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new ConsoleController();
            }

            return _instance;
        }
    }

    public delegate void CommandHandler(string[] args);

    /// <summary>
    /// Object to hold information about each command
    /// </summary>
    class CommandRegistration
    {
        public string command { get; private set; }
        public CommandHandler handler { get; private set; }
        public string help { get; private set; }

        public CommandRegistration(string command, CommandHandler handler, string help)
        {
            this.command = command;
            this.handler = handler;
            this.help = help;
        }
    }

    static Dictionary<string, CommandRegistration> commands = new Dictionary<string, CommandRegistration>();

    void registerCommand(string command, CommandHandler handler, string help)
    {
        commands.Add(command, new CommandRegistration(command, handler, help));
    }

    public void runCommandString(string commandString)
    {
        Debug.Log("$ " + commandString);

        string[] commandSplit = parseArguments(commandString);
        string[] args = new string[0];
        if (commandSplit.Length < 1)
        {
            Debug.Log(string.Format("Unable to process command '{0}'", commandString));
            return;

        }
        else if (commandSplit.Length >= 2)
        {
            int numArgs = commandSplit.Length - 1;
            args = new string[numArgs];
            Array.Copy(commandSplit, 1, args, 0, numArgs);
        }
        runCommand(commandSplit[0].ToLower(), args);
        //commandHistory.Add(commandString);
    }

    public void runCommand(string command, string[] args)
    {
        CommandRegistration reg = null;
        if (!commands.TryGetValue(command, out reg))
        {
            Debug.Log(string.Format("Unknown command '{0}', type 'help' for list.", command));
        }
        else
        {
            if (reg.handler == null)
            {
                Debug.Log(string.Format("Unable to process command '{0}', handler was null.", command));
            }
            else
            {
                reg.handler(args);
            }
        }
    }

    static string[] parseArguments(string commandString)
    {
        LinkedList<char> parmChars = new LinkedList<char>(commandString.ToCharArray());
        bool inQuote = false;
        var node = parmChars.First;
        while (node != null)
        {
            var next = node.Next;
            if (node.Value == '"')
            {
                inQuote = !inQuote;
                parmChars.Remove(node);
            }
            if (!inQuote && node.Value == ' ')
            {
                node.Value = '\n';
            }
            node = next;
        }
        char[] parmCharsArr = new char[parmChars.Count];
        parmChars.CopyTo(parmCharsArr, 0);
        return (new string(parmCharsArr)).Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
    }

    public static void Execute(string cmd)
    {
        instance.runCommandString(cmd);
    }

    #endregion
}