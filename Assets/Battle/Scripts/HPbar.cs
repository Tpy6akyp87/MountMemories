using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HPbar : MonoBehaviour
{
    [SerializeField] private Image fillImage;
   // [SerializeField] private TextMeshProUGUI hpText; // если используешь цифры
    [SerializeField] private float smoothSpeed = 5f; // для плавного уменьшения

    private float targetFill = 1f;

    public void SetHP(float currentHP, float maxHP)
    {
        targetFill = currentHP / maxHP;
    }

    private void Update()
    {
        // Плавное изменение (очень красиво выглядит)
        fillImage.fillAmount = Mathf.Lerp(fillImage.fillAmount, targetFill, Time.deltaTime * smoothSpeed);

        // Всегда смотрит в камеру (билборд)
        transform.forward = Camera.main.transform.forward;
    }
}
