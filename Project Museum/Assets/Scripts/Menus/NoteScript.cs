using UnityEngine;
using UnityEngine.UI;

public class NoteScript : MonoBehaviour {
    public Canvas canvas;               // Reference to the Canvas GameObject
    public Sprite noteSprite;           // The new sprite for the note
    private RectTransform rectTransform;

    public Image noteImage;

    public void GetNoteImage() {
        // Attributes the noteSprite to the noteImage
        noteImage.sprite = noteSprite;
    }

    public void UIImageScaler() {

        rectTransform = noteImage.GetComponent<RectTransform>();
        if (noteImage.sprite != null) {
            float spriteWidth = noteImage.sprite.rect.width;
            float spriteHeight = noteImage.sprite.rect.height;

            rectTransform.sizeDelta = new Vector2(spriteWidth, spriteHeight);
        }
    }
}