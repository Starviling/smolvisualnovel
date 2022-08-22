using Godot;
using System;
using System.Collections.Generic;
using Visual_Novel.Lines;

public class DialogueUI : Panel
{
    #region Variables
    [Export]
    // Main Text Name
    public String MainTextName = "MainText";
    private RichTextLabel mainText;

    private Timer lineTimer;

    [Signal]
    public delegate void LineDelivered();
    [Signal]
    public delegate void BeginNextLine();
    #endregion

    private LineInformation line;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        mainText = GetNode<RichTextLabel>(MainTextName);
        //lineTimer = GetNode<Timer>(lineTimerName);
        lineTimer = new Timer();
        lineTimer.Connect("timeout", this, nameof(RevealChar));
        AddChild(lineTimer);
    }

    // Make sure everything is clean
    public override void _ExitTree()
    {
        base._ExitTree();
        lineTimer.Stop();
        lineTimer.QueueFree();
    }

    /// <summary>
    /// Determines if the DialogueUI is ready to deliver the next line
    /// </summary>
    /// <returns></returns>
    public bool ReadyToDeliver()
    {
        return lineTimer.IsStopped();
    }

    /// <summary>
    /// Begins the loop for the next line being displayed. Emits BeginNextLine signal
    /// upon start.
    /// </summary>
    /// <param name="line"></param>
    public void NextLineInstruction(LineInformation line)
    {
        this.line = line;
        mainText.VisibleCharacters = 0;
        mainText.BbcodeText = line.Text;

        lineTimer.WaitTime = line.TimeSec;
        EmitSignal(nameof(BeginNextLine));
        lineTimer.Start();
    }

    /// <summary>
    /// Connected to timer timeout and loops until all characters are shown. Emits
    /// LineDelivered signal upon completion.
    /// </summary>
    public void RevealChar()
    {
        mainText.VisibleCharacters++;
        if (mainText.VisibleCharacters >= mainText.GetTotalCharacterCount())
        {
            lineTimer.Stop();
            EmitSignal(nameof(LineDelivered));
            return;
        }
        // Change the timer wait to suit the character being displayed
        switch (mainText.Text[mainText.VisibleCharacters])
        {
            case '.':
                lineTimer.WaitTime = line.TimeSec + 0.5f;
                break;
            case '?':
                lineTimer.WaitTime = line.TimeSec + 0.85f;
                break;
            case '!':
                lineTimer.WaitTime = line.TimeSec + 0.395f;
                break;
            default:
                lineTimer.WaitTime = line.TimeSec;
                break;
        }
        
    }

    /// <summary>
    /// Reveals all text and emits LineDelivered signal indicating completion of line.
    /// </summary>
    public void RevealAll()
    {
        mainText.PercentVisible = 1;
        lineTimer.Stop();
        EmitSignal(nameof(LineDelivered));
    }
}
