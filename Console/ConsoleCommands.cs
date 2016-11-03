using UnityEngine;

public partial class ConsoleController {

    public ConsoleController()
    {
        //When adding commands, you must add a call below to registerCommand() with its name, implementation method, and help text.
        //registerCommand("babble", babble, "Example command that demonstrates how to parse arguments. babble [word] [# of times to repeat]");
        //registerCommand("hide", hide, "Hide the console.");
        //registerCommand(repeatCmdName, repeatCommand, "Repeat last command.");
        //registerCommand("reload", reload, "Reload game.");
        //registerCommand("resetprefs", resetPrefs, "Reset & saves PlayerPrefs.");
        registerCommand("help", help, "Print this help.");
        registerCommand("echo", echo, "echoes arguments back as array (for testing argument parser)");
        registerCommand("loadnext", loadNext, "Loads next level (Game manager Tester!)");
        registerCommand("endlevel", endLevel, "Ends current level (Game manager Tester!)");

    }

    void help(string[] args)
    {
        foreach (CommandRegistration reg in commands.Values)
        {
            Debug.Log(string.Format("{0}: {1}", reg.command, reg.help));
        }
    }

    void echo (string[] args)
    {
        Debug.Log("Echo! "+args[0]+" "+args[1]);
    }

    void loadNext (string[] args)
    {
        Debug.Log("Loading next level");
        GameManager.LoadThings();
    }

    void endLevel (string[] args)
    {
        Debug.Log("Starting next level");
        GameManager.EndLevel();
    }

}
