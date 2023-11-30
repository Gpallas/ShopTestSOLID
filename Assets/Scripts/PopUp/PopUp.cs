using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopUp : MonoBehaviour, IPopUpInfo
{
    const float titleFontSize = 30f;
    const float regularFontSize = 27f;

    [SerializeField]
    GameObject textPrefab;
    [SerializeField]
    GameObject divisoryPrefab;
    [SerializeField]
    GameObject textAndImagePrefab;
    [SerializeField]
    GameObject imageNumberTextPrefab;

    [SerializeField]
    Sprite goldIcon;

    [SerializeField]
    RectTransform background;
    [SerializeField]
    RectTransform componentsParent;
    [SerializeField]
    GameObject popUpParent;

    [SerializeField]
    Vector3 popUpOffset;

    List<GameObject> objectsToReparent;

    void Awake()
    {
        objectsToReparent = new List<GameObject>();
    }

    void Update()
    {
        transform.position = Input.mousePosition + popUpOffset;
    }

    public void AddTitle(string titleText, bool shouldResize = true)
    {
        GameObject aux = Instantiate(textPrefab, new Vector3(-10000f, -10000f, 0), Quaternion.identity, transform.parent);

        TextMeshProUGUI textComp = aux.GetComponentInChildren<TextMeshProUGUI>();
        textComp.text = titleText;
        textComp.fontSize = titleFontSize;

        if (shouldResize)
        {
            if (aux.TryGetComponent(out ContentSizeFitter contentFitter))
            {
                contentFitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
                contentFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
            }
        }

        objectsToReparent.Add(aux);
    }

    public void AddText(string text, bool shouldResize = true)
    {
        GameObject aux = Instantiate(textPrefab, new Vector3(-10000f, -10000f, 0), Quaternion.identity, transform.parent);

        TextMeshProUGUI textComp = aux.GetComponentInChildren<TextMeshProUGUI>();
        textComp.text = text;
        textComp.fontSize = regularFontSize;

        if (shouldResize)
        {
            if (aux.TryGetComponent(out ContentSizeFitter contentFitter))
            {
                contentFitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
                contentFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
            }
        }

        objectsToReparent.Add(aux);
    }

    public void AddGoldAmount(int goldAmount, bool shouldResize = true)
    {
        GameObject aux = Instantiate(textAndImagePrefab, new Vector3(-10000f, -10000f, 0), Quaternion.identity, transform.parent);

        aux.GetComponentInChildren<Image>().sprite = goldIcon;
        TextMeshProUGUI textComp = aux.GetComponentInChildren<TextMeshProUGUI>();
        textComp.text = goldAmount.ToString();
        textComp.fontSize = regularFontSize;

        if (shouldResize)
        {
            if (aux.TryGetComponent(out ContentSizeFitter contentFitter))
            {
                contentFitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
                contentFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
            }
        }

        objectsToReparent.Add(aux);
    }

    public void AddDivisory(bool shouldResize = false)
    {
        GameObject aux = Instantiate(divisoryPrefab, new Vector3(-10000f, -10000f, 0), Quaternion.identity, transform.parent);

        if (shouldResize)
        {
            if (aux.TryGetComponent(out ContentSizeFitter contentFitter))
            {
                contentFitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
                contentFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
            }
        }

        objectsToReparent.Add(aux);
    }

    public void AddTextAndImage(string text, Sprite sprite, bool shouldResize = true)
    {
        GameObject aux = Instantiate(textAndImagePrefab, new Vector3(-10000f, -10000f, 0), Quaternion.identity, transform.parent);

        aux.GetComponentInChildren<Image>().sprite = sprite;
        TextMeshProUGUI textComp = aux.GetComponentInChildren<TextMeshProUGUI>();
        textComp.text = text;
        textComp.fontSize = regularFontSize;
        if (shouldResize)
        {
            if (textComp.TryGetComponent(out ContentSizeFitter textContentFitter))
            {
                textContentFitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
                textContentFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
            }
            if (aux.TryGetComponent(out ContentSizeFitter goContentFitter))
            {
                goContentFitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
                goContentFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
            }
        }

        objectsToReparent.Add(aux);
    }

    public void AddCategory(ItemCategory category, bool shouldResize = true)
    {
        if (category != null)
        {
            GameObject aux = Instantiate(textPrefab, new Vector3(-10000f, -10000f, 0), Quaternion.identity, transform.parent);

            TextMeshProUGUI textComp = aux.GetComponentInChildren<TextMeshProUGUI>();
            textComp.text = category.categoryName;
            textComp.fontSize = regularFontSize;
            textComp.color = category.categoryColor;

            if (shouldResize)
            {
                if (aux.TryGetComponent(out ContentSizeFitter contentFitter))
                {
                    contentFitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
                    contentFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
                }
            }

            objectsToReparent.Add(aux);
        }
    }

    public void AddImageWithNumberAndText(Sprite sprite, int amount, string text, bool shouldResize = true)
    {
        GameObject aux = Instantiate(imageNumberTextPrefab, new Vector3(-10000f, -10000f, 0), Quaternion.identity, transform.parent);

        foreach (Transform t in aux.transform)
        {
            if (t.TryGetComponent(out Image imageComp))
            {
                imageComp.sprite = sprite;
                TextMeshProUGUI textComp = imageComp.gameObject.GetComponentInChildren<TextMeshProUGUI>();
                textComp.text = amount.ToString();
            }
            else if (t.TryGetComponent(out TextMeshProUGUI textComp))
            {
                textComp.text = text;
                textComp.fontSize = regularFontSize;

                if (shouldResize)
                {
                    if (textComp.TryGetComponent(out ContentSizeFitter textContentFitter))
                    {
                        textContentFitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
                        textContentFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
                    }
                    if (aux.TryGetComponent(out ContentSizeFitter goContentFitter))
                    {
                        goContentFitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
                        goContentFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
                    }
                }
            }
        }

        objectsToReparent.Add(aux);
    }

    public void FinishPopUp()
    {
        StartCoroutine(ReparentAndActivate());
    }

    IEnumerator ReparentAndActivate()
    {
        yield return null;

        for (int i = 0; i < objectsToReparent.Count; i++)
        {
            objectsToReparent[i].transform.SetParent(componentsParent.transform);
        }

        popUpParent.gameObject.SetActive(true);

        yield return null;

        background.sizeDelta = componentsParent.sizeDelta;
    }

    public void ClearPopUp()
    {
        StopAllCoroutines();
        foreach (GameObject g in objectsToReparent)
        {
            Destroy(g);
        }
        objectsToReparent.Clear();
        popUpParent.gameObject.SetActive(false);
    }
}
