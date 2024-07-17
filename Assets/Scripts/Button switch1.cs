using UnityEngine;
using UnityEngine.UI;

public class ChangeSpriteOnClick : MonoBehaviour
{
    public Sprite glowingSprite; // Assign the glowing sprite in the inspector
    private Image buttonImage;
    private Sprite originalSprite;

    void Start()
    {
        buttonImage = GetComponent<Image>();
        originalSprite = buttonImage.sprite;
    }

    public void ChangeSprite()
    {
        buttonImage.sprite = glowingSprite;
    }

    public void RevertSprite()
    {
        buttonImage.sprite = originalSprite;
    }
}
