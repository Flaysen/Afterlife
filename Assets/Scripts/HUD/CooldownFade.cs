using UnityEngine;
using UnityEngine.UI;

public class CooldownFade : MonoBehaviour
{
    private Image _image;

    void Awake()
    {
        _image = GetComponent<Image>();

        _image.fillMethod = Image.FillMethod.Radial360;

        _image.fillOrigin = (int)Image.Origin360.Top;

        _image.fillClockwise = false;

        _image.fillAmount = 0;
    }

    public void AdjustFade(float damageToCast, float cooldown)
    {
        _image.fillAmount = damageToCast / cooldown;
    }
}
