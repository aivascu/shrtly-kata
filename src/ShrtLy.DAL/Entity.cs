using System.ComponentModel.DataAnnotations;

namespace ShrtLy.DAL
{
    public abstract class Entity
    {
        [Key]
        public int Id { get; set; }
    }
}
