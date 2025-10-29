// Include the namespaces (code libraries) you need below.
using System.Diagnostics.Metrics;
using System.Numerics;

// The namespace your code is in.
namespace MohawkGame2D
{
    /// <summary>
    ///     Your game code goes inside this class!
    /// </summary>
    public class Game
    {
        // Place your variables here:
        Eyeball[] eyeballs = new Eyeball[10];
        bool gameWon = false;

        /// <summary>
        ///     Setup runs once before the game loop begins.
        /// </summary>
        public void Setup()
        {
            Window.SetTitle("Eyeballs");
            Window.SetSize(600, 600);
            Window.TargetFPS = 60;

            for (int i = 0; i < eyeballs.Length; i++)
            {
                eyeballs[i] = new Eyeball();
                eyeballs[i].Setup();
            }
        }

        /// <summary>
        ///     Update runs every frame.
        /// </summary>
        public void Update()
        {
            Window.ClearBackground(Color.White);

            int eyeClosedCounter = 0;

            // update the eyeballs
            bool resetPressed = Input.IsKeyboardKeyPressed(KeyboardInput.Space);
            for (int i = 0; i < eyeballs.Length; i++)
            {
                // protect against null references
                if (eyeballs[i] == null)
                {
                    continue;
                }

                // right click to remove the eyeball
                if (Input.IsMouseButtonPressed(MouseInput.Right) && eyeballs[i].IsMouseInside())
                {
                    // remove the eyeball
                    eyeballs[i] = null;
                    continue;
                }

                if (gameWon && resetPressed)
                {
                    eyeballs[i].Setup();
                }
                eyeballs[i].Update();
                if (eyeballs[i].closed)
                {
                    eyeClosedCounter++;
                }
            }

            if (gameWon && resetPressed) gameWon = false;

            Text.Draw($"Counter: {eyeClosedCounter}", new Vector2(10, 10));

            // win condition
            if (eyeClosedCounter == eyeballs.Length)
            {
                gameWon = true;
                Text.Draw("YOU WON!!! WOW", new Vector2(200, 280));
                Text.Draw("Press Spacebar to reset", new Vector2(200, 350));
            }
        }

    }

}