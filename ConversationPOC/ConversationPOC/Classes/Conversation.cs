﻿using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConversationPOC.Classes
{
    class Conversation
    {
        DialogueNode dialogue, temp;
        KeyboardState currentKeyboard, prevKeyboard;
        MouseState currentMouse, prevMouse;
        bool active;
        int option;

        public Conversation(DialogueNode dialogue)
        {
            this.dialogue = dialogue;
            currentKeyboard = new KeyboardState();
            prevKeyboard = new KeyboardState();
            currentMouse = new MouseState();
            prevMouse = new MouseState();
            active = false;
            option = 0;
        }

        public void start()
        {
            active = true;
            Console.WriteLine("+----------------------+");
            Console.WriteLine("| Conversation started |");
            Console.WriteLine("+----------------------+");
            Console.Write(dialogue.read());
            Console.WriteLine(dialogue.getResponses());
        }

        public void end()
        {
            active = false;
            Console.WriteLine("\n+----------------------+");
            Console.WriteLine(  "|  Conversation ended  |");
            Console.WriteLine(  "+----------------------+");
        }

        public void processInput()
        {
            option = 0;

            //process keyboard input
            prevKeyboard = currentKeyboard;
            currentKeyboard = Keyboard.GetState();

            //needs a flexible system for input selection based on number of nodes
            if (currentKeyboard.IsKeyDown(Keys.D1) && prevKeyboard.IsKeyUp(Keys.D1))
            {
                if (dialogue.isAtLastPiece())
                {
                    temp = dialogue.Respond(1);
                    option = 1;
                }
            }
            else if (currentKeyboard.IsKeyDown(Keys.D2) && prevKeyboard.IsKeyUp(Keys.D2))
            {
                if (dialogue.isAtLastPiece())
                {
                    temp = dialogue.Respond(2);
                    option = 2;
                }
            }
            else if (currentKeyboard.IsKeyDown(Keys.D3) && prevKeyboard.IsKeyUp(Keys.D3))
            {
                if (dialogue.isAtLastPiece())
                {
                    temp = dialogue.Respond(3);
                    option = 3;
                }
            }

            //advance to next node based on keyboard input
            if (temp != null && temp != dialogue)
            {
                dialogue = temp;
                Console.WriteLine("\t> Option " + option + "\n");
                Console.WriteLine("NPC says: " + dialogue.read());
                if (dialogue.isAtLastPiece())
                {
                    Console.WriteLine(dialogue.getResponses());
                }
            }

            //process mouse input
            prevMouse = currentMouse;
            currentMouse = Mouse.GetState();

            if ((currentMouse.LeftButton == ButtonState.Pressed &&
                prevMouse.LeftButton == ButtonState.Released) ||
                (currentMouse.RightButton == ButtonState.Pressed &&
                prevMouse.RightButton == ButtonState.Released))
            {
                if (!dialogue.isAtLastPiece())
                {
                    dialogue.next();
                    Console.WriteLine("NPC says: " + dialogue.read());
                    if (dialogue.isAtLastPiece())
                    {
                        Console.WriteLine(dialogue.getResponses());
                    }
                }
                else if (!dialogue.hasChildren())
                {
                    this.end();
                }
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
