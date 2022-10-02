using Godot;
using System;
using Visual_Novel.Lines;

public class BGScene : Node2D
{
    [Export]
    public string BGSceneTweenName = "TransitionTween";
    private TransitionTween bGSceneTween;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        bGSceneTween = GetNode<TransitionTween>(BGSceneTweenName);
    }

    /// <summary>
    /// Receiving end of signal - called by source to execute command from a given line.
    /// </summary>
    /// <param name="line">The line currently active.</param>
    public void NextLineInstruction(LineInformation line)
    {
        if (line == null)
            return;
        line.SceneCommand?.ExecuteCommand(bGSceneTween);
    }
}
