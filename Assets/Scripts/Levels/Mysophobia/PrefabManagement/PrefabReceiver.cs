using UnityEngine;

public class PrefabReceiver : ScriptableObject
{
    private PrefabDictionary _prefabs;

    private string _providerName = null;

    protected void ReceivePrefab(string providerName)
    {
        _providerName = providerName;
        _prefabs = GameObject.Find(providerName).GetComponent<PrefabProvider>().Prefabs;
    }

    protected GameObject GetPrefab(string key)
    {
        if (_providerName == null)
        {
            Debug.LogError("Please receive the prefab before trying to get it");
            return (null);
        }
        if (!_prefabs.ContainsKey(key))
        {
            Debug.LogError("Prefab not found in provider '" + _providerName + "' for key '" + key + "'");
            return (null);
        }
        return (_prefabs[key]);
    }
}
