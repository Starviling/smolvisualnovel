using System;
using Godot;

namespace Visual_Novel.Textbox
{
    public struct LineInformation
    {
        public String text;
        public LineCharacter character;
        public LineInformation(String text, LineCharacter character)
        {
            this.text = text;
            this.character = character;
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
