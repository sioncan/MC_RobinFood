using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingTextManager : MonoBehaviour
{
    public GameObject textContainer;
    public GameObject textPrefabs;
    private List<FloatingText> floatingTexts = new List<FloatingText>();

    // mostra il testo
    public void Show(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration)
    {
        FloatingText floatingText = GetFloatingText();
        floatingText.text.text = msg;
        floatingText.text.fontSize = fontSize;
        floatingText.text.color = color;
        floatingText.go.transform.position = Camera.main.WorldToScreenPoint(position); // trasferisce WorldSpace to ScreenSpace cosi possiamo usarlo nella UI
        floatingText.motion = motion;
        floatingText.duration = duration;
        floatingText.Show();
    }

    // prende un testo dal pool, se nessuno è disponibile, ne crea uno
    private FloatingText GetFloatingText()
    {
        // trova un testo che non è stato attivato
        FloatingText text = floatingTexts.Find(t => !t.activated);

        // se non ne troviamo nessuno, ne creo uno nuovo (l'idea è quella di avere un pool di testi disponibili al volo)
        if(text == null)
        {
            text = new FloatingText();
            text.go = Instantiate(textPrefabs);  // gli assegno un nuovo gameObject
            text.go.transform.SetParent(textContainer.transform);
            text.text = text.go.GetComponent<Text>();

            floatingTexts.Add(text);
        } 
        return text;
    }

    // Update is called once per frame
    void Update()
    {
        foreach(FloatingText text in floatingTexts)
        {
            text.UpdateFloatingText();
        }
    }
}
