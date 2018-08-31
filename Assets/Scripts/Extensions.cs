using UnityEngine;

public static class Extensions
{
    public static void SetTransButtonImageColor(this UnityEngine.UI.Image p_image, float p_transparency)
    {
        if (p_image != null)
        {
            UnityEngine.Color __alpha = p_image.color;
            __alpha.a = p_transparency;
            p_image.color = __alpha;
        }
    }

    public static void SetTransTextColor(this UnityEngine.UI.Text p_text, float p_transparency)
    {
        if (p_text != null)
        {
            UnityEngine.Color __alpha = p_text.color;
            __alpha.a = p_transparency;
            p_text.color = __alpha;
        }
    }

    public static void SetSize(this RectTransform self, Vector2 size)
    {
        Vector2 oldSize = self.rect.size;
        Vector2 deltaSize = size - oldSize;

        self.offsetMin = self.offsetMin - new Vector2(
            deltaSize.x * self.pivot.x,
            deltaSize.y * self.pivot.y);
        self.offsetMax = self.offsetMax + new Vector2(
            deltaSize.x * (1f - self.pivot.x),
            deltaSize.y * (1f - self.pivot.y));
    }

    public static void SetWidth(this RectTransform self, float size)
    {
        self.SetSize(new Vector2(size, self.rect.size.y));
    }

    public static void SetHeight(this RectTransform self, float size)
    {
        self.SetSize(new Vector2(self.rect.size.x, size));
    }

}