using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingsPanel : MonoBehaviour
{
    public GameObject buildingPanelPrefab;
    public List<BuildableEntity> buildings;
    void Start()
    {
        if (buildings != null)
        {
            buildings.ForEach(item =>
            {
                GameObject panel = Instantiate(buildingPanelPrefab, transform.position, Quaternion.identity);
                panel.name = item.name;
                Image sprite = panel.transform.Find("Sprite").GetComponent<Image>();
                sprite.overrideSprite = item.sprite;

                panel.transform.SetParent(transform, true);

                Button button = panel.GetComponent<Button>();
                button.onClick.AddListener(() => { BuildingMode.INSTANCE.setBuildingMode(BuildingState.BUILDING); BuildingMode.INSTANCE.setBuildingEntity(item); });
            });
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
