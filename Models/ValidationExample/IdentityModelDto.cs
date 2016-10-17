using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MVCLearning.Models.ValidationExample
{
    public class IDentityModelDto 
    {
        public IDentityModelDto()
        {
            this.ActiveParty = new PartyDto();
        }

        public virtual PartyDto ActiveParty { get; set; }
    }
}
