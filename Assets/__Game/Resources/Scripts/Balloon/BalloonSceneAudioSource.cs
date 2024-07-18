using __Game.Resources.Scripts.EventBus;
using UnityEngine;

namespace Assets.__Game.Resources.Scripts.Balloon
{
  public class BalloonSceneAudioSource : MonoBehaviour
  {
    private AudioSource _audioSource;

    private EventBinding<EventStructs.BalloonAudioEvent> _balloonAudioEvent;

    private void Awake()
    {
      _audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
      _balloonAudioEvent = new EventBinding<EventStructs.BalloonAudioEvent>(PlayWordClip);
    }

    private void OnDisable()
    {
      _balloonAudioEvent.Remove(PlayWordClip);
    }

    private void PlayWordClip(EventStructs.BalloonAudioEvent balloonAudioEvent)
    {
      _audioSource.PlayOneShot(balloonAudioEvent.WordClip);
    }
  }
}