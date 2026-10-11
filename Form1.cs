using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace snake_game_sem_II
{
    public partial class Form1 : Form
    {
        public int GamePanelWidth => pnnGame.ClientSize.Width;
        public int GamePanelHeight => pnnGame.ClientSize.Height;
        private Snake snake; // Stores the Snake object, including its body positions and direction.
        private Point food; // Stores the position of normal food.
        private Random random = new Random();  // Generates random positions for normal and big food.
        private int score = 0;  // Stores the player's current score.
        private int foodsEaten = 0;  // Counts how many normal foods have been eaten.  // After five normal foods, big food appears.
        private bool bigFoodActive = false;   // Determines whether big food is currently visible.
        private Point bigFood; // Stores the position of big food.
        private bool gameOver = false;  // Becomes true when the player loses. // It prevents the game from continuing.
        private bool isPaused = false;  // Tracks whether the game is paused.
       
public Form1()
        {
            InitializeComponent();
            pnnGame.BackColor = Color.Black;
            pnnGame.BorderStyle = BorderStyle.FixedSingle; // Add a border around the game panel.
            // Allow the form to receive keyboard events before its controls.
            // This helps the arrow keys control the snake.
            this.KeyPreview = true;
            this.KeyDown += Form1_KeyDown;
            snake = new Snake(3);  // Create a new snake containing six body segments.
            GenerateFood(); // Generate the first normal food at a random position.
            lblScore.Text = "Score: 0"; // Display the initial score.
// Set the timer interval to 100 milliseconds.
            // The Tick event moves the snake after each interval.
            timer1.Interval = 150;
            timer1.Start();
        }
private void GenerateFood()
        {
            int maxX = pnnGame.Width / snake.circleSize; // Calculate how many 20-pixel cells fit across the panel.
            int maxY = pnnGame.Height / snake.circleSize;  // Calculate how many 20-pixel cells fit vertically.
            do
            {
                int x = random.Next(maxX) * snake.circleSize; // Generate a random horizontal cell and convert it into pixels.
                int y = random.Next(maxY) * snake.circleSize;  // Generate a random vertical cell and convert it into pixels.
                food = new Point(x, y); // Store the generated position of the food.
                // Generate another position if food overlaps the snake.
            } while (snake.body.Contains(food));
        }
private void CheckFoodCollision()
        {
            Point head = snake.body[0]; // Get the current position of the snake's head.
            // Normal food
            if (head == food)
            {
                score += 1; // Increase the score by one point.
                foodsEaten += 1; // Count the normal food that has been eaten.
                lblScore.Text = "Score: " + score; // Update the score displayed on the form.
                // Grow snake by 1
                // Add one extra segment at the end of the snake.
                snake.body.Add(snake.body[snake.body.Count - 1]);
                // After 5 normal foods, create big food
                if (foodsEaten == 5)
                {
                    // Generate a position for the bonus food.
                    GenerateBigFood();

                    // Tell the Paint event to draw big food.
                    bigFoodActive = true;

                    // Reset the counter for the next group of normal foods.
                    foodsEaten = 0;
                }
                else
                {
                    // Generate new normal food if big food is not due yet.
                    GenerateFood();
                }
            }
            // Big food
            // Check whether big food is active and the snake's head
            // is inside the 2-by-2-cell area occupied by the big food.
            if (bigFoodActive &&
                head.X >= bigFood.X &&
                head.X < bigFood.X + snake.circleSize * 2 &&
                head.Y >= bigFood.Y &&
                head.Y < bigFood.Y + snake.circleSize * 2)
            {
                // Award three points for eating big food.
                score += 3;
                // Update the score label.
                lblScore.Text = "Score: " + score;
                // Grow snake by 3
                // Add three extra body segments.
                for (int i = 0; i < 3; i++)
                {
                    snake.body.Add(snake.body[snake.body.Count - 1]);
                }
                // Remove big food from the game.
                bigFoodActive = false;
                // Generate new normal food.
                GenerateFood();
            }
        }
        private void GenerateBigFood()
        {
            int maxX = pnnGame.Width / snake.circleSize - 1;
            int maxY = pnnGame.Height / snake.circleSize - 1;

            do
            {
                int x = random.Next(maxX) * snake.circleSize;
                int y = random.Next(maxY) * snake.circleSize;

                bigFood = new Point(x, y);

            } while (snake.body.Contains(bigFood));
        }
 
private void pnnGame_Paint(object sender, PaintEventArgs e)
        {
            // Draw snake body
            for (int i = 1; i < snake.body.Count; i++)
            {
                Point circle = snake.body[i];

                e.Graphics.FillEllipse(
                    Brushes.Green,
                    circle.X,
                    circle.Y,
                    snake.circleSize,
                    snake.circleSize
                );
            }
            // Draw snake head
            Point head = snake.body[0];
            e.Graphics.FillEllipse(
                Brushes.Lime,
                head.X,
                head.Y,
                snake.circleSize,
                snake.circleSize
            );
            // Draw eyes
            e.Graphics.FillEllipse(
                Brushes.Black,
                head.X + 4,
                head.Y + 4,
                4,
                4
            );

            e.Graphics.FillEllipse(
                Brushes.Black,
                head.X + 12,
                head.Y + 4,
                4,
                4
            );

            // Draw normal food
            if (!bigFoodActive)
            {
                e.Graphics.FillEllipse(
                    Brushes.Red,
                    food.X,
                    food.Y,
                    snake.circleSize,
                    snake.circleSize
                );
            }

            // Draw big food
            if (bigFoodActive)
            {
                e.Graphics.FillEllipse(
                    Brushes.Gold,
                    bigFood.X,
                    bigFood.Y,
                    snake.circleSize * 2,
                    snake.circleSize * 2
                );
            }
            // Draw game walls
            using (Pen wallPen = new Pen(Color.Green, 5))
            {
                e.Graphics.DrawRectangle(
                    wallPen,
                    2,
                    2,
                    pnnGame.Width - 5,
                    pnnGame.Height - 5
                );
            }
        }

private void timer1_Tick(object sender, EventArgs e)
        {
            // Do nothing if the game has ended.
            if (gameOver)
                return;

            // Move the snake one step in its current direction.
            snake.Move();

            // Check whether the snake has eaten normal or big food.
            CheckFoodCollision();

            // Check whether the snake has hit a wall or itself.
            CheckGameOver();

            // Request the game panel to redraw its contents.
            pnnGame.Invalidate();
        }
private void CheckGameOver()
        {
            // Get the current position of the snake's head.
            Point head = snake.body[0];

            // Wall collision
            // Check whether the head has moved beyond any panel boundary.
            if (head.X < 0 ||
                head.Y < 0 ||
                head.X >= pnnGame.Width ||
                head.Y >= pnnGame.Height)
            {
                // Stop the game if the snake hits a wall.
                EndGame();
                return;
            }

            // Snake hits itself
            // Start at index 1 because index 0 is the head.
            for (int i = 1; i < snake.body.Count; i++)
            {
                // Compare the head position with every body segment.
                if (head == snake.body[i])
                {
                    // End the game if the head touches the body.
                    EndGame();
                    return;
                }
            }
        }
private void EndGame()
        {
            // Mark the game as finished.
            gameOver = true;

            // Stop the timer so the snake no longer moves.
            timer1.Stop();

            // Show a message containing the player's final score.
            MessageBox.Show(
                "Game Over!\nYour Score: " + score,
                "Snake Game"
            );
        }
        private void pnnGame_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
          
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
         
            if (gameOver || isPaused)
                return;

            if (e.KeyCode == Keys.Up)
                snake.ChangeDirection(0);
            else if (e.KeyCode == Keys.Right)
                snake.ChangeDirection(1);
            else if (e.KeyCode == Keys.Down)
                snake.ChangeDirection(2);
            else if (e.KeyCode == Keys.Left)
                snake.ChangeDirection(3);
        }
        private void lblScore_Click(object sender, EventArgs e)
        {

        }
private void Pause_Click(object sender, EventArgs e)
        {
            // Do not allow Pause or Resume after Game Over.
            if (gameOver)
                return;

            // Check whether the game is currently running.
            if (!isPaused)
            {
                // Stop the timer, freezing the snake's movement.
                timer1.Stop();

                // Record that the game is paused.
                isPaused = true;

                // Change the button text so the player can resume.
                Pause.Text = "Resume";
            }
            else
            {
                // Restart the timer and continue the game.
                timer1.Start();

                // Record that the game is no longer paused.
                isPaused = false;

                // Restore the original button text.
                Pause.Text = "Pause";
            }
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (gameOver || isPaused)
                return base.ProcessCmdKey(ref msg, keyData);

            switch (keyData & Keys.KeyCode)
            {
                case Keys.Up:
                    snake.ChangeDirection(0);
                    pnnGame.Invalidate();
                    return true;

                case Keys.Right:
                    snake.ChangeDirection(1);
                    pnnGame.Invalidate();
                    return true;

                case Keys.Down:
                    snake.ChangeDirection(2);
                    pnnGame.Invalidate();
                    return true;

                case Keys.Left:
                    snake.ChangeDirection(3);
                    pnnGame.Invalidate();
                    return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
        private void Restart_Click(object sender, EventArgs e)
        {
            // Stop the old game timer before resetting anything.
            timer1.Stop();

            // Reset the score to zero.
            score = 0;

            // Reset the normal-food counter.
            foodsEaten = 0;

            // Remove any active big food.
            bigFoodActive = false;

            // Allow the new game to run.
            gameOver = false;

            // Ensure the new game is not paused.
            isPaused = false;

            // Update the score label.
            lblScore.Text = "Score: 0";

            // Create a fresh snake with six segments.
            // Its position and direction return to their initial values.
            snake = new Snake(3);

            // Generate new food for the restarted game.
            GenerateFood();

            // Request the game panel to redraw the snake and food.
            pnnGame.Invalidate();

            // Reset the Pause button text.
            Pause.Text = "Pause";

            // Start the new game.
            timer1.Start();

            // Remove focus from a button so the form can receive keyboard input.
            this.ActiveControl = null;
            this.Focus();
        }

    }

}
