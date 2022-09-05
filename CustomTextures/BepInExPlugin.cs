using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using Pathea;
using Pathea.FrameworkNs;
using Pathea.SleepNs;
using Pathea.TimeNs;
using Pathea.UISystemV2.UI;
using Pathea.UISystemV2.UIControl;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CustomTextures
{
    [BepInPlugin("aedenthorn.CustomTextures", "Custom Textures", "0.1.0")]
    public partial class BepInExPlugin : BaseUnityPlugin
    {
        private static BepInExPlugin context;

        public static ConfigEntry<bool> modEnabled;
        public static ConfigEntry<bool> isDebug;

        public static Dictionary<string, string> customTextures = new Dictionary<string, string>();
        public static Dictionary<string, DateTime> fileWriteTimes = new Dictionary<string, DateTime>();
        public static List<string> texturesToLoad = new List<string>();
        public static List<string> layersToLoad = new List<string>();
        public static Dictionary<string, Texture2D> cachedTextures = new Dictionary<string, Texture2D>();

        public static bool dumpOutput = false;
        public static List<string> outputDump = new List<string>();
        public static List<string> logDump = new List<string>();


        public static void Dbgl(string str = "", LogLevel logLevel = LogLevel.Debug)
        {
            if (isDebug.Value)
                context.Logger.Log(logLevel, str);
        }
        private void Awake()
        {

            context = this;
            modEnabled = Config.Bind<bool>("General", "Enabled", true, "Enable this mod");
            isDebug = Config.Bind<bool>("General", "IsDebug", true, "Enable debug logs");

            Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), null);

            Dbgl("Mod Loaded");
            SceneManager.sceneLoaded += SceneManager_sceneLoaded;

            LoadCustomTextures();
        }

        private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
        {
            ReplaceTextures();
        }

        private static bool ShouldLoadCustomTexture(string id)
        {
            return texturesToLoad.Contains(id) || layersToLoad.Contains(id);
        }

        [HarmonyPatch(typeof(OptionPartControl), nameof(OptionPartControl.RefreshUI))]
        static class OptionPartControl_RefreshUI_Patch
        {
            public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
            {
                Dbgl("Transpiling OptionPartControl.RefreshUI");
                List<CodeInstruction> codes = instructions.ToList();
                for(int i = 0; i < codes.Count; i++)
                {
                    if (i < codes.Count - 3 && codes[i].operand != null && codes[i].operand is FieldInfo && (FieldInfo)codes[i].operand == AccessTools.Field(typeof(OptionPartUI), nameof(OptionPartUI.timeScale)) && codes[i + 3].operand != null && codes[i + 3].operand is MethodInfo && (MethodInfo)codes[i + 3].operand == AccessTools.Method(typeof(OptionSliderValueElement), nameof(OptionSliderValueElement.SetMinMax)))
                    {
                        codes[i + 1].operand = 0.01f;
                        codes[i + 2].operand = 10f;
                    }
                }
                return codes.AsEnumerable();
            }
        }
        private static void LoadCustomTextures()
        {
            string path = AedenthornUtils.GetAssetPath(context, true);

            texturesToLoad.Clear();

            foreach (string file in Directory.GetFiles(path, "*.*", SearchOption.AllDirectories))
            {
                string fileName = Path.GetFileName(file);
                string id = Path.GetFileNameWithoutExtension(fileName);


                if (!fileWriteTimes.ContainsKey(id) || (cachedTextures.ContainsKey(id) && !DateTime.Equals(File.GetLastWriteTimeUtc(file), fileWriteTimes[id])))
                {
                    cachedTextures.Remove(id);
                    texturesToLoad.Add(id);
                    layersToLoad.Add(Regex.Replace(id, @"_[^_]+\.", "."));
                    fileWriteTimes[id] = File.GetLastWriteTimeUtc(file);
                    //Dbgl($"adding new {fileName} custom texture.");
                }

                customTextures[id] = file;
            }
            Dbgl($"Prepared to load {customTextures.Count} textures");
        }

        private static void ReplaceTextures()
        {
            MeshRenderer[] mrs = FindObjectsOfType<MeshRenderer>();
            SkinnedMeshRenderer[] smrs = FindObjectsOfType<SkinnedMeshRenderer>();
            ParticleSystemRenderer[] prs = FindObjectsOfType<ParticleSystemRenderer>();
            LineRenderer[] lrs = FindObjectsOfType<LineRenderer>();

            if (mrs.Length > 0)
            {
                if (dumpOutput)
                    outputDump.Add($"{mrs.Length} MeshRenderers:");
                foreach (MeshRenderer r in mrs)
                {
                    if (r == null)
                    {
                        if (dumpOutput)
                            outputDump.Add($"\tnull");
                        continue;
                    }

                    if (dumpOutput)
                        outputDump.Add($"\tMeshRenderer name: {r.name}");
                    if (r.materials == null || !r.materials.Any())
                    {
                        if (dumpOutput)
                            outputDump.Add($"\t\trenderer {r.name} has no materials");
                        continue;
                    }
                    if (dumpOutput)
                        outputDump.Add($"\t\trenderer {r.name} has {r.materials.Length} materials");

                    foreach (Material m in r.materials)
                    {
                        try
                        {
                            if (dumpOutput)
                                outputDump.Add($"\t\t\t{m.name}:");

                            ReplaceMaterialTextures(r.gameObject.name, m, "MeshRenderer", r.name);
                        }
                        catch (Exception ex)
                        {
                            logDump.Add($"\t\t\tError loading {r.name}:\r\n{ex}");
                        }
                    }

                }
            }
            if (smrs.Length > 0)
            {
                if (dumpOutput)
                    outputDump.Add($"{smrs.Length} SkinnedMeshRenderers:");
                foreach (SkinnedMeshRenderer r in smrs)
                {
                    if (r == null)
                    {
                        if (dumpOutput)
                            outputDump.Add($"\tnull");
                        continue;
                    }
                    if (dumpOutput)
                        outputDump.Add($"\tSkinnedMeshRenderer name: {r.name}");
                    if (r.materials == null || !r.materials.Any())
                    {
                        if (dumpOutput)
                            outputDump.Add($"\t\tsmr {r.name} has no materials");
                        continue;
                    }

                    if (dumpOutput)
                        outputDump.Add($"\t\tsmr {r.name} has {r.materials.Length} materials");

                    foreach (Material m in r.materials)
                    {
                        try
                        {
                            if (dumpOutput)
                                outputDump.Add($"\t\t\t{m.name}:");

                            ReplaceMaterialTextures(r.gameObject.name, m, "SkinnedMeshRenderer", r.name);
                        }
                        catch (Exception ex)
                        {
                            logDump.Add($"\t\t\tError loading {r.name}:\r\n{ex}");
                        }
                    }

                }
            }
            if (prs.Length > 0)
            {
                if (dumpOutput)
                    outputDump.Add($"{prs.Length} ParticleSystemRenderers:");
                foreach (ParticleSystemRenderer r in prs)
                {
                    if (r == null)
                    {
                        if (dumpOutput)
                            outputDump.Add($"\tnull");
                        continue;
                    }

                    if (dumpOutput)
                        outputDump.Add($"\tParticleSystemRenderer name: {r.name}");
                    foreach (Material m in r.materials)
                    {
                        try
                        {
                            if (dumpOutput)
                                outputDump.Add($"\t\t\t{m.name}:");

                            ReplaceMaterialTextures(r.gameObject.name, m, "ParticleSystemRenderer", r.name);
                        }
                        catch (Exception ex)
                        {
                            logDump.Add($"\t\t\tError loading {r.name}:\r\n{ex}");
                        }
                    }
                }
            }
            if (lrs.Length > 0)
            {
                if (dumpOutput)
                    outputDump.Add($"{lrs.Length} LineRenderers:");
                foreach (LineRenderer r in lrs)
                {
                    if (r == null)
                    {
                        if (dumpOutput)
                            outputDump.Add($"\tnull");
                        continue;
                    }

                    if (dumpOutput)
                        outputDump.Add($"\tLineRenderers name: {r.name}");
                    foreach (Material m in r.materials)
                    {
                        try
                        {
                            if (dumpOutput)
                                outputDump.Add($"\t\t\t{m.name}:");

                            ReplaceMaterialTextures(r.gameObject.name, m, "LineRenderer", r.name);
                        }
                        catch (Exception ex)
                        {
                            logDump.Add($"\t\t\tError loading {r.name}:\r\n{ex}");
                        }
                    }
                }
            }
        }

        private static void ReplaceMaterialTextures(string goName, Material m, string rendererType, string rendererName)
        {
            if (dumpOutput)
                outputDump.Add("\t\t\t\tproperties:");

            foreach (string property in m.GetTexturePropertyNames())
            {
                if (dumpOutput)
                    outputDump.Add($"\t\t\t\t\t{property} {m.GetTexture(property)?.name}");

                int propHash = Shader.PropertyToID(property);

                string name = m.GetTexture(propHash)?.name;

                if (name == null)
                    name = "";

                CheckSetMatTextures(goName, m, rendererType, rendererName, name, property);

            }
        }

        private static void CheckSetMatTextures(string goName, Material m, string rendererType, string rendererName, string name, string property)
        {
            foreach (string str in MakePrefixStrings(goName, rendererName, m.name, name))
            {
                if (ShouldLoadCustomTexture(str + property) || ShouldLoadCustomTexture(str + property) || (property == "_MainTex" && ShouldLoadCustomTexture(str + "_texture")) || (property == "_StyleTex" && ShouldLoadCustomTexture(str + "_style")) || (property == "_BumpMap" && ShouldLoadCustomTexture(str + "_bump")))
                {

                    int propHash = Shader.PropertyToID(property);
                    if (m.HasProperty(propHash))
                    {
                        logDump.Add($"{rendererType} {rendererName}, material {m.name}, texture {name}, using {str}{property} for {property}.");

                        Texture vanilla = m.GetTexture(propHash);

                        Texture2D result = null;

                        bool isBump = property.Contains("Bump") || property.Contains("Normal");


                        if (ShouldLoadCustomTexture(str + property))
                            result = LoadTexture(str + property, vanilla, isBump);

                        if (result != null)
                        {
                            result.name = name;

                            m.SetTexture(propHash, result);
                            if (result != null && property == "_MainTex")
                                m.SetColor(propHash, Color.white);
                            break;

                        }
                    }
                }
            }
        }

        private static string[] MakePrefixStrings(string goName, string rendererName, string matName, string name)
        {
            return new string[]
            {
                goName,
                "mesh_"+goName+"_"+rendererName,
                "renderer_"+goName+"_"+rendererName,
                "mat_"+goName+"_"+matName,
                "renderermat_"+goName+"_"+rendererName+"_"+matName,
                "texture_"+goName+"_"+name,
                "mesh_"+rendererName,
                "renderer_"+rendererName,
                "mat_"+matName,
                "texture_"+name
            };
        }


        private static Texture2D LoadTexture(string id, Texture vanilla, bool isBump, bool point = false, bool needCustom = false, bool isSprite = false)
        {
            Texture2D texture;
            if (cachedTextures.ContainsKey(id))
            {
                logDump.Add($"loading cached texture for {id}");
                texture = cachedTextures[id];
                if (customTextures.ContainsKey(id))
                {
                    if (customTextures[id].Contains("bilinear"))
                    {
                        texture.filterMode = FilterMode.Bilinear;
                    }
                    else if (customTextures[id].Contains("trilinear"))
                    {
                        texture.filterMode = FilterMode.Trilinear;
                    }
                    else if (customTextures[id].Contains($"{Path.DirectorySeparatorChar}point{Path.DirectorySeparatorChar}"))
                    {
                        texture.filterMode = FilterMode.Trilinear;
                    }
                    else if (point)
                        texture.filterMode = FilterMode.Point;
                }
                return texture;
            }

            var layers = customTextures.Where(p => p.Key.StartsWith(id + "_"));

            if (!customTextures.ContainsKey(id) && layers.Count() == 0)
            {
                if (needCustom)
                    return null;
                return (Texture2D)vanilla;
            }

            logDump.Add($"loading custom texture for {id} {layers.Count()} layers");


            if (vanilla == null)
            {
                texture = new Texture2D(2, 2, TextureFormat.RGBA32, true, isBump);
                if (!customTextures.ContainsKey(id))
                {
                    byte[] layerData = File.ReadAllBytes(layers.First().Value);
                    texture.LoadImage(layerData);
                }
            }
            else
                texture = new Texture2D(vanilla.width, vanilla.height, TextureFormat.RGBA32, true, isBump);

            if (customTextures.ContainsKey(id))
            {
                if (customTextures[id].Contains($"{Path.DirectorySeparatorChar}bilinear{Path.DirectorySeparatorChar}"))
                {
                    texture.filterMode = FilterMode.Bilinear;
                }
                else if (customTextures[id].Contains($"{Path.DirectorySeparatorChar}trilinear{Path.DirectorySeparatorChar}"))
                {
                    texture.filterMode = FilterMode.Trilinear;
                }
                else if (customTextures[id].Contains($"{Path.DirectorySeparatorChar}point{Path.DirectorySeparatorChar}"))
                {
                    texture.filterMode = FilterMode.Trilinear;
                }
                else if (point)
                    texture.filterMode = FilterMode.Point;
            }
            else if (point)
                texture.filterMode = FilterMode.Point;

            if (customTextures.ContainsKey(id))
            {
                logDump.Add($"loading custom texture file for {id}");
                byte[] imageData = File.ReadAllBytes(customTextures[id]);
                texture.LoadImage(imageData);
            }
            else if (vanilla != null)
            {
                Dbgl($"texture {id} has no custom texture, using vanilla");

                // https://support.unity.com/hc/en-us/articles/206486626-How-can-I-get-pixels-from-unreadable-textures-

                // Create a temporary RenderTexture of the same size as the texture
                RenderTexture tmp = RenderTexture.GetTemporary(
                                    texture.width,
                                    texture.height,
                                    0,
                                    RenderTextureFormat.Default,
                                    isBump ? RenderTextureReadWrite.Linear : RenderTextureReadWrite.Default);

                // Blit the pixels on texture to the RenderTexture
                Graphics.Blit(vanilla, tmp);

                // Backup the currently set RenderTexture
                RenderTexture previous = RenderTexture.active;

                // Set the current RenderTexture to the temporary one we created
                RenderTexture.active = tmp;

                // Create a new readable Texture2D to copy the pixels to it
                Texture2D myTexture2D = new Texture2D(vanilla.width, vanilla.height, TextureFormat.RGBA32, true, isBump);

                // Copy the pixels from the RenderTexture to the new Texture
                myTexture2D.ReadPixels(new Rect(0, 0, tmp.width, tmp.height), 0, 0);
                myTexture2D.Apply();

                // Reset the active RenderTexture
                RenderTexture.active = previous;

                // Release the temporary RenderTexture
                RenderTexture.ReleaseTemporary(tmp);

                // "myTexture2D" now has the same pixels from "texture" and it's readable.

                texture.SetPixels(myTexture2D.GetPixels());
                texture.Apply();
            }
            if (layers.Count() > 0)
            {
                Dbgl($"texture {id} has {layers.Count()} layers");
                foreach (var layer in layers.Skip(vanilla == null && !customTextures.ContainsKey(id) ? 1 : 0))
                {

                    Texture2D layerTex = new Texture2D(2, 2, TextureFormat.RGBA32, true, isBump);
                    layerTex.filterMode = isSprite ? FilterMode.Bilinear : FilterMode.Point;
                    byte[] layerData = File.ReadAllBytes(layer.Value);
                    layerTex.LoadImage(layerData);

                    int layerx = 0;
                    int layery = 0;
                    int layerw = layerTex.width;
                    int layerh = layerTex.height;

                    if (isSprite)
                    {
                        string[] coords = layer.Key.Substring(id.Length + 1).Split('_');
                        if (coords.Length != 4 || !int.TryParse(coords[0], out layerx) || !int.TryParse(coords[1], out layery) || !int.TryParse(coords[2], out layerw) || !int.TryParse(coords[3], out layerh))
                        {
                            //logDump.Add($"Improper sprite layer format {layer.Key}");
                            continue;
                        }
                        else
                        {
                            //logDump.Add($"sprite coords {layerx},{layery}, layer sheet size {layerw},{layerh}");
                        }
                    }

                    //8x5, 2x2

                    float scale = texture.width / (float)layerw; // 8 / 2 = 4 or 2 / 8 = 0.25
                    float scaleY = texture.height / (float)layerh; // 5 / 2 = 2.5 or 2 / 5 = 0.4

                    if (scale != scaleY)
                    {
                        //logDump.Add($"incompatible image ratios {tex.width},{tex.height} {layerw},{layerh}");
                        continue;
                    }


                    logDump.Add($"adding layer {layer.Key} to {id}, scale diff {scale}");

                    int startx = 0;
                    int starty = 0;
                    int endx = layerTex.width;
                    int endy = layerTex.height;


                    if (isSprite)
                    {

                        startx = layerx;
                        starty = layery;
                        endx = layerx + layerTex.width;
                        endy = layery + layerTex.height;
                    }

                    // scale

                    if (scale < 1) // layer is bigger, increase tex size
                    {
                        //logDump.Add($"scaling texture up");

                        TextureScale.Bilinear(texture, (int)(texture.width / scale), (int)(texture.height / scale));
                    }
                    else if (scale > 1) // increase layer size
                    {
                        //logDump.Add($"scaling layer up");

                        TextureScale.Bilinear(layerTex, (int)(layerTex.width * scale), (int)(layerTex.height * scale));

                        startx = (int)(layerx * scale);
                        starty = (int)(layery * scale);
                        endx = (int)((layerx + layerTex.width) * scale);
                        endy = (int)((layery + layerTex.height) * scale);
                    }

                    //logDump.Add($"startx {startx}, endx {endx}, starty {starty}, endy {endy}");

                    List<string> coordsl = new List<string>();

                    for (int x = startx; x < endx; x++)
                    {
                        for (int y = starty; y < endy; y++)
                        {
                            int lx = x - startx;
                            int ly = y - starty;

                            Color layerColor = layerTex.GetPixel(lx, ly);

                            if (isSprite)
                            {
                                layerColor = layerTex.GetPixel(lx, layerTex.height - ly);
                                //coordsl.Add($"{x},{y} {lx},{ly} {layerColor}");
                                texture.SetPixel(x, texture.height - y, layerColor);
                            }
                            else
                            {
                                if (layerColor.a == 0)
                                    continue;
                                //coordsl.Add($"{x},{y} {lx},{ly} {layerColor}");
                                Color texColor = texture.GetPixel(x, y);

                                Color final_color = Color.Lerp(texColor, layerColor, layerColor.a / 1.0f);

                                texture.SetPixel(x, y, final_color);
                            }
                        }
                    }
                    //Dbgl(string.Join("\n", coordsl));
                    texture.Apply();
                }
                if (false)
                {
                    //Dbgl($"tex {tex.width},{tex.height}");
                    //byte[] bytes = ImageConversion.EncodeToPNG(tex);
                    //string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), id + "_test.png");
                    //File.WriteAllBytes(path, bytes);
                }
            }

            cachedTextures[id] = texture;
            return texture;
        }
    }
}
