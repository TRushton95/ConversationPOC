using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConversationPOC.Classes
{
    class Task
    {
        public int maxProgress { get; set; }
        public int progress { get; set; }

        //Implement observer pattern on tasks (NOT QUESTS)
        public Task(int maxProgress)
        {
            this.maxProgress = maxProgress;
            this.progress = 0;
        }

        public void increment()
        {
            if (progress < maxProgress)
            {
                progress++;
            }
        }

        public void decrement()
        {
            if (progress > 0)
            {
                progress--;
            }
        }

        public bool isComplete()
        {
            return progress >= maxProgress;
        }
    }
}
