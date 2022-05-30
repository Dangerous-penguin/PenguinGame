using System.Collections;
using DangerousPenguin.Abilities;
using UnityEngine;

namespace DangerousPenguin
{
    public class AbilityEnabler : MonoBehaviour
    {
        [SerializeField]
        private float _triggerDistance = 4f;

        [SerializeField]
        private Transform _player;

        [SerializeField]
        private bool _active = true;

        [SerializeField]
        private AbilityBase _ability;

        private Light _light;
        private ParticleSystem _particles;

        private void Awake()
        {
            _light = GetComponent<Light>();
            _particles = GetComponent<ParticleSystem>();
        }
        private void Update()
        {
            if (!_active || !_player) return;
            if (Vector3.Distance(transform.position, _player.position) < _triggerDistance)
            {
                _active = false;
                _player.GetComponent<SkillManager>().ReceiveSkill(_ability);
                StartCoroutine(FadeAway());
            }
        }

        private IEnumerator FadeAway()
        {
            _light.intensity = 50f;
          //  _ability.isActive = true;
            while (_light.intensity > 0)
            {
                _light.intensity -= 0.5f;
                yield return new WaitForSeconds(0.01f);
            }

            _particles.Stop();
            yield return new WaitForSeconds(2f);
            Destroy(gameObject);
        }
    }
}
