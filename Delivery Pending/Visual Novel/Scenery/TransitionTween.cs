using Godot;
using System;
using Visual_Novel.Scenery;

public class TransitionTween : Tween
{
    #region Variables
    // Nodes containing the scenery.
    public Node NodeActive { get; private set; }
    public Node NodeTransitionary { get; private set; }

    [Export]
    public Texture BlackTexture;

    // Action that allows the swapping of the scenery nodes.
    // private Action Swap;
    // Swap = () => { (nodeActive, nodeTransitionary) = (nodeTransitionary, nodeActive); };
    #endregion

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        NodeActive = GetNode<Node>("SceneA");
        NodeTransitionary = GetNode<Node>("SceneB");
    }

    /// <summary>
    /// This will only swap the references between NodeActive and NodeTransitionary. It will
    /// NOT alter the textures in any way.
    /// </summary>
    public void SwapActiveNode()
    {
        (NodeActive, NodeTransitionary) = (NodeTransitionary, NodeActive);
    }
}
