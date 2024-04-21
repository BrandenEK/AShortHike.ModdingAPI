using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;

namespace AShortHike.ModdingAPI;

public class ModdingAPI : ShortHikeMod
{
    public ModdingAPI() : base(ModInfo.MOD_ID, ModInfo.MOD_NAME, ModInfo.MOD_AUTHOR, ModInfo.MOD_VERSION) { }

    protected internal override void OnLevelLoaded(string level)
    {
        if (level == "TitleScene")
        {
            // Find canvas
            Canvas canvas = Object.FindObjectOfType<Canvas>();
            if (canvas == null)
                return;

            // Create text for mod list
            StringBuilder fullText = new();
            foreach (var mod in Main.ModLoader.AllMods.OrderBy(GetModPriority).ThenBy(x => x.Name))
            {
                fullText.AppendLine(GetModText(mod, true));
            }

            // Create rect transform
            RectTransform r = new GameObject().AddComponent<RectTransform>();
            r.name = "Mod list";
            r.SetParent(canvas.transform, false);
            r.anchorMin = new Vector2(0, 0);
            r.anchorMax = new Vector2(1, 1);
            r.pivot = new Vector2(0, 1);
            r.anchoredPosition = new Vector2(5, -5);
            r.sizeDelta = new Vector2(400, 100);

            // Create text
            TextMeshProUGUI t = r.gameObject.AddComponent<TextMeshProUGUI>();
            t.text = fullText.ToString();
            t.alignment = TextAlignmentOptions.TopLeft;
            t.font = Object.FindObjectOfType<TextMeshProUGUI>().font;
            t.fontSize = 16;
            t.richText = true;
        }
    }

    private int GetModPriority(ShortHikeMod mod)
    {
        if (mod == this)
            return -1;

        if (mod.Name.EndsWith("Framework"))
            return 0;

        return 1;
    }

    private string GetModText(ShortHikeMod mod, bool addColor)
    {
        string line = $"{mod.Name} v{mod.Version}";

        if (!addColor)
            return line;

        string color = mod == this || mod.Name.EndsWith("Framework") ? "7CA7BF" : "D3D3D3";
        return $"<color=#{color}>{line}</color>";
    }
}