using UnityEngine;

public class LineClearEffect : MonoBehaviour
{
    public float blinkingPeriod = 0.1f;

    private SpriteRenderer spriteRenderer;
    
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float phase = (Time.time % blinkingPeriod) / blinkingPeriod;
        if (phase > 0.5)
        {
            spriteRenderer.enabled = false;
        }
        else
        {
            spriteRenderer.enabled = true;
        }
    }
}
