using Godot;
using Visual_Novel.Lines;

public class AudioPlayer : Node
{
    #region Variables
    [Export]
    public string FirstTrackName = "Track1";
    public AudioStreamPlayer FirstTrack { get; private set; }
    [Export]
    public string SecondTrackName = "Track2";
    public AudioStreamPlayer SecondTrack { get; private set; }
    [Export]
    public string MusicAnimationPlayerName = "AnimationPlayer";
    public AnimationPlayer MusicAnimationPlayer { get; private set; }
    #endregion

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        FirstTrack = GetNode<AudioStreamPlayer>(FirstTrackName);
        SecondTrack = GetNode<AudioStreamPlayer>(SecondTrackName);
        MusicAnimationPlayer = GetNode<AnimationPlayer>(MusicAnimationPlayerName);
    }

    /// <summary>
    /// Receiving end of signal - called by source to execute command from a given line.
    /// </summary>
    /// <param name="line">The line currently active.</param>
    public void NextLineInstruction(LineInformation line)
    {
        if (line == null)
            return;
        line.AudioCommand?.ExecuteCommand(this);
    }
}
