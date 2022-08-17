using Godot;
using System;
using System.Collections.Generic;
using Visual_Novel.Lines;

public class DialogueUI : Panel
{
    #region Variables
    [Export]
    // Main Text Name
    public String mainTextName = "MainText";
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
        mainText = GetNode<RichTextLabel>(mainTextName);
        //lineTimer = GetNode<Timer>(lineTimerName);
        lineTimer = new Timer();
        lineTimer.Connect("timeout", this, nameof(RevealChar));
        AddChild(lineTimer);

        #region Test code
        line = new LineInformation("And with the great event that occurred not too long ago... I felt the [tornado][rainbow]need[/rainbow][/tornado] to [shake]achieve the best[/shake] that I could. [wave]AHHH!![/wave] Here we go...",
            new LineCharacter());
        NextLine(line);
        #endregion
    }

    // Make sure everything is clean
    public override void _ExitTree()
    {
        base._ExitTree();
        lineTimer.Stop();
        lineTimer.Dispose();
    }

    // Indicates that the nextline is being delivered. Triggered upon click
    public void NextLine(LineInformation line)
    {
        this.line = line;
        mainText.VisibleCharacters = 0;
        mainText.BbcodeText = line.text;

        lineTimer.WaitTime = line.timeSec;
        EmitSignal(nameof(BeginNextLine));
        lineTimer.Start();
    }

    // This is connected to the timer timeout and loops until all characters are shown
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
                lineTimer.WaitTime = line.timeSec + 0.5f;
                break;
            case '?':
                lineTimer.WaitTime = line.timeSec + 0.85f;
                break;
            case '!':
                lineTimer.WaitTime = line.timeSec + 0.395f;
                break;
            default:
                lineTimer.WaitTime = line.timeSec;
                break;
        }
        
    }
    // For when the player wants to go fast
    public void RevealAll()
    {
        mainText.PercentVisible = 1;
        lineTimer.Stop();
        EmitSignal(nameof(LineDelivered));
    }
}
