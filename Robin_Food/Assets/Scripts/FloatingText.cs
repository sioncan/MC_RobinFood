using UnityEngine;
using UnityEngine.UI;

public class FloatingText
{
    public bool activated;
    public GameObject go;
    public Text text;
    public Vector3 motion;
    public float duration;
    public float lastShown;

    // mostra il testo
    public void Show()
    {
        activated = true;
        lastShown = Time.time;
        go.SetActive(activated);
    }

    // nasconde il testo (non lo distrugge)
    public void Hide()
    {
        activated = false;
        go.SetActive(activated);
    }

    public void UpdateFloatingText()
    {
        if (!activated)
            return;
        if (Time.time - lastShown > duration)
            Hide();

        go.transform.position += motion * Time.deltaTime;
    }
}
