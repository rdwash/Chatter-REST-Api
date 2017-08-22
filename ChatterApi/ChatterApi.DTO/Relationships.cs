using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatterApi.DTO
{
    public class Relationships
    {
        public Relationships()
        {
            Creator = new RelationshipCreator();
        }
        public RelationshipCreator Creator { get; set; } 
    }

    public class RelationshipCreator
    {
        public RelationshipCreator()
        {
            Data = new DTO.User();
            Links = new RelationshipCreatorLinks();
        }
        public DTO.User Data { get; set; }
        public RelationshipCreatorLinks Links { get; set; }
    }

    public class RelationshipCreatorLinks
    {
        public RelationshipCreatorLinks()
        {

        }
        public string Self { get; set; }
        public string Related { get; set; }
    }
}
