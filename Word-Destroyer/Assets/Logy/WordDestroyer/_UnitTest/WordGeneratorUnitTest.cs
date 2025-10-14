using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using TMPro;
using UnityEngine;

namespace Logy.WordDestroyer
{
    public class WordGeneratorUnitTest
    {
        private LevelDatas _levelDatas;
        private WordGeneratorModel _wordGeneratorModel;
        private CancellationTokenSource _cancellationTokenSource;

        [Test]
        public void CheckGenerate()
        {
            _levelDatas = LevelDatas.BuildTestDatas();
            _wordGeneratorModel = new(_levelDatas);
            _wordGeneratorModel.Initialize(_levelDatas);

            Word _word = _wordGeneratorModel.Generate(BuildGenerateParam(Vector3.zero, "Test"));

            Assert.AreEqual(1, _levelDatas.wordHashSet.Count);
            Assert.AreEqual("Test", _word.GetViewTextName());
        }

        private WordGeneratorModel.GenerateParam BuildGenerateParam(Vector3 _wordPosition, string _wordName)
        {
            WordGeneratorModel.GenerateParam generateParam = new()
            {
                wordPosition = _wordPosition,
                wordName = _wordName
            };

            return generateParam;
        }

        /// <summary>
        /// Check if the settings data for repeated generation tests and validations are the same.
        /// </summary>
        [Test]
        public void CheckGenerateWordLoadStat()
        {
            _levelDatas = LevelDatas.BuildTestDatas();
            _wordGeneratorModel = new(_levelDatas);
            _wordGeneratorModel.Initialize(_levelDatas);

            int _count = 0;
            while (_count < 100)
            {
                _count++;
                Word _word = _wordGeneratorModel.Generate(BuildGenerateParam(Vector3.zero, $"Test{_count}"));
                Assert.AreEqual(_levelDatas.wordSetting.wordStats[_word.stat.index], _word.stat);
            }
        }

        /// <summary>
        /// Check if the generated position are correct.
        /// </summary>
        [Test]
        public void CheckGenerateWordPosition()
        {
            _levelDatas = LevelDatas.BuildTestDatas();
            _wordGeneratorModel = new(_levelDatas);
            _wordGeneratorModel.Initialize(_levelDatas);

            int _count = 0;
            while (_count < 100)
            {
                _count++;

                float minX = -10f;
                float maxX = 10f;
                float minY = -10f;
                float maxY = 10f;

                // Generate random X and Y components separately
                float randomX = Random.Range(minX, maxX);
                float randomY = Random.Range(minY, maxY);
                Vector3 _position = new Vector3(randomX, randomY, 0f);

                var _word1 = _wordGeneratorModel.Generate(BuildGenerateParam(_position, "Test1"));
                var _word2 = _wordGeneratorModel.Generate(BuildGenerateParam(_position, "Test2"));

                Assert.AreEqual(_position, _word1.GetViewPosition());
                Assert.AreEqual(_position, _word2.GetViewPosition());
            }
        }

        /// <summary>
        /// Check if the generated position are correct.
        /// </summary>
        [Test]
        public async Task CheckStart()
        {
            _levelDatas = LevelDatas.BuildTestDatas();
            _wordGeneratorModel = new(_levelDatas);
            _wordGeneratorModel.Initialize(_levelDatas);
            _cancellationTokenSource = new();

            await _wordGeneratorModel.RepeatGenerate(100, 0, _cancellationTokenSource.Token);
            
            Assert.AreEqual(100, _wordGeneratorModel.currentCount);
        }
    }
}


