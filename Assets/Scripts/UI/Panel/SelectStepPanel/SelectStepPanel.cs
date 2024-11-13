
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectStepPanel : MonoBehaviour
{
    public Button m_Item;
    public Transform m_Parent;
    public GameObject stepName;
    public List<string> stepNameList = new List<string>();

    private List<Button> m_Items = new List<Button>();
    private bool isSpawnItem = false;

    public Button leftButton;
    public Button rightButton;
    public Scrollbar scrollBar;

    public void Awake() { }

    public void Start()
    {
        SpawItems();
        stepName.gameObject.SetActive(false);
        leftButton.onClick.AddListener(() => { scrollBar.value -= 0.05f;});
        rightButton.onClick.AddListener(() => { scrollBar.value += 0.05f; });
    }

    private void Update()
    {
        ShowStepName();
    }

    public void SpawItems()
    {
        for (int i = 0; i < stepNameList.Count; i++)
        {
            Button button = Instantiate(m_Item, m_Parent);
            button.gameObject.SetActive(true);
            m_Items.Add(button);
        }
        isSpawnItem = true;
    }

    /// <summary>
    /// 展示步骤名字
    /// </summary>
    public void ShowStepName()
    {
        if (isSpawnItem)
        {
            bool allNoHightlight = false;
            for (int i = 0; i < m_Items.Count(); ++i)
            {
                var item = m_Items[i];
                bool highlight = RectTransformUtility.RectangleContainsScreenPoint(item.GetComponent<RectTransform>(), Input.mousePosition);
                if (highlight)
                {
                    Vector3 itemPostion = item.transform.position;
                    stepName.transform.position = new Vector3(itemPostion.x, stepName.transform.position.y, stepName.transform.position.z);
                    stepName.GetComponentInChildren<TextMeshProUGUI>().text = stepNameList[i];
                    stepName.gameObject.SetActive(true);
                }
                allNoHightlight |= highlight;
            }

            if (!allNoHightlight)
                stepName.gameObject.SetActive(false);
        }
    }

    private bool check(string txt)
    {
        return !string.IsNullOrEmpty(txt) && txt.Count() > 0;
    }
}
