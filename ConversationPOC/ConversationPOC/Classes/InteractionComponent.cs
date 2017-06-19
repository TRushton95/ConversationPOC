using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConversationPOC.Classes
{
    class InteractionComponent
    {
        public List<Quest> quests { get; set; }

        public InteractionComponent(List<Quest> quests)
        {
            this.quests = quests;
        }


    }
}
