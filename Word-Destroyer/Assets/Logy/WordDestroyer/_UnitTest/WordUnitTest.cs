using NUnit.Framework;
using UnityEngine;

namespace Logy.WordDestroyer
{
    public class WordUnitTest
    {
        private WordModel _wordModel;
        private WordView _wordView;
        private Word _word;

        [Test]
        public void CheckWordModelTickIsCall()
        {
            _word = Word.BuildTestWord("Test");

            _word.Tick();
        }

        [Test]
        public void CheckMoveDownResult()
        {
            // Build WordMoveDown
            WordStat _stat = new(0, 1f, 1);
            float _attackPosY = -10f;
            _wordModel = new WordModel(Vector3.zero, _attackPosY);
            _wordModel.stat = _stat;

            float _previousPositionY = _wordModel.position.y;
            _wordModel.Tick();

            float _distance = _previousPositionY - _wordModel.position.y;

            Assert.IsTrue(_wordModel.position.y < _previousPositionY);
            Assert.IsTrue(_distance == _stat.moveSpeed * Time.deltaTime);
        }

        [Test]
        public void CheckWordMoveDownAttackAction()
        {
            // Build MoveDown
            WordStat _stat = new(0, 1f, 1);
            Vector3 _position = new Vector3(0f, -6f, 0f);
            float _attackPosY = -5f;
            _wordModel = new WordModel(_position, _attackPosY);
            _wordModel.stat = _stat;

            _wordModel.Tick();

            Assert.AreEqual(_attackPosY, _wordModel.position.y);
        }

        [Test]
        public void CheckWordViewPosition()
        {
            // Build MoveDown
            float _attackPosY = -5f;
            _wordModel = new WordModel(Vector3.zero, _attackPosY);

            // Build WordView
            _wordView = WordView.BuildTestWordView("Test");

            // Build Word
            Word _word = new(_wordModel, _wordView);

            _word.Tick();

            Assert.AreEqual(_wordModel.position, _wordView.GetPosition());
        }

        [Test]
        public void CheckWordViewTextName()
        {
            // Build WordView
            _wordView = WordView.BuildTestWordView("");

            _wordView.SetTextName("Test");

            Assert.AreEqual("Test", _wordView.GetTextName());
        }
    }
}


