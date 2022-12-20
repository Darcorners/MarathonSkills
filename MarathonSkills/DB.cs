using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarathonSkills
{
    public static class Util
    {
        private static MarathonSkillsEntities1 database;

        public static MarathonSkillsEntities1 db
        {
            get 
            {
                if (database == null)
                    database = new MarathonSkillsEntities1();

                return database;
            }
        }
    }
}
