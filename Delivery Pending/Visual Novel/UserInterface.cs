using Godot;
using System.Collections.Generic;
using Visual_Novel.Lines;
using Visual_Novel.Textbox;

public class UserInterface : CanvasLayer
{
    #region Variables
    [Export]
    public string DialogueUIName = "DialogueUI";
    private DialogueUI dialogueBox;
    [Export]
    public string instructionsFilePath;
    private List<LineInformation> instructions;

    private int indexInstruction;

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
        instructions = TextParser.ParseFile(instructionsFilePath);
        indexInstruction = 0;
        NextLine();
    }

    private PackedScene MainMenuScene = ResourceLoader.Load<PackedScene>("res://Visual Novel/Main Menu.tscn");

    /// <summary>
    /// Runs the next line available
    /// </summary>
    public void NextLine()
    {
        LineInformation nextLine = null;
        if (indexInstruction < instructions.Count)
            nextLine = instructions[indexInstruction];
        else
            GetTree().ChangeSceneTo(MainMenuScene);
        // TODO: Implement else for file completion/nextfile

        // Access parsed information and emit signals based on parsed instructions
        if (dialogueBox.ReadyToDeliver())
        {
            EmitSignal(nameof(NextInstructionSignal), nextLine);
            indexInstruction++;
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
