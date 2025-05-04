using UnityEngine;

public class SpellCreator : MonoBehaviour
{
    public RenderTexture renderTexture;

    private Texture2D renderTextureToTexture;
    private Texture2D spellTextureFile;

    void Start()
    {
        renderTextureToTexture = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.ARGB32, false);
        //Load a Texture (Assets/Resources/Spells/Spell1.png)
        spellTextureFile = Resources.Load<Texture2D>("Spells/Spell1");
        SaveTexture();
    }

    private void SaveTexture()
    {
        ToTexture2D();
        for (int x = 0; x < spellTextureFile.width; x++)
        {
            for (int y = 0; y < spellTextureFile.height; y++)
            {
                spellTextureFile.SetPixel(x, y, renderTextureToTexture.GetPixel(x + 510, y + 90));
            }
        }
        spellTextureFile.Apply();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            SaveTexture();
        }
    }

    private void ToTexture2D()
    {
        RenderTexture.active = renderTexture;
        renderTextureToTexture.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        renderTextureToTexture.Apply();
    }
}
