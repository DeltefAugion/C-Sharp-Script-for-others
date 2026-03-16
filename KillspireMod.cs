using BepInEx;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

[BepInPlugin("com.mod.killspire.uiexample", "UI Example Mod", "1.0.0")]
public class KillspireMod : BaseUnityPlugin
{
    void Start()
    {
        Logger.LogInfo("UI Mod loaded!");
        CreateMenuUI();
    }

    void CreateMenuUI()
    {
        // Canvas in der Szene finden
        Canvas menuCanvas = GameObject.FindObjectOfType<Canvas>();
        if (menuCanvas == null)
        {
            Logger.LogWarning("Kein Canvas gefunden!");
            return;
        }

        // Neues Panel erstellen
        GameObject panelGO = new GameObject("ModPanel");
        panelGO.transform.SetParent(menuCanvas.transform, false);
        Image panelImage = panelGO.AddComponent<Image>();
        panelImage.color = new Color(0f, 0f, 0f, 0.5f); // halbtransparent

        RectTransform panelRT = panelGO.GetComponent<RectTransform>();
        panelRT.sizeDelta = new Vector2(300, 200);
        panelRT.anchoredPosition = new Vector2(0, 0);

        // Button erstellen
        GameObject buttonGO = new GameObject("ModButton");
        buttonGO.transform.SetParent(panelGO.transform, false);
        Button button = buttonGO.AddComponent<Button>();
        Image btnImage = buttonGO.AddComponent<Image>();
        btnImage.color = Color.gray;

        RectTransform btnRT = buttonGO.GetComponent<RectTransform>();
        btnRT.sizeDelta = new Vector2(200, 50);
        btnRT.anchoredPosition = new Vector2(0, -50);

        // Button Text (TextMeshPro)
        GameObject textGO = new GameObject("ButtonText");
        textGO.transform.SetParent(buttonGO.transform, false);
        TextMeshProUGUI tmp = textGO.AddComponent<TextMeshProUGUI>();
        tmp.text = "Click Me!";
        tmp.alignment = TextAlignmentOptions.Center;
        tmp.fontSize = 24;

        RectTransform textRT = tmp.GetComponent<RectTransform>();
        textRT.sizeDelta = new Vector2(200, 50);
        textRT.anchoredPosition = Vector2.zero;

        // Button Event
        button.onClick.AddListener(() => { Logger.LogInfo("Button geklickt!"); });
    }
}
