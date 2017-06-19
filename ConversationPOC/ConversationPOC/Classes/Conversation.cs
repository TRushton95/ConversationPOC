using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConversationPOC.Classes
{
    class Conversation
    {
        KeyboardState currentKeyboard, prevKeyboard;
        bool active;

        public Conversation()
        {
            currentKeyboard = new KeyboardState();
            prevKeyboard = new KeyboardState();
        }

        public void start()
        {
            active = true;
            Console.WriteLine("--------------------");
            Console.WriteLine("Conversation started");
            Console.WriteLine("--------------------");
        }

        public void end()
        {
            active = false;
            Console.WriteLine("--------------------");
            Console.WriteLine(" Conversation ended ");
            Console.WriteLine("--------------------");
        }

        public void processInput()
        {
            prevKeyboard = currentKeyboard;
            currentKeyboard = Keyboard.GetState();
            
            if (currentKeyboard.IsKeyDown(Keys.D1) && prevKeyboard.IsKeyUp(Keys.D1))
            {

            }
            else if (currentKeyboard.IsKeyDown(Keys.D2) && prevKeyboard.IsKeyUp(Keys.D2))
            {

            }
            else if (currentKeyboard.IsKeyDown(Keys.D3) && prevKeyboard.IsKeyUp(Keys.D3))
            {

            }
        }

        public void update()
        {
            if (active == true)
            {
                processInput();
            }
        }
    }
}
