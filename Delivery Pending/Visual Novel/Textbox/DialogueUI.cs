using Godot;
using System;
using System.Collections.Generic;
using Visual_Novel.Textbox;

public class DialogueUI : Panel
{
    #region Variables
    [Export]
    // Main Text Name
    String mainTextName = "MainText";
    RichTextLabel mainText;

    //[Export]
    // Line Timer Name
    private Timer lineTimer;
    #endregion

    private LineInformation line;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        mainText = GetNode<RichTextLabel>(mainTextName);
        //lineTimer = GetNode<Timer>(lineTimerName);
        lineTimer = new Timer();
        lineTimer.Connect("timeout", this, nameof(revealChar));
        AddChild(lineTimer);

        #region Test code
        line = new LineInformation("And with the great event that occurred not too long ago... I felt the [tornado]need[/tornado] to [shake]achieve the best[/shake] that I could. [wave]AHHH!![/wave] Here we go...",
            new LineCharacter());
        nextLine(line);
        #endregion
    }

    public override void _ExitTree()
    {
        base._ExitTree();
        lineTimer.Stop();
        lineTimer.Dispose();
    }

    // Indicates that the nextline is being delivered. Triggered upon click
    public void nextLine(LineInformation line)
    {
        this.line = line;
        mainText.VisibleCharacters = 0;
        mainText.BbcodeText = line.text;

        lineTimer.WaitTime = line.timeSec;
        lineTimer.Start();
    }

    // This is connected to the timer timeout and loops until all characters are shown
    public void revealChar()
    {
        mainText.VisibleCharacters++;
        if (mainText.VisibleCharacters == mainText.GetTotalCharacterCount())
        {
            lineTimer.Stop();
            // TODO: Add signal here to indicate arrow indicator
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
                lineTimer.WaitTime = line.timeSec + 0.195f;
                break;
            default:
                lineTimer.WaitTime = line.timeSec;
                break;
        }
    }
}
