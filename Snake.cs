
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Security.Cryptography.X509Certificates;

namespace snake_game_sem_II
{
    internal class Snake
    {
        // This list stores the position of every circle of the snake.
        // Index 0 represents the head, while the last index represents the tail.
        public List<Point> body;

        // This variable stores the current direction of the snake.
        // 0 = Up, 1 = Right, 2 = Down, 3 = Left.
        public int direction;

        // This variable defines the width and height of each snake circle.
        // Every movement is equal to this size.
        public int circleSize = 20;
public Snake(int snakeLength)
        {
            // Create an empty list to store the snake's body positions.
            body = new List<Point>();

            direction = 1; // defines the direction ie left or right

            // The snake initially moves to the right.
            // Direction 1 means moving right in our program.

            // Find the game panel using the currently active Form1 instance.
            Form1 form = System.Windows.Forms.Application
                .OpenForms.OfType<Form1>()
                .FirstOrDefault();

            // Use a safe default position if the form is not available.
            int panelWidth = form != null ? form.GamePanelWidth : 400;
            int panelHeight = form != null ? form.GamePanelHeight : 300;

            // Calculate a starting position near the center of the panel.
            int startX = (panelWidth / circleSize / 2) * circleSize;
            int startY = (panelHeight / circleSize / 2) * circleSize;

            // Ensure that the complete snake fits inside the panel.
            int minimumX = (snakeLength - 1) * circleSize;

            if (startX < minimumX)
                startX = minimumX;

            // Repeat the loop according to the required snake length.
            // For example, snakeLength = 6 creates six circles.
            for (int i = 0; i < snakeLength; i++) // increase the length of the snake ie add circles to the snake body
            {
                // Subtract circleSize from X for every new segment.
                // This places each new segment to the left of the previous one.
                // Therefore, the snake initially faces and moves to the right.
                body.Add(new Point(startX - i * circleSize, startY));
            }
        }

        public void Move()
        {
            Point head = body[0]; // define the head of the snake with initial index which is 0

            // Copy the current head position.
            // We will change this copy to calculate the next head position.
            Point newHead = head;

            // Check the current direction and update the head position.
            if (direction == 0)
            {
                newHead.Y = newHead.Y - circleSize;
            }// move up // we minus the circle size from the Y coordinate to move up

            else if (direction == 1)
            {
                newHead.X = newHead.X + circleSize;
            } // move right // we plus the circle size to the X coordinate to move right

            else if (direction == 2)
            {
                newHead.Y = newHead.Y + circleSize;
            }// move down // we plus the circle size to the Y coordinate to move down

            else if (direction == 3)
            {
                newHead.X = newHead.X - circleSize;
            }// move left // we minus the circle size from the X coordinate to move left

            // Insert the new head at index 0.
            // All existing body segments will shift one position down the list.
            body.Insert(0, newHead); // add the new head to the body of the snake

            // Remove the last segment to keep the snake's length unchanged.
            // When food is eaten, Form1 adds extra segments to make the snake grow.
            body.RemoveAt(body.Count - 1); // remove the last circle of the snake body to maintain the length of the snake
        }

        public void ChangeDirection(int newDirection)
        {
            // Prevent the snake from moving directly down when it is moving up.
            // Otherwise, the head could immediately collide with the second segment.
            if (direction == 0 && newDirection == 2)
                return;

            // Prevent the snake from moving directly up when it is moving down.
            if (direction == 2 && newDirection == 0)
                return;

            // Prevent the snake from moving directly left when it is moving right.
            if (direction == 1 && newDirection == 3)
                return;

            // Prevent the snake from moving directly right when it is moving left.
            if (direction == 3 && newDirection == 1)
                return;

            // If the new direction is valid, update the snake's direction.
            direction = newDirection;
        }
    }
}
