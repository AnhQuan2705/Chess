using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ResourceController 
{
    private ResourceController()
    { }
    private static ResourceController _instance = null;
    public static ResourceController Instance
    {
        get
        {
            if(_instance == null)
                _instance = new ResourceController();
            return _instance;     
        } 
    }
    private Material _blackCellMaterial = null;
    public Material BlackCellMaterial
    {
        get 
        {
            if (_blackCellMaterial == null)
            {
                _blackCellMaterial = Resources.Load<Material>("Materials/BlackCell");
            }
            return _blackCellMaterial;
        }
    }
    private Material _whiteCellMaterial = null;
    public Material WhiteCellMaterial
    {
        get
        {
            if (_whiteCellMaterial == null)
        
                _whiteCellMaterial = Resources.Load<Material>("Materials/WhiteCell");
           
            return _whiteCellMaterial;
        }
    }

    private Dictionary<string, GameObject> _dist = new Dictionary<string, GameObject>();
    public GameObject GetGameObject(string path)
    {
        if (_dist.ContainsKey(path) == false)
            _dist.Add(path, Resources.Load<GameObject>(path));
        return _dist[path];
        
    }
}
