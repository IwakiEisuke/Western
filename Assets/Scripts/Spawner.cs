using System.Threading.Tasks;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject _spawnObj;
    [SerializeField, Min(1)] int _spawnSpan = 100;
    [SerializeField] Vector3 _min;
    [SerializeField] Vector3 _max;

    async void Start()
    {
        while (UnityEditor.EditorApplication.isPlaying)
        {
            Instantiate(_spawnObj, new Vector3(Random.Range(_min.x, _max.x), Random.Range(_min.y, _max.y), Random.Range(_min.z, _max.z)), Quaternion.identity);
            await Task.Delay(_spawnSpan);
        }
    }
}
