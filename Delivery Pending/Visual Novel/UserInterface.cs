using Godot;
using System;
using System.Runtime.CompilerServices;
using Visual_Novel.Lines;
using Visual_Novel.Scenery;
//using Visual_Novel.Textbox;

public class UserInterface : CanvasLayer
{
    #region Variables
    [Export]
    public string DialogueUIName = "DialogueUI";
    private DialogueUI dialogueBox;

    #region DialogueUI Signals
    [Signal]
    public delegate void ToggleDialogueUI();
    [Signal]
    public delegate void NextInstructionSignal(LineInformation line);
    #endregion
    #endregion

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        dialogueBox = GetNode<DialogueUI>(DialogueUIName);
    }

    public void NextLine()
    {
        SceneEffectCommand test = new CrossFadeCommand("res://Assets/Images/TalkingSelf.PNG");
        LineInformation nextLine = new LineInformation("And with the great event that occurred not too long ago... I felt the [tornado][rainbow]need[/rainbow][/tornado] to [shake]achieve the best[/shake] that I could. [wave]AHHH!![/wave] Here we go...", null, 0.039f, test);
        //TextParser.PrintSerializedJSON(test);
        // Access parsed information and emit signals based on parsed instructions
        if (dialogueBox.ReadyToDeliver())
        {
            EmitSignal(nameof(NextInstructionSignal), nextLine);
        }
        else
        {
            dialogueBox.RevealAll();
            // Start a cooldown on delivering line
        }
    }

    

    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event.IsActionPressed("ui_accept"))
            NextLine();
    }
}
