using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ConversationPOC.Classes
{
    class DialogueSet
    {
        int currentDialoguePiece;
        int lastDialoguePiece; //max number of dialogue pieces

        public List<string> dialogue { get; set; }

        /// <summary>
        /// A dialogue set represents a single dialogue that spans one interaction option
        /// eg. Player Says -> "Talk to me"
        ///     NPC responds with a single long dialogue piece spanning several text panels
        /// Many of these are likely to be a single node
        /// </summary>
        /// <param name="dialogueFilePath"></param>
        public DialogueSet(string dialogueFilePath)
        {
            if (Path.GetExtension(dialogueFilePath) != ".xml")
            {
                throw new ArgumentException("Invalid file type");
            }

            currentDialoguePiece = 0;
            dialogue = new List<string>();
            string dialogueXML = File.ReadAllText(dialogueFilePath);

            using (XmlReader reader = XmlReader.Create(dialogueFilePath))
            {
                int n = 0;
                string content;
                while (reader.ReadToFollowing("Piece"))
                {
                    content = "AN ERROR OCCURED!";
                    reader.MoveToContent();
                    content = reader.ReadInnerXml();
                    dialogue.Add(content);
                    n++;
                }

                lastDialoguePiece = n-1;
            }
        }

        public DialogueSet(List<string> dialogue)
        {
            this.dialogue = dialogue;
            this.lastDialoguePiece = dialogue.Count();


        }

        public void Add(string piece)
        {
            dialogue.Add(piece);
            lastDialoguePiece++;
        }

        public string read()
        {
            string dialoguePiece = "";
            dialoguePiece = dialogue[currentDialoguePiece];

            return dialoguePiece;
        }

        public void next()
        {
            if (currentDialoguePiece < lastDialoguePiece-1)
            {
                currentDialoguePiece++;
            }
        }

        public void prev()
        {
            if (currentDialoguePiece > 0)
            {
                currentDialoguePiece--;
            }
        }

        public void reset()
        {
            currentDialoguePiece = 0;
        }

        public void final()
        {
            currentDialoguePiece = lastDialoguePiece;
        }

        public bool isAtLastPiece()
        {
            return currentDialoguePiece == lastDialoguePiece-1;
        }

        public override string ToString()
        {
            if (dialogue.Count == 0)
            {
                return "{}";
            }

            string output = "";
            output += "{";

            for (int i = 0; i < dialogue.Count; i++)
            {
                output += "'" + dialogue[i] + "'";
                if (i < (dialogue.Count - 1)) //if not the last element
                {
                    output += ", ";
                }
            }
            output += "}";

            return output;
        }
    }
}
