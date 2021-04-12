using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ScriptLoader.Entities
{
    [Table("Setting")]
    public class Setting
    {
        [Key]
        public int Id { get; set; }

        public string ScriptDirectory { get; set; }
    }
}
