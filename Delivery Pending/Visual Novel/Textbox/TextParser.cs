//using System.Collections.Generic;
//using System.IO;
//using System.Text.Json;
//using Visual_Novel.Lines;

//using Visual_Novel.Scenery;

//namespace Visual_Novel.Textbox
//{
//    public static class TextParser
//    {
//        public static List<LineInformation> ParseFile()
//        {
//            string fileName = "WeatherForecast.json";
//            string jsonString = File.ReadAllText(fileName);

//            List<LineInformation> lines = new List<LineInformation>();
            
//            lines.Add(JsonSerializer.Deserialize<LineInformation>(""));
//            return lines;
//        }

//        public static void PrintSerializedJSON(SceneEffectCommand input)
//        {
//            Godot.GD.Print(JsonSerializer.Serialize<SceneEffectCommand>(input));
//        }
//    }
//}
