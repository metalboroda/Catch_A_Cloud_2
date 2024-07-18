using __Game.Resources.Scripts.EventBus;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.__Game.Resources.Scripts.Balloon
{
  public class BalloonHandler : MonoBehaviour, IPointerClickHandler
  {
    [SerializeField] private SpriteRenderer _cloudSpriteRenderer;
    [Space]
    [SerializeField] private Sprite[] _cloudSpritesToSpawn;

    private string _balloonValue;
    private bool _correct;
    private AudioClip _wordClip;

    public string BalloonValue
    {
      get => _balloonValue;
      private set => _balloonValue = value;
    }

    public bool Correct
    {
      get => _correct;
      private set => _correct = value;
    }

    private Collider _collider;

    private void Awake()
    {
      _collider = GetComponent<Collider>();

      SpawnRandomCLoudSprite();
    }

    public void SetBalloonDetails(string value, bool correct, AudioClip wordClip, bool tutorial = false)
    {
      _balloonValue = value;
      _correct = correct;
      _wordClip = wordClip;

      EventBus<EventStructs.BalloonUiEvent>.Raise(new EventStructs.BalloonUiEvent
      {
        BalloonId = transform.GetInstanceID(),
        BalloonValue = _balloonValue,
        Correct = _correct,
        WordClip = _wordClip,
        Tutorial = tutorial
      });
    }

    public void OnPointerClick(PointerEventData eventData)
    {
      EventBus<EventStructs.BalloonClickEvent>.Raise(new EventStructs.BalloonClickEvent
      {
        BalloonHandler = this,
        BalloonValue = _balloonValue
      });
    }

    public void DestroyBalloon(bool correct)
    {
      EventBus<EventStructs.BalloonDestroyEvent>.Raise(new EventStructs.BalloonDestroyEvent
      {
        BalloonId = transform.GetInstanceID(),
        Correct = correct
      });

      _collider.enabled = false;

      EventBus<EventStructs.BalloonAudioEvent>.Raise(new EventStructs.BalloonAudioEvent { WordClip = _wordClip });

      //DOTween.Kill(transform);
      //Destroy(gameObject);
    }

    private void SpawnRandomCLoudSprite()
    {
      _cloudSpriteRenderer.sprite = _cloudSpritesToSpawn[Random.Range(0, _cloudSpritesToSpawn.Length)];
    }
  }
}