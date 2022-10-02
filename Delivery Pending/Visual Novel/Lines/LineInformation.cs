using System;
using Godot;
using Visual_Novel.Audio;
using Visual_Novel.Scenery;

namespace Visual_Novel.Lines
{
    /// <summary>
    /// The class for line information (implemented as a struct with the purpose
    /// of not being modified). Inherits from Godot.Object to be passed through signals.
    /// </summary>
    public class LineInformation : Godot.Reference
    {
        public String Text { get; private set; }
        public LineCharacter Character { get; private set; }
        public float TimeSec { get; private set; }
        public SceneEffectCommand SceneCommand { get; private set; }
        public AudioStreamCommand AudioCommand { get; private set; }

        /// <summary>
        /// Constructor for storing line information. Used when delivering lines.
        /// </summary>
        /// <param name="text">The text to be displayed on the line.</param>
        /// <param name="timeSec">The time in seconds between each character being displayed.</param>
        /// <param name="character">The character that is currently talking.</param>
        /// <param name="sceneEffect">The SceneEffectCommand to execute from the given line.</param>
        /// <param name="audioStream">The AudioStreamCommand to execute from the given line.</param>
        public LineInformation(String text, float timeSec = 0.039f, LineCharacter character = null, 
            SceneEffectCommand sceneEffect = null, AudioStreamCommand audioStream = null)
        {
            this.Text = text;
            this.TimeSec = timeSec;
            this.Character = character;
            this.SceneCommand = sceneEffect;
            this.AudioCommand = audioStream;
        }
    }

    public class LineCharacter
    {
        public String name { get; private set; }
        public TextureRect chImage { get; private set; }
        public AudioEffect audio { get; private set; }
        public LineCharacter(String name, TextureRect chImage, AudioEffect audio)
        {
            this.name = name;
            this.chImage = chImage;
            this.audio = audio;
        }
    }
}
