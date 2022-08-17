using System;
using Godot;

namespace Visual_Novel.Lines
{
    public struct LineInformation
    {
        public String text;
        public LineCharacter character;
        public float timeSec;
        public LineInformation(String text, LineCharacter character) : this(text, character, 0.039f) { }
        public LineInformation(String text, LineCharacter character, float timeSec)
        {
            this.text = text;
            this.character = character;
            this.timeSec = timeSec;
        }
    }

    public struct LineCharacter
    {
        public String name;
        public TextureRect chImage;
        public AudioEffect audio;
        public LineCharacter(String name, TextureRect chImage, AudioEffect audio)
        {
            this.name = name;
            this.chImage = chImage;
            this.audio = audio;
        }
    }
}
