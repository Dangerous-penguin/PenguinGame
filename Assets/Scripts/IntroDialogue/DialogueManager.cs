using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace DangerousPenguin
{
    public class DialogueManager : MonoBehaviour
    {

        private int _characterIndex = 0;
        [SerializeField] private float _timePerCharacter;
        private float _timer;

        private string _textToWrite = "";

        [SerializeField] private string[] _dialogueData;

        [SerializeField] private TMP_Text textBox;

         private float textMultiplier;
        private float speedMultiplier;
        private float sinMultiplier;

        [SerializeField] private AnimationCurve wave;

        private int _counter = 0;

        private void Awake() 
        {
            _counter = 0;
            Next();
        }

        void Update()
        {
            AnimateText();
            if(_characterIndex < _textToWrite.Length && _textToWrite != "")
            {
                _timer -= Time.deltaTime;
                if(_timer <=0f)
                {
                    _timer += _timePerCharacter;
                    _characterIndex++;
                    string tempText = _textToWrite.Substring(0, _characterIndex);
                    tempText += "<color=#00000000>" + _textToWrite.Substring(_characterIndex) + "</color>";
                    textBox.text = tempText;
                }
            }
        }

        private void AnimateText()
        {
            textBox.ForceMeshUpdate();
            var textInfo = textBox.textInfo;

            for(int i=0; i<textInfo.characterCount; ++i)
            {
                var charInfo = textInfo.characterInfo[i];

                if(!charInfo.isVisible)
                {
                    continue;
                }

                var verts = textInfo.meshInfo[charInfo.materialReferenceIndex].vertices;

                for(int j=0; j<4; ++j)
                {
                    var origin = verts[charInfo.vertexIndex + j];
                    verts[charInfo.vertexIndex + j] = origin + new Vector3(wave.Evaluate(Random.Range(0,1f)*speedMultiplier) * textMultiplier, Mathf.Sin(Time.time * 2f + origin.x * 0.01f) * sinMultiplier, 0);
                }
            }

            for(int i=0; i<textInfo.meshInfo.Length; ++i)
            {
                var meshInfo = textInfo.meshInfo[i];
                meshInfo.mesh.vertices = meshInfo.vertices;
                textBox.UpdateGeometry(meshInfo.mesh, i);
            }

        }

        public void Next()
        {
            textMultiplier += 6f/_dialogueData.Length;
            speedMultiplier += .4f/_dialogueData.Length;
            sinMultiplier += 2f/_dialogueData.Length;

            if(_characterIndex < _textToWrite.Length)
            {
                textBox.text = _textToWrite;
                _characterIndex = _textToWrite.Length;
                return;
            }

            textBox.text = "";
            _characterIndex = 0;
            if(_dialogueData.Length <= _counter)
            {
                _textToWrite = "";
                _counter = 0;
                //ChangeScene
                return;
            }
            _textToWrite = _dialogueData[_counter];
            _counter++;
        }
    }
}
