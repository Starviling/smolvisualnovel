using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using Visual_Novel.Lines;

using Visual_Novel.Scenery;

namespace Visual_Novel.Textbox
{
    public static class TextParser
    {
        public static List<LineInformation> ParseFile(string fileName)
        {
            var file = new Godot.File();
            file.Open(fileName, Godot.File.ModeFlags.Read);
            string jsonString = file.GetAsText();
            file.Close();
            List<LineInformation> lines = new List<LineInformation>();

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

        private static LineInformation CreateLineInformation(JsonElement instruction)
        {
            string mainText = null;
            LineCharacter character = null;
            SceneEffectCommand sceneEffectCommand = null;
            float timeSec = 0.039f;

            if (instruction.TryGetProperty("mainText", out JsonElement textJsonElement))
                mainText = textJsonElement.GetString();
            if (instruction.TryGetProperty("character", out JsonElement charJsonElement))
            {
                // TODO: Implement Character for instruction
            }
            if (instruction.TryGetProperty("sceneEffect", out JsonElement sceneJsonElement))
            {
                float fadeTime = 2;
                sceneJsonElement.GetProperty("fadeTime").TryGetSingle(out fadeTime);
                switch (sceneJsonElement.GetProperty("type").GetString())
                {
                    case "crossfade":
                        sceneEffectCommand = new CrossFadeCommand(sceneJsonElement.GetProperty("path").GetString(), fadeTime);
                        break;
                    case "fadeblack":
                        sceneEffectCommand = new FadeToBlackCommand(fadeTime);
                        break;
                    default:
                        break;
                }

            }
            if (instruction.TryGetProperty("timeSec", out JsonElement timeJsonElement))
                timeSec = timeJsonElement.GetSingle();
            return new LineInformation(mainText, character, timeSec, sceneEffectCommand);
        }
    }
}
