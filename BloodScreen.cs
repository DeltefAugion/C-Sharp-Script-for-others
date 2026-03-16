using UnityEngine;
using UnityEngine.UI;

public class BloodScreen : MonoBehaviour
{
    [Header("Trigger Einstellungen (Auf das Trigger-Objekt legen)")]
    public GameObject bloodUIPrefab; // Das UI-Image 
    public Transform canvasTransform; // Dein canvas
    public bool isTrigger = true;

    [Header("Blut-Effekt Einstellungen (Auf das Prefab legen)")]
    public Sprite[] bloodSprites;
    public float fadeSpeed = 1.0f;
    
  
    private Image img;
    private Color clr;

    private void Start()
    {
    
        if (GetComponent<Image>() != null)
        {
            RectTransform rct = GetComponent<RectTransform>();
            // Zufällige Position 
            rct.anchoredPosition = new Vector2(Random.Range(-400, 400), Random.Range(-250, 250));
            
            img = GetComponent<Image>();
            // Zufälliges Bild
            if (bloodSprites.Length > 0)
                img.sprite = bloodSprites[Random.Range(0, bloodSprites.Length)];
            
            clr = img.color;
        }
    }

// alles flexibel
    private void Update()
    {
        
        if (img != null)
        {
            if (clr.a > 0f)
            {
                clr.a -= Time.deltaTime * fadeSpeed;
                img.color = clr;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    // wenn in trigger blut screen
    private void OnTriggerEnter(Collider other)
    {
        if (isTrigger && other.CompareTag("Player"))
        {
            
            if (bloodUIPrefab != null && canvasTransform != null)
            {
                Instantiate(bloodUIPrefab, canvasTransform);
            }
        }
    }
}
