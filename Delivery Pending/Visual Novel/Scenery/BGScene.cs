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

    public void NextLineInstruction(LineInformation line)
    {
        line.sceneEffectCommand?.ExecuteCommand(bGSceneTween);
    }
}
