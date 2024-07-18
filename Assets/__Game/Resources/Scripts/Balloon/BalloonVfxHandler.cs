using __Game.Resources.Scripts.EventBus;
using DG.Tweening;
using UnityEngine;

namespace Assets.__Game.Resources.Scripts.Balloon
{
  public class BalloonVfxHandler : MonoBehaviour
  {
    [SerializeField] private GameObject _bubblesParticlesPrefab;
    [SerializeField] private GameObject _starPrefab;
    [SerializeField] private GameObject _angryFaceParticlesPrefab;
    [SerializeField] private Transform _particlesSpawnPoint;

    private EventBinding<EventStructs.BalloonDestroyEvent> _fishDestroyEvent;

    private void OnEnable()
    {
      _fishDestroyEvent = new EventBinding<EventStructs.BalloonDestroyEvent>(SpawnDestroyParticles);
    }

    private void OnDisable()
    {
      _fishDestroyEvent.Remove(SpawnDestroyParticles);
    }

    private void SpawnDestroyParticles(EventStructs.BalloonDestroyEvent balloonDestroyEvent)
    {
      if (balloonDestroyEvent.BalloonId != transform.GetInstanceID()) return;

      transform.DOPunchScale(new Vector3(0.25f, 0.25f), 0.5f)
        .SetEase(Ease.InOutQuad)
        .OnComplete(() =>
        {
          SpawnParticle(balloonDestroyEvent.Correct ? _starPrefab : _angryFaceParticlesPrefab);
          SpawnParticle(_bubblesParticlesPrefab);
          Destroy(gameObject);
        });
    }

    private void SpawnParticle(GameObject prefab)
    {
      Instantiate(prefab, _particlesSpawnPoint.position, Quaternion.identity);
    }
  }
}