using Godot;
using System.Collections.Generic;
using System.Text.Json;
using Visual_Novel.Audio;
using Visual_Novel.Lines;

using Visual_Novel.Scenery;

namespace Visual_Novel.Textbox
{
    /// <summary>
    /// Parser class for reading in instructions for the Visual Novel.
    /// </summary>
    public static class TextParser
    {
        /// <summary>
        /// Parses file for JSON information on lines to deliver to visual novel.
        /// </summary>
        /// <param name="fileName">The name of the file to read in JSON from.</param>
        /// <returns>A list of LineInformation parsed from the file.</returns>
        public static List<LineInformation> ParseFile(string fileName)
        {
            #region File Reading


            //var file = ResourceLoader.Load<Godot.File>(fileName);
            var file = new Godot.File();
            file.Open(fileName, Godot.File.ModeFlags.Read);
            string jsonString = file.GetAsText();
            file.Close();

            #endregion

            List<LineInformation> lines = new List<LineInformation>();

            // Parse every JSON instruction within the file.
            using (var jsonDoc = JsonDocument.Parse(jsonString))
            {
                foreach (var instruction in jsonDoc.RootElement.GetProperty("instructions").EnumerateArray())
                {
                    lines.Add(CreateLineInformation(instruction));
                }
                //foreach (var property in jsonDoc.RootElement.EnumerateObject())
                //{
                //    Godot.GD.Print($"{property.Name} ValueKind={property.Value.ValueKind} Value={property.Value}");
                //}
            }
            return lines;
        }

        /// <summary>
        /// Creates the LineInformation (Godot.Reference) based on the given JsonElement instruction.
        /// </summary>
        /// <param name="instruction">The instruction to convert into LineInformation.</param>
        /// <returns>LineInformation created from the given JsonElement.</returns>
        private static LineInformation CreateLineInformation(JsonElement instruction)
        {
            // TODO: Refactor commands to follow factory pattern
            string mainText = null;
            float timeSec = 0.039f;
            LineCharacter character = null;
            SceneEffectCommand sceneEffectCommand = null;
            AudioStreamCommand audioStreamCommand = null;
            // Text
            {
                if (instruction.TryGetProperty("text", out JsonElement textJsonElement))
                    mainText = textJsonElement.GetString();
            }
            // TimeSec
            {
                if (instruction.TryGetProperty("timeSec", out JsonElement timeJsonElement))
                    timeSec = timeJsonElement.GetSingle();
            }
            // Character
            {
                if (instruction.TryGetProperty("character", out JsonElement charJsonElement))
                {
                    // TODO: Implement Character for instruction
                }
            }
            // SceneCommand
            {
                if (instruction.TryGetProperty("sceneCommand", out JsonElement sceneJsonElement))
                {
                    float fadeTime = 2;
                    sceneJsonElement.GetProperty("fadeTime").TryGetSingle(out fadeTime);
                    switch (sceneJsonElement.GetProperty("type").GetString())
                    {
                        case "crossFade":
                            sceneEffectCommand = new CrossFadeCommand(sceneJsonElement.GetProperty("path").GetString(), fadeTime);
                            break;
                        case "fadeBlack":
                            sceneEffectCommand = new FadeToBlackCommand(fadeTime);
                            break;
                        default:
                            break;
                    }

                }
            }
            // AudioCommand
            {
                if (instruction.TryGetProperty("audioCommand", out JsonElement audioJsonElement))
                {
                    switch (audioJsonElement.GetProperty("type").GetString())
                    {
                        case "quickFade":
                            audioStreamCommand = new QuickFadeCommand(audioJsonElement.GetProperty("path").GetString());
                            break;
                        case "silentFade":
                            audioStreamCommand = new SilentFadeCommand();
                            break;
                        default:
                            break;
                    }
                }
            }

            return new LineInformation(mainText, timeSec, character, sceneEffectCommand, audioStreamCommand);
        }
    }
}
