using System.Linq;
using NUnit.Framework;
using PatronTest.FileReading;

namespace PatronTest.Tests
{
    [TestFixture]
    public class FileListenerHashTests
    {
        private FileListenerHash _hash = null!;

        [SetUp]
        public void Setup()
        {
            _hash = new FileListenerHash();
        }

        [Test]
        public void Defaults()
        {
            Assert.That(_hash.ListenersCount, Is.EqualTo(0));
        }

        [Test]
        public void AddRemove_NonRepeatedPaths()
        {
            var listener1 = new FileReadListener("A");
            _hash.Add(listener1);
            Assert.That(_hash.ListenersCount, Is.EqualTo(1));
            Assert.That(_hash.GetListeners("A").Count, Is.EqualTo(1));
            Assert.That(_hash.GetListeners("A").Contains(listener1));
            Assert.That(_hash.GetListeners("B").Count, Is.EqualTo(0));

            var listener2 = new FileReadListener("B");
            _hash.Add(listener2);
            Assert.That(_hash.ListenersCount, Is.EqualTo(2));
            Assert.That(_hash.GetListeners("A").Count, Is.EqualTo(1));
            Assert.That(_hash.GetListeners("B").Count, Is.EqualTo(1));
            Assert.That(_hash.GetListeners("B").Contains(listener2));

            _hash.Remove(listener1);
            Assert.That(_hash.ListenersCount, Is.EqualTo(1));
            Assert.That(_hash.GetListeners("A").Count, Is.EqualTo(0));
            Assert.That(_hash.GetListeners("B").Count, Is.EqualTo(1));

            _hash.Remove(listener2);
            Assert.That(_hash.ListenersCount, Is.EqualTo(0));
            Assert.That(_hash.GetListeners("A").Count, Is.EqualTo(0));
            Assert.That(_hash.GetListeners("B").Count, Is.EqualTo(0));
        }

        [Test]
        public void AddRemove_RepeatedPaths()
        {
            var listener1 = new FileReadListener("A");
            _hash.Add(listener1);
            Assert.That(_hash.ListenersCount, Is.EqualTo(1));
            Assert.That(_hash.GetListeners("A").Count, Is.EqualTo(1));
            Assert.That(_hash.GetListeners("A").Contains(listener1));

            var listener2 = new FileReadListener("A");
            _hash.Add(listener2);
            Assert.That(_hash.ListenersCount, Is.EqualTo(1));
            Assert.That(_hash.GetListeners("A").Count, Is.EqualTo(2));
            Assert.That(_hash.GetListeners("A").Contains(listener2));

            _hash.Remove(listener1);
            Assert.That(_hash.ListenersCount, Is.EqualTo(1));
            Assert.That(_hash.GetListeners("A").Count, Is.EqualTo(1));
            Assert.That(_hash.GetListeners("A").Contains(listener2));

            _hash.Remove(listener2);
            Assert.That(_hash.ListenersCount, Is.EqualTo(0));
            Assert.That(_hash.GetListeners("A").Count, Is.EqualTo(0));
        }

        [Test]
        public void Add_SameListener()
        {
            var listener1 = new FileReadListener("A");
            _hash.Add(listener1);
            Assert.That(_hash.ListenersCount, Is.EqualTo(1));
            Assert.That(_hash.GetListeners("A").Count, Is.EqualTo(1));
            Assert.That(_hash.GetListeners("A").Contains(listener1));

            _hash.Add(listener1);
            Assert.That(_hash.ListenersCount, Is.EqualTo(1));
            Assert.That(_hash.GetListeners("A").Count, Is.EqualTo(1));
            Assert.That(_hash.GetListeners("A").Contains(listener1));
        }

        [Test]
        public void Remove_MissingListener()
        {
            _hash.Remove(new FileReadListener("A"));
            Assert.That(_hash.ListenersCount, Is.EqualTo(0));
        }
    }
}
