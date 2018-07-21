using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    // We are not gonna use the private members of the class, for checking and testing.
    // Exercise 3
    [TestFixture]
    class StackTests
    {
        private Stack<string> _stack;
        [SetUp]
        public void SetUp()
        {
            _stack = new Stack<string>();
        }

        [Test]
        public void Push_ArgIsNull_ThrowArgNullException()
        {
            Assert.That(() => _stack.Push(null), Throws.ArgumentNullException);
        }

        [Test]
        public void Push_ValidArg_AddTheObjectToTheStack()
        {
            _stack.Push("a");
            Assert.That(_stack.Count, Is.EqualTo(1));
        }

        // To check that count property is implemented correctly
        public void Count_EmptyStack_ReturnZero()
        {
            Assert.That(_stack.Count, Is.EqualTo(0));
        }

        [Test]
        public void Pop_EmptyStack_ThrowInvalidOperationException()
        {
            Assert.That(() => _stack.Pop(), Throws.InvalidOperationException);
        }

        // Here 2 tests are needed for Pop...

        [Test]
        public void Pop_StackWithFewObjects_ReturnObjectOnTop()
        {
            _stack.Push("a");
            _stack.Push("b");
            _stack.Push("c");

            var result = _stack.Pop();

            Assert.That(result, Is.EqualTo("c"));
        }

        [Test]
        public void Pop_StackWithFewObjects_RemoveObjectOnTop()
        {
            _stack.Push("a");
            _stack.Push("b");
            _stack.Push("c");

            _stack.Pop();

            Assert.That(_stack.Count, Is.EqualTo(2));
        }

        [Test]
        public void Peek_EmptyStack_ThrowInvalidOperationException()
        {
            Assert.That(() => _stack.Peek(), Throws.InvalidOperationException);
        }

        // Here 2 tests are needed for Peek...

        [Test]
        public void Peek_StackWithFewObjects_ReturnObjectOnTop()
        {
            _stack.Push("a");
            _stack.Push("b");
            _stack.Push("c");

            var result = _stack.Peek();

            Assert.That(result, Is.EqualTo("c"));
        }

        [Test]
        public void Peek_StackWithFewObjects_DoesNotRemoveObjectOnTop()
        {
            _stack.Push("a");
            _stack.Push("b");
            _stack.Push("c");

            _stack.Peek();
            // Count should not change
            Assert.That(_stack.Count, Is.EqualTo(3));
        }
    }
}
