using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingTextManager : MonoBehaviour
{
    public GameObject textContainer;
    public GameObject textPrefab;
    private List<FloatingText> floatingTexts = new List<FloatingText>();

    // mostra il testo
    public void Show(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration)
    {
        FloatingText floatingText = GetFloatingText();
        floatingText.txt.text = msg;
        floatingText.txt.fontSize = fontSize;
        floatingText.txt.color = color;
        floatingText.go.transform.position = Camera.main.WorldToScreenPoint(position); // trasferisce WorldSpace to ScreenSpace cosi possiamo usarlo nella UI
        floatingText.motion = motion;
        floatingText.duration = duration;
        floatingText.Show();
    }

    // prende un testo dal pool, se nessuno è disponibile, ne crea uno
    private FloatingText GetFloatingText()
    {
        // trova un testo che non è stato attivato
        FloatingText txt = floatingTexts.Find(t => !t.active);

        // se non ne troviamo nessuno, ne creo uno nuovo (l'idea è quella di avere un pool di testi disponibili al volo)
        if(txt == null)
        {
            txt = new FloatingText();
            txt.go = Instantiate(textPrefab);  // gli assegno un nuovo gameObject
            txt.go.transform.SetParent(textContainer.transform);
            txt.txt = txt.go.GetComponent<Text>();

            floatingTexts.Add(txt);
        } 
        return txt;
    }

    // Update is called once per frame
    private void Update()
    {
        foreach(FloatingText txt in floatingTexts)
        {
            txt.UpdateFloatingText();
        }
    }
}