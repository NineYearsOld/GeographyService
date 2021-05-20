using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GeographyService.Models
{
    public class River
    {
        public River()
        {

        }
        public River(string name, int length)
        {
            Name = name;
            Length = length;
        }
        public River(int id, string name, int length)
        {
            Id = id;
            Name = name;
            Length = length;
        }
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity), Key()]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Length { get; set; }
    }
}
