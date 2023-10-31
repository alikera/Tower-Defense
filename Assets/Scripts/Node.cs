using TMPro;
using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
public class Node : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI towerExists;
    
    [SerializeField] private Renderer mashRend;
    private Color _initialColor;
    [SerializeField] private Color hoverColor;

    private GameObject _tower;

    [SerializeField] private Vector3 towerPositionOffset;
    private void Awake()
    {
        towerPositionOffset = new Vector3(0f, 0.5f, 0f);
        mashRend = GetComponent<Renderer>();
        _initialColor = mashRend.material.color;
    }

    private void OnMouseEnter()
    {
        if(EventSystem.current.IsPointerOverGameObject())
            return;
        GameObject towerToBuild = BuildManager.Instance.GetTowerToBuild();
        if(towerToBuild != null)
            mashRend.material.color = hoverColor;
    }
    
    private void OnMouseExit()
    {
        mashRend.material.color = _initialColor;
    }
    private IEnumerator IntroFade () {
        yield return StartCoroutine(FadeInText(3f, towerExists));
        yield return new WaitForSeconds(2f);
        yield return StartCoroutine(FadeOutText(1f, towerExists));
    }
    private IEnumerator FadeInText(float timeSpeed, TextMeshProUGUI text)
    {
        text.color = new Color(text.color.r, text.color.g, text.color.b, 0);
        while (text.color.a < 1.0f)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a + (Time.deltaTime * timeSpeed));
            yield return null;
        }
    }

    private IEnumerator FadeOutText(float timeSpeed, TextMeshProUGUI text)
    {
        text.color = new Color(text.color.r, text.color.g, text.color.b, 1);
        while (text.color.a > 0.0f)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b,
                text.color.a - (Time.deltaTime * timeSpeed));
            yield return null;
        }
    }

    private void OnMouseDown()
    {
        if(EventSystem.current.IsPointerOverGameObject())
            return;
        GameObject towerToBuild = BuildManager.Instance.GetTowerToBuild();
        if (_tower != null && towerToBuild != null)
        {
            StartCoroutine(IntroFade());
        }
        else
        {
            if (towerToBuild != null)
            {
                _tower = Instantiate(towerToBuild, transform.position + towerPositionOffset, transform.rotation);
                BuildManager.Instance.SetTowerToBuild(null);
                GetComponent<Shop>().SetTurretPurchased();
            }
        }
    }
}
