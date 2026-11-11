using TMPro;
using UnityEngine;
using System.Globalization;

public class DamageNumber : MonoBehaviour
{
    [SerializeField] TMP_Text text;

    [Header("Movimento")]
    [SerializeField] float rise_speed = 1f;
    [SerializeField] float duration = 0.8f;

    [Header("Aparência")]
    [SerializeField] Color normal_color = Color.white;
    [SerializeField] Color crit_color = Color.red;
    [SerializeField] float crit_scale = 1.3f;

    float timer = 0f;
    Color start_color;

    public void Setup(float damage, bool is_crit)
    {
        string value = damage.ToString("0.#", CultureInfo.GetCultureInfo("pt-BR"));
        text.text = is_crit ? value + "!" : value;

        start_color = is_crit ? crit_color : normal_color;
        text.color = start_color;

        if (is_crit)
        {
            transform.localScale *= crit_scale;
        }
    }

    private void Update()
    {
        timer += Time.deltaTime;

        // Sobe
        transform.position += Vector3.up * rise_speed * Time.deltaTime;

        // Some aos poucos: fica opaco nos primeiros 40% e depois desaparece
        float t = timer / duration;
        float alpha = 1f - Mathf.InverseLerp(0.4f, 1f, t);

        Color color = start_color;
        color.a = alpha;
        text.color = color;

        if (timer >= duration)
        {
            Destroy(gameObject);
        }
    }
}