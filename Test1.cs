using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zmeika;
using System.Linq;

namespace ZmeikaTests
{
    [TestClass]
    public class SnakeTests
    {
        [TestMethod]
        public void SnakeDirectionChange()
        {
            // Arrange
            var snake = new Snake(5, 5, ConsoleColor.Blue, ConsoleColor.Cyan, drawOnCreate: false);
            var originalHead = snake.Head;

            // Act
            snake.Move(Direction.Up);
            var newHead = snake.Head;

            // Assert
            Assert.AreEqual(originalHead.X, newHead.X);
            Assert.AreEqual(originalHead.Y - 1, newHead.Y);
        }

        [TestMethod]
        public void SnakeGrowsAfterEating()
        {
            // Arrange
            var snake = new Snake(5, 5, ConsoleColor.Blue, ConsoleColor.Cyan, drawOnCreate: false);
            int originalLength = snake.Body.Count;

            // Act
            snake.Move(Direction.Right, eat: true);
            int newLength = snake.Body.Count;

            // Assert
            Assert.AreEqual(originalLength + 1, newLength);
        }

        [TestMethod]
        public void FoodGeneratedOutsideSnake()
        {
            // Arrange
            var snake = new Snake(5, 5, ConsoleColor.Blue, ConsoleColor.Cyan, drawOnCreate: false);
            for (int i = 0; i < 10; i++)
                snake.Move(Direction.Right, eat: true);

            // Act
            var food = GenerateFoodOutsideSnake(snake);

            // Assert
            bool foodOnSnake = snake.Body.Any(p => p.X == food.X && p.Y == food.Y)
                            || (snake.Head.X == food.X && snake.Head.Y == food.Y);

            Assert.IsFalse(foodOnSnake);
        }

        private Pixel GenerateFoodOutsideSnake(Snake snake)
        {
            const int mapWidth = 30;
            const int mapHeight = 10;
            var rand = new Random();
            Pixel food;

            do
            {
                food = new Pixel(rand.Next(1, mapWidth - 2), rand.Next(1, mapHeight - 2), ConsoleColor.Green);
            }
            while (snake.Head.X == food.X && snake.Head.Y == food.Y ||
                   snake.Body.Any(b => b.X == food.X && b.Y == food.Y));

            return food;
        }
    }
}