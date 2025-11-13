using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Rendering;


public class CityGenerator : EditorWindow
{
    int gridSizeX = 10;
    int gridSizeZ = 10;
    float buildingSpacing = 15f;
    float roadWidth = 5f;

    bool makeStatic = true;

    [MenuItem("Tools/City Generator")]

    public static void ShowWindow()
    {
        GetWindow<CityGenerator>("City Generator");
    }

    private void OnGUI()
    {
        GUILayout.Label("Simple City Generator", EditorStyles.boldLabel);

        gridSizeX = EditorGUILayout.IntField("Grid Size X", gridSizeX);
        gridSizeZ = EditorGUILayout.IntField("Grid Size Z", gridSizeZ);

        buildingSpacing = EditorGUILayout.FloatField("Building Spacing", buildingSpacing);

        roadWidth = EditorGUILayout.FloatField("Road Width", roadWidth);
        makeStatic = EditorGUILayout.Toggle("Make Static", makeStatic);
        GUILayout.Space(10);
        if(GUILayout.Button("Generate City"))
        {
            GenerateCity();
        }

        if(GUILayout.Button("Clear City"))
        {
            ClearCity();
        }
    }

    void CreateBuilding(Vector3 position, Transform parent)
    {
        GameObject building = GameObject.CreatePrimitive(PrimitiveType.Cube);
        building.name = "Building";

        float height = Random.Range(5.0f, 20.0f);
        building.transform.position = position + Vector3.up * height / 2.0f;
        building.transform.localScale = new Vector3(buildingSpacing - roadWidth - 1f, height, buildingSpacing - roadWidth - 1f);
        building.transform.SetParent(parent);

        Renderer renderer = building.GetComponent<Renderer>();
        renderer.material.color = new Color(Random.Range(0.5f, 0.8f), Random.Range(0.5f, 0.8f), Random.Range(0.5f, 0.8f));

        if(makeStatic)
        {
            building.isStatic = true;
        }
    }

    void CreateRoad(Vector3 position, Transform parent)
    {
        GameObject road = GameObject.CreatePrimitive(PrimitiveType.Cube);

        road.transform.position = position + Vector3.up * 0.1f;
        road.transform.localScale = new Vector3(buildingSpacing, 0.2f, buildingSpacing);
        road.transform.SetParent(parent);

        Renderer renderer = road.GetComponent<Renderer>();
        renderer.material.color = new Color(0.3f, 0.3f, 0.3f);

        if(makeStatic)
        {
            road.isStatic = true;
        }
    }

    void ClearCity()
    {
        GameObject city = GameObject.Find("City");

        if(city != null)
        {
            DestroyImmediate(city);
        }

    }

    void GenerateCity()
    {
        GameObject cityParent = new GameObject("City");

        GameObject buildingsParent = new GameObject("Buildings");
        buildingsParent.transform.SetParent(cityParent.transform, false);

        GameObject roadsParent = new GameObject("Roads");
        roadsParent.transform.SetParent(cityParent.transform, false);

        for(int x = 0; x < gridSizeX; x++)
        {
            for(int z = 0; z < gridSizeZ; z++)
            {
                Vector3 position = new Vector3(x * buildingSpacing, 0, z * buildingSpacing);

                if(x % 2 == 0 || z % 2 == 0)
                {
                    CreateRoad(position, roadsParent.transform);
                }
                else
                {
                    CreateBuilding(position, buildingsParent.transform);
                }
            }
        }
    }
}