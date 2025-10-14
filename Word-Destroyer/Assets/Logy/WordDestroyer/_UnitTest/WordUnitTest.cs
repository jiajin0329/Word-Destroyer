using Moq;
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
            Mock<WordModel> _wordModelMock = new();

            _wordView = WordView.BuildTestWordView("Test");

            _word = new(_wordModelMock.Object, new WordStat(), _wordView);

            _word.Tick();

            _wordModelMock.Verify(m => m.Tick(), Times.Once);
        }

        [Test]
        public void CheckMoveDownResult()
        {
            // Build WordMoveDown
            WordStat _stat = new(0, 1f, 1);
            float _attackPosY = -10f;
            _wordModel = new WordMoveDown(_stat, Vector3.zero, _attackPosY);

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
            bool isAttack = false;
            _wordModel = new WordMoveDown(_stat, _position, _attackPosY);
            _wordModel.attackAction += () => isAttack = true;

            _wordModel.Tick();

            Assert.AreEqual(_attackPosY, _wordModel.position.y);
            Assert.IsTrue(isAttack);
        }

        [Test]
        public void CheckWordViewPosition()
        {
            // Build MoveDown
            WordStat _stat = new(0, 1f, 1);
            float _attackPosY = -5f;
            _wordModel = new WordMoveDown(_stat, Vector3.zero, _attackPosY);

            // Build WordView
            _wordView = WordView.BuildTestWordView("Test");

            // Build Word
            Word _word = new(_wordModel, new WordStat(), _wordView);

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


