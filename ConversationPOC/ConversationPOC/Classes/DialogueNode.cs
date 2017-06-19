using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ConversationPOC.Classes
{
    class DialogueNode
    {
        List<DialogueNode> children;
        List<string> responses;
        DialogueSet dialogueSet;
        Quest quest;


        public DialogueNode(string documentPath)
        {
            this.children = new List<DialogueNode>();
            this.dialogueSet = new DialogueSet(new List<string>());
            this.responses = new List<string>();

            XmlDocument dialogueXML = new XmlDocument();
            dialogueXML.Load(documentPath);
            PopulateDialogueNode(dialogueXML.FirstChild);
        }

        private DialogueNode(XmlNode rootNode)
        {
            this.children = new List<DialogueNode>();
            this.dialogueSet = new DialogueSet(new List<string>());
            this.responses = new List<string>();

            PopulateDialogueNode(rootNode);
        }

        public void PopulateDialogueNode(XmlNode node)
        {
            foreach(XmlNode n in node)
            {
                if (n.Name == "DialogueNode")
                {
                    children.Add(new DialogueNode(n));
                    responses.Add(n.Attributes["response"].InnerText);
                }
                else if (n.Name == "DialogueSet")
                {
                    foreach(XmlNode p in n)
                    {
                        if (p.Name == "Piece")
                        {
                            dialogueSet.Add(p.InnerText);
                        }
                        else
                        {
                            throw new FormatException();
                        }
                    }
                }
                else
                {
                    throw new FormatException();
                }
            }
        }

        public Quest getQuest()
        {
            return quest;
        }

        public DialogueNode GoTo(int n)
        {
            return children[n+1];
        }

        public string read()
        {
            return dialogueSet.read();
        }

        public void next()
        {
            dialogueSet.next();
        }

        public void prev()
        {
            dialogueSet.prev();
        }

        public void reset()
        {
            dialogueSet.reset();
        }

        public void final()
        {
            dialogueSet.final();
        }

        public bool isAtLastPiece()
        {
            return dialogueSet.isAtLastPiece();
        }

        public override string ToString()
        {
            string output = "";

            output += "Node: Set" + dialogueSet.ToString() + "\n";
            foreach (DialogueNode n in children)
            {
                output += n.ToString(1);
            }

            return output;
        }

        public string ToString(int tabNumber)
        {
            string output = "";
            string indent = new String('\t', tabNumber);

            output += indent + "|\n";
            output += indent + "-> ";
            output += "Node: Set" + dialogueSet.ToString() + "\n";
            foreach (DialogueNode n in children)
            {
                output += n.ToString(tabNumber+1);
            }

            return output;
        }

        public DialogueNode Respond(int choice)
        {
            if (choice <= children.Count)
            {
                return children[choice - 1];
            }

            return null;
        }

        public bool hasChildren()
        {
            return children.Count > 0;
        }

        public string getResponses()
        {
            string output = "";

            if (responses.Count==0)
            {
                return output;
            }

            output += "\n>>>>>>>>>>>>>>>>>>>>>\n";
            for (int i = 0; i < responses.Count; i++)
            {
                output += (i+1) + ") " + responses[i] + "\n";
            }
            output += ">>>>>>>>>>>>>>>>>>>>>\n";

            return output;
        }
    }
}
