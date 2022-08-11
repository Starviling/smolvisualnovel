using Godot;
using System;
using Visual_Novel.Textbox;

public class DialogueUI : Panel
{
    #region Variables
    [Export]
    // Main Text Name
    String mainTextName = "MainText";
    RichTextLabel mainText;
    #endregion


    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        mainText = GetNode<RichTextLabel>(mainTextName);
    }

    public void nextLine(LineInformation line)
    {
        mainText.VisibleCharacters = 0;
        mainText.BbcodeText = line.text;
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
