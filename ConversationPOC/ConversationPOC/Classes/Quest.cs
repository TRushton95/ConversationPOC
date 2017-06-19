using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ConversationPOC.Classes
{
    class Quest
    {
        public List<Task> tasks { get; set; }

        public Quest(string questPath)
        {

            if (Path.GetExtension(questPath) != ".xml")
            {
                throw new ArgumentException("Invalid file type");
            }

            tasks = new List<Task>();
            string questXML = File.ReadAllText(questPath);

            using (XmlReader reader = XmlReader.Create(questPath))
            {
                int n = 0;
                string content;
                while (reader.ReadToFollowing("Task"))
                {
                    content = "AN ERROR OCCURED!";
                    reader.MoveToContent();
                    content = reader.ReadInnerXml();
                    tasks.Add(new Task(Convert.ToInt32(content)));
                }
            }
        }

        public bool isTaskComplete(int i)
        {
            return tasks[i].isComplete();
        }

        public bool isComplete()
        {
            foreach (Task t in tasks)
            {
                if (!t.isComplete())
                {
                    return false;
                }
            }

            return true;
        }

        public override string ToString()
        {
            string output = "";

            output = "Quest: isComplete=" + isComplete() + " - " + tasks.Count + " tasks={\n";

            int i = 0;
            foreach (Task t in tasks) {
                output += "\ttask " + i + "={maxProgress=" + t.maxProgress + ", progress=" + t.progress + ", isComplete=" + t.isComplete() + "}\n";
                i++;
            }
            output += "}\n";

            return output;
        }
    }
}
