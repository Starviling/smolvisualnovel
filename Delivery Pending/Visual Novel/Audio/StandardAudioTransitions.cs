using Godot;

namespace Visual_Novel.Audio
{
    /// <summary>
    /// Command for cross fading backgrounds.
    /// </summary>
    public class QuickFadeCommand : AudioStreamCommand
    {
        private string resourcePathTrans;

        /// <summary>
        /// Crossfades the active scene with a new background.
        /// </summary>
        /// <param name="resourcePathTrans"></param>
        public QuickFadeCommand(string resourcePathTrans)
        {
            this.resourcePathTrans = resourcePathTrans;
        }

        /// <summary>
        /// Executes the transition for the backgrounds.
        /// </summary>
        public override void ExecuteCommand(AudioPlayer audioPlayer)
        {
            if (audioPlayer.FirstTrack.Playing && audioPlayer.SecondTrack.Playing)
                return;
            AudioStream audioStream = (AudioStream)GD.Load(resourcePathTrans);
            if (audioPlayer.SecondTrack.Playing)
            {
                audioPlayer.FirstTrack.Stream = audioStream;
                audioPlayer.FirstTrack.Play();
                audioPlayer.MusicAnimationPlayer.Play("FadeToFirst");
            } else
            {
                audioPlayer.SecondTrack.Stream = audioStream;
                audioPlayer.SecondTrack.Play();
                audioPlayer.MusicAnimationPlayer.Play("FadeToSecond");
            }
        }
    }

    public class SilentFadeCommand : AudioStreamCommand
    {
        public SilentFadeCommand() {}

        /// <summary>
        /// Executes the transition for the backgrounds.
        /// </summary>
        public override void ExecuteCommand(AudioPlayer audioPlayer)
        {
            if (audioPlayer.FirstTrack.Playing && audioPlayer.SecondTrack.Playing)
                return;
            if (audioPlayer.SecondTrack.Playing)
            {
                audioPlayer.MusicAnimationPlayer.Play("FadeToFirst");
            }
            else
            {
                audioPlayer.MusicAnimationPlayer.Play("FadeToSecond");
            }
        }
    }
}
