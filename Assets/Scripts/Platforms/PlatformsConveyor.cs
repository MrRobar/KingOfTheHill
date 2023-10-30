using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class PlatformsConveyor : MonoBehaviour
{
    [SerializeField] private InputObserver _inputObserver;
    [SerializeField] private bool _isMoving = false;
    [SerializeField] private float _speed = 1f;
    [SerializeField] private int _amount = 15;
    private int _cyclingPivot = 1;
    private CancellationToken _token;
    private CancellationTokenSource _source;

    public GameObject[] Platforms { get; set; }

    private void Start()
    {
        _source = new CancellationTokenSource();
        _token = _source.Token;
    }

    private void OnEnable()
    {
        _inputObserver.OnForwardJumpInputRegistered += TryCycle;
    }

    private void OnDisable()
    {
        _inputObserver.OnForwardJumpInputRegistered -= TryCycle;
    }

    private void TryCycle()
    {
        if (_isMoving)
        {
            return;
        }

        _isMoving = true;
        CyclePlatformsArray();
    }

    private void CyclePlatformsArray()
    {
        GameObject[] newArr = new GameObject[_amount];
        for (int i = 0; i < Platforms.Length; i++)
        {
            if (_cyclingPivot + i < Platforms.Length)
            {
                newArr[i] = Platforms[_cyclingPivot + i];
            }
            else
            {
                newArr[i] = Platforms[_cyclingPivot + i - Platforms.Length];
            }
        }

        Platforms = newArr;
        ShiftPlatforms();
    }

    private void ShiftPlatforms()
    {
        var newLast = Platforms[_amount - 1];
        newLast.transform.localPosition = new Vector3(0f, _amount, _amount);
        for (int i = 0; i < _amount; i++)
        {
            DisplacePlatformAsync(Platforms[i], new Vector3(0f, i, i), _token);
        }
    }

    private async void DisplacePlatformAsync(GameObject platform, Vector3 position, CancellationToken token)
    {
        await MovePlatformTask(platform, position, token);
        _isMoving = false;
    }

    private async Task MovePlatformTask(GameObject platform, Vector3 position, CancellationToken token)
    {
        while (platform.transform.position != position)
        {
            platform.transform.position =
                Vector3.MoveTowards(platform.transform.position, position, _speed * Time.deltaTime);
            if (token.IsCancellationRequested)
            {
                break;
            }

            await Task.Yield();
        }
    }
}