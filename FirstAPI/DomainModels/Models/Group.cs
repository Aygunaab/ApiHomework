using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModels.Models
{
    public class Group:BaseEntity
    {
        public int Id { get; set; }
        public string GroupName { get; set; }
        public List<Student> Students { get; set; }

    }
}
