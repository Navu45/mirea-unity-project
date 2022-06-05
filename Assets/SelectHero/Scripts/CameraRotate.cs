using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;


namespace SelectHero.Scripts
{
    [RequireComponent(typeof(AudioSource))]
    public class CameraRotate : MonoBehaviour
    {
        [Header("Animation speed")] [SerializeField]
        float rotateSpeed = 5;

        int _index = 0;


        [Header("Turn on audio")] [SerializeField]
        AudioClip lightaudioClip;

        AudioSource _audioSource;

        [Serializable]
        public class Hero
        {
            public string name;
            [TextArea] public string description;
            public Light light;
            public Transform cameraPosition;
        }

        [Header("Heroes")] [SerializeField] Hero[] heros;
        [SerializeField] TMP_Text heroName;
        [SerializeField] TMP_Text heroDescription;
        [SerializeField] GameObject heroPanel;

        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
            TurnOnLight();
        }

        public void SelectRight()
        {
            TurnOffLight();

            if (_index == 2)
                _index = 0;
            else
                _index++;
            transform.DOMove(heros[_index].cameraPosition.position, rotateSpeed);
            transform.DORotateQuaternion(heros[_index].cameraPosition.rotation, rotateSpeed).OnComplete(() =>
            {
                TurnOnLight();
            });
        }

        void TurnOnLight()
        {
            heroPanel.gameObject.SetActive(true);
            heros[_index].light.enabled = true;
            heroName.gameObject.SetActive(true);
            heroDescription.gameObject.SetActive(true);

            heroName.text = heros[_index].name;
            heroDescription.text = heros[_index].description;

            PlayLightSound();
        }

        void TurnOffLight()
        {
            heroName.gameObject.SetActive(false);
            heroDescription.gameObject.SetActive(false);
            heros[_index].light.enabled = false;
            heroPanel.gameObject.SetActive(false);
        }

        void PlayLightSound()
        {
            _audioSource.PlayOneShot(lightaudioClip);
        }

        public void SelectLeft()
        {
            TurnOffLight();

            if (_index == 0)
                _index = 2;
            else
                _index--;
            transform.DOMove(heros[_index].cameraPosition.position, rotateSpeed);
            transform.DORotateQuaternion(heros[_index].cameraPosition.rotation, rotateSpeed).OnComplete(() =>
            {
                TurnOnLight();
            });
        }
    }
}